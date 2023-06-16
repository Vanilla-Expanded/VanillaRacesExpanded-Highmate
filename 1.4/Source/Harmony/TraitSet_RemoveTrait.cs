using HarmonyLib;
using RimWorld;

using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaRacesExpandedHighmate
{


    [HarmonyPatch(typeof(TraitSet))]
    [HarmonyPatch("RemoveTrait")]
    public static class VanillaRacesExpandedHighmate_TraitSet_RemoveTrait_Patch
    {
        [HarmonyPostfix]
        static void RemoveFromStaticList(Trait trait, Pawn ___pawn)
        {
            if (trait.def == InternalDefOf.VRE_Distressed)
            {
                StaticCollectionsClass.RemoveFromDistressedTraitPawns(___pawn);
                GameComponent_PawnListsSaver comp = Current.Game.GetComponent<GameComponent_PawnListsSaver>();
                if (comp != null)
                {
                    comp.distressedTraitPawns_backup = StaticCollectionsClass.distressedTraitPawns;

                }
            }
            BondUtils.TryApplyBondEffects(___pawn);
        }
    }
}
