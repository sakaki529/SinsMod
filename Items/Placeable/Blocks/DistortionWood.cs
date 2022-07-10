using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Blocks
{
    public class DistortionWood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Distortion Wood");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = 0;
            item.rare = 0;
            item.createTile = mod.TileType("DistortionWood");
            item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DistortionWoodWall", 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DistortionWoodPlatform", 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}