using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class OrbStand : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orb Stand");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 16;
			item.maxStack = 99;
			item.rare = 8;
			item.value = Item.sellPrice(0, 0, 50, 0);
		}
	}
}