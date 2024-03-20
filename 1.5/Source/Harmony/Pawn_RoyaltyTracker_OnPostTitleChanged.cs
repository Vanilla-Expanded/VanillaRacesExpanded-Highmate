using HarmonyLib;
using RimWorld;
using RimWorld.QuestGen;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace VanillaRacesExpandedHighmate
{
  

    [HarmonyPatch(typeof(Pawn_RoyaltyTracker), "OnPostTitleChanged")]
    public static class VanillaRacesExpandedHighmate_Pawn_RoyaltyTracker_OnPostTitleChanged_Patch
    {
        public static void Postfix(Pawn_RoyaltyTracker __instance, Faction faction, RoyalTitleDef prevTitle, RoyalTitleDef newTitle)
        {
            if (newTitle != null && __instance.pawn.IsColonist && PawnGenerator.IsBeingGenerated(__instance.pawn) is false
                && Current.CreatingWorld is null && __instance.pawn.Dead is false && __instance.pawn.Map!=null
                && (prevTitle is null || prevTitle.seniority < newTitle.seniority)
                && (newTitle == InternalDefOf.Baron || prevTitle is null && newTitle.seniority > InternalDefOf.Baron.seniority)
                && faction == Faction.OfEmpire)
            {

                var letter = LetterMaker.MakeLetter("VRE_AwardedHighmate".Translate(),"VRE_AwardedHighmateDesc".Translate(__instance.pawn.LabelCap),LetterDefOf.PositiveEvent);              
                Find.LetterStack.ReceiveLetter(letter);
                PawnGenerationRequest request = new PawnGenerationRequest(Faction.OfPlayer.def.basicMemberKind, Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: true, allowDead: false, allowDowned: false, canGeneratePawnRelations: false, false, 20f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null, forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, null, null, InternalDefOf.Highmate, null, null, 0f, DevelopmentalStage.Adult, null, null);
                Pawn pawn = PawnGenerator.GeneratePawn(request);
                DropPodUtility.DropThingsNear(__instance.pawn.Position, __instance.pawn.Map, new List<Thing>() { pawn }, 110, false, false, false, false);
            }
        }
    }
}
