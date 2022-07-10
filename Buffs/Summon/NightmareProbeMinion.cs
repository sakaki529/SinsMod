using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Summon
{
    public class NightmareProbeMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Nightmare Probe");
            Description.SetDefault("The nightmare probe will protect you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("NightmareProbe")] > 0)
            {
                modPlayer.NightmareProbeMinion = true;
            }
            if (!modPlayer.NightmareProbeMinion)
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