using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace UseSourceHanSerifSCFont
{
    public class UseSCFont : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            Harmony harmony = new Harmony("fun.wangyanan.patch.use.sourcehanserifc.fonts");
            if (harmony.GetPatchedMethods().IsEmpty())
            {
                new Harmony("fun.wangyanan.patch.use.sourcehanserifc.fonts").PatchAll();
            }
        }
    }
}