using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class AbyssalFlamethrower : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Guardian's Breath");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 24;
            item.damage = 210;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 4.5f;
            item.useTime = 1;
			item.useAnimation = 5;
            item.useAmmo = AmmoID.Gel;
            item.shoot = mod.ProjectileType("AbyssalFlamesFriendly");
            item.shootSpeed = 12f;
            item.value = Item.sellPrice(0, 75, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item20;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 1f / 9;
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 2f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}