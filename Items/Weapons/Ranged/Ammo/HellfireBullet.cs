using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged.Ammo
{
	public class HellfireBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.damage = 13;
			item.knockBack = 8f;
			item.consumable = true;
			item.ammo = AmmoID.Bullet;
            item.rare = 2;
			item.ranged = true;
			item.value = Item.sellPrice(0, 0, 0, 20);
			item.shoot = mod.ProjectileType("HellfireBullet");
			item.shootSpeed = 4f;
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