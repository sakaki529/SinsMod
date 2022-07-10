using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class EclipseDrip : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eclipse Drip");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 28;
			item.maxStack = 999;
			item.rare = 7;
			item.value = Item.sellPrice(0, 0, 20, 0);
		}
	}
}