using HarmonyLib;
using Verse;
using Verse.AI;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(MentalBreakWorker), "BreakCanOccur")]
    public static class MentalBreakWorker_BreakCanOccur_Patch
    {
        public static bool Prefix(MentalBreakWorker __instance, ref bool __result, Pawn pawn)
        {
            if (pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VRE_PsychicBondAbrasive) != null && __instance.def.mentalState != null
                 && typeof(MentalState_InsultingSpree).IsAssignableFrom(__instance.def.mentalState.stateClass))
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
