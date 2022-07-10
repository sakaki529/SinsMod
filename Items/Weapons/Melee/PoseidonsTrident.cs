using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class PoseidonsTrident : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poseidons Trident");
        }
        public override void SetDefaults()
		{
            item.width = 24;
			item.height = 24;
			item.damage = 85;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.autoReuse = true;
            item.useTime = 28;
            item.useAnimation = 28;
			item.useStyle = 5;
			item.knockBack = 10;
            item.shootSpeed = 3.2f;
            item.shoot = mod.ProjectileType("PoseidonsTrident");
			item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 7;
			item.UseSound = SoundID.Item1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Trident, 1);
            recipe.AddTile(16);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //Projectile.NewProjectile(position.X, position.Y, speedX * 1, speedY * 1, ProjectileID.Starfury, damage, knockBack, player.whoAmI);
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }
}
