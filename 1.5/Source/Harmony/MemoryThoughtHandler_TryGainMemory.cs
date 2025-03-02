using HarmonyLib;
using RimWorld;
using Verse;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaRacesExpandedHighmate
{

    [HarmonyPatch(typeof(MemoryThoughtHandler))]
    [HarmonyPatch("TryGainMemory")]
    [HarmonyPatch(new Type[] { typeof(Thought_Memory), typeof(Pawn) })]
    public static class VanillaRacesExpandedHighmate_MemoryThoughtHandler_TryGainMemory_Patch
    {
        [HarmonyPostfix]
        static void HandlePawnMemories(Thought_Memory newThought, Pawn otherPawn, MemoryThoughtHandler __instance)
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
                    __instance.RemoveMemory(__instance.OldestMemoryOfDef(ThoughtDefOf.GotSomeLovin));
                    __instance.pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(InternalDefOf.VRE_GotSomeLovin, otherPawn);
                    __instance.pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(InternalDefOf.VRE_WhatAPerfectBody, otherPawn);
                } else if (StaticCollectionsClass.pawnsWhoFucked.Contains(__instance.pawn))
                {

                    __instance.RemoveMemory(__instance.OldestMemoryOfDef(ThoughtDefOf.GotSomeLovin));
                }
            }

            if (newThought.MoodOffset() < 0 || newThought is Thought_MemorySocial { opinionOffset: < 0 })
            {
                if (StaticCollectionsClass.distressedTraitPawns.Contains(__instance.pawn) && StaticCollectionsClass.distressedThoughts.Contains(newThought.def))
                {
                    newThought.durationTicksOverride = Mathf.RoundToInt(newThought.DurationTicks * 2);
                }
            }
        }
    }
}
