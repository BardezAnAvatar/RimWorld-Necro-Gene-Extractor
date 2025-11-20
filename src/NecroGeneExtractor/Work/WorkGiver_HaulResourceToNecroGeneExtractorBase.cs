using Bardez.Biotech.NecroGeneExtractor.Buildings;
using Bardez.Biotech.NecroGeneExtractor.Defs;
using Bardez.Biotech.NecroGeneExtractor.Work.Helpers;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Bardez.Biotech.NecroGeneExtractor.Work;

public abstract class WorkGiver_HaulResourceToNecroGeneExtractorBase : WorkGiver_Scanner
{
    public override PathEndMode PathEndMode => PathEndMode.InteractionCell;

    public override bool ShouldSkip(Pawn pawn, bool forced = false)
    {
        return !ModsConfig.BiotechActive;
    }

    public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t.IsForbidden(pawn) || !pawn.CanReserve(t, 1, -1, null, forced))
        {
            return false;
        }

        if (pawn.Map.designationManager.DesignationOn(t, DesignationDefOf.Deconstruct) != null)
        {
            return false;
        }

        if (t.IsBurning())
        {
            return false;
        }

        if (t is not NecroGeneExtractorBase geneVat)
        {
            return false;
        }

        if (geneVat.NeutroamineNeeded >= 1f)
        {
            if (FindHelper.FindNeutroamine(pawn) == null)
            {
                JobFailReason.Is("NoIngredient".Translate(NecroGeneExtractor_DefsOf.Neutroamine));
                return false;
            }

            return true;
        }

        return false;
    }

    public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t is not NecroGeneExtractorBase geneVat)
        {
            return null;
        }

        //Building_GeneExtractorTier geneVat = (Building_GeneExtractorTier)t;
        if (geneVat.NeutroamineNeeded > 0f)
        {
            Thing thing = FindHelper.FindNeutroamine(pawn);
            if (thing != null)
            {
                var fetch = Mathf.Min(geneVat.NeutroamineNeeded, thing.stackCount);
                int insert = Mathf.CeilToInt(fetch);
                Job job = JobMaker.MakeJob(JobDefOf.HaulToContainer, thing, t);
                job.count = insert;
                return job;
            }
        }
        return null;
    }
}
