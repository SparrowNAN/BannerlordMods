using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using VillageTaxRate.ViewModel;

namespace VillageTaxRate.Patchs
{
    [HarmonyPatch(typeof(ClanVM), "set_ClanFiefs")]
    // [HarmonyPatch("ClanFiefs", MethodType.Setter)]
    public class VillageRatePatch
    {
        // static void Postfix(ref ClanFiefsVM __result)
        public static void Prefix(ref ClanFiefsVM value)
        {
            FileLog.Log("start patch");
            // if (!(__result is ClanFiefsVM vm))
                // return;
            // Traverse traverse = Traverse.Create((object) clanVm);
            // Action a = () =>
            // {
            //     Patch2.MyRefreshCategoryValues(clanVm);
            //     FileLog.Log("init refresh");
            // };
            FileLog.Log("clan field start");
            Traverse traverse = Traverse.Create((object) value);
            ClanFiefsWithVillageRateVM vm = new ClanFiefsWithVillageRateVM(traverse.Field<Action>("_onRefresh").Value);
            FileLog.Log("clan field end");
            // clanVm.ClanFiefs = clanFiefsVm;
            FileLog.Log("all end");
            // vm.RefreshValues();
            // vm.RefreshFiefsList();
            value = vm;
        }
    }

    // [HarmonyPatch]
    // class Patch2
    // {
    //     [HarmonyReversePatch]
    //     [HarmonyPatch(typeof(ClanVM), "RefreshCategoryValues")]
    //     public static void MyRefreshCategoryValues(object instance)
    //     {
    //         
    //     }
    // }
}