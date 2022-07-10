using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Summon
{
    public class NightfallMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Nightfall Sphere");
            Description.SetDefault("The nightfall sphere will protect you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("NightfallSphere")] > 0)
            {
                modPlayer.NightfallMinion = true;
            }
            if (!modPlayer.NightfallMinion)
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