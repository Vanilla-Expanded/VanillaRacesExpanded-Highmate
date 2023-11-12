using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace VanillaRacesExpandedHighmate
{
    public class PregnancyApproachData : IExposable
    {
        public HashSet<Pawn> pawnsWithPsychicConception = new();
        public HashSet<Pawn> pawnsWithLovinForPleasure = new();
        public void ExposeData()
        {
            Scribe_Collections.Look(ref pawnsWithPsychicConception, "pawnsWithPsychicConception", LookMode.Reference);
            Scribe_Collections.Look(ref pawnsWithLovinForPleasure, "pawnsWithLovinForPleasure", LookMode.Reference);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                pawnsWithPsychicConception ??= new HashSet<Pawn>();
                pawnsWithLovinForPleasure ??= new HashSet<Pawn>();
            }
        }
    }
}
