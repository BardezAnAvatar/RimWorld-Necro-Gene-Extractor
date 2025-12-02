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
            text += "ConfiguredTargetFuelLevel".Translate(TargetFuelLevel.ToStringDecimalIfSmall())
                + " [" + Props.fuelCapacity.ToStringDecimalIfSmall() + "]";
        }
        else
        {
            text += Props.fuelCapacity.ToStringDecimalIfSmall();
        }

        if (!Props.consumeFuelOnlyWhenUsed && HasFuel)
        {
            text += " ";
            int numTicks = (int)(Fuel / Props.fuelConsumptionRate * 60000f);

            if (parent is NecroGeneExtractor_Base necroVat && necroVat.Working)
            {
                var hourlyNeutroRate = necroVat.NeutroConsumedPerHour.ToString("F2");
                text += "(-" + "PerHour".Translate(hourlyNeutroRate) + ") ";
            }
            text += "NGET_RemainingFuelTime".Translate().Formatted(numTicks.ToStringTicksToPeriod());
        }

        if (!HasFuel && !Props.outOfFuelMessage.NullOrEmpty())
        {
            string arg = Props.outOfFuelMessage;
            text += $"\n{arg} ({GetFuelCountToFullyRefuel()}x {Props.fuelFilter.AnyAllowedDef.label})";
        }

        return text;
    }
}
