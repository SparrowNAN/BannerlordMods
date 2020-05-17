﻿﻿using HarmonyLib;
using TaleWorlds.GauntletUI;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.TwoDimension;

namespace CustomChineseFont
{
    [HarmonyPatch(typeof(FontFactory), "OnLanguageChange")]
    public class DefaultFontPatch
    {
        public static void Postfix(ref string newLanguageCode)
        {
            if (newLanguageCode.Equals("简体中文"))
            {
                UIResourceManager.FontFactory.DefaultFont = UIResourceManager.FontFactory.GetFont("SourceHanSerifSC-Regular");
            }
        }
    }
    
    [HarmonyPatch(typeof(FontFactory), "GetFont")]
    public class GetFontPatch
    {
        public static void Prefix(ref string fontName)
        {
            if (fontName.Equals("simkai") && (UIResourceManager.FontFactory.CurrentLangageID.Equals("简体中文")))
            {
                fontName = "SourceHanSerifSC-Regular";
            }
        }
    }
    
    [HarmonyPatch(typeof(FontFactory), "GetMappedFontForLocalization")]
    public class GetMappedFontPatch
    {
        public static void Postfix(ref string englishFontName, ref Font __result)
        {
            if (__result.Name.Equals("simkai") && (UIResourceManager.FontFactory.CurrentLangageID.Equals("简体中文")))
            {
                __result = UIResourceManager.FontFactory.GetFont("SourceHanSerifSC-Regular");
            }
        }
    }
    
}