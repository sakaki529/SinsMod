using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Spiral
{
    [AutoloadEquip(EquipType.Legs)]
	public class SpiralLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral Leggings");
            Tooltip.SetDefault("11% increased ranged damage and critical strike chance" +
                "\n14% increased movement speed");
        }
        public override void SetDefaults()
		{
            item.width = 18;
			item.height = 18;
			item.rare = 10;
            item.defense = 26;
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 11;
            player.rangedDamage += 0.11f;
            player.moveSpeed += 0.14f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexLeggings);
            recipe.AddIngredient(ItemID.FragmentVortex, 15);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}