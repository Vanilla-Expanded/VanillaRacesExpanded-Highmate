using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(NegativeInteractionUtility), "NegativeInteractionChanceFactor")]
    public static class NegativeInteractionUtility_NegativeInteractionChanceFactor_Patch
    {
        public static void Postfix(ref float __result, Pawn initiator)
        {
            if (initiator.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VRE_PsychicBondAbrasive) != null)
            {
                __result = 0;
            }
        }
    }
}
