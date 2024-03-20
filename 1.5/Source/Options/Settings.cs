using RimWorld;
using UnityEngine;
using Verse;


namespace VanillaRacesExpandedHighmate
{


    public class VanillaRacesExpandedHighmate_Settings : ModSettings

    {


        public static bool flagCatHighmates = true;
       



        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref flagCatHighmates, "flagCatHighmates", true, true);
           



        }

        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();


            ls.Begin(inRect);
           
            ls.CheckboxLabeled("VRE_EnableCatHighmates".Translate(), ref flagCatHighmates, "VRE_EnableCatHighmatesDescription".Translate());
            ls.Gap(12f);
           

            ls.End();
        }



    }










}
