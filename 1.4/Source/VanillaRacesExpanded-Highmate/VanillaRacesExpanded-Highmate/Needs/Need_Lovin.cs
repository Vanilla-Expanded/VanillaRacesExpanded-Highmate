using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaRacesExpandedHighmate
{
	public class Need_Lovin : Need
	{


		private const float MinAgeForNeed = 16f;


		public LovinNeedCategory CurCategory
		{
			get
			{
				if (this.CurLevel < 0.01f)
				{
					return LovinNeedCategory.GivenUp;
				}
				if (this.CurLevel < 0.5f)
				{
					return LovinNeedCategory.Demand;
				}
				if (this.CurLevel < 0.83f)
				{
					return LovinNeedCategory.Need;
				}
				return LovinNeedCategory.Satisfied;
			}
		}

		public override bool ShowOnNeedList
		{
			get
			{
				if ((float)pawn.ageTracker.AgeBiologicalYears < MinAgeForNeed)
				{
					return false;
				}
				if (pawn?.genes?.HasGene(InternalDefOf.VRE_LovinDependency)!=true)
				{
					return false;
				}

				else return true;


			}
		}

		protected override bool IsFrozen
		{
			get
			{
				if ((float)pawn.ageTracker.AgeBiologicalYears < MinAgeForNeed)
				{
					return true;
				}
				return base.IsFrozen;
			}
		}

		public override void NeedInterval()
		{
			if (!this.IsFrozen)
			{

				CurLevel -= 1.38888E-05f;

				if(CurLevel < 0.01f)
                {
					TriggerBreak();
                }
			}
		}

		public override void SetInitialLevel()
		{
			CurLevelPercentage = 1f;
		}


		public Need_Lovin(Pawn pawn) : base(pawn)
		{
			threshPercents = new List<float>
			{
				0.83f,0.5f,0.01f
			};
		}

		public void LovinDone()
		{
			CurLevel = 1f;
		}

		public void TriggerBreak()
        {
			MentalBreakDef mentalBreak = InternalDefOf.GiveUpExit;
			mentalBreak.Worker.TryStart(pawn, "VRE_NotEnoughLovin".Translate(), true);
			CurLevel = 0.5f;

		}






	}
}
