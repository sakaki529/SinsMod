using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Summon
{
    public class MoonlightMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Moonlight Bit");
            Description.SetDefault("The moonlight bits will attack your enemies");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("MoonlightBit")] > 0)
            {
                modPlayer.MoonlightMinion = true;
            }
            if (!modPlayer.MoonlightMinion)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}