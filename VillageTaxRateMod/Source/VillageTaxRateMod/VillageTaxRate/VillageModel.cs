using TaleWorlds.SaveSystem;
using System;
using System.Collections.Generic;

namespace VillageTaxRate.Models
{
    [SaveableClass(190000)]
    [Serializable]
    public class VillageModel
    {
        [SaveableProperty(1)]
        public Dictionary<string, int> Attributes { get; set; }
    }
}