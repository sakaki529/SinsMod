using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Platforms
{
    public class MysticWoodPlatform : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Wood Platform");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 14;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 2;
            item.useAnimation = 15;
            item.value = 0;
            item.rare = 0;
            item.createTile = mod.TileType("MysticWoodPlatform");
            item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MysticWood");
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }
}