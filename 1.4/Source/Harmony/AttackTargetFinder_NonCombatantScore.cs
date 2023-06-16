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

    [HarmonyPatch(typeof(AttackTargetFinder))]
    [HarmonyPatch("NonCombatantScore")]

    public static class VanillaRacesExpandedHighmate_AttackTargetFinder_NonCombatantScore_Patch
    {
        [HarmonyPostfix]
        static void DisableHarmless(Thing target, ref float __result)
        {
            Pawn pawn = target as Pawn;
            
            if(pawn?.genes?.HasGene(InternalDefOf.VRE_Harmless)==true)
            {
                if(pawn.equipment?.PrimaryEq==null)
                {
                    __result = 25f; 

                }


            }
        }
    }
}
