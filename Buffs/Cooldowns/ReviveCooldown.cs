using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Cooldowns
{
    public class ReviveCooldown : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Revive Cooldown");
            Description.SetDefault("You can't revive now");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }
    }
}