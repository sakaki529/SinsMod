using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged.Ammo
{
    public class NightEnergyArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightenergy Arrow");
			Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 10;
            item.height = 28;
            item.damage = 4;
            item.ranged = true;
            item.consumable = true;
            item.maxStack = 999;
            item.knockBack = 3f;
            item.ammo = AmmoID.Arrow;
            item.shoot = mod.ProjectileType("NightEnergyArrow");
            item.shootSpeed = 5.0f;
            item.value = Item.sellPrice(0, 0, 0, 90);
			item.rare = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenArrow, 100);
            recipe.AddIngredient(null, "NightEnergizedBar");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}