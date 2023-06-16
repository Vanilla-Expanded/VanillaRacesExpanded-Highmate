
using System.Collections.Generic;
using System.Xml;
using Verse;
namespace VanillaRacesExpandedHighmate
{
	public class PatchOperationModOption : PatchOperation
	{
		
		private PatchOperation match;

		private PatchOperation nomatch;

        public override bool ApplyWorker(XmlDocument xml)
		{
			
			if (VanillaRacesExpandedHighmate_Settings.flagCatHighmates)
			{
				if (match != null)
				{
					return match.Apply(xml);
				}
			}
			else if (nomatch != null)
			{
				return nomatch.Apply(xml);
			}
			return true;
		}

		
	}
}