using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using VFECore;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(Pawn_RelationsTracker), "ExposeData")]
    public static class Pawn_RelationsTracker_ExposeData_Patch
    {
        public static Dictionary<Pawn_RelationsTracker, PregnancyApproachData> pawnPregnancyApproachData = new();
        public static void Postfix(Pawn_RelationsTracker __instance)
        {
            try
            {
                if (Scribe.mode != LoadSaveMode.Saving)
                {
                    var data = __instance.GetBackCompat();
                    Scribe_Deep.Look(ref data, "VRE_additionalPregnancyApproachData");
                    if (data != null)
                    {
                        pawnPregnancyApproachData[__instance] = data;
                    }
                    if (data != null)
                    {
                        var VRE_LovinForPleasure = DefDatabase<PregnancyApproachDef>.GetNamed("VRE_LovinForPleasure");
                        var VRE_PsychicConception = DefDatabase<PregnancyApproachDef>.GetNamed("VRE_PsychicConception");

                        if (data.pawnsWithPsychicConception != null)
                        {
                            foreach (var pawn in data.pawnsWithPsychicConception)
                            {
                                pawn.relations.GetAdditionalPregnancyApproachData().partners[__instance.pawn] = VRE_PsychicConception;
                                __instance.pawn.relations.GetAdditionalPregnancyApproachData().partners[pawn] = VRE_PsychicConception;
                            }
                        }

                        if (data.pawnsWithLovinForPleasure != null)
                        {
                            foreach (var pawn in data.pawnsWithLovinForPleasure)
                            {
                                pawn.relations.GetAdditionalPregnancyApproachData().partners[__instance.pawn] = VRE_LovinForPleasure;
                                __instance.pawn.relations.GetAdditionalPregnancyApproachData().partners[pawn] = VRE_LovinForPleasure;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public static PregnancyApproachData GetBackCompat(this Pawn_RelationsTracker tracker)
        {
            if (tracker is not null)
            {
                if (!pawnPregnancyApproachData.TryGetValue(tracker, out var data) || data is null)
                {
                    pawnPregnancyApproachData[tracker] = data = new PregnancyApproachData();
                }
                data.pawnsWithPsychicConception ??= new HashSet<Pawn>();
                data.pawnsWithLovinForPleasure ??= new HashSet<Pawn>();
                return data;
            }
            throw new System.Exception("Pawn_RelationsTracker was null by some reason");
        }
    }
}
