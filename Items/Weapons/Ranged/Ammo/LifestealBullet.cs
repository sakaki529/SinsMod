using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged.Ammo
{
	public class LifestealBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Lifesteal Bullet");
            Tooltip.SetDefault("There is an 20% chance to steal enemy life");
		}
		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 8;
            item.ranged = true;
			item.damage = 10;
			item.knockBack = 4f;
            item.maxStack = 999;
			item.rare = 11;
			item.value = Item.sellPrice(0, 0, 0, 50);
			item.consumable = true;
			item.shoot = mod.ProjectileType("LifestealBullet");
			item.shootSpeed = 4f;
            item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MusketBall, 200);
            recipe.AddIngredient(ItemID.LifeFruit, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 200);
			recipe.AddRecipe();
		}
	}
}