using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.PolarNight
{
	[AutoloadEquip(EquipType.Legs)]
	public class PolarNightLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Polar Night Leggings");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 30;
			item.height = 18;
			item.rare = 4;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 14;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightEnergizedBar", 15);
            recipe.AddIngredient(ItemID.SoulofNight, 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}