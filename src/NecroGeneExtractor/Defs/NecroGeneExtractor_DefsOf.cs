using RimWorld;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Defs;

public static class NecroGeneExtractor_DefsOf
{
    [DefOf]
    public static class Buildings
    {
        public static ThingDef NGET_NecroGeneExtractorII;
        public static ThingDef NGET_NecroGeneExtractorIII;
    }

    [DefOf]
    public static class Resources
    {
        public static ThingDef Neutroamine;
    }

    [DefOf]
    public static class Jobs
    {
        public static JobDef CarryCorpseToNecroGeneExtractor_TierII;
        public static JobDef CarryCorpseToNecroGeneExtractor_TierIII;
    }
}
