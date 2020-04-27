using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace VillageTaxRate
{
    public class TaxLoader : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if(game.GameType is Campaign) {
                //The current game is a campaign
                CampaignGameStarter campaignStarter = (CampaignGameStarter) gameStarterObject;
                campaignStarter.AddModel(new TaxModel());
                campaignStarter.AddModel(new TaxHealthModel());
                
                //ExampleBehavoir is our custom class which extends CampaignBehaviorBase
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
                exemptRate += CalculateVillageExemptFromTax(clan, village);
            }
            TextObject description = new TextObject("赋税减免", (Dictionary<string, TextObject>) null);
            goldChange.Add((float) -exemptRate, description);
        }

        private float CalculateVillageExemptFromTax(Clan clan, Village village)
        {
            int baseTax = village.VillageState == Village.VillageStates.Looted || village.VillageState == Village.VillageStates.BeingRaided ? 0 : (int) ((double) village.TradeTaxAccumulated / (double) this.RevenueSmoothenFraction());
            float tax = baseTax;
            if (clan.Kingdom != null && clan.Kingdom.ActivePolicies.Contains(DefaultPolicies.LandTax))
                tax = tax * 0.95f;
            float exemptRate = ExemptLevelRate(village.Hearth);
            return tax * exemptRate;
        }

        private float ExemptLevelRate(float health)
        {
            if (health <= 100)
            {
                return 0.9f;
            } else if (health <= 200)
            {
                return 0.7f;
            }
            else if (health <= 500)
            {
                return 0.5f;
            } else if (health <= 1000)
            {
                return 0.3f;
            }
            else
            {
                return 0.0f;
            }
        }
    }

    public class TaxHealthModel : DefaultSettlementProsperityModel
    {
        public override float CalculateHearthChange(Village village, StatExplainer explanation = null)
        {
            float change = base.CalculateHearthChange(village, explanation);
            change = change * HealthIncrementRate(village.Hearth);
            return change;
        }
        
        private float HealthIncrementRate(float health)
        {
            if (health <= 100)
            {
                return 5f;
            } else if (health <= 200)
            {
                return 3f;
            }
            else if (health <= 500)
            {
                return 2f;
            } else if (health <= 1000)
            {
                return 1.5f;
            }
            else
            {
                return 1f;
            }
        }
    }
}