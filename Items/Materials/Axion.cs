using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class Axion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Axion");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 11;
            item.createTile = mod.TileType("AxionTile");
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
	}
}