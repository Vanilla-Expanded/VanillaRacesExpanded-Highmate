using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(Pawn_RelationsTracker), "ExposeData")]
    public static class Pawn_RelationsTracker_ExposeData_Patch
    {
        public static void Postfix(Pawn_RelationsTracker __instance)
        {
            var data = __instance.GetAdditionalPregnancyApproachData();
            Scribe_Deep.Look(ref data, "VRE_additionalPregnancyApproachData");
            pawnPregnancyApproachData[__instance] = data;
        }

        public static Dictionary<Pawn_RelationsTracker, PregnancyApproachData> pawnPregnancyApproachData = new();
        public static PregnancyApproachData GetAdditionalPregnancyApproachData(this Pawn_RelationsTracker tracker)
        {
            if (!pawnPregnancyApproachData.TryGetValue(tracker, out var data))
            {
                pawnPregnancyApproachData[tracker] = data = new PregnancyApproachData();
            }
            return data;
        }
    }
}
