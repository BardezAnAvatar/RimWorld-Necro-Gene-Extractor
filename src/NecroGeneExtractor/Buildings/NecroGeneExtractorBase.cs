using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bardez.Biotech.NecroGeneExtractor.Defs;
using Bardez.Biotech.NecroGeneExtractor.Gui;
using Bardez.Biotech.NecroGeneExtractor.Settings;
using Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;
using GeneExtractorTiers.Extractors;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Bardez.Biotech.NecroGeneExtractor.Buildings;

public abstract class NecroGeneExtractorBase : GeneExtractorBase
//Building_Enterable, IStoreSettingsParent, IThingHolderWithDrawnPawn, IThingHolder
{
    protected NecroGeneExtractorSettings NecroSettings => NecroGeneExtractorMod.Settings;

    protected abstract TierSettings TierSettings { get; }

    private float containedNeutroamine;
    private int starvationTicks;
    private Corpse containedCorpse;
    private Corpse selectedCorpse;

    public Corpse Corpse => this.containedCorpse;

    public Corpse TargetedCorpse => this.selectedCorpse;

    public override bool TargetSelected => selectedCorpse != null;

    protected override void UnsetTarget() => selectedCorpse = null;

    public float NeutroamineStored
    {
        get
        {
            float num = 0;
            for (int i = 0; i < innerContainer.Count; i++)
            {
                Thing thing = innerContainer[i];
                if (thing.def == NecroGeneExtractor_DefsOf.Neutroamine)
                {
                    num += thing.stackCount;
                }
            }

            return num;
        }
    }

    public float NeutroamineNeeded
    {
        get
        {
            if (selectedCorpse == null)
            {
                return 0f;
            }

            return 50f - NeutroamineStored;
        }
    }

    public float NeutroamineStarvationSeverity
    {
        get
        {
            //presume that 4 hours is starvation period
            return (2500 * 4f) / starvationTicks;
        }
    }

    public float NeutroamineStarvationPerHourOffset
    {
        get
        {
            if (!base.Working)
            {
                return 0f;
            }

            if (!PowerOn || containedNeutroamine <= 0f)
            {
                return 0.5f;
            }

            return -0.1f;
        }
    }

    public float NeutroConsumedPerHour
    {
        get
        {
            var corpseMultiplier = TargetCorpseRotStage switch
            {
                RotStage.Rotting => NecroSettings.CorpseRotting.CostMultiplierResource,
                RotStage.Dessicated => NecroSettings.CorpseDesiccated.CostMultiplierResource,
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

    protected virtual RotStage TargetCorpseRotStage => selectedCorpse.GetRotStage();

    //TODO: figure this out based on pawn decay state
    public override int ExtractionTimeInTicks
    {
        get
        {
            var corpseType = TargetCorpseRotStage;
            var multiplier = corpseType switch
            {
                RotStage.Rotting => NecroSettings.CorpseRotting.CostMultiplierTime,
                RotStage.Dessicated => NecroSettings.CorpseDesiccated.CostMultiplierTime,
                RotStage.Fresh or _ => 1f,
            };

            var hours = NecroSettings.CorpseFresh.CostTime * multiplier * TierSettings.CostMultiplierTime;
            if (OverchargeActive)
            {
                hours *= TierSettings.CostMultiplierOverdriveTime;
            }

            return Convert.ToInt32(hours);
        }
    }



    //Accept Pawn
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

        //if (!corpse.InnerPawn.Dead) //only want dead bodies for Necro
        //{
        //    return false;
        //}

        if (!PowerOn)
        {
            return "NoPower".Translate().CapitalizeFirst();
        }

        if (corpse?.InnerPawn?.genes?.GenesListForReading?.Any(x => x.def.defName == "VREA_Power") == true)
        {
            return "VREA.CannotUseAndroid".Translate().CapitalizeFirst();
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
                SetStartTick();
                TicksRemaining = ExtractionTimeInTicks;
            }
            if (deselect)
            {
                Find.Selector.Select(corpse, playSound: false, forceDesignatorDeselect: false);
            }
        }
    }



    // Float Menu
    /// <summary>
    ///     Returns the list of interaction options that <paramref name="selPawn" /> would have when
    ///     right-clicking this building with <paramref name="selPawn" /> selected.
    /// </summary>
    /// <param name="selPawn"><see cref="Pawn" /> that is currently selected</param>
    /// <returns>The collection of interaction options that <paramref name="selPawn" /> has</returns>
    public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn)
    {
        foreach (FloatMenuOption floatMenuOption in base.GetFloatMenuOptions(selPawn))
        {
            yield return floatMenuOption;
        }

        //Fill with Neutroamine
        //Haul selected corpse to this building

        //TODO: is this reasonable? Maybe if another pawn can haul the corpse?
        if (!selPawn.CanReach(this, PathEndMode.InteractionCell, Danger.Deadly))
        {
            yield return new FloatMenuOption("NoPath".Translate().CapitalizeFirst(), null);
            yield break;
        }

        AcceptanceReport acceptanceReport = CanAcceptPawn(selPawn);
        if (acceptanceReport.Accepted)
        {
            yield return FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption("EnterBuilding".Translate(this), delegate
            {
                SelectPawn(selPawn);
            }), selPawn, this);
        }
        else if (!acceptanceReport.Reason.NullOrEmpty())
        {
            yield return new FloatMenuOption("CannotEnterBuilding".Translate(this) + ": " + acceptanceReport.Reason.CapitalizeFirst(), null);
        }
    }

    protected virtual void BuildFloatMenuAvailableCorpses()
    {
        FloatMenuHelper.BuildFloatMenuAvailableCorpses(Map, CanAcceptCorpse, SelectCorpse);
    }

    protected virtual void SelectCorpse(Corpse corpse)
    {
        selectedCorpse = corpse;
        //if (!pawn.IsPrisonerOfColony && !pawn.Downed)
        //{
        //    pawn.jobs.TryTakeOrderedJob(JobMaker.MakeJob(JobDefOf.EnterBuilding, this), JobTag.Misc);
        //}
    }



    // Gizmos
    protected override Gizmo BuildInsertGizmo()
        => GizmoHelper.BuildGizmoInsertCorpse(BuildFloatMenuAvailableCorpses, PowerOn);



    // Inspect String
    protected override void InspectStringAddResourceStarvation(StringBuilder stringBuilder)
    {
        float starvationSeverityPercent = NeutroamineStarvationSeverity;
        if (starvationSeverityPercent > 0f)
        {
            var deficiency = "NGET_NeutroamineDeficiency".Translate();
            string text = ((NeutroamineStarvationSeverity >= 0f) ? "+" : "-");
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

        if (base.Working)
        {
            stringBuilder.Append(" (-").Append("PerHour".Translate((NeutroConsumedPerHour * Settings.nutritionMultiplier).ToString("F2"))).Append(")");
        }
    }

    protected override NamedArgument GetTargetName()
    {
        return selectedCorpse.InnerPawn.Named("PAWN");
    }



    // Ticks
    protected override bool Tick_ResourceStarvation()
    {
        if (NeutroamineStarvationSeverity >= 1f)
        {
            Fail();
            return true;
        }

        return false;
    }

    protected override void Tick_ConsumeResources()
    {
        //how much - per hour * multiplier / 1 hour of ticks
        var value = containedNeutroamine - NeutroConsumedPerHour * Settings.nutritionMultiplier / 2500f;
        containedNeutroamine = Mathf.Clamp(value, 0f, 2.1474836E+09f); //yuge

        if (containedNeutroamine <= 0f)
        {
            starvationTicks++;
        }
        else if (starvationTicks > 0)
        {
            starvationTicks--;
        }
    }

    // Operations
    protected override void CancelEnterBuilding()
    {
        // No job to cancel ...?

        //if (selectedPawn.CurJobDef == JobDefOf.EnterBuilding)
        //{
        //    selectedPawn.jobs.EndCurrentJob(JobCondition.InterruptForced);
        //}
    }

    protected override void KillPawnFromStarvation()
    {
        // Corpse is dead, no need to kill it

        //Hediff firstHediffOfDef = selectedPawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.BioStarvation);
        //selectedPawn.Kill(null, firstHediffOfDef);
    }

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
}
