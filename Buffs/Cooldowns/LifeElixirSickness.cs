using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Cooldowns
{
    public class LifeElixirSickness : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Life Elixir Sickness");
            Description.SetDefault("Cannot consume life elixir");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.potionDelayTime = player.GetModPlayer<SinsPlayer>().lifeElixirCooldown;
        }
    }
}