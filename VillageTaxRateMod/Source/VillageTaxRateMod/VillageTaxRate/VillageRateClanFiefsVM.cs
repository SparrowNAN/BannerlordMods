using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using VillageTaxRate.Actions;

namespace VillageTaxRate.ViewModel
{

  public class ClanFiefsWithVillageRateVM : ClanFiefsVM
    {
      
    private readonly Clan _faction;
    
    private readonly Action _onRefresh;
      
    public ClanFiefsWithVillageRateVM(Action onRefresh) : base(onRefresh)
    {
      this._onRefresh = onRefresh;
      this._faction = Hero.MainHero.Clan;
    }
    
     private void ExecuteAssignGovernor()
    {
      if (this.CurrentSelectedFief == null)
        return;
      List<InquiryElement> inquiryElements = new List<InquiryElement>();
      foreach (Hero hero in this._faction.Heroes.Where<Hero>((Func<Hero, bool>) (h => !h.IsDisabled)).Union<Hero>((IEnumerable<Hero>) this._faction.Companions))
      {
        if (hero.IsActive && !hero.IsChild)
        {
          if (hero.PartyBelongedToAsPrisoner != null)
          {
            string hint = new TextObject("{=knwId8DG}You cannot assign a prisoner as a governor of a settlement", (Dictionary<string, TextObject>) null).ToString();
            inquiryElements.Add(new InquiryElement((object) hero, hero.Name.ToString(), new ImageIdentifier(CharacterCode.CreateFrom((BasicCharacterObject) hero.CharacterObject)), false, hint));
          }
          else if (hero == Hero.MainHero)
          {
            string hint = new TextObject("{=uoDuiBZR}You cannot assign yourself as a governor", (Dictionary<string, TextObject>) null).ToString();
            inquiryElements.Add(new InquiryElement((object) hero, hero.Name.ToString(), new ImageIdentifier(CharacterCode.CreateFrom((BasicCharacterObject) hero.CharacterObject)), false, hint));
          }
          else if (hero.PartyBelongedTo != null && hero.PartyBelongedTo.LeaderHero == hero)
          {
            string hint = new TextObject("{=pWObBhj5}You cannot assign a party leader as a new governor of a settlement", (Dictionary<string, TextObject>) null).ToString();
            inquiryElements.Add(new InquiryElement((object) hero, hero.Name.ToString(), new ImageIdentifier(CharacterCode.CreateFrom((BasicCharacterObject) hero.CharacterObject)), false, hint));
          }
          else if (hero.GovernorOf != null)
          {
            TextObject textObject = new TextObject("{=YbGu9rSH}This character is already the governor of {SETTLEMENT_NAME}", (Dictionary<string, TextObject>) null);
            textObject.SetTextVariable("SETTLEMENT_NAME", hero.GovernorOf.Name);
            inquiryElements.Add(new InquiryElement((object) hero, hero.Name.ToString(), new ImageIdentifier(CharacterCode.CreateFrom((BasicCharacterObject) hero.CharacterObject)), false, textObject.ToString()));
          }
          else
            inquiryElements.Add(new InquiryElement((object) hero, hero.Name.ToString(), new ImageIdentifier(CharacterCode.CreateFrom((BasicCharacterObject) hero.CharacterObject))));
        }
      }
      if (inquiryElements.Count > 0)
      {
        string title = new TextObject("{=koX9okuG}None", (Dictionary<string, TextObject>) null).ToString();
        inquiryElements.Add(new InquiryElement((object) null, title, new ImageIdentifier(ImageIdentifierType.Null)));
        InformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(new TextObject("{=PAUsUq4Z}Select the Leader of the Settlement", (Dictionary<string, TextObject>) null).ToString(), string.Empty, inquiryElements, true, true, GameTexts.FindText("str_done", (string) null).ToString(), "", new Action<List<InquiryElement>>(this.OnGovernorSelectionOver), new Action<List<InquiryElement>>(this.OnGovernorSelectionOver), ""), false);
      }
      else
        InformationManager.AddQuickInformation(new TextObject("{=JzrodcIR}There is no one available in your clan who can govern this settlement right now.", (Dictionary<string, TextObject>) null), 0, (BasicCharacterObject) null, "");
    }
     
     private void OnGovernorSelectionOver(List<InquiryElement> element)
     {
       if (element.Count <= 0)
         return;
       ChangeGovernorAction.Apply(this.CurrentSelectedFief.Settlement.Town, (Hero) element[0].Identifier);
       this.RefreshFiefsList();
       Action onRefresh = this._onRefresh;
       if (onRefresh == null)
         return;
       onRefresh();
     }
    
    private void ExecuteChangeVillageRateGovernor()
    {
      if (this.CurrentSelectedFief == null)
        return;
      List<InquiryElement> inquiryElements = new List<InquiryElement>();
      inquiryElements.Add(new InquiryElement(10, "减免10%", new ImageIdentifier()));
      inquiryElements.Add(new InquiryElement(20, "减免20%", new ImageIdentifier()));
      inquiryElements.Add(new InquiryElement(30, "减免30%", new ImageIdentifier()));
      inquiryElements.Add(new InquiryElement(40, "减免40%", new ImageIdentifier()));
      inquiryElements.Add(new InquiryElement(50, "减免50%", new ImageIdentifier()));
      inquiryElements.Add(new InquiryElement(60, "减免60%", new ImageIdentifier()));
      inquiryElements.Add(new InquiryElement(70, "减免70%", new ImageIdentifier()));
      inquiryElements.Add(new InquiryElement(80, "减免80%", new ImageIdentifier()));
      inquiryElements.Add(new InquiryElement(90, "减免90%", new ImageIdentifier()));
      inquiryElements.Add(new InquiryElement(100, "减免100%", new ImageIdentifier()));
      if (inquiryElements.Count > 0)
      {
        string title = new TextObject("减什么减!我要钱!!!", (Dictionary<string, TextObject>) null).ToString();
        inquiryElements.Add(new InquiryElement((object) 0, title, new ImageIdentifier(ImageIdentifierType.Null)));
        InformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(new TextObject("请调节税率", (Dictionary<string, TextObject>) null).ToString(), string.Empty, inquiryElements, true, true, GameTexts.FindText("str_done", (string) null).ToString(), "", new Action<List<InquiryElement>>(this.OnChangeVillageRate), new Action<List<InquiryElement>>(this.OnChangeVillageRate), ""), false);
      }
      else
        InformationManager.AddQuickInformation(new TextObject("你只能要钱 -_- 手动滑稽", (Dictionary<string, TextObject>) null), 0, (BasicCharacterObject) null, "");
    }

    private void OnChangeVillageRate(List<InquiryElement> element)
    {
      if (element.Count <= 0)
        return;
      ChangeVillageTaxRateAction.Apply(this.CurrentSelectedFief.Settlement.Village, (int) element[0].Identifier);
      RefreshFiefsList();
    }

    private bool _canChangeVillageRateOfCurrentFief;
    
    [DataSourceProperty]
    public bool CanChangeVillageRateOfCurrentFief
    {
      get
      {
        bool? isVillage = this.CurrentSelectedFief?.Settlement.IsVillage;
        return isVillage != null && (bool) isVillage;
      }
      set
      {
        if (value == this._canChangeVillageRateOfCurrentFief)
          return;
        this._canChangeVillageRateOfCurrentFief = value;
        this.OnPropertyChanged(nameof (CanChangeVillageRateOfCurrentFief));
      }
    }
    }
}