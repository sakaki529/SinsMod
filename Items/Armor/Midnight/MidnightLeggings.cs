using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Midnight
{
	[AutoloadEquip(EquipType.Legs)]
	public class MidnightLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Leggings");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 30;
			item.height = 18;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 46;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override bool DrawLegs()
        {
            return false;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallLeggings");
            recipe.AddIngredient(null, "EssenceOfEnvy", 8);
            recipe.AddIngredient(null, "EssenceOfGluttony", 8);
            recipe.AddIngredient(null, "EssenceOfGreed", 8);
            recipe.AddIngredient(null, "EssenceOfLust", 8);
            recipe.AddIngredient(null, "EssenceOfPride", 8);
            recipe.AddIngredient(null, "EssenceOfSloth", 8);
            recipe.AddIngredient(null, "EssenceOfWrath", 8);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}