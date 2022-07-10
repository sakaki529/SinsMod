using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Midnight
{
	[AutoloadEquip(EquipType.Body)]
	public class MidnightArmor : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Armor");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 44;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 52;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallArmor");
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