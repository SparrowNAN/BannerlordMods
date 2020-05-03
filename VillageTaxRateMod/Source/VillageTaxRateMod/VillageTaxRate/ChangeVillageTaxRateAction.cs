using TaleWorlds.CampaignSystem;
using VillageTaxRate.calculate;

namespace VillageTaxRate.Actions
{
    public class ChangeVillageTaxRateAction
    {
        public static void Apply(Village village, int reduceRate)
        {
            VillageTaxRateMemory.AddVillageRate(village, reduceRate);
        }
    }
}