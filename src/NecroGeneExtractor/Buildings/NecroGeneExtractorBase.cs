using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public float NeutroamineStored
    {
        get
        {
            float num = containedNeutroamine;
            for (int i = 0; i < innerContainer.Count; i++)
            {
                Thing thing = innerContainer[i];
                if (thing.def.defName == "neutroamine")
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
            if (selectedPawn == null)
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

    protected virtual RotStage TargetCorpseRotStage
    {
        get
        {
            var corpse = FindCorpseOfPawn(selectedPawn);
            return corpse.GetRotStage();
        }
    }

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
        if (selectedCorpse != null && selectedCorpse != corpse) //don't accept new corpse if already selected
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
                startTick = Find.TickManager.TicksGame;
                TicksRemaining = ExtractionTimeInTicks;
            }
            if (deselect)
            {
                Find.Selector.Select(corpse, playSound: false, forceDesignatorDeselect: false);
            }
        }
    }

    public static Corpse FindCorpseOfPawn(Pawn deadPawn)
    {
        // Use Linq to find the first Corpse whose InnerPawn matches the deadPawn
        Corpse corpse = Find.CurrentMap.listerThings.AllThings
            .OfType<Corpse>() // Filter for objects of type Corpse
            .FirstOrDefault(c => c.InnerPawn == deadPawn); // Find the first one matching the predicate

        return corpse;
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

        //TODO: is this reasonable? Maybe if another pawn can haul the corpse?
        if (!selPawn.CanReach(this, PathEndMode.InteractionCell, Danger.Deadly))
        {
            yield return new FloatMenuOption("CannotEnterBuilding".Translate(this) + ": " + "NoPath".Translate().CapitalizeFirst(), null);
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
        stringBuilder.AppendLineIfNotEmpty().Append("Nutrition".Translate()).Append(": ")
            .Append(NeutroamineStored.ToStringByStyle(ToStringStyle.FloatMaxOne));

        if (base.Working)
        {
            stringBuilder.Append(" (-").Append("PerHour".Translate((NeutroConsumedPerHour * Settings.nutritionMultiplier).ToString("F2"))).Append(")");
        }
    }



    // Ticks
    protected override bool Tick_ResourceStarvation()
    {
        //TODO: consume X neutroamine per day. If we are empty, count ticks
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
}
