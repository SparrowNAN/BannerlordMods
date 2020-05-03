using System.Collections.Concurrent;
using TaleWorlds.CampaignSystem;
using TaleWorlds.SaveSystem;
using VillageTaxRate.enums;

namespace VillageTaxRate.calculate
{
    // [SaveableClass(10086)]
    public static class VillageTaxRateMemory
    {
        [SaveableField(10086)]
        public static ConcurrentDictionary<string, int> _villageRateDictionary = new ConcurrentDictionary<string, int>();

        public static void AddVillageRate(Village village, int reduceRate)
        {
            _villageRateDictionary[VillageHash(village)] = reduceRate;
        }

        public static float GetReduceCoinRate(Village village)
        {
            return _villageRateDictionary.GetOrAdd(VillageHash(village), 0) / 100f;
        }

        public static float CalculateAddHealthRate(Village village)
        {
            int reduceRate = _villageRateDictionary.GetOrAdd(VillageHash(village), 0);
            float villageRateInfluence = CalculateRateInfluenceCoefficient(reduceRate);
            float healthInfluence = CalculateHealthInfluenceCoefficient(reduceRate, CalculateHealthLevel(village));
            return 1 + villageRateInfluence * healthInfluence;
        }

        private static string VillageHash(Village village)
        {
            return village.Name.ToString();
        }

        private static float CalculateHealthInfluenceCoefficient(int reduceRate, VillageHealthLevel level)
        {
            if (reduceRate == 0)
                return 1f;
            float influence = 1f;
            switch (level)
            {
                case VillageHealthLevel.VERRY_LITTLE: influence = 2.0f; break;
                case VillageHealthLevel.LITTELE: influence = 1.5f; break;
                case VillageHealthLevel.MIDDLE: influence = 0.9f; break;
                case VillageHealthLevel.HIGH: influence = 0.8f; break;
                case VillageHealthLevel.VERY_HIGN: influence = 0.6f; break;
                case VillageHealthLevel.TOP: influence = 0f; break;
            }
            return influence;
        } 

        private static VillageHealthLevel CalculateHealthLevel(Village village)
        {
            float health = village.Hearth;
            if (health < 200f)
            {
                return VillageHealthLevel.VERRY_LITTLE;
            }
            else if (health < 400f)
            {
                return VillageHealthLevel.LITTELE;
            }
            else if (health < 600f)
            {
                return VillageHealthLevel.MIDDLE;
            }
            else if (health < 800f)
            {
                return VillageHealthLevel.HIGH;
            }
            else if (health < 1000f)
            {
                return VillageHealthLevel.VERY_HIGN;
            }
            else
            {
                return VillageHealthLevel.TOP;
            }
        }

        private static float CalculateRateInfluenceCoefficient(int reduceRate)
        {
            switch (reduceRate)
            {
                case 100:
                {
                    return 2.0f;
                }
                case 90:
                {
                    return 1.9f;
                }
                case 80:
                {
                    return 1.8f;
                }
                case 70:
                {
                    return 1.7f;
                }
                case 60:
                {
                    return 1.6f;
                }
                case 50:
                {
                    return 1.5f;
                }
                case 40:
                {
                    return 1.2f;
                }
                case 30:
                {
                    return 0.9f;
                }
                case 20:
                {
                    return 0.6f;
                }
                case 10:
                {
                    return 0.3f;
                }
                default:
                {
                    return 0f;
                }
            }
        } 
    }
}