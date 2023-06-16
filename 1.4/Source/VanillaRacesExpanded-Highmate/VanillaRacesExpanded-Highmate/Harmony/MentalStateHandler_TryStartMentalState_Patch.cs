using HarmonyLib;
using Verse;
using Verse.AI;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(MentalStateHandler), "TryStartMentalState")]
    public static class MentalStateHandler_TryStartMentalState_Patch
    {
        public static bool Prefix(Pawn ___pawn, ref bool __result, MentalStateDef stateDef)
        {
            if (___pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VRE_PsychicBondAbrasive) != null
                 && typeof(MentalState_InsultingSpree).IsAssignableFrom(stateDef.stateClass))
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
