using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaRacesExpandedHighmate
{
    [DefOf]
    public static class InternalDefOf
    {
        static InternalDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
        }

        public static TraitDef VRE_Flirty;
        public static TraitDef VRE_Distressed;

        public static ThoughtDef VRE_Flirted;
        public static ThoughtDef VRE_WhatAPerfectBody;

        public static GeneDef VRE_PerfectBody;
        public static GeneDef VRE_DominantGenome;
        public static GeneDef VRE_RecessiveGenome;
        public static GeneDef VRE_Harmless;
        public static GeneDef VRE_LovinDependency;

        public static MentalBreakDef GiveUpExit;

        public static ThoughtDef KnowGuestExecuted;
        public static ThoughtDef KnowColonistExecuted;
        public static ThoughtDef KnowPrisonerDiedInnocent;
        public static ThoughtDef KnowColonistDied;
        public static ThoughtDef BondedAnimalDied;
        public static ThoughtDef PawnWithGoodOpinionDied;
        public static ThoughtDef MySonDied;
        public static ThoughtDef MyDaughterDied;
        public static ThoughtDef MyHusbandDied;
        public static ThoughtDef MyWifeDied;
        public static ThoughtDef MyFianceDied;
        public static ThoughtDef MyFianceeDied;
        public static ThoughtDef MyLoverDied;
        public static ThoughtDef MyBrotherDied;
        public static ThoughtDef MySisterDied;
        public static ThoughtDef MyGrandchildDied;
        public static ThoughtDef MyFatherDied;
        public static ThoughtDef MyMotherDied;
        public static ThoughtDef MyNieceDied;
        public static ThoughtDef MyNephewDied;
        public static ThoughtDef MyHalfSiblingDied;
        public static ThoughtDef MyAuntDied;
        public static ThoughtDef MyUncleDied;
        public static ThoughtDef MyGrandparentDied;
        public static ThoughtDef MyCousinDied;
        public static ThoughtDef MyKinDied;
        public static ThoughtDef KnowPrisonerSold;
        public static ThoughtDef KnowGuestOrganHarvested;
        public static ThoughtDef KnowColonistOrganHarvested;
        public static ThoughtDef MyOrganHarvested;
        public static ThoughtDef ButcheredHumanlikeCorpse;
        public static ThoughtDef KnowButcheredHumanlikeCorpse;
        public static ThoughtDef ObservedLayingCorpse;
        public static ThoughtDef ObservedLayingRottingCorpse;
        public static ThoughtDef WitnessedDeathAlly;
        public static ThoughtDef WitnessedDeathNonAlly;
        public static ThoughtDef WitnessedDeathFamily;
        public static ThoughtDef DeniedJoining;
        public static ThoughtDef ColonistBanished;
        public static ThoughtDef ColonistBanishedToDie;
        public static ThoughtDef PrisonerBanishedToDie;
        public static ThoughtDef BondedAnimalBanished;

        [MayRequireIdeology]
        public static PreceptDef Lovin_FreeApproved;

        [MayRequire("VanillaExpanded.VanillaTraitsExpanded")]
        public static TraitDef VTE_Prude;



    }
}