using Bardez.Biotech.NecroGeneExtractor.Defs;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Work;

public class WorkGiver_HaulResourceToNecroGeneExtractorTier2 : WorkGiver_HaulResourceToNecroGeneExtractorBase
{
    public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(NecroGeneExtractor_DefsOf.NGET_NecroGeneExtractorII);
}
