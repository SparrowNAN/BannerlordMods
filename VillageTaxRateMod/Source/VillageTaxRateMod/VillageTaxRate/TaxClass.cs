using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using VillageTaxRate.calculate;
using VillageTaxRate.Models;

namespace VillageTaxRate
{
    
    public class TaxLoader : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if(game.GameType is Campaign) {
                //The current game is a campaign
                CampaignGameStarter campaignStarter = (CampaignGameStarter) gameStarterObject;
                campaignStarter.AddBehavior(new CustomSaveBehavior());
                Harmony harmony = new Harmony("fun.wangyanan.patch.villagerate");
                if (harmony.GetPatchedMethods().IsEmpty())
                {
                    new Harmony("fun.wangyanan.patch.villagerate").PatchAll();
                }
            }
        }

        public override void OnGameEnd(Game game)
        {
            base.OnGameEnd(game);
            // 清楚缓存的数据
            if (VillageTaxRateMemory._villageRateDictionary != null)
            {
                VillageTaxRateMemory._villageRateDictionary.Clear();
            }
        }
    }

    public class CustomSaveBehavior : CampaignBehaviorBase
    {
        private static VillageModel _customDataMap = new VillageModel();

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("customDataMap", ref _customDataMap);
        }

        public override void RegisterEvents()
        {
            CampaignEvents.OnBeforeSaveEvent.AddNonSerializedListener(this, () =>
            {
                VillageModel model = new VillageModel();
                model.Attributes = VillageTaxRateMemory._villageRateDictionary;
                _customDataMap = model;
            });
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, starter =>
            {
                if (_customDataMap.Attributes != null)
                {
                    if (!_customDataMap.Attributes.IsEmpty())
                    {
                        VillageTaxRateMemory._villageRateDictionary = _customDataMap.Attributes;
                    }   
                }
            });
        }
    }
    
    
}