using RimWorld;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;
namespace VanillaRacesExpandedHighmate
{
    public class InteractionWorker_FlirtingAttempt : InteractionWorker
    {
 
        public override float RandomSelectionWeight(Pawn initiator, Pawn recipient)
        {

            if (initiator.DevelopmentalStage.Juvenile() || recipient.DevelopmentalStage.Juvenile())
            {
                return 0f;
            }

            if (initiator.story?.traits?.HasTrait(InternalDefOf.VRE_Flirty) != true)
            {
                return 0f;
            }

            return 1f;
        }

        public static float SuccessChance(Pawn initiator, Pawn recipient)
        {

            if (initiator.IsQuestHelper() || recipient.IsQuestHelper())
            {
                return 0f;
            }
            float opinion = recipient.relations.OpinionOf(initiator);
            
            return opinion;

        }

       

      

        public override void Interacted(Pawn initiator, Pawn recipient, List<RulePackDef> extraSentencePacks, out string letterText, out string letterLabel, out LetterDef letterDef, out LookTargets lookTargets)
        {
            letterText = null;
            letterLabel = null;
            letterDef = null;
            lookTargets = null;
            if (Rand.Value*100 < SuccessChance(initiator, recipient))
            {

                initiator.needs?.mood?.thoughts?.memories?.TryGainMemory(InternalDefOf.VRE_Flirted, recipient);
                recipient.needs?.mood?.thoughts?.memories?.TryGainMemory(InternalDefOf.VRE_Flirted, initiator);

            }
           
        }

       


    
      
    }
}