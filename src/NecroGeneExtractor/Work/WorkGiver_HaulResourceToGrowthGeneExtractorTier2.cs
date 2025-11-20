using Bardez.Biotech.NecroGeneExtractor.Defs;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Work;

public class WorkGiver_HaulResourceToGrowthGeneExtractorTier2 : WorkGiver_HaulResourceToGrowthGeneExtractorBase
{
    public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(NecroGeneExtractor_DefsOf.NGET_NecroGeneExtractorII);
}
