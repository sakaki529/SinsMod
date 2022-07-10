using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class PlasmaticMagnet : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Plasmatic Magnet");
            Tooltip.SetDefault("");
            Item.staff[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.damage = 30;
            item.mana = 25;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 30;
			item.useAnimation = 30;
            item.knockBack = 4f;
            item.shoot = mod.ProjectileType("PlasmaticBeam");
            item.shootSpeed = 12f;
            item.value = Item.sellPrice(0, 18, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item43;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 3, knockBack, player.whoAmI, 1f, 0);
            return false;
        }
    }
}