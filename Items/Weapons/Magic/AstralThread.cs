using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AstralThread : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Astral Thread");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 30;
            item.height = 30;
            item.damage = 50;
            item.mana = 40;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 15;
			item.useAnimation = 15;
			item.knockBack = 0f;
            item.shootSpeed = 20f;
            item.shoot = mod.ProjectileType("AstralThread");
            item.value = Item.sellPrice(0, 18, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item43;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y + 85f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y - 85f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y + 170f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y - 170f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y + 265f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y - 265f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y + 340f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y - 340f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y + 425f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y - 425f, 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            /*Projectile.NewProjectile(player.position.X + 900f, player.position.Y, - 20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y + 85f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y - 85f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y + 170f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y - 170f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y + 265f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y - 265f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y + 340f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y - 340f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y + 425f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 900f, player.position.Y - 425f, -20f, 0f, type, damage, 0f, player.whoAmI, 0f, 0f);*/

            Projectile.NewProjectile(player.Center.X, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X + 85f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 85f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X + 170f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 170f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X + 265f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 265f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X + 340f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 340f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X + 425f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.Center.X - 425f, player.Center.Y - 425f, 0f, 20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            /*Projectile.NewProjectile(player.position.X - 20f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 85f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X - 85f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 170f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X - 170f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 265f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X - 265f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 340f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X - 340f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X + 425f, player.position.Y + 900f, 0, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(player.position.X - 425f, player.position.Y + 900f, 0f, -20f, type, damage, 0f, player.whoAmI, 0f, 0f);*/
            return false;
        }
    }
}