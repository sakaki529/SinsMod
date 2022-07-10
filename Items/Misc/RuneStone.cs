using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
	public class RuneStone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rune Stone");
			Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 30;
			item.maxStack = 999;
			item.rare = -11;
			item.value = Item.sellPrice(0, 0, 0, 0);
		}
	}
}