using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Walls
{
    public class DistortionWoodFence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Distortion Wood Fence");
        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;
            item.useTime = 7;
            item.useAnimation = 15;
            item.value = 0;
            item.rare = 0;
            item.createWall = mod.WallType("DistortionWoodFence");
            item.consumable = true;
            item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DistortionWood");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}