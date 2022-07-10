using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class MoonDrip : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Drip");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 28;
			item.maxStack = 999;
			item.rare = 9;
			item.value = Item.sellPrice(0, 1, 0, 0);
		}
	}
}