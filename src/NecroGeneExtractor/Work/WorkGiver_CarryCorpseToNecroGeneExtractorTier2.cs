using Bardez.Biotech.NecroGeneExtractor.Defs;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Work;

public class WorkGiver_CarryCorpseToNecroGeneExtractorTier2 : WorkGiver_CarryCorpseToNecroGeneExtractorBase
{
    public override ThingRequest PotentialWorkThingRequest
        => ThingRequest.ForDef(NecroGeneExtractor_DefsOf.NGET_NecroGeneExtractorII);
}
