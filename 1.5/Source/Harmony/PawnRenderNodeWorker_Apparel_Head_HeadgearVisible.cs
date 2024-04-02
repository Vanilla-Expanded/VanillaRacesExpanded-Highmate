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


    [HarmonyPatch(typeof(PawnRenderNodeWorker_Apparel_Head), "HeadgearVisible")]
    public static class VanillaRacesExpandedHighmate_PawnRenderNodeWorker_Apparel_Head_HeadgearVisible_Patch
    {
        public static void Postfix(PawnDrawParms parms, ref bool __result)
        {
            if (parms.pawn.health.hediffSet.HasHediff(InternalDefOf.VRE_Naked))
            {
                __result = false;
            }
        }
    }








}



