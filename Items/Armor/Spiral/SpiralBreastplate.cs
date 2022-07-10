using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Spiral
{
    [AutoloadEquip(EquipType.Body)]
	public class SpiralBreastplate : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral Breastplate");
            Tooltip.SetDefault("16% increased ranged damage and critical strike chance" +
                "\n25% chance not to consume ammo");
        }
        public override void SetDefaults()
		{
            item.width = 18;
			item.height = 18;
			item.rare = 10;
            item.defense = 36;
        }
        public override void UpdateEquip(Player player)
        {
            player.ammoCost75 = true;
            player.rangedDamage += 0.16f;
            player.rangedCrit += 16;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexBreastplate);
            recipe.AddIngredient(ItemID.FragmentVortex, 20);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}