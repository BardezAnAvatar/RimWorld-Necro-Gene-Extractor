using System;
using System.Collections.Generic;
using Bardez.Biotech.NecroGeneExtractor.Buildings;
using Bardez.Biotech.NecroGeneExtractor.Utilities;
using Verse;
using Verse.AI;

namespace Bardez.Biotech.NecroGeneExtractor.Jobs;

public abstract class JobDriver_HaulCorpseToNecroGeneExtractor_Base<TGeneVat> : JobDriver
    where TGeneVat : NecroGeneExtractor_Base
{
    private int FillDuration => 300;

    protected virtual TGeneVat GeneVat
    {
        get
        {
            LocalTargetInfo target = job.GetTarget(TargetIndex.A);
            return target.Thing as TGeneVat;
        }
    }

    protected Corpse TargetCorpse
    {
        get
        {
            LocalTargetInfo target = job.GetTarget(TargetIndex.B);
            return target.Thing as Corpse;
        }
    }

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        var reserveGeneVat = ReservationUtility.Reserve(this.pawn, this.GeneVat, this.job, 1, -1, null, errorOnFailed, false);
        var reserveCorpse = ReservationUtility.Reserve(this.pawn, this.TargetCorpse, this.job, 1, -1, null, errorOnFailed, false);

        DebugMessaging.DebugMessage($"Pawn {pawn.Name} {(reserveCorpse ? "did" : "did not")} reserve corpse {TargetCorpse.InnerPawn.Name}.");
        DebugMessaging.DebugMessage($"Pawn {pawn.Name} {(reserveGeneVat ? "did" : "did not")} reserve the gene vat.");

        return reserveCorpse && reserveGeneVat;
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        ToilFailConditions.FailOnDespawnedNullOrForbidden(this, TargetIndex.A);
        AddEndCondition(GetJobStatus);

        yield return ToilFailConditions.FailOnSomeonePhysicallyInteracting(
            ToilFailConditions.FailOnDespawnedNullOrForbidden(
                Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.InteractionCell, false), TargetIndex.B),
            TargetIndex.B);

        yield return ToilFailConditions.FailOnDestroyedNullOrForbidden(
            Toils_Haul.StartCarryThing(TargetIndex.B, false, true, false, true, false),
            TargetIndex.B);

        yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell, false);

        yield return ToilEffects.WithProgressBarToilDelay(
            ToilFailConditions.FailOnCannotTouch(
                ToilFailConditions.FailOnDestroyedNullOrForbidden(
                    ToilFailConditions.FailOnDestroyedNullOrForbidden(
                        Toils_General.Wait(this.FillDuration, TargetIndex.None),
                        TargetIndex.B),
                    TargetIndex.A),
                TargetIndex.A, PathEndMode.Touch),
            TargetIndex.A, false, -0.5f);

        yield return new Toil()
        {
            initAction = new Action(() =>
            {
                this.GeneVat.TryAcceptCorpse(TargetCorpse);
                this.pawn.jobs.EndCurrentJob(JobCondition.Succeeded, true, true);
                //TargetCorpse.DeSpawn(DestroyMode.Vanish); handled in building method
            }),
            defaultCompleteMode = ToilCompleteMode.Instant,
        };
    }

    private JobCondition GetJobStatus()
        => this.GeneVat.Corpse != null 
            ? JobCondition.Succeeded
            : JobCondition.Ongoing;
}
