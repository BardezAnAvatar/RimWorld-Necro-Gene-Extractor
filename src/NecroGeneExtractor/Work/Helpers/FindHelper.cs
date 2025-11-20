using Bardez.Biotech.NecroGeneExtractor.Defs;
using RimWorld;
using Verse;
using Verse.AI;

namespace Bardez.Biotech.NecroGeneExtractor.Work.Helpers;

public static class FindHelper
{
    public static Thing FindNeutroamine(Pawn pawn)
    {
        return GenClosest.ClosestThingReachable(
            pawn.Position,
            pawn.Map,
            ThingRequest.ForDef(NecroGeneExtractor_DefsOf.Neutroamine),
            PathEndMode.InteractionCell,
            TraverseParms.For(pawn),
            9999f,
            (Thing x) => !x.IsForbidden(pawn) && pawn.CanReserve(x));
    }
}
