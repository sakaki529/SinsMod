using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.PolarNight
{
	[AutoloadEquip(EquipType.Body)]
	public class PolarNightArmor : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Polar Night Armor");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 44;
			item.rare = 4;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 18;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightEnergizedBar", 20);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}