using HarmonyLib;
using RimWorld;

using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaRacesExpandedHighmate
{

    [HarmonyPatch(typeof(JobDriver_Lovin))]
    [HarmonyPatch("GenerateRandomMinTicksToNextLovin")]
    public static class VanillaRacesExpandedHighmate_JobDriver_Lovin_GenerateRandomMinTicksToNextLovin_Patch
    {
        [HarmonyPostfix]
        static void PawnFucks(Pawn pawn)
        {
            Need_Lovin need = pawn?.needs?.TryGetNeed<Need_Lovin>();
            if (need != null)
            {
                need.CurLevel = 1f;
            }

            
        }
    }
}
