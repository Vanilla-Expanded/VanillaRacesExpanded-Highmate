using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


namespace VanillaRacesExpandedHighmate
{
    public class GameComponent_PawnListsSaver : GameComponent
    {




        public HashSet<Pawn> distressedTraitPawns_backup = new HashSet<Pawn>();
        public HashSet<Pawn> pawnsWhoFucked_backup = new HashSet<Pawn>();


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref distressedTraitPawns_backup, "distressedTraitPawns_backup", LookMode.Reference);
            Scribe_Collections.Look(ref pawnsWhoFucked_backup, "pawnsWhoFucked_backup", LookMode.Reference);

        }

        public GameComponent_PawnListsSaver(Game game) : base()
        {
        }

        public override void FinalizeInit()
        {
            StaticCollectionsClass.distressedTraitPawns = this.distressedTraitPawns_backup;
            StaticCollectionsClass.pawnsWhoFucked = this.pawnsWhoFucked_backup;

            base.FinalizeInit();

        }





    }


}
