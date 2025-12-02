using System.Runtime.CompilerServices;
using RimWorld;

namespace Bardez.Biotech.NecroGeneExtractor.Comps.Properties;

public class CompProperties_RefuelableNecroVat : CompProperties_Refuelable
{
    public CompProperties_RefuelableNecroVat()
    {
        this.compClass = typeof(CompRefuelableNecroVat);
    }
}
