using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Summon
{
    public class LunaticMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lunatic");
            Description.SetDefault("'Do you feel it, the moon's power?'");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("Lunatic")] > 0)
            {
                modPlayer.LunaticMinion = true;
            }
            if (!modPlayer.LunaticMinion)
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