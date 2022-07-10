using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged.Ammo
{
	public class BlackCrystalBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Black Crystal Bullet");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 8;
            item.ranged = true;
			item.damage = 18;
			item.knockBack = 3f;
            item.maxStack = 999;
			item.rare = 11;
			item.value = Item.sellPrice(0, 0, 0, 18);
			item.consumable = true;
			item.shoot = mod.ProjectileType("BlackCrystalBullet");
			item.shootSpeed = 5f;
            item.ammo = AmmoID.Bullet;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddIngredient(null, "BlackCrystalShard");
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}