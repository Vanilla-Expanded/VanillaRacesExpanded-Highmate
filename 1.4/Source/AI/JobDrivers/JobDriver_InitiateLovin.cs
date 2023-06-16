using System.Collections.Generic;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace VanillaRacesExpandedHighmate
{
    public class JobDriver_InitiateLovin : JobDriver
    {
        private int ticksLeft;

        private TargetIndex PartnerInd = TargetIndex.A;

        public List<PawnRelationDef> relationDefs = new List<PawnRelationDef>() { PawnRelationDefOf.Lover, PawnRelationDefOf.Spouse, PawnRelationDefOf.Fiance };

        private static List<Pawn> tmpSpouses = new List<Pawn>();

        private const int TicksBetweenHeartMotes = 100;

        private static float PregnancyChance = 0.05f;

        private bool jumping;

        private int moveChangeInterval = 240;


        private Pawn Partner => (Pawn)(Thing)job.GetTarget(PartnerInd);



        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref ticksLeft, "ticksLeft", 0);
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {

            return pawn.Reserve(Partner, job, 1, -1, null, errorOnFailed);

        }


        public override IEnumerable<Toil> MakeNewToils()
        {

            this.FailOnDespawnedOrNull(PartnerInd);
            this.FailOn(() => !Partner.health.capacities.CanBeAwake);


            Toil toil = ToilMaker.MakeToil("MakeNewToils");
            toil.initAction = delegate
            {
                ticksLeft = 1250;
                pawn.apparel.Notify_ApparelChanged();
                pawn.Drawer.renderer.graphics.apparelGraphics.Clear();

                if (!ModsConfig.IdeologyActive || (ModsConfig.IdeologyActive &&
                pawn.Ideo?.HasPrecept(InternalDefOf.Lovin_FreeApproved) != true &&
                Partner.Ideo?.HasPrecept(InternalDefOf.Lovin_FreeApproved) != true))
                {
                    List<Pawn> lovers = GetLovers(pawn);

                    if (lovers.Count > 0)
                    {
                        foreach (Pawn recipient in lovers)
                        {
                            if (recipient != Partner)
                            {
                                ProcessBreakups(pawn, recipient);
                            }

                        }
                    }

                }


                if (Partner.CurJob == null || Partner.CurJob.def != InternalDefOf.VRE_InitiateLovin)
                {
                    Job newJob = JobMaker.MakeJob(InternalDefOf.VRE_InitiateLovin, pawn);
                    Partner.jobs.StartJob(newJob, JobCondition.InterruptForced);

                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.InitiatedLovin, pawn.Named(HistoryEventArgsNames.Doer)));
                    if (InteractionWorker_RomanceAttempt.CanCreatePsychicBondBetween_NewTemp(pawn, Partner) && InteractionWorker_RomanceAttempt.TryCreatePsychicBondBetween(pawn, Partner) && (PawnUtility.ShouldSendNotificationAbout(pawn) || PawnUtility.ShouldSendNotificationAbout(Partner)))
                    {
                        Find.LetterStack.ReceiveLetter("LetterPsychicBondCreatedLovinLabel".Translate(), "LetterPsychicBondCreatedLovinText".Translate(pawn.Named("BONDPAWN"), Partner.Named("OTHERPAWN")), LetterDefOf.PositiveEvent, new LookTargets(pawn, Partner));
                    }
                }

            };
            toil.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return toil;



            Toil toil2 = Toils_General.Wait(1250, TargetIndex.None).FailOnDestroyedNullOrForbidden(TargetIndex.A).WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            toil2.FailOn(() => Partner.CurJob == null || Partner.CurJob.def != InternalDefOf.VRE_InitiateLovin);


            toil2.AddPreTickAction(delegate
            {
                ticksLeft--;
                if (ticksLeft <= 0)
                {
                    ReadyForNextToil();
                }
                else if (pawn.IsHashIntervalTick(TicksBetweenHeartMotes))
                {
                    FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.Heart);
                }
            });

            jumping = Rand.Bool;
            toil2.tickAction = delegate
            {
                if (ticksLeft % moveChangeInterval == 0)
                {
                    jumping = !jumping;
                }
                if (ticksLeft % 125 == 0 && !jumping)
                {
                    pawn.Rotation = Rot4.Random;
                }

            };
            toil2.handlingFacing = true;

            toil2.AddFinishAction(delegate
            {

                pawn.Drawer.renderer.graphics.SetApparelGraphicsDirty();
                PortraitsCache.SetDirty(pawn);
                GlobalTextureAtlasManager.TryMarkPawnFrameSetDirty(pawn);



                Thought_Memory thought_Memory = (Thought_Memory)ThoughtMaker.MakeThought(ThoughtDefOf.GotSomeLovin);
                if ((base.pawn.health != null && base.pawn.health.hediffSet != null && base.pawn.health.hediffSet.hediffs.Any((Hediff h) => h.def == HediffDefOf.LoveEnhancer)) || (Partner.health != null && Partner.health.hediffSet != null && Partner.health.hediffSet.hediffs.Any((Hediff h) => h.def == HediffDefOf.LoveEnhancer)))
                {
                    thought_Memory.moodPowerFactor = 1.5f;
                }
                if (base.pawn.needs.mood != null)
                {
                    base.pawn.needs.mood.thoughts.memories.TryGainMemory(thought_Memory, Partner);
                }
                Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.GotLovin, base.pawn.Named(HistoryEventArgsNames.Doer)));
                HistoryEventDef def = (base.pawn.relations.DirectRelationExists(PawnRelationDefOf.Spouse, Partner) ? HistoryEventDefOf.GotLovin_Spouse : HistoryEventDefOf.GotLovin_NonSpouse);
                Find.HistoryEventsManager.RecordEvent(new HistoryEvent(def, base.pawn.Named(HistoryEventArgsNames.Doer)));
                if (ModsConfig.BiotechActive)
                {
                    Pawn pawn = ((base.pawn.gender == Gender.Male) ? base.pawn : ((Partner.gender == Gender.Male) ? Partner : null));
                    Pawn pawn2 = ((base.pawn.gender == Gender.Female) ? base.pawn : ((Partner.gender == Gender.Female) ? Partner : null));
                    if (pawn != null && pawn2 != null && Rand.Chance(PregnancyChance * PregnancyUtility.PregnancyChanceForPartners(pawn2, pawn)))
                    {
                        bool success;
                        GeneSet inheritedGeneSet = PregnancyUtility.GetInheritedGeneSet(pawn, pawn2, out success);
                        if (success)
                        {
                            Hediff_Pregnant hediff_Pregnant = (Hediff_Pregnant)HediffMaker.MakeHediff(HediffDefOf.PregnantHuman, pawn2);
                            hediff_Pregnant.SetParents(null, pawn, inheritedGeneSet);
                            pawn2.health.AddHediff(hediff_Pregnant);
                        }
                        else if (PawnUtility.ShouldSendNotificationAbout(pawn) || PawnUtility.ShouldSendNotificationAbout(pawn2))
                        {
                            Messages.Message("MessagePregnancyFailed".Translate(pawn.Named("FATHER"), pawn2.Named("MOTHER")) + ": " + "CombinedGenesExceedMetabolismLimits".Translate(), new LookTargets(pawn, pawn2), MessageTypeDefOf.NegativeEvent);
                        }
                    }
                }
            });
            toil2.socialMode = RandomSocialMode.Off;
            yield return toil2;
        }


        public List<Pawn> GetLovers(Pawn pawn)
        {
            tmpSpouses.Clear();

            List<DirectPawnRelation> directRelations = pawn.relations.DirectRelations;
            for (int i = 0; i < directRelations.Count; i++)
            {
                if (relationDefs.Contains(directRelations[i].def))
                {
                    if (!directRelations[i].otherPawn.Dead) { tmpSpouses.Add(directRelations[i].otherPawn); }

                }
            }
            return tmpSpouses;
        }

        public void ProcessBreakups(Pawn initiator, Pawn recipient)
        {
            bool flag = false;
            bool flag2 = false;
            if (initiator.relations.DirectRelationExists(PawnRelationDefOf.Spouse, recipient))
            {
                initiator.relations.RemoveDirectRelation(PawnRelationDefOf.Spouse, recipient);
                initiator.relations.AddDirectRelation(PawnRelationDefOf.ExSpouse, recipient);
                SpouseRelationUtility.RemoveGotMarriedThoughts(initiator, recipient);
                flag = SpouseRelationUtility.ChangeNameAfterDivorce(initiator);
                flag2 = SpouseRelationUtility.ChangeNameAfterDivorce(recipient);
            }
            else
            {
                initiator.relations.TryRemoveDirectRelation(PawnRelationDefOf.Lover, recipient);
                initiator.relations.TryRemoveDirectRelation(PawnRelationDefOf.Fiance, recipient);
                initiator.relations.AddDirectRelation(PawnRelationDefOf.ExLover, recipient);
                if (recipient.needs.mood != null)
                {
                    recipient.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.BrokeUpWithMe, initiator);
                }
            }
            if (initiator.ownership.OwnedBed != null && initiator.ownership.OwnedBed == recipient.ownership.OwnedBed)
            {
                ((Rand.Value < 0.5f) ? initiator : recipient).ownership.UnclaimBed();
            }
            TaleRecorder.RecordTale(TaleDefOf.Breakup, initiator, recipient);
            if (PawnUtility.ShouldSendNotificationAbout(initiator) || PawnUtility.ShouldSendNotificationAbout(recipient))
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("LetterNoLongerLovers".Translate(initiator.LabelShort, recipient.LabelShort, initiator.Named("PAWN1"), recipient.Named("PAWN2")));
                stringBuilder.AppendLine();
                if (flag)
                {
                    stringBuilder.Append("LetterNoLongerLovers_BackToBirthName".Translate(initiator.Named("PAWN")));
                }
                if (flag2)
                {
                    if (flag)
                    {
                        stringBuilder.Append(" ");
                    }
                    stringBuilder.Append("LetterNoLongerLovers_BackToBirthName".Translate(recipient.Named("PAWN")));
                }
                if (flag || flag2)
                {
                    stringBuilder.AppendLine();
                }
                Find.LetterStack.ReceiveLetter("LetterLabelBreakup".Translate(), stringBuilder.ToString().TrimEndNewlines(), LetterDefOf.NegativeEvent, new LookTargets(initiator, recipient));

            }
        }

            public override Vector3 ForcedBodyOffset
        {
            get
            {
                float num = Mathf.Sin((float)ticksLeft / 60f * 8f);
                if (jumping)
                {
                    float z = Mathf.Max(Mathf.Pow((num + 1f) * 0.5f, 2f) * 0.2f - 0.06f, 0f);
                    return new Vector3(0f, 0f, z);
                }
                float num2 = Mathf.Sign(num);
                return new Vector3(EaseInOutQuad(Mathf.Abs(num) * 0.6f) * 0.09f * num2, 0f, 0f);
                float EaseInOutQuad(float v)
                {
                    if (!((double)v < 0.5))
                    {
                        return 1f - Mathf.Pow(-2f * v + 2f, 4f) / 2f;
                    }
                    return 8f * v * v * v * v;
                }
            }
        }



    }

}
