using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Cooldowns
{
    public class NullificationPotionSickness : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Nullification Potion Sickness");
            Description.SetDefault("Cannot consume nullification potion");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            longerExpertDebuff = false;
        }
    }
}