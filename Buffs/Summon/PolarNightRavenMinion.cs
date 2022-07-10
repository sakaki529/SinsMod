using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Summon
{
    public class PolarNightRavenMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Polar Night Raven");
            Description.SetDefault("The polar night ravens will attack your enemies");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("PolarNightRaven")] > 0)
            {
                modPlayer.PolarNightRavenMinion = true;
            }
            if (!modPlayer.PolarNightRavenMinion)
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