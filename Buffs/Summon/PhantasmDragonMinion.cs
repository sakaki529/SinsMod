using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Summon
{
    public class PhantasmDragonMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Phantasm Dragon");
            Description.SetDefault("The phantasmal dragon will protect you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("PhantasmDragonHead")] > 0)
            {
                modPlayer.PhantasmDragonMinion = true;
            }
            if (!modPlayer.PhantasmDragonMinion)
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