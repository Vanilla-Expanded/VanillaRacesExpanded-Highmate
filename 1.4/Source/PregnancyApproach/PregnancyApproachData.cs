using System.Collections.Generic;
using Verse;

namespace VanillaRacesExpandedHighmate
{
    public class PregnancyApproachData : IExposable
    {
        public List<Pawn> pawnsWithPsychicConception = new();
        public void SetPsychicConceptionApproach(Pawn partner)
        {
            pawnsWithPsychicConception.Add(partner);
        }
        public void RemovePsychicConceptionApproach(Pawn partner)
        {
            pawnsWithPsychicConception.Remove(partner);
        }
        public void ExposeData()
        {
            Scribe_Collections.Look(ref pawnsWithPsychicConception, "pawnsWithPsychicConception", LookMode.Reference);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                pawnsWithPsychicConception ??= new List<Pawn>();
            }
        }
    }
}
