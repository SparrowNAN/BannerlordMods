using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using VillageTaxRate.Actions;

namespace VillageTaxRate.ViewModel
{
    public class ClanFiefsWithVillageRateVM : ClanFiefsVM
    {

    public ClanFiefsWithVillageRateVM(Action onRefresh) : base(onRefresh)
    {

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