using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged.Ammo
{
	public class NightEnergyBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Nightenergy Bullet");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 8;
            item.damage = 3;
            item.ranged = true;
            item.consumable = true;
            item.maxStack = 999;
			item.knockBack = 3.5f;
            item.ammo = AmmoID.Bullet;
            item.shoot = mod.ProjectileType("NightEnergyBullet");
            item.shootSpeed = 6.0f;
            item.value = Item.sellPrice(0, 0, 0, 40);
            item.rare = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddIngredient(null, "NightEnergizedBar");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}