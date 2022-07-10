using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Mounts
{
    public class CleyeraMount : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Cleyyyyyyyy");
            Description.SetDefault("You don't need to mind.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }
		public override void Update(Player player, ref int buffIndex)
		{
            player.mount.SetMount(mod.MountType("CleyeraMount"), player);
            player.buffTime[buffIndex] = 10;
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            //modPlayer.CleyeraMount = true;
        }
	}
}