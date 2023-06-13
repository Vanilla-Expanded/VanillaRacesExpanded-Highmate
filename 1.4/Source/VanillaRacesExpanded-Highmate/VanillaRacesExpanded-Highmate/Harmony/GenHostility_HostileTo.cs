using HarmonyLib;
using RimWorld;
using System;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaRacesExpandedHighmate
{

    [HarmonyPatch(typeof(GenHostility))]
    [HarmonyPatch("HostileTo")]
    [HarmonyPatch(new Type[] { typeof(Thing), typeof(Thing) })]
    public static class VanillaRacesExpandedHighmate_GenHostility_HostileTo_Patch
    {
        [HarmonyPostfix]
        static void DisableHarmless(Thing a, Thing b, ref bool __result)
        {
            Pawn pawn = a as Pawn;
            Pawn pawn2 = b as Pawn;

            if(pawn?.genes?.HasGene(InternalDefOf.VRE_Harmless)==true || pawn2?.genes?.HasGene(InternalDefOf.VRE_Harmless) == true)
            {
                if(pawn.equipment?.PrimaryEq==null || pawn2.equipment?.PrimaryEq == null)
                {
                    __result = false;

                }


            }
        }
    }
}
