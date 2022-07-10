using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.TrueMidnight
{
	[AutoloadEquip(EquipType.Legs)]
	public class TrueMidnightLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Midnight Leggings");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 30;
			item.height = 18;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 52;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
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
            recipe.AddIngredient(null, "MidnightLeggings");
            recipe.AddIngredient(null, "Axion", 12);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}