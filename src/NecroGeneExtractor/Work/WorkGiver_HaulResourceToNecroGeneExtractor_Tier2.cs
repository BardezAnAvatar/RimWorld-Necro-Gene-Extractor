using Bardez.Biotech.NecroGeneExtractor.Defs;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Work;

public class WorkGiver_HaulResourceToNecroGeneExtractor_Tier2 : WorkGiver_HaulResourceToNecroGeneExtractor_Base
{
    public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(NecroGeneExtractor_DefsOf.Buildings.NGET_NecroGeneExtractorII);
}
