using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(SkillRecord), "Interval")]
    public static class SkillRecord_Interval_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            foreach (var code in codeInstructions)
            {
                yield return code;
                if (code.opcode == OpCodes.Stloc_0)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(SkillRecord_Interval_Patch), "SetMultiplier"));
                    yield return new CodeInstruction(OpCodes.Stloc_0);
                }
            }
        }

        public static float SetMultiplier(float multiplier, SkillRecord skillRecord) 
        {
            if (skillRecord.Pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VRE_PsychicBondGreatMemory) != null)
            {
                return 0.75f;
            }
            return multiplier;
        }
    }
}
