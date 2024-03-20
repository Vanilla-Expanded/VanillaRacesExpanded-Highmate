using HarmonyLib;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaRacesExpandedHighmate
{

    [HarmonyPatch(typeof(Gene))]
    [HarmonyPatch("Notify_PawnDied")]
    public static class VanillaRacesExpandedHighmate_Gene_Notify_PawnDied_Patch
    {
        [HarmonyPostfix]
        static void RemoveFromStaticList(Gene __instance)
        {
            if (__instance.pawn.story?.traits?.HasTrait(InternalDefOf.VRE_Distressed)==true)
            {
                if (StaticCollectionsClass.RemoveFromDistressedTraitPawns(__instance.pawn)) {
                    GameComponent_PawnListsSaver comp = Current.Game.GetComponent<GameComponent_PawnListsSaver>();
                    if (comp != null)
                    {
                        comp.distressedTraitPawns_backup = StaticCollectionsClass.distressedTraitPawns;

                    }
                }
                
            }

            if (StaticCollectionsClass.RemoveFromPawnsWhoFucked(__instance.pawn))
            {
                GameComponent_PawnListsSaver comp = Current.Game.GetComponent<GameComponent_PawnListsSaver>();
                if (comp != null)
                {
                    comp.pawnsWhoFucked_backup = StaticCollectionsClass.pawnsWhoFucked;

                }
            }
        }
    }
}
