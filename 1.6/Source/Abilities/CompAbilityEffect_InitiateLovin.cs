﻿
using RimWorld;
using System;
using Verse;
using RimWorld.Planet;
using System.Collections.Generic;
using VanillaRacesExpandedHighmate;
using System.Linq;
using Verse.AI;

namespace VanillaRacesExpandedHighmate
{
    public class CompAbilityEffect_InitiateLovin : CompAbilityEffect
    {

        public List<PawnRelationDef> relationDefs = new List<PawnRelationDef>() { PawnRelationDefOf.Lover,PawnRelationDefOf.Spouse,PawnRelationDefOf.Fiance};

       

        private const float MinAgeForLovin = 16f;

        public new CompProperties_InitiateLovin Props => (CompProperties_InitiateLovin)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                if (pawn.CurJob.def != InternalDefOf.VRE_InitiateLovin) {
                    Job job = JobMaker.MakeJob(InternalDefOf.VRE_InitiateLovin, pawn);
                    parent.pawn.jobs.StartJob(job, JobCondition.InterruptForced);
                }
                         

            }

        }

        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            return Valid(target);
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn pawn = target.Pawn;
            if (pawn == null)
            {
                return false;
            }
            if (!AbilityUtility.ValidateMustBeHumanOrWildMan(pawn, throwMessages, parent))
            {
                return false;
            }
            if((float)pawn.ageTracker.AgeBiologicalYears < MinAgeForLovin)
            {
                if (throwMessages)
                {
                    Messages.Message("VRE_TooYoungForLovin".Translate(parent.pawn.LabelShortCap, pawn.LabelShortCap), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }
            if ((float)parent.pawn.ageTracker.AgeBiologicalYears < MinAgeForLovin)
            {
                if (throwMessages)
                {
                    Messages.Message("VRE_TooYoungForLovinCaster".Translate(parent.pawn.LabelShortCap, pawn.LabelShortCap), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

            if (pawn.Faction != null && !pawn.IsSlaveOfColony && !pawn.IsPrisonerOfColony)
            {
                if (pawn.Faction.HostileTo(parent.pawn.Faction))
                {
                   
                        if (throwMessages)
                        {
                            Messages.Message("VRE_MessageCantUseLovinOnResistingPersons".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                        }
                        return false;
                    
                }
                else if (pawn.IsQuestLodger() || pawn.Faction != parent.pawn.Faction)
                {
                    if (throwMessages)
                    {
                        Messages.Message("VRE_MessageCantUseLovinOnOtherFactions".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                    }
                    return false;
                }
            }

            if(!ModsConfig.IdeologyActive || (ModsConfig.IdeologyActive && 
                pawn.Ideo?.HasPrecept(InternalDefOf.Lovin_FreeApproved)!=true && 
                parent.pawn.Ideo?.HasPrecept(InternalDefOf.Lovin_FreeApproved) != true))
            {
                if (pawn.relations?.OpinionOf(parent.pawn) <= 0)
                {
                    if (throwMessages)
                    {
                        Messages.Message("VRE_CantUseLovinLowOpinion".Translate(parent.pawn.LabelShortCap, pawn.LabelShortCap, pawn.relations.OpinionOf(parent.pawn)), pawn, MessageTypeDefOf.RejectInput, historical: false);
                    }
                    return false;
                }
            }

            if (pawn.story?.traits?.HasTrait(TraitDefOf.Asexual) == true)
            {
                if (throwMessages)
                {
                    Messages.Message("VRE_CantUseLovinWithTraitAsexual".Translate(parent.pawn.LabelShortCap, pawn.LabelShortCap), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;

            }
            if (InternalDefOf.VTE_Prude!=null&&pawn.story?.traits?.HasTrait(InternalDefOf.VTE_Prude) == true)
            {
                if (throwMessages)
                {
                    Messages.Message("VRE_CantUseLovinWithTraitPrude".Translate(parent.pawn.LabelShortCap, pawn.LabelShortCap), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;

            }

            if (pawn.relations?.FamilyByBlood?.Contains(parent.pawn)==true)
            {
                if (throwMessages)
                {
                    Messages.Message("VRE_CantUseOnFamily".Translate(parent.pawn.LabelShortCap, pawn.LabelShortCap), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;

            }



            return true;


        }

        public override Window ConfirmationDialog(LocalTargetInfo target, Action confirmAction)
        {
            Pawn targetPawn = target.Pawn;
            Pawn casterPawn = parent.pawn;

            if (targetPawn != null)
            {

               if (!CheckHasAnyOfThePrecepts(targetPawn, casterPawn))
                {
                    List<Pawn> casterLovers = GetLovers(casterPawn);
                    List<Pawn> targetLovers = GetLovers(targetPawn);

                    if (casterLovers.Count>0 && !casterLovers.Contains(targetPawn))
                    {                    
                        foreach(Pawn lover in casterLovers)
                        {                       
                            if (lover != targetPawn)
                            {                              
                                return Dialog_MessageBox.CreateConfirmation("VRE_RelationshipWillBreak".Translate(casterPawn.LabelShortCap, targetPawn.LabelShortCap, casterLovers.ToStringSafeEnumerable()), confirmAction, destructive: true);                              
                            }
                        }
                    }
                    if (targetLovers.Count > 0 && !targetLovers.Contains(casterPawn))
                    {
                        foreach (Pawn lover in targetLovers)
                        {
                            if (lover != casterPawn)
                            {
                                return Dialog_MessageBox.CreateConfirmation("VRE_RelationshipWillBreakTarget".Translate(casterPawn.LabelShortCap, targetPawn.LabelShortCap, targetLovers.ToStringSafeEnumerable()), confirmAction, destructive: true);
                            }
                        }
                    }
                }

          
            }
            return null;
        }


       public bool CheckHasAnyOfThePrecepts(Pawn targetPawn, Pawn casterPawn)
       {
            if (ModsConfig.IdeologyActive)
            {
                bool ideosOnCaster = (casterPawn.Ideo?.HasPrecept(InternalDefOf.Lovin_FreeApproved) != true) ||
                    (casterPawn.Ideo?.HasPrecept(InternalDefOf.Lovin_Free) != true);

                bool ideosOnTarget = (targetPawn.Ideo?.HasPrecept(InternalDefOf.Lovin_FreeApproved) != true) ||
                    (targetPawn.Ideo?.HasPrecept(InternalDefOf.Lovin_Free) != true);

                return ideosOnCaster && ideosOnTarget;
            }
            return false;
       }



        public List<Pawn> GetLovers(Pawn pawn)
        {
            List<Pawn> tmpSpouses = new List<Pawn>();
          
            List<DirectPawnRelation> directRelations = pawn.relations.DirectRelations;
            for (int i = 0; i < directRelations.Count; i++)
            {
                if (relationDefs.Contains(directRelations[i].def))
                {
                    if (!directRelations[i].otherPawn.Dead && directRelations[i].otherPawn!=pawn) { 
                        tmpSpouses.Add(directRelations[i].otherPawn); 
                    }
                    
                }
            }
         
            return tmpSpouses;
        }




    }
}