using System.Collections.Generic;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using VillageTaxRate.calculate;
using VillageTaxRate.Models;

namespace VillageTaxRate
{
    
    public class TaxLoader : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            // base.OnSubModuleLoad();
            // new Harmony("fun.wangyanan.patch.villagerate").PatchAll();
        }
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if(game.GameType is Campaign) {
                //The current game is a campaign
                CampaignGameStarter campaignStarter = (CampaignGameStarter) gameStarterObject;
                campaignStarter.AddModel(new TaxModel());
                campaignStarter.AddModel(new TaxHealthModel());
                campaignStarter.AddBehavior(new CustomSaveBehavior());
                base.OnSubModuleLoad();
                new Harmony("fun.wangyanan.patch.villagerate").PatchAll();
            }
        }

        public override void OnGameEnd(Game game)
        {
            base.OnGameEnd(game);
            // 清楚缓存的数据
            if (VillageTaxRateMemory._villageRateDictionary != null)
            {
                VillageTaxRateMemory._villageRateDictionary.Clear();
                new Harmony("fun.wangyanan.patch.villagerate").UnpatchAll();
            }
        }
    }

    public class TaxModel : DefaultClanFinanceModel
    {
        public override void CalculateClanIncome(Clan clan, ref ExplainedNumber goldChange, bool applyWithdrawals = false)
        {
            base.CalculateClanIncome(clan, ref goldChange, applyWithdrawals);

            float exemptRate = 0.0f;
            // 每个村庄的减免税收计算
            foreach (Village village in clan.Villages)
            {
                var reduce = CalculateVillageExemptFromTax(clan, village);
                TextObject description = new TextObject($"{village.Name.ToString()} - 赋税减免", (Dictionary<string, TextObject>) null);
                goldChange.Add((float) -reduce, description);
            }
        }

        private float CalculateVillageExemptFromTax(Clan clan, Village village)
        {
            int baseTax = village.VillageState == Village.VillageStates.Looted || village.VillageState == Village.VillageStates.BeingRaided ? 0 : (int) ((double) village.TradeTaxAccumulated / (double) this.RevenueSmoothenFraction());
            float tax = baseTax;
            if (clan.Kingdom != null && clan.Kingdom.ActivePolicies.Contains(DefaultPolicies.LandTax))
                tax = tax * 0.95f;
            float reduceRate = VillageTaxRateMemory.GetReduceCoinRate(village);
            return tax * reduceRate;
        }
    }

    public class TaxHealthModel : DefaultSettlementProsperityModel
    {
        public override float CalculateHearthChange(Village village, StatExplainer explanation = null)
        {
            float change = base.CalculateHearthChange(village, explanation);
            // 户数可能减，这里还没有相关的处理逻辑
            if (change > 0)
            {
                float reduceInfluenceRate = VillageTaxRateMemory.CalculateAddHealthRate(village);
                change = change * reduceInfluenceRate;
            }
            return change;
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