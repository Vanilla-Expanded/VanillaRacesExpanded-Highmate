using HarmonyLib;

using Verse;



namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.DeSpawn))]
    public static class Pawn_Despawn_Patch 
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
