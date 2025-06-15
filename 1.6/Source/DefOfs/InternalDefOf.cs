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

        public static XenotypeDef Highmate;

        public static ThoughtDef VRE_Flirted;
        public static ThoughtDef VRE_WhatAPerfectBody;
        public static ThoughtDef VRE_GotSomeLovin;

        public static GeneDef VRE_PerfectBody;
        public static GeneDef VRE_DominantGenome;
        public static GeneDef VRE_RecessiveGenome;
        public static GeneDef VRE_Harmless;
        public static GeneDef VRE_LovinDependency;

        public static MentalBreakDef GiveUpExit;

        public static JobDef VRE_InitiateLovin;

        public static RulePackDef VRE_Sentence_FlirtingAttemptAccepted;
        public static RulePackDef VRE_Sentence_FlirtingAttemptRejected;

        [MayRequireRoyalty]
        public static RoyalTitleDef Baron;

        [MayRequireIdeology]
        public static PreceptDef Lovin_FreeApproved;

        public static PreceptDef Lovin_Free;

        [MayRequire("VanillaExpanded.VanillaTraitsExpanded")]
        public static TraitDef VTE_Prude;

        public static HediffDef VRE_PsychicBondBloodlust;
        public static HediffDef VRE_PsychicBondGreatMemory;
        public static HediffDef VRE_PsychicBondAbrasive;
        public static HediffDef VRE_Naked;
    }
}