using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(PregnancyUtility), "PregnancyChanceForPartners")]
    public static class PregnancyUtility_PregnancyChanceForPartners_Patch
    {
        public static void Postfix(Pawn woman, Pawn man, ref float __result)
        {
            var data = woman.relations.GetAdditionalPregnancyApproachData();
            if (data.pawnsWithPsychicConception.Contains(man))
            {
                __result = 20f;
            }
            else if (data.pawnsWithLovinForPleasure.Contains(man))
            {
                __result = 0f;
            }
        }
    }
}
