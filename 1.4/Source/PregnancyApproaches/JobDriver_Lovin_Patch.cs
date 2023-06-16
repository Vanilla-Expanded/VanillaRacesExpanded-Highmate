using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch]
    public static class JobDriver_Lovin_Patch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(JobDriver_Lovin).GetMethods(AccessTools.all).FirstOrFallback(x => x.Name.Contains("<MakeNewToils>b__12_1"));
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var setTicks = AccessTools.Field(typeof(JobDriver_Lovin), nameof(JobDriver_Lovin.ticksLeft));
            foreach (var code in instructions)
            {
                yield return code;
                if (code.StoresField(setTicks))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(JobDriver_Lovin_Patch), "SetLovinDuration"));
                }
            }
        }

        public static void SetLovinDuration(JobDriver_Lovin jobDriver_Lovin)
        {
            if (jobDriver_Lovin.pawn.relations.GetAdditionalPregnancyApproachData().pawnsWithLovinForPleasure.Contains(jobDriver_Lovin.Partner))
            {
                jobDriver_Lovin.ticksLeft = (int)(jobDriver_Lovin.ticksLeft * 1.25f);
            }
        }
    }
}
