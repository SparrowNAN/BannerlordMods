using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using VillageTaxRate.ViewModel;
using TaleWorlds.Localization;
using System.Collections.Generic;

using VillageTaxRate.calculate;

namespace VillageTaxRate.Patchs
{
    [HarmonyPatch(typeof(ClanVM), "set_ClanFiefs")]
    public class VillageRatePatch
    {
        public static void Prefix(ref ClanFiefsVM value)
        {
            Traverse traverse = Traverse.Create((object) value);
            ClanFiefsWithVillageRateVM vm = new ClanFiefsWithVillageRateVM(traverse.Field<Action>("_onRefresh").Value);
            value = vm;
        }
    }
    
    [HarmonyPatch(typeof(DefaultClanFinanceModel), "CalculateClanIncome")]
    public class VillageRateCalculatePatch
    {
        public static void Postfix(ref Clan clan, ref ExplainedNumber goldChange)
        {
            float exemptRate = 0.0f;
            // 每个村庄的减免税收计算
            foreach (Village village in clan.Villages)
            {
                var reduce = CalculateVillageExemptFromTax(clan, village);
                TextObject description = new TextObject($"{village.Name.ToString()} - 赋税减免", (Dictionary<string, TextObject>) null);
                goldChange.Add((float) -reduce, description);
            }
        }
        
        private static float CalculateVillageExemptFromTax(Clan clan, Village village)
        {
            int baseTax = village.VillageState == Village.VillageStates.Looted || village.VillageState == Village.VillageStates.BeingRaided ? 0 : (int) ((double) village.TradeTaxAccumulated / (double) 5f);
            float tax = baseTax;
            if (clan.Kingdom != null && clan.Kingdom.ActivePolicies.Contains(DefaultPolicies.LandTax))
                tax = tax * 0.95f;
            float reduceRate = VillageTaxRateMemory.GetReduceCoinRate(village);
            return tax * reduceRate;
        }
    }
    
    [HarmonyPatch(typeof(DefaultSettlementProsperityModel), "CalculateHearthChange")]
    public class VillageRateCalculateHealthPatch
    {
        public static void Postfix(ref Village village,ref float __result)
        {
            if (__result > 0)
            {
                float reduceInfluenceRate = VillageTaxRateMemory.CalculateAddHealthRate(village);
                __result = __result * reduceInfluenceRate;
            }
        }
    }
}