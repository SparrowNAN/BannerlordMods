
using TaleWorlds.MountAndBlade;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.TwoDimension;
using TaleWorlds.Library;
using HarmonyLib;
using TaleWorlds.Core;

namespace CustomChineseFont
{
    public class CustomChinese : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            Harmony harmony = new Harmony("fun.wangyanan.patch.custom.chinese.fonts");
            if (harmony.GetPatchedMethods().IsEmpty())
            {
                new Harmony("fun.wangyanan.patch.custom.chinese.fonts").PatchAll();
                enableFont();
            }
        }
        private void enableFont()
        {
            SpriteCategory category = new SpriteCategory("customfonts", UIResourceManager.SpriteData, 1);
            category.SheetSizes = new Vec2i[1]
            {
                new Vec2i(8192, 8192)
            };
            category.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
            SpritePart spritePart = new SpritePart("sim1", category, 8192, 8192)
            {
                SheetID = 1
            };
            string currentModulePath = ModuleInfo.GetPath("CustomChineseFont");
            string currentModuleDirPath = currentModulePath.Substring(0, currentModulePath.LastIndexOf("/"));
            string fontPath = currentModuleDirPath + "/GUI/GauntletUI/Fonts";
            string fontFilePath = fontPath + "/simkai/";
            UIResourceManager.FontFactory.AddFontDefinition(fontFilePath, "simkai2", UIResourceManager.SpriteData);
            Font font = UIResourceManager.FontFactory.GetFont("simkai2");
            font.GetType().GetProperty("FontSprite").SetValue((object) font, (object) spritePart);
            TaleWorlds.Engine.Texture engineTexture = TaleWorlds.Engine.Texture.LoadTextureFromPath("simkai2.png", fontPath);
            font.FontSprite.Category.SpriteSheets[font.FontSprite.SheetID - 1] = new Texture(new EngineTexture(engineTexture));
            UIResourceManager.FontFactory.DefaultFont = font;
        }
    }
}