
using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;


namespace VanillaRacesExpandedHighmate
{

    public static class StaticCollectionsClass
    {

        //This static class stores lists of pawns for different things.

        public static HashSet<Pawn> distressedTraitPawns = new HashSet<Pawn>();

        // These pawns FUCK

        public static HashSet<Pawn> pawnsWhoFucked = new HashSet<Pawn>();


        public static HashSet<ThoughtDef> distressedThoughts = new HashSet<ThoughtDef>() { InternalDefOf.KnowGuestExecuted, 
            InternalDefOf.KnowColonistExecuted, InternalDefOf.KnowPrisonerDiedInnocent, InternalDefOf.KnowColonistDied, InternalDefOf.BondedAnimalDied
            , InternalDefOf.PawnWithGoodOpinionDied, InternalDefOf.MySonDied, InternalDefOf.MyDaughterDied, InternalDefOf.MyHusbandDied, InternalDefOf.MyWifeDied
            , InternalDefOf.MyFianceDied, InternalDefOf.MyFianceeDied, InternalDefOf.MyLoverDied, InternalDefOf.MyBrotherDied, InternalDefOf.MySisterDied
            , InternalDefOf.MyGrandchildDied, InternalDefOf.MyFatherDied, InternalDefOf.MyMotherDied, InternalDefOf.MyNieceDied, InternalDefOf.MyNephewDied
            , InternalDefOf.MyHalfSiblingDied, InternalDefOf.MyAuntDied, InternalDefOf.MyUncleDied, InternalDefOf.MyGrandparentDied
            , InternalDefOf.MyCousinDied, InternalDefOf.MyKinDied, InternalDefOf.KnowPrisonerSold, InternalDefOf.KnowGuestOrganHarvested, InternalDefOf.KnowColonistOrganHarvested
            , InternalDefOf.MyOrganHarvested, InternalDefOf.ButcheredHumanlikeCorpse, InternalDefOf.KnowButcheredHumanlikeCorpse, InternalDefOf.ObservedLayingCorpse
            , InternalDefOf.ObservedLayingRottingCorpse, InternalDefOf.WitnessedDeathAlly, InternalDefOf.WitnessedDeathNonAlly, InternalDefOf.WitnessedDeathFamily
            , InternalDefOf.DeniedJoining, InternalDefOf.ColonistBanished, InternalDefOf.ColonistBanishedToDie, InternalDefOf.PrisonerBanishedToDie
            , InternalDefOf.BondedAnimalBanished
};


        public static void AddToDistressedTraitPawns(Pawn pawn)
        {
            if (!distressedTraitPawns.Contains(pawn))
            {
                distressedTraitPawns.Add(pawn);
            }

        }
        public static bool RemoveFromDistressedTraitPawns(Pawn pawn)
        {
            if (distressedTraitPawns.Contains(pawn))
            {
                distressedTraitPawns.Remove(pawn);
                return true;
            }
            else return false;

        }

        public static void AddToPawnsWhoFucked(Pawn pawn)
        {
            if (!pawnsWhoFucked.Contains(pawn))
            {
                pawnsWhoFucked.Add(pawn);
            }

        }
        public static bool RemoveFromPawnsWhoFucked(Pawn pawn)
        {
            if (pawnsWhoFucked.Contains(pawn))
            {
                pawnsWhoFucked.Remove(pawn);
                return true;
            }
            else return false;

        }

    }
}
