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
            if (woman.relations.GetAdditionalPregnancyApproachData().pawnsWithPsychicConception.Contains(man))
            {
                __result = 20f;
            }
        }
    }
}
