using System;
using Bardez.Biotech.NecroGeneExtractor.Buildings;
using RimWorld;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Comps;

/// <summary>Subclass of <see cref="CompRefuelable" /> that prints the InspectString additions differently</summary>
public class CompRefuelableNecroVat : CompRefuelable
{
    public override string CompInspectStringExtra()
    {
        string text = Props.FuelLabel + ": " + Fuel.ToStringDecimalIfSmall() + " / ";
        if (Props.targetFuelLevelConfigurable)
        {
            text += TargetFuelLevel.ToStringDecimalIfSmall()
                + " [" + Props.fuelCapacity.ToStringDecimalIfSmall() + "]";
        }
        else
        {
            text += Props.fuelCapacity.ToStringDecimalIfSmall();
        }

        if (HasFuel)
        {
            if (parent is not NecroGeneExtractor_Base necroVat)
                return text;

            if (necroVat.Working)
            {
                var hourlyNeutroRate = necroVat.NeutroConsumedPerHour.ToString("F2");
                text += " (-" + "PerHour".Translate(hourlyNeutroRate) + ")";
            }
            int numTicks = Convert.ToInt32(Fuel / necroVat.NeutroConsumedPerTick);
            text += " " + "NGET_RemainingFuelTime".Translate().Formatted(numTicks.ToStringTicksToPeriod());
        }
        else if (!HasFuel && !Props.outOfFuelMessage.NullOrEmpty())
        {
            string arg = Props.outOfFuelMessage;
            text += $"\n{arg} ({GetFuelCountToFullyRefuel()}x {Props.fuelFilter.AnyAllowedDef.label})";
        }

        return text;
    }
}
