using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Summon
{
    public class TartarusMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Abyssal Guardian");
            Description.SetDefault("The abyssal guardian will protect you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("TartarusMinionHead")] > 0)
            {
                modPlayer.TartarusMinion = true;
            }
            if (!modPlayer.TartarusMinion)
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