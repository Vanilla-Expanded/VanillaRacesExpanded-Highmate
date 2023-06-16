using HarmonyLib;
using RimWorld;
using System;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaRacesExpandedHighmate
{

    [HarmonyPatch(typeof(PregnancyUtility))]
    [HarmonyPatch("GetInheritedGenes")]
    [HarmonyPatch(new Type[] { typeof(Pawn), typeof(Pawn) })]
    public static class VanillaRacesExpandedHighmate_PregnancyUtility_GetInheritedGenes_Patch
    {

      

        [HarmonyPostfix]
        static void CalculateDominance(Pawn father, Pawn mother, ref List<GeneDef> __result )
        {
            bool flagFather = father?.genes?.HasGene(InternalDefOf.VRE_DominantGenome) == true;
            bool flagMother = mother?.genes?.HasGene(InternalDefOf.VRE_DominantGenome) == true;

            bool flagFatherRecessive = father?.genes?.HasGene(InternalDefOf.VRE_RecessiveGenome) == true;
            bool flagMotherRecessive = mother?.genes?.HasGene(InternalDefOf.VRE_RecessiveGenome) == true;

            if (flagFather & !flagMother)
            {
                List<GeneDef> listToReturn = new List<GeneDef>();
                foreach (Gene gene in father.genes.GenesListForReading)
                {
                    listToReturn.Add(gene.def);
                }
                __result = listToReturn;
            }

            if (!flagFather & flagMother)
            {
                List<GeneDef> listToReturn = new List<GeneDef>();
                foreach (Gene gene in mother.genes.GenesListForReading)
                {
                    listToReturn.Add(gene.def);
                }
                __result = listToReturn;
            }

            if(flagFatherRecessive & !flagMotherRecessive)
            {
                __result = PregnancyUtility.GetInheritedGenes(null, mother);
            }
            if (!flagFatherRecessive & flagMotherRecessive)
            {
                __result = PregnancyUtility.GetInheritedGenes(father, null);
            }

        }

       

    }
}
