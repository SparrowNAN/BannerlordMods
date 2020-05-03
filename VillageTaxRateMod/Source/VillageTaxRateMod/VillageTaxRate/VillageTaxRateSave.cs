using TaleWorlds.SaveSystem;
using VillageTaxRate.Models;

namespace VillageTaxRate.save
{
    public class VillageTaxRateSave:SaveableTypeDefiner
    {
        public VillageTaxRateSave() : base(20_05_03_1254)
        {
        }
        
        protected override void DefineClassTypes()
        {
            // The Id's here are local and will be related to the Id passed to the constructor
            this.AddClassDefinition(typeof(VillageModel), 1);
        }
    }
}