using RimWorld;
using UnityEngine;
using Verse;


namespace VanillaRacesExpandedHighmate
{



    public class VanillaRacesExpandedHighmate_Mod : Mod
    {


        public VanillaRacesExpandedHighmate_Mod(ModContentPack content) : base(content)
        {
            GetSettings<VanillaRacesExpandedHighmate_Settings>();
        }
        public override string SettingsCategory()
        {

            return "VRE - Highmate";



        }



        public override void DoSettingsWindowContents(Rect inRect)
        {
            VanillaRacesExpandedHighmate_Settings.DoWindowContents(inRect);
        }
    }


}
