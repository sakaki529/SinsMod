using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class MysticBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mystic Bar");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
            item.useTurn = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.consumable = true;
            item.maxStack = 99;
			item.rare = 9;
			item.value = Item.sellPrice(0, 2, 0, 0);
            item.createTile = mod.TileType("MysticBar");
            item.placeStyle = 0;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MysticOre", 6);
            recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}