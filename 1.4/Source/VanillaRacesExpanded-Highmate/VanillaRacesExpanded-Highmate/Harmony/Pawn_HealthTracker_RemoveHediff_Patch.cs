using HarmonyLib;
using RimWorld;
using Verse;



namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(Pawn_HealthTracker), nameof(Pawn_HealthTracker.RemoveHediff))]
    public static class Pawn_HealthTracker_RemoveHediff_Patch
    {
        public static void Postfix(Pawn ___pawn, Hediff hediff)
        {
            if (hediff is Hediff_PsychicBond psychicBond && psychicBond.target is Pawn pawn)
            {
                BondUtils.TryApplyBondEffects(___pawn);
                BondUtils.TryApplyBondEffects(pawn);
            }
        }
    }
}
