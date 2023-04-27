using Aki.Reflection.Patching;
using EFT;
using HarmonyLib;
using static NoGrenadeYell.Config.GrenadeYell;
using System.Reflection;

namespace NoGrenadeYell.Patches
{
    public class PlayerSayPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player), "Say");
        }

        [PatchPrefix]
        public static void PatchPrefix(ref EPhraseTrigger @event)
        {
            bool allowVoiceChance = UnityEngine.Random.value < (GrenadeYellChance.Value / 100f);

            bool badVoice =
                @event == EPhraseTrigger.OnEnemyGrenade ||
                @event == EPhraseTrigger.OnGrenade;

            if (badVoice == true && allowVoiceChance == false)
                @event = EPhraseTrigger.PhraseNone;
        }
    }
}