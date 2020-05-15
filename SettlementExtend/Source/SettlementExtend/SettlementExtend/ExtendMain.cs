
using TaleWorlds.MountAndBlade;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.TwoDimension;
using TaleWorlds.Library;

namespace SettlementExtend
{
    public class ExtendMain : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            this.enableFont();
            // Font font = UIResourceManager.FontFactory.GetFont("simkai");
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
            SpriteData data = new SpriteData("ccc");
            ResourceDepot depot =
                new ResourceDepot("C:/Program Files (x86)/Steam/steamapps/common/Mount & Blade II Bannerlord/Modules/SettlementExtend/GUI/");
            depot.AddLocation("GauntletUI/");
            depot.CollectResources();
            data.Load(depot);
            UIResourceManager.FontFactory.AddFontDefinition("C:/Program Files (x86)/Steam/steamapps/common/Mount & Blade II Bannerlord/Modules/SettlementExtend/GUI/GauntletUI/Fonts/simkai/", "simkai2", data);
            Font font = UIResourceManager.FontFactory.GetFont("simkai2");
            
            // Font font = UIResourceManager.FontFactory.GetFont("simkai");
            // // UIResourceManager.FontFactory.add
            font.GetType().GetProperty("FontSprite").SetValue((object) font, (object) spritePart);
            TaleWorlds.Engine.Texture engineTexture = TaleWorlds.Engine.Texture.LoadTextureFromPath("Galahad.png", "C:/Program Files (x86)/Steam/steamapps/common/Mount & Blade II Bannerlord/Modules/SettlementExtend/GUI/GauntletUI/Fonts");
            font.FontSprite.Category.SpriteSheets[font.FontSprite.SheetID - 1] = new Texture(new EngineTexture(engineTexture));
            UIResourceManager.FontFactory.DefaultFont = font;
        }
        
    }
}