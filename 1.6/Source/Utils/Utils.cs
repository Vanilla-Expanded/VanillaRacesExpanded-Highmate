using HarmonyLib;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;


namespace VanillaRacesExpandedHighmate
{
    public static class Utils

    {
        public static bool HasLovinNeed(Pawn pawn)
        {
            Need_Lovin lovin = pawn.needs?.TryGetNeed<Need_Lovin>();
            if (lovin != null) { return true; }
            return false;

        }

        public static void SetLovinNeed(Pawn pawn,float value)
        {
            Need_Lovin lovin = pawn.needs?.TryGetNeed<Need_Lovin>();
            lovin.CurLevel = value;

        }
        public static float GetLovinNeed(Pawn pawn)
        {
            Need_Lovin lovin = pawn.needs?.TryGetNeed<Need_Lovin>();
            return lovin.CurLevel;

        }
        public static bool HasActiveGene(this Pawn pawn, GeneDef geneDef)
        {
            if (pawn.genes is null) return false;
            return pawn.genes.GetGene(geneDef)?.Active ?? false;
        }


    }
}
