using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.TrueMidnight
{
	[AutoloadEquip(EquipType.Body)]
	public class TrueMidnightArmor : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Midnight Armor");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 44;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 58;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MidnightArmor");
            recipe.AddIngredient(null, "Axion", 15);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}