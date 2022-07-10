using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class CosmicShiv : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Cosmic Shiv");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 28;
			item.height = 28;
			item.damage = 36;
            item.melee = true;
            item.autoReuse = true;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = 3;
			item.knockBack = 10;
            item.shootSpeed = 17f;
            item.shoot = ProjectileID.Starfury;
            item.value = Item.sellPrice(0, 2, 30, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(item.shootSpeed * player.direction, 0f);
            Projectile.NewProjectile(player.Center, speed, type, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(player.Center, speed, ProjectileID.HallowStar, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(player.Center, speed, ProjectileID.FallingStar, damage, knockBack, player.whoAmI);
            return false;
        }
	}
}