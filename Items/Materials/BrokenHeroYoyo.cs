using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class BrokenHeroYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Hero Yoyo");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.maxStack = 99;
			item.rare = 6;
			item.value = Item.sellPrice(0, 2, 0, 0);
		}
	}
}