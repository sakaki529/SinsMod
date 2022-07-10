using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Nightfall
{
	[AutoloadEquip(EquipType.Body)]
	public class NightfallArmor : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightfall Armor");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 44;
			item.rare = 10;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 28;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WhiteNightArmor");
            recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
            recipe.AddIngredient(ItemID.FragmentSolar, 16);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}