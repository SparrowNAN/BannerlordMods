using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using TaleWorlds.Library;

namespace VillageTaxRate.ViewModel
{
    public class ClanFiefsWithVillageRateVM : ClanFiefsVM
    {

    public ClanFiefsWithVillageRateVM(Action onRefresh) : base(onRefresh)
    {

    }

    private bool _canChangeVillageRateOfCurrentFief;
    
    [DataSourceProperty]
    public bool CanChangeVillageRateOfCurrentFief
    {
      get => true;
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