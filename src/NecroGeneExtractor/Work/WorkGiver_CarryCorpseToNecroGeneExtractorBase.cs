using Bardez.Biotech.NecroGeneExtractor.Buildings;
using Bardez.Biotech.NecroGeneExtractor.Defs;
using RimWorld;
using Verse;
using Verse.AI;

namespace Bardez.Biotech.NecroGeneExtractor.Work;

public abstract class WorkGiver_CarryCorpseToNecroGeneExtractorBase : WorkGiver_CarryToBuilding
{
    public override bool ShouldSkip(Pawn pawn, bool forced = false)
    {
        if (!base.ShouldSkip(pawn, forced))
        {
            return !ModsConfig.BiotechActive;
        }

        return true;
    }

    public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t.IsBurning())
        {
            return false;
        }

        if (t is not NecroGeneExtractorBase geneVat)
        {
            return false;
        }
        Corpse selectedCorpse = geneVat.TargetedCorpse;

        // filled already -- no way to fill this
        if (geneVat.Corpse != null)
        {
            return false;
        }

        if (selectedCorpse == null || selectedCorpse.Map != pawn.Map || !geneVat.CanAcceptCorpse(selectedCorpse))
        {
            return false;
        }

        //the actual job
        if (def.workType != null && pawn.WorkTypeIsDisabled(def.workType))
        {
            return pawn.CanReserveAndReach(selectedCorpse, PathEndMode.OnCell, Danger.Deadly, 1, -1, null, forced);
        }

        return false;
    }

    public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t is not NecroGeneExtractorBase geneVat)
        {
            return null;
        }

        Job job = JobMaker.MakeJob(JobDefOf.CarryToBuilding, geneVat, geneVat.TargetedCorpse);
        job.count = 1;
        return job;
    }
}
