using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaRacesExpandedHighmate
{


    [HarmonyPatch(typeof(Thought_Memory))]
    [HarmonyPatch("DurationTicks", MethodType.Getter)]
    public static class VanillaRacesExpandedHighmate_Thought_Memory_DurationTicks_Patch
    {
        [HarmonyPostfix]
        public static void IncreaseDuration(ref int __result,Thought_Memory __instance)
        {

            if (StaticCollectionsClass.distressedTraitPawns.Contains(__instance.pawn) && StaticCollectionsClass.distressedThoughts.Contains(__instance.def)) {
                __result *= 2;
            }
            
           

        }
    }








}
