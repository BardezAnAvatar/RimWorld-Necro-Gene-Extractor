using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bardez.Biotech.NecroGeneExtractor.Gui;
using Bardez.Biotech.NecroGeneExtractor.Settings;
using Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;
using Bardez.GeneExtractorTiers.Buildings;
using RimWorld;
using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Buildings;

public abstract class NecroGeneExtractor_Base : GeneExtractorBase
//Building_Enterable, IStoreSettingsParent, IThingHolderWithDrawnPawn, IThingHolder
{
    private int starvationTicks;
    private Corpse containedCorpse;
    private Corpse selectedCorpse;

    protected NecroGeneExtractorSettings NecroSettings => NecroGeneExtractorMod.Settings;

    protected abstract TierSettings TierSettings { get; }

    protected override Graphic TopGraphic
    {
        get
        {
            if (cachedTopGraphic == null)
            {
                cachedTopGraphic = GraphicDatabase.Get<Graphic_Multi>("NextroGeneExtractorTiers/NecroGeneExtractor_Top", ShaderDatabase.Transparent, def.graphicData.drawSize, Color.white);
            }
            return cachedTopGraphic;
        }
    }

    public Corpse Corpse => this.containedCorpse;

    public Corpse TargetedCorpse => this.selectedCorpse;

    public override bool ContainsTarget => innerContainer.Contains(selectedCorpse);

    public override bool TargetSelected => selectedCorpse != null;

    protected override void UnsetTarget() => selectedCorpse = null;

    protected CompRefuelable Refuelable => GetComp<CompRefuelable>();

    public float NeutroamineStored => Refuelable.Fuel;

    public float NeutroamineNeeded => Refuelable.GetFuelCountToFullyRefuel();

    public float NeutroamineStarvationSeverity
    {
        get
        {
            float starvation = 0f;

            if (starvationTicks > 0)
            {
                //presume that 4 hours is starvation period
                starvation = starvationTicks / (GenDate.TicksPerHour * 4f);
            }

            return starvation;
        }
    }

    public float NeutroamineStarvationPerHourOffset
    {
        get
        {
            if (!Working)
            {
                return 0f;
            }

            if (!PowerOn || NeutroamineStored <= 0f)
            {
                return 0.5f;
            }

            return -0.1f;
        }
    }

    protected override float OverchargeSpeedFactor 
        => 1f / TierSettings.CostMultiplierOverdriveTime; //it's a % of time multiplier

    public float NeutroConsumedPerHour
    {
        get
        {
            var corpseMultiplier = TargetCorpseRotStage switch
            {
                RotStage.Rotting => NecroSettings.CorpseRotting.CostMultiplierResource,
                RotStage.Dessicated => NecroSettings.CorpseDessicated.CostMultiplierResource,
                RotStage.Fresh or _ => 1f,
            };
            var multipliers = TierSettings.CostMultiplierResource * corpseMultiplier;

            var neutroPerHour = NecroSettings.CorpseFresh.CostResource * multipliers;

            if (NeutroamineStarvationSeverity > 0f)
            {
                //if starving, consume more to get back to normal.
                neutroPerHour *= 1.1f;
            }

            if (OverchargeActive)
            {
                neutroPerHour *= TierSettings.CostMultiplierOverdriveResource;
            }

            return neutroPerHour;
        }
    }

    //how much = per hour / 1 hour of ticks
    public float NeutroConsumedPerTick => (NeutroConsumedPerHour / GenDate.TicksPerHour);

    protected virtual RotStage TargetCorpseRotStage => selectedCorpse.GetRotStage();

    public override float ExtractionTimeInTicks
    {
        get
        {
            var corpseType = TargetCorpseRotStage;
            var corpseMultiplier = corpseType switch
            {
                RotStage.Rotting => NecroSettings.CorpseRotting.CostMultiplierTime,
                RotStage.Dessicated => NecroSettings.CorpseDessicated.CostMultiplierTime,
                RotStage.Fresh or _ => 1f,
            };

            var hours = NecroSettings.CorpseFresh.CostTime * corpseMultiplier * TierSettings.CostMultiplierTime;

            return hours * GenDate.TicksPerHour;
        }
    }



    // Accept Pawn
    public override AcceptanceReport CanAcceptPawn(Pawn pawn) => false;

    public AcceptanceReport CanAcceptCorpse(Corpse corpse)
    {
        if (TargetSelected && selectedCorpse != corpse) //don't accept new corpse if already selected
        {
            return "NGET_CorpseAlreadyTargeted".Translate();
        }

        if (innerContainer.Any(x => x is Corpse)) //already occupied
        {
            return "Occupied".Translate();
        }

        if (!corpse.InnerPawn.RaceProps.Humanlike)  //has to be a human-like pawn
        {
            return "NGET_CorpseMustBeHumanlike".Translate();
        }

        if (!PowerOn)
        {
            return "NoPower".Translate().CapitalizeFirst();
        }

        if (corpse?.InnerPawn?.genes?.GenesListForReading?.Any(x => x.def.defName == "VREA_Power") == true)
        {
            return "VREA.CannotUseAndroid".Translate().CapitalizeFirst();
        }

        if (corpse.GetRotStage() == RotStage.Rotting && !TierSettings.AcceptRotten)
        {
            return "NGET_CannotProcessCorpseRotting".Translate();
        }

        if (corpse.GetRotStage() == RotStage.Dessicated && !TierSettings.AcceptDessicated)
        {
            return "NGET_CannotProcessCorpseDessicated".Translate();
        }

        // consider:
        //       corpse.InnerPawn.genes != null
        //       && corpse.InnerPawn.genes.GenesListForReading.Any(x => (x.def).passOnDirectly)
        //       && corpse.InnerPawn.genes.GenesListForReading.Any(x => (x.def).biostatArc == 0);

        return true;
    }

    public void TryAcceptCorpse(Corpse corpse)
    {
        if (CanAcceptCorpse(corpse))
        {
            selectedCorpse = containedCorpse = corpse;
            bool deselect = corpse.DeSpawnOrDeselect();

            if (innerContainer.TryAddOrTransfer(corpse))
            {
                var comp = corpse.GetComp<CompRottable>();
                comp.disabled = true;
                StartNewCycle();
            }
            if (deselect)
            {
                Find.Selector.Select(corpse, playSound: false, forceDesignatorDeselect: false);
            }
        }
    }



    // Fuel
    protected void UpdateConsumptionRate()
    {
        Refuelable.Props.consumeFuelOnlyWhenUsed = false;
        Refuelable.Props.fuelConsumptionRate = NeutroConsumedPerTick * GenDate.TicksPerDay;
    }

    protected void DisableConsumptionRate()
    {
        Refuelable.Props.consumeFuelOnlyWhenUsed = true;
        Refuelable.Props.fuelConsumptionRate = 0;
    }

    public void TryAddNeutroamine(int count)
    {
        //how many stacks are we adding?
        Refuelable.Refuel(count);
    }



    // Float Menu
    protected virtual void BuildFloatMenuAvailableCorpses()
    {
        FloatMenuHelper.BuildFloatMenuAvailableCorpses(Map, CanAcceptCorpse, SelectCorpse);
    }

    protected virtual void SelectCorpse(Corpse corpse)
    {
        selectedCorpse = corpse;
    }



    // Gizmos
    public override IEnumerable<Gizmo> GetGizmos()
    {
        foreach (var gizmo in base.GetGizmos())
        {
            yield return gizmo;
        }

        if (this.Corpse != null)
        {
            yield return BuildCorpseSelectGizmo();
        }
    }

    protected override Gizmo BuildInsertGizmo()
        => GizmoHelper.BuildGizmoInsertCorpse(BuildFloatMenuAvailableCorpses, PowerOn);

    protected virtual Gizmo BuildCorpseSelectGizmo()
        => GizmoHelper.BuildCorpseSelectGizmo(this, Corpse);



    // Inspect String
    protected override void InspectStringAddResourceStarvation(StringBuilder stringBuilder)
    {
        float starvationSeverityPercent = NeutroamineStarvationSeverity;

        if (starvationSeverityPercent > 0f)
        {
            var deficiency = "NGET_NeutroamineDeficiency".Translate();
            string text = ((NeutroamineStarvationSeverity > 0f) ? "+" : "-");
            var perHour = "PerHour".Translate(text + NeutroamineStarvationPerHourOffset.ToStringPercent());
            var starvationPct = starvationSeverityPercent.ToStringPercent();
            stringBuilder
                .AppendLineIfNotEmpty()
                .Append($"{deficiency}: {starvationPct} ({perHour})");
        }
    }

    protected override void InspectStringAddResourceConsumption(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLineIfNotEmpty().Append("NGET_Neutroamine".Translate()).Append(": ")
            .Append(NeutroamineStored.ToStringByStyle(ToStringStyle.FloatMaxOne));

        if (Working)
        {
            stringBuilder.Append(" (-").Append("PerHour".Translate((NeutroConsumedPerHour).ToString("F2"))).Append(")");
        }
    }

    protected override NamedArgument GetTargetName()
    {
        return selectedCorpse.InnerPawn.Named("PAWN");
    }

    protected override string GetContainedNameColorized() => Corpse.InnerPawn.NameShortColored.Resolve();

    protected override int GetContainedAge() => Corpse.InnerPawn.ageTracker.AgeBiologicalYears;



    // Ticks
    protected override bool Tick_AbortDueToResourceStarvation()
    {
        if (NeutroamineStarvationSeverity >= 1f)
        {
            Fail();
            return true;
        }

        if (NeutroamineStored <= 0f)
        {
            starvationTicks++;
        }
        else if (starvationTicks > 0)
        {
            starvationTicks--;
        }

        return false;
    }

    protected override void Tick_ConsumeResources()
    {
        UpdateConsumptionRate();

        //Note: consumption is handled in CompRefuelable instead
    }



    // Operations
    protected override void StartNewCycle()
    {
        UpdateConsumptionRate();
        base.StartNewCycle();
    }

    protected override void OnStop(bool minifying = false)
    {
        DisableConsumptionRate();
        base.OnStop(minifying);
        containedCorpse = null;
    }

    protected override void CancelEnterBuilding()
    {
        // No job to cancel ...?

        //if (selectedPawn.CurJobDef == JobDefOf.EnterBuilding)
        //{
        //    selectedPawn.jobs.EndCurrentJob(JobCondition.InterruptForced);
        //}
    }

    protected override void Fail()
    {
        if (ContainsTarget)
        {
            innerContainer.TryDrop(Corpse, InteractionCell, base.Map, ThingPlaceMode.Near, 1, out var _);
        }
        OnStop();
    }



    // Pawn
    protected override Pawn GetContainedPawn() => containedCorpse?.InnerPawn;

    protected override void SetPawnHediffXenogermReplicating(Pawn containedPawn)
    {
        //Don't need to add a Hediff to a corpse
    }

    protected override void DrawPawn()
    {
        if (innerContainer.Contains(containedCorpse))
        {
            GetContainedPawn()
                .Drawer
                .renderer
                .RenderPawnAt(DrawPos + PawnDrawOffset, null, neverAimWeapon: true);
        }
    }



    // Container
    protected override void DropContents(bool minifying = false)
    {
        foreach (var thing in innerContainer)
        {
            if (thing is Corpse corpse)
            {
                var comp = corpse.GetComp<CompRottable>();
                comp.disabled = false;

                if (!minifying)
                {
                    innerContainer.TryDrop(corpse, InteractionCell, Map, ThingPlaceMode.Near, 1, out var _);
                    break;
                }
            }
            //else if Neutroamine, don't drop it (unless minifying). Keep it in, for the vat is a vampire of Neutroamine.
        }

        if (minifying)
        {
            base.DropContents(minifying);
        }
    }



    //Expose data
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref starvationTicks, nameof(starvationTicks), -1);
        Scribe_References.Look(ref selectedCorpse, nameof(selectedCorpse));
        Scribe_References.Look(ref containedCorpse, nameof(containedCorpse));
    }
}
