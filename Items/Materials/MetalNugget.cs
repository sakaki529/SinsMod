using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class MetalNugget : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Metal Nugget");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 26;
			item.maxStack = 999;
			item.rare = 11;
			item.value = Item.sellPrice(0, 0, 1, 0);
		}
	}
}
