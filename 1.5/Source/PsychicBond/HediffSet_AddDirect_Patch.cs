using HarmonyLib;
using RimWorld;
using Verse;



namespace VanillaRacesExpandedHighmate
{

    [HarmonyPatch(typeof(HediffSet), nameof(HediffSet.AddDirect))]
    public static class HediffSet_AddDirect_Patch
    {
        public static void Postfix(Pawn ___pawn, Hediff hediff)
        {
            if (hediff.def == HediffDefOf.PsychicBond)
            {
                BondUtils.TryApplyBondEffects(___pawn);
            }
        }
    }
}
