
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


        public static HashSet<ThoughtDef> distressedThoughts = new HashSet<ThoughtDef>();

        static StaticCollectionsClass()
        {
            // Honestly, this list grew so big that using DefOf is just inconvenient at this point.
            distressedThoughts.AddRange(new[]
                {
                    // Death
                    "KnowPrisonerDiedInnocent", "KnowColonistDied", "PawnWithGoodOpinionDied", "TameVeneratedAnimalDied",
                    "WitnessedDeathAlly", "WitnessedDeathNonAlly", "WitnessedDeathFamily", "OtherTravelerDied", "KilledMyFriend",
                    "OnKill_BadThought", "SlaughteredAnimal_Know_Prohibited", "SlaughteredAnimal_Know_Prohibited_Mood",
                    "SlaughteredAnimal_Horrible", "SlaughteredAnimal_Know_Horrible", "SlaughteredAnimal_Know_Horrible_Mood",
                    "SlaughteredAnimal_Disapproved", "SlaughteredAnimal_Know_Disapproved", "SlaughteredAnimal_Know_Disapproved_Mood",
                    "KilledInnocentAnimal_Know_Abhorrent", "KilledInnocentAnimal_Know_Abhorrent_Mood", "KilledInnocentAnimal_Horrible",
                    "KilledInnocentAnimal_Know_Horrible", "KilledInnocentAnimal_Know_Horrible_Mood", "KilledInnocentAnimal_Disapproved",
                    "KilledInnocentAnimal_Know_Disapproved", "KilledInnocentAnimal_Know_Disapproved_Mood", "InnocentPrisonerDied_Know_Abhorrent",
                    "InnocentPrisonerDied_Know_Abhorrent_Mood", "InnocentPrisonerDied_Abhorrent", "InnocentPrisonerDied_Horrible",
                    "InnocentPrisonerDied_Know_Horrible", "BloodfeederDied_Revered", "KilledChild", "VME_Death_Troubling",
                    "VME_Insectoids_Exalted", "AM_Horses_Desired_HorseDied",
                    // Executing people
                    "KnowGuestExecuted", "KnowColonistExecuted", "ExecutedGuest_Know_Abhorrent_Mood", "ExecutedColonist_Know_Abhorrent_Mood",
                    "ExecutedPrisoner_Know_Abhorrent", "ExecutedPrisoner_Horrible", "ExecutedPrisoner_Know_Horrible",
                    "ExecutedPrisonerInnocent_Horrible", "ExecutedPrisonerInnocent_Know_Horrible",
                    // Someone lost
                    "ColonistLost", "PawnWithGoodOpinionLost",
                    // Banished
                    "ColonistBanished", "ColonistBanishedToDie", "PrisonerBanishedToDie", "BondedAnimalBanished",
                    // Cannibalism/butchering humanoids
                    "ButcheredHumanlikeCorpse", "KnowButcheredHumanlikeCorpse", "ButcheredHumanlikeCorpseOpinion", "AteCorpse",
                    "AteHumanlikeMeatDirect", "AteHumanlikeMeatAsIngredient", "AteRawHumanlikeMeat", "AteHumanMeat_Abhorrent",
                    "ButcheredHuman_Abhorrent", "ButcheredHuman_Know_Abhorrent", "ButcheredHuman_Know_Abhorrent_Opinion",
                    "AteHumanMeat_Know_Abhorrent", "AteHumanMeat_Horrible", "ButcheredHuman_Horrible", "ButcheredHuman_Know_Horrible",
                    "ButcheredHuman_Know_Horrible_Opinion", "AteHumanMeat_Know_Horrible", "AteHumanMeat_Disapproved", "ButcheredHuman_Disapproved",
                    "ButcheredHuman_Know_Disapproved", "ButcheredHuman_Know_Disapproved_Opinion", "ButcheredHuman_Know_Disapproved_Opinion",
                    // Observing corpses
                    "ObservedLayingCorpse", "ObservedLayingRottingCorpse",
                    // Someone or something getting hurt
                    "BlindingCeremony_Know_Horrible", "ScarificationCeremony_Know_Horrible", "VME_Ranching_Disliked_AttackedAnimal",
                    "VME_Violence_Disapproved", "VME_Violence_Abhorrent",
                    // Body part harvesting/using/trading
                    "KnowGuestOrganHarvested", "KnowColonistOrganHarvested", "MyOrganHarvested", "TradedOrgan_Abhorrent", "TradedOrgan_Know_Abhorrent",
                    "TradedOrgan_Know_Abhorrent_Mood", "SoldOrgan_Disapproved", "SoldOrgan_Know_Disapproved", "SoldOrgan_Know_Horrible_Mood",
                    "InstalledOrgan_Abhorrent", "InstalledOrgan_Know_Abhorrent", "InstalledOrgan_Know_Abhorrent_Mood", "HarvestedOrgan_Abhorrent",
                    "HarvestedOrgan_Know_Abhorrent", "HarvestedOrgan_Know_Abhorrent_Mood", "HarvestedOrgan_Horrible", "HarvestedOrgan_Know_Horrible",
                    "HarvestedOrgan_Know_Horrible_Mood",
                    // Charity refusal
                    "DeniedJoining", "CharityRefused_Essential_Beggars", "CharityRefused_Important_Beggars", "CharityRefused_Worthwhile_Beggars",
                    "CharityRefused_Essential_Beggars_Betrayed", "CharityRefused_Important_Beggars_Betrayed", "CharityRefused_Worthwhile_Beggars_Betrayed",
                    "CharityRefused_Essential_Pilgrims", "CharityRefused_Important_Pilgrims", "CharityRefused_Worthwhile_Pilgrims",
                    "CharityRefused_Essential_Pilgrims_Betrayed", "CharityRefused_Important_Pilgrims_Betrayed", "CharityRefused_Worthwhile_Pilgrims_Betrayed",
                    "CharityRefused_Essential_WandererJoins", "CharityRefused_Important_WandererJoins", "CharityRefused_Worthwhile_WandererJoins",
                    "CharityRefused_Essential_ShuttleCrashRescue", "CharityRefused_Important_ShuttleCrashRescue", "CharityRefused_Worthwhile_ShuttleCrashRescue",
                    "CharityRefused_Essential_RefugeePodCrash", "CharityRefused_Important_RefugeePodCrash", "CharityRefused_Worthwhile_RefugeePodCrash",
                    "CharityRefused_Essential_HospitalityRefugees", "CharityRefused_Important_HospitalityRefugees", "CharityRefused_Worthwhile_HospitalityRefugees",
                    "CharityRefused_Essential_IntroWimp", "CharityRefused_Important_IntroWimp", "CharityRefused_Worthwhile_IntroWimp",
                    "CharityRefused_Essential_ThreatReward_Joiner", "CharityRefused_Important_ThreatReward_Joiner", "CharityRefused_Worthwhile_ThreatReward_Joiner",
                    // Slavery
                    "SoldSlave_Know_Abhorrent", "SoldSlave_Know_Abhorrent_Mood", "EnslavedPrisoner_Know_Abhorrent", "SoldSlave_Horrible",
                    "EnslavedPrisoner_Horrible", "SoldSlave_Know_Horrible", "SoldSlave_Know_Horrible_Mood", "EnslavedPrisoner_Know_Horrible",
                    "EnslavedPrisoner_Know_Horrible_Mood", "SoldSlave_Disapproved", "EnslavedPrisoner_Disapproved", "SoldSlave_Know_Disapproved",
                    "SoldSlave_Know_Disapproved_Mood", "EnslavedPrisoner_Know_Disapproved", "EnslavedPrisoner_Know_Disapproved_Mood",
                    "VME_SoldSlave_Know_Forbidden", "VME_SoldSlave_Know_Forbidden_Mood",
                    // Terror
                    "ObservedGibbetCage", "ObservedSkullspike",
                    // Pregnancy
                    "PregnancyTerminated", "Stillbirth", "Miscarried", "PartnerMiscarried",
                    // Other
                    "BondedAnimalReleased", "FailedToRescueRelative", "AteVeneratedAnimalMeat",
                    "KnowPrisonerSold", "BecameGhoul", "VME_ParticipatedInRaid_Abhorrent",
                    // For debug purposes only
                    "DebugBad",

                    // Excluded thoughts include ones that give positive mood, like cannibalism
                    // thought for cannibal pawns or thoughts about rivals dying.
                }
                // Just in case
                .Distinct()
                // Change false to true if you want to test for possibly incorrect entries,
                // may come in handy when an update drops and removes/renames some defs.
                .Select(x => DefDatabase<ThoughtDef>.GetNamed(x, false))
                .Where(x => x != null));

            // Handle thoughts related to pawn relations
            foreach (var def in DefDatabase<PawnRelationDef>.AllDefs)
            {
                AddIfNotNull(def.diedThought);
                AddIfNotNull(def.diedThoughtFemale);
                AddIfNotNull(def.killedThought);
                AddIfNotNull(def.killedThoughtFemale);
                AddIfNotNull(def.lostThought);
                AddIfNotNull(def.lostThoughtFemale);
                if (def.soldThoughts != null)
                {
                    foreach (var thought in def.soldThoughts)
                        AddIfNotNull(thought);
                }
            }

            if (!distressedThoughts.Any())
            {
                // If distressedThoughts is empty, then most likely a mod
                // attempted to access this class way too early and we
                // attempted to initialize here before defs were initialized.
                Log.Error($"List of thoughts affected by distressed trait is empty. Did any mod access StaticCollectionsClass too early in RimWorld startup?");
            }
            else
            {
                // A cleanup just in case
                var incorrectEntries = distressedThoughts.Where(x => !x.IsMemory).ToList();
                if (incorrectEntries.Any())
                {
                    Log.Error($"List of thoughts affected by distressed trait includes non-memory thoughts: {incorrectEntries.ToStringSafeEnumerable()}");
                    distressedThoughts.RemoveWhere(x => !x.IsMemory);
                }
            }

            void AddIfNotNull(ThoughtDef def)
            {
                if (def != null)
                    distressedThoughts.Add(def);
            }
        }


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
