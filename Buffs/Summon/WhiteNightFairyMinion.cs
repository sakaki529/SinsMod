using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Summon
{
    public class WhiteNightFairyMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("White Night Fairy");
            Description.SetDefault("The white night fairy will attack your enemies");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("WhiteNightFairy")] > 0)
            {
                modPlayer.WhiteNightFairyMinion = true;
            }
            if (!modPlayer.WhiteNightFairyMinion)
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