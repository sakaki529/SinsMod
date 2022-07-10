using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.StatBuffs
{
    public class AndromedaBlaze3 : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Andromeda Blaze");
            Description.SetDefault("Damage taken reduced by 30%, repel enemies when taking damage");
            Main.buffNoTimeDisplay[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            player.endurance += 0.3f;
        }
    }
}