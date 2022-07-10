using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Nightfall
{
	[AutoloadEquip(EquipType.Legs)]
	public class NightfallLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightfall Leggings");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 30;
			item.height = 18;
			item.rare = 10;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 22;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WhiteNightLeggings");
            recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
            recipe.AddIngredient(ItemID.FragmentSolar, 16);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}