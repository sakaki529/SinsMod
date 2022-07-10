using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class NightmareBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Bar");
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
			item.rare = 11;
			item.value = Item.sellPrice(0, 10, 0, 0);
            item.createTile = mod.TileType("NightmareBar");
            item.placeStyle = 0;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightmareOre", 4);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}