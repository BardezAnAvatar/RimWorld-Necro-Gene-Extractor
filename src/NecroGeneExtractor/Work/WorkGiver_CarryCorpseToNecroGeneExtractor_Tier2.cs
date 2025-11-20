using Bardez.Biotech.NecroGeneExtractor.Defs;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Work;

public class WorkGiver_CarryCorpseToNecroGeneExtractor_Tier2 : WorkGiver_CarryCorpseToNecroGeneExtractor_Base
{
    public override ThingRequest PotentialWorkThingRequest
        => ThingRequest.ForDef(NecroGeneExtractor_DefsOf.Buildings.NGET_NecroGeneExtractorII);

    protected override JobDef ExtractorCorpseJob
        => NecroGeneExtractor_DefsOf.Jobs.CarryCorpseToNecroGeneExtractor_TierII;
}
