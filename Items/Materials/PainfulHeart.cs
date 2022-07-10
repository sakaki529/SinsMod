using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
	public class PainfulHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Painful Heart");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 22;
			item.maxStack = 99;
			item.rare = 11;
			item.value = Item.sellPrice(0, 15, 0, 0);
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
	}
}