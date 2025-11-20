using Bardez.Biotech.NecroGeneExtractor.Buildings;
using Bardez.Biotech.NecroGeneExtractor.Defs;
using Bardez.Biotech.NecroGeneExtractor.Utilities;
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

        DebugMessaging.DebugMessage("Not burning.");

        if (pawn.Map.designationManager.DesignationOn(t, DesignationDefOf.Deconstruct) != null)
        {
            return false;
        }

        DebugMessaging.DebugMessage("Not deconstructing.");

        if (t is not NecroGeneExtractorBase geneVat)
        {
            return false;
        }

        DebugMessaging.DebugMessage("Is a gene vat.");

        // filled already -- no way to fill this
        if (geneVat.Corpse != null)
        {
            return false;
        }

        DebugMessaging.DebugMessage("gene vat is not filled.");

        Corpse selectedCorpse = geneVat.TargetedCorpse;
        if (selectedCorpse == null)
        {
            return false;
        }

        DebugMessaging.DebugMessage($"Targeted Corpse is not null; name is `{selectedCorpse.InnerPawn.Name}`.");

        if (selectedCorpse.Map != pawn.Map)
        {
            return false;
        }
        DebugMessaging.DebugMessage("Corpse is on the same map.");

        if (!geneVat.CanAcceptCorpse(selectedCorpse))
        {
            return false;
        }
        DebugMessaging.DebugMessage($"Gene vat can accept `{selectedCorpse.InnerPawn.Name}`.");

        DebugMessaging.DebugMessage($"Work Type: `{def?.workType}`.");


        //the actual job
        if (def.workType != null && pawn.WorkTypeIsDisabled(def.workType))
        {
            DebugMessaging.DebugMessage($"Pawn can do work type.");

            return pawn.CanReserveAndReach(selectedCorpse, PathEndMode.InteractionCell, Danger.Deadly, 1, -1, null, forced);
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
