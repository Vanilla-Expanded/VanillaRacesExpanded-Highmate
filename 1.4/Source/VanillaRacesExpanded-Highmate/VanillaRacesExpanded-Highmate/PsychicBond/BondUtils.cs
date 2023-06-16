using RimWorld;
using System.Linq;
using Verse;

namespace VanillaRacesExpandedHighmate
{
    public static class BondUtils
    {
        public static void TryApplyBondEffects(Pawn pawn)
        {
            var bondHediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.PsychicBond) as Hediff_PsychicBond;
            if (bondHediff != null && bondHediff.target is Pawn target)
            {
                foreach (var def in DefDatabase<HighmateBondEffectDef>.AllDefs)
                {
                    if (def.bondHediffTraits != null)
                    {
                        foreach (var bondHediffTrait in def.bondHediffTraits)
                        {
                            if (pawn.MapHeld == target.MapHeld && (bondHediffTrait.traitRequirement.HasTrait(pawn)
                                    || bondHediffTrait.traitRequirement.HasTrait(target)))
                            {
                                bondHediffTrait.Apply(pawn);
                                bondHediffTrait.Apply(target);
                            }
                            else
                            {
                                bondHediffTrait.TryRemove(pawn);
                                bondHediffTrait.TryRemove(target);
                            }

                        }
                    }
                }
            }
            else
            {
                foreach (var def in DefDatabase<HighmateBondEffectDef>.AllDefs)
                {
                    if (def.bondHediffTraits != null)
                    {
                        foreach (var bondHediffTrait in def.bondHediffTraits)
                        {
                            bondHediffTrait.TryRemove(pawn);
                        }
                    }
                }
            }
        }
    }
}
