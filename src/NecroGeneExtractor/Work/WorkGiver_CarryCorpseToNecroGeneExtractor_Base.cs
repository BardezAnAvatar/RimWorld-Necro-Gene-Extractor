using Bardez.Biotech.NecroGeneExtractor.Buildings;
using Bardez.Biotech.NecroGeneExtractor.Defs;
using Bardez.Biotech.NecroGeneExtractor.Utilities;
using RimWorld;
using Verse;
using Verse.AI;

namespace Bardez.Biotech.NecroGeneExtractor.Work;

public abstract class WorkGiver_CarryCorpseToNecroGeneExtractor_Base : WorkGiver_Scanner
{
    protected abstract JobDef ExtractorCorpseJob { get; }

    public override PathEndMode PathEndMode => PathEndMode.InteractionCell;

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

        if (pawn.Map.designationManager.DesignationOn(t, DesignationDefOf.Deconstruct) != null)
        {
            return false;
        }

        if (t is not NecroGeneExtractor_Base geneVat)
        {
            return false;
        }

        // filled already -- no way to fill this
        if (geneVat.Corpse != null)
        {
            return false;
        }

        Corpse selectedCorpse = geneVat.TargetedCorpse;
        if (selectedCorpse == null)
        {
            return false;
        }

        if (selectedCorpse.Map != pawn.Map)
        {
            return false;
        }

        if (!geneVat.CanAcceptCorpse(selectedCorpse))
        {
            return false;
        }

        //the actual job
        if (def.workType != null && !pawn.WorkTypeIsDisabled(def.workType))
        {
            DebugMessaging.DebugMessage($"Pawn {pawn.Name} can do work type.");

            //TODO: inline these. Potentially expensive
            var corpseForbidden = selectedCorpse.IsForbidden(pawn);
            var canReserveCorpse = pawn.CanReserveAndReach(selectedCorpse, PathEndMode.InteractionCell, Danger.Deadly, 1, -1, null, forced);
            var canReserveGeneVat = pawn.CanReserveAndReach(geneVat, PathEndMode.InteractionCell, Danger.Deadly, 1, -1, null, forced);
            DebugMessaging.DebugMessage($"Pawn {pawn.Name} {(canReserveCorpse ? "can" : "cannot")} reserve corpse {selectedCorpse.InnerPawn.Name}.");
            DebugMessaging.DebugMessage($"Pawn {pawn.Name} {(canReserveCorpse ? "can" : "cannot")} reserve the gene vat.");

            return !corpseForbidden && canReserveCorpse && canReserveGeneVat;
        }

        return false;
    }

    public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t is not NecroGeneExtractor_Base geneVat)
        {
            return null;
        }
        
        Job job = JobMaker.MakeJob(ExtractorCorpseJob, geneVat, geneVat.TargetedCorpse);
        job.count = 1;
        return job;
    }
}
