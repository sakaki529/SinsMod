using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Summon
{
    public class SmallBlackCoreMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Small Black Core");
            Description.SetDefault("The small black core will protect you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("SmallBlackCore")] > 0)
            {
                modPlayer.SmallBlackCoreMinion = true;
            }
            if (!modPlayer.SmallBlackCoreMinion)
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