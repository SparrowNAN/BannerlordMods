using HarmonyLib;
using TaleWorlds.GauntletUI;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.TwoDimension;

namespace SettlementExtend
{
    [HarmonyPatch(typeof(FontFactory), "OnLanguageChange")]
    public class DefaultFontPatch
    {
        public static void Postfix(ref string newLanguageCode)
        {
            if (newLanguageCode.Equals("简体中文") || newLanguageCode.Equals("繁體中文"))
            {
                UIResourceManager.FontFactory.DefaultFont = UIResourceManager.FontFactory.GetFont("simkai2");
            }
        }
    }
    
    [HarmonyPatch(typeof(FontFactory), "GetFont")]
    public class GetFontPatch
    {
        public static void Prefix(ref string fontName)
        {
            fontName = "simkai2";
        }
    }
    
    [HarmonyPatch(typeof(FontFactory), "GetMappedFontForLocalization")]
    public class GetMappedFontPatch
    {
        public static void Postfix(ref string englishFontName, ref Font __result)
        {
            if (__result.Name.Equals("simkai") && (UIResourceManager.FontFactory.CurrentLangageID.Equals("简体中文") || UIResourceManager.FontFactory.CurrentLangageID.Equals("繁體中文")))
            {
                __result = UIResourceManager.FontFactory.GetFont("simkai2");
            }
        }
    }
    
}