using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Misc
{
    public class PlanteraBulbPlaceable : ModItem
    {
        public override string Texture => "SinsMod/Items/Boss/PlanteraBulb";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plantera's Bulb");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = Item.buyPrice(0, 0, 5, 0);
            item.rare = 2;
            item.createTile = TileID.PlanteraBulb;
            item.maxStack = 20;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("PlanteraBulbSummon"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}