using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class SkySpear : ModItem
	{
		public override void SetDefaults()
		{
            item.width = 30;
			item.height = 30;
            item.damage = 18;
            item.useTime = 40;
            item.useAnimation = 40;
			item.melee = true;
            item.noUseGraphic = true;
            item.autoReuse = false;
			item.useStyle = 5;
			item.knockBack = 2;
            item.shootSpeed = 1.3f;
            item.shoot = mod.ProjectileType("SkySpear");
            item.value = 1000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
		}
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX * 2, speedY * 2, ProjectileID.Starfury, damage, knockBack, player.whoAmI);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Starfury, 1);
            recipe.AddTile(16);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}