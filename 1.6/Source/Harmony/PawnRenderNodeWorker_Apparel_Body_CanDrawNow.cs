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


    [HarmonyPatch(typeof(PawnRenderNodeWorker_Apparel_Body), "CanDrawNow")]
    public static class VanillaRacesExpandedHighmate_PawnRenderNodeWorker_Apparel_Body_CanDrawNow_Patch
    {
        public static void Postfix(PawnRenderNode node, PawnDrawParms parms, ref bool __result)
        {
            if (node.tree.pawn.health.hediffSet.HasHediff(InternalDefOf.VRE_Naked))
            {
                __result = false;
            }
        }
    }








}



