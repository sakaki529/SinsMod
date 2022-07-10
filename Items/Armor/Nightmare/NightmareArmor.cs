using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Nightmare
{
	[AutoloadEquip(EquipType.Body)]
	public class NightmareArmor : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightmare Armor");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 34;
			item.height = 20;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 70;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void UpdateEquip(Player player)
        {

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TrueMidnightArmor");
            recipe.AddIngredient(null, "EssenceOfMadness", 10);
            recipe.AddIngredient(null, "NightmareBar", 15);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}