using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class LaserAntenna : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser Antenna");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 26;
			item.maxStack = 99;
			item.rare = 6;
			item.value = Item.sellPrice(0, 1, 30, 0);
		}
	}
}
