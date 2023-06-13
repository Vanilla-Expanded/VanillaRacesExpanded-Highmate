using System;
using Verse;
using RimWorld;

namespace VanillaRacesExpandedHighmate
{
	public class ThoughtWorker_LovinNeed : ThoughtWorker
	{
		protected override ThoughtState CurrentStateInternal(Pawn p)
		{
			if (p.needs?.TryGetNeed<Need_Lovin>() == null)
			{
				return ThoughtState.Inactive;
			}

			if (p.genes?.HasGene(InternalDefOf.VRE_LovinDependency) != true)
			{
				return ThoughtState.Inactive;
			}
			Need_Lovin need = p.needs.TryGetNeed<Need_Lovin>();
			switch (need.CurCategory)
			{
				case LovinNeedCategory.Need:
					return ThoughtState.ActiveAtStage(0);
				case LovinNeedCategory.Demand:
					return ThoughtState.ActiveAtStage(1);
				case LovinNeedCategory.GivenUp:
					return ThoughtState.ActiveAtStage(2);
				case LovinNeedCategory.Satisfied:
					return ThoughtState.Inactive;

			
				default:
					throw new NotImplementedException();
			}
		}
	}
}
