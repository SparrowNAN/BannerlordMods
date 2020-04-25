using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using SandBox.GauntletUI;

namespace VillageTaxRate
{
    public class TaxEvent:CampaignBehaviorBase
    {
        public override void RegisterEvents() {   
            CampaignEvents.OnClanDestroyedEvent.AddNonSerializedListener(this, clan => {
                String clanName = clan.Name.ToString();
                InformationManager.DisplayMessage(new InformationMessage("The " + clanName + " was destroyed!"));
            });
        }

        public override void SyncData(IDataStore dataStore)
        {
            
        }
    }

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
            TextObject description = new TextObject("外星人支持", (Dictionary<string, TextObject>) null);
            goldChange.Add((float) 100000, description);
        }
    }

    public class TaxHealthModel : DefaultSettlementProsperityModel
    {
        public override float CalculateHearthChange(Village village, StatExplainer explanation = null)
        {
            float change = base.CalculateHearthChange(village, explanation);
            return RichHearthChange(change);
        }

        private float RichHearthChange(float change)
        {
            return change * 10;
        }
    }

    public class TaxUI : GauntletClanScreen
    {
        pub
    }
    
    public class TaxViewModel : ClanFiefsVM
    {
        public TaxViewModel(Action onRefresh) : base(onRefresh)
        {
       
        }

        [DataSourceProperty]
        public bool CanChangeTaxRateOfCurrentFief
        {
            get
            {
                return !base.CanChangeGovernorOfCurrentFief;
            }
        }
    }

}