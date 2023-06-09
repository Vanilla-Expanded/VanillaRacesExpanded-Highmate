using HarmonyLib;
using RimWorld;

using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaRacesExpandedHighmate
{

    [HarmonyPatch(typeof(MemoryThoughtHandler))]
    [HarmonyPatch("TryGainMemory")]
    public static class VanillaRacesExpandedHighmate_MemoryThoughtHandler_TryGainMemory_Patch
    {
        [HarmonyPostfix]
        static void AddToPawnsWhoFucked(Thought_Memory newThought, Pawn otherPawn, MemoryThoughtHandler __instance)
        {
            if(newThought.def == ThoughtDefOf.GotSomeLovin)
            {
                if (otherPawn.genes?.HasGene(InternalDefOf.VRE_PerfectBody) == true)
                {
                    StaticCollectionsClass.AddToPawnsWhoFucked(__instance.pawn);
                    GameComponent_PawnListsSaver comp = Current.Game.GetComponent<GameComponent_PawnListsSaver>();
                    if (comp != null)
                    {
                        comp.pawnsWhoFucked_backup = StaticCollectionsClass.pawnsWhoFucked;

                    }
                    __instance.pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(InternalDefOf.VRE_WhatAPerfectBody, otherPawn);
                } else if (StaticCollectionsClass.pawnsWhoFucked.Contains(__instance.pawn))
                {

                    __instance.RemoveMemory(__instance.OldestMemoryOfDef(ThoughtDefOf.GotSomeLovin));
                }
            }
        }
    }
}
