using HarmonyLib;
using Verse;
using Verse.AI;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(MentalBreakWorker), "CommonalityFor")]
    public static class MentalBreakWorker_CommonalityFor_Patch
    {
        public static void Postfix(MentalBreakWorker __instance, Pawn pawn, ref float __result)
        {
            if (__instance.def.mentalState != null && __instance.def.mentalState.IsAggro)
            {
                if (pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VRE_PsychicBondBloodlust) != null)
                {
                    __result *= 2f;
                }
            }
        }
    }
}
