using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(Pawn_RelationsTracker), "SetPregnancyApproach")]
    public static class Pawn_RelationsTracker_SetPregnancyApproach_Patch
    {
        public static void Postfix(Pawn_RelationsTracker __instance, Pawn partner)
        {
            __instance.pawn.relations.GetAdditionalPregnancyApproachData().pawnsWithLovinForPleasure.Remove(partner);
            partner.relations.GetAdditionalPregnancyApproachData().pawnsWithLovinForPleasure.Remove(__instance.pawn);
            __instance.pawn.relations.GetAdditionalPregnancyApproachData().pawnsWithPsychicConception.Remove(partner);
            partner.relations.GetAdditionalPregnancyApproachData().pawnsWithPsychicConception.Remove(__instance.pawn);
        }
    }
}
