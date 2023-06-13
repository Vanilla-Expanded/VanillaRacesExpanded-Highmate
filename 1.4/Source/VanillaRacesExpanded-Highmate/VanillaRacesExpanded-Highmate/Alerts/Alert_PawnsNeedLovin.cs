
using RimWorld;
using System;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace VanillaRacesExpandedHighmate
{
	public class Alert_PawnsNeedLovin : Alert
	{


		private List<Pawn> pawnsNeedingLovinResult = new List<Pawn>();

		private List<Pawn> PawnsNeedingLovin
		{
			get
			{
				pawnsNeedingLovinResult.Clear();
				foreach (Pawn pawn in PawnsFinder.AllMaps_FreeColonistsSpawned)
				{
					if ((pawn.needs?.TryGetNeed<Need_Lovin>()?.CurLevel < 0.5f)
						&& pawn.genes?.HasGene(InternalDefOf.VRE_LovinDependency) == true)
					{
						pawnsNeedingLovinResult.Add(pawn);
					}
				}
				return pawnsNeedingLovinResult;
			}
		}

		public Alert_PawnsNeedLovin()
		{
			defaultLabel = "VRE_LovinNeed".Translate();
			defaultPriority = AlertPriority.Medium;
		}

		public override AlertReport GetReport()
		{
			return AlertReport.CulpritsAre(PawnsNeedingLovin);
		}

		public override TaggedString GetExplanation()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Pawn pawn = null;
			foreach (Pawn lovinNeedPawn in PawnsNeedingLovin)
			{
				stringBuilder.AppendLine("   " + lovinNeedPawn.Label);
				if (pawn == null)
				{
					pawn = lovinNeedPawn;
				}
			}

			return "VRE_LovinNeedDesc".Translate(stringBuilder.ToString().TrimEndNewlines(), pawn.Named("PAWN"));
		}
	}
}

