using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Mounts
{
    public class Cleyera : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Cleyera");
            Tooltip.SetDefault("Summons a Cleyera mount" +
                "\nPress jump key to rise high speed");
        }
		public override void SetDefaults()
		{
            item.width = 36;
            item.height = 34;
            item.noMelee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 4;
            item.rare = 3;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.UseSound = SoundID.Item25;
            item.mountType = mod.MountType("CleyeraMount");
        }
	}
}