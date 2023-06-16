using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using Verse;
using static RimWorld.SocialCardUtility;

namespace VanillaRacesExpandedHighmate
{
    [HarmonyPatch(typeof(SocialCardUtility), "DrawPregnancyApproach")]
    public static class SocialCardUtility_DrawPregnancyApproach_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var get_WindowStackInfo = AccessTools.PropertyGetter(typeof(Find), "WindowStack");
            var drawTextureInfo = AccessTools.Method(typeof(GUI), "DrawTexture", new Type[] { typeof(Rect), typeof(Texture) });
            var tooltipHandlerTipRegionInfo = AccessTools.Method(typeof(TooltipHandler), "TipRegion", new Type[] {typeof(Rect), typeof(TipSignal)});
            var codes = codeInstructions.ToList();
            for (var i =  0; i < codes.Count; i++)
            {
                var code = codes[i];
                if (code.Calls(drawTextureInfo))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(SocialCardUtility_DrawPregnancyApproach_Patch), "InterceptTexture"));
                }
                if (code.Calls(tooltipHandlerTipRegionInfo))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Ldloc_2);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(SocialCardUtility_DrawPregnancyApproach_Patch), "InterceptTooltip"));
                }
                yield return new CodeInstruction(code);
                if (code.Calls(get_WindowStackInfo) && codes[i + 1].opcode == OpCodes.Ldloc_3)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Ldloc_3);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(SocialCardUtility_DrawPregnancyApproach_Patch), "AddPregnancyApproachOption"));
                }
            }
        }
        public static TipSignal InterceptTooltip(TipSignal tipSignal, CachedSocialTabEntry entry, Pawn selPawnForSocialInfo, AcceptanceReport acceptanceReport)
        {
            var data = selPawnForSocialInfo.relations.GetAdditionalPregnancyApproachData();
            if (data.pawnsWithPsychicConception.Contains(entry.otherPawn))
            {
                return acceptanceReport ? ("PregnancyApproach".Translate().Colorize(ColoredText.TipSectionTitleColor) + "\n" +
                    "VRE_PsychicConceptionDesc".Translate() + "\n\n" + "ClickToChangePregnancyApproach".Translate().Colorize
                    (ColoredText.SubtleGrayColor)) : ("PregnancyNotPossible".Translate().Resolve() + ": "
                    + acceptanceReport.Reason.CapitalizeFirst());
            }
            else if (data.pawnsWithLovinForPleasure.Contains(entry.otherPawn))
            {
                return acceptanceReport ? ("PregnancyApproach".Translate().Colorize(ColoredText.TipSectionTitleColor) + "\n" +
                    "VRE_LovinForPleasureDesc".Translate() + "\n\n" + "ClickToChangePregnancyApproach".Translate().Colorize
                    (ColoredText.SubtleGrayColor)) : ("PregnancyNotPossible".Translate().Resolve() + ": "
                    + acceptanceReport.Reason.CapitalizeFirst());
            }
            return tipSignal;
        }
        public static Texture2D InterceptTexture(Texture2D texture, CachedSocialTabEntry entry, Pawn selPawnForSocialInfo)
        {
            var data = selPawnForSocialInfo.relations.GetAdditionalPregnancyApproachData();
            if (data.pawnsWithPsychicConception.Contains(entry.otherPawn))
            {
                return PregnancyApproachUtils.PregnancyApproachIcon_PsychicConception.Texture;
            }
            else if (data.pawnsWithLovinForPleasure.Contains(entry.otherPawn))
            {
                return PregnancyApproachUtils.PregnancyApproachIcon_LovinForPleasure.Texture;
            }
            return texture;
        }

        public static void AddPregnancyApproachOption(CachedSocialTabEntry entry, Pawn selPawnForSocialInfo, List<FloatMenuOption> list)
        {
            if (selPawnForSocialInfo.genes.HasGene(InternalDefOf.PsychicBonding))
            {
                list.Add(new FloatMenuOption("VRE_PsychicConceptionDesc".Translate(), delegate
                {
                    selPawnForSocialInfo.relations.GetAdditionalPregnancyApproachData().pawnsWithPsychicConception.Add(entry.otherPawn);
                    selPawnForSocialInfo.relations.GetAdditionalPregnancyApproachData().pawnsWithLovinForPleasure.Remove(entry.otherPawn);
                    entry.otherPawn.relations.GetAdditionalPregnancyApproachData().pawnsWithPsychicConception.Add(selPawnForSocialInfo);
                    entry.otherPawn.relations.GetAdditionalPregnancyApproachData().pawnsWithLovinForPleasure.Remove(selPawnForSocialInfo);
                }, PregnancyApproachUtils.PregnancyApproachIcon_PsychicConception.Texture, Color.white));
            }
            if (selPawnForSocialInfo.genes.HasGene(InternalDefOf.VRE_Libido_VeryHigh))
            {
                list.Add(new FloatMenuOption("VRE_LovinForPleasureDesc".Translate(), delegate
                {
                    selPawnForSocialInfo.relations.GetAdditionalPregnancyApproachData().pawnsWithLovinForPleasure.Add(entry.otherPawn);
                    selPawnForSocialInfo.relations.GetAdditionalPregnancyApproachData().pawnsWithPsychicConception.Remove(entry.otherPawn);
                    entry.otherPawn.relations.GetAdditionalPregnancyApproachData().pawnsWithLovinForPleasure.Add(selPawnForSocialInfo);
                    entry.otherPawn.relations.GetAdditionalPregnancyApproachData().pawnsWithPsychicConception.Remove(selPawnForSocialInfo);
                }, PregnancyApproachUtils.PregnancyApproachIcon_LovinForPleasure.Texture, Color.white));
            }
        }
    }
}
