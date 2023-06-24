using System.Collections.Generic;
using Verse;

namespace VanillaRacesExpandedHighmate
{
    public class BondTraitEffect
    {
        public List<TraitRequirement> traitRequirements;

        public HediffDef hediff;
        public void Apply(Pawn pawn)
        {
            pawn.health.AddHediff(hediff);
        }
        public void TryRemove(Pawn pawn)
        {
            var hed = pawn.health.hediffSet.GetFirstHediffOfDef(hediff);
            if (hed != null)
            {
                pawn.health.RemoveHediff(hed);
            }
        }
    }
}
