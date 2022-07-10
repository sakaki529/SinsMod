using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Nightmare
{
	[AutoloadEquip(EquipType.Legs)]
	public class NightmareLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightmare Leggings");
            Tooltip.SetDefault("100% increased movement speed" +
                "\nAllows fast running ");
        }
        public override void SetDefaults()
		{
            item.width = 22;
			item.height = 18;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 66;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 1.0f;
            player.runAcceleration *= 1.5f;
            player.maxRunSpeed *= 1.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TrueMidnightLeggings");
            recipe.AddIngredient(null, "EssenceOfMadness", 8);
            recipe.AddIngredient(null, "NightmareBar", 12);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}