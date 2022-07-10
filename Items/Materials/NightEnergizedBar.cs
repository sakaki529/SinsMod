using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class NightEnergizedBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightenergized Bar");
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
			item.rare = 4;
			item.value = Item.sellPrice(0, 2, 0, 0);
            item.createTile = mod.TileType("NightEnergizedBar");
            item.placeStyle = 0;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightEnergizedOre", 4);
            recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}