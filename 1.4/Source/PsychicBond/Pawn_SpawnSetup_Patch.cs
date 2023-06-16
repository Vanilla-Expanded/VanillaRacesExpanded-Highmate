using HarmonyLib;

using Verse;



namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.SpawnSetup))]
    public static class Pawn_SpawnSetup_Patch
    {
        public static void Postfix(Pawn __instance)
        {
            if (__instance.RaceProps.Humanlike)
            {
                BondUtils.TryApplyBondEffects(__instance);
            }
        }
    }
}
