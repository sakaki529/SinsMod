using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class Hellheat : ModItem
	{
        public override string Texture => "SinsMod/Extra/Placeholder/Placeholder";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Hellheat");
            Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 140;
            item.mana = 12;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 15;
            item.useAnimation = 30;
            item.knockBack = 2.0f;
            item.shoot = mod.ProjectileType("Hellheat");
            item.shootSpeed = 10;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item60;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 1; i < 3; i++)
            {
                Projectile.NewProjectile(position.X - (player.direction == 1 ? 50 : -50), position.Y - (i == 1 ? 100 : -100), player.direction == 1 ? item.shootSpeed : -item.shootSpeed, 0, type, damage, knockBack, player.whoAmI, 0, i);
            }
            int num = Main.rand.Next(0, 360);
            for (int j = 0; j < 6; j++)
            {
                Vector2 vector = Utils.RotatedBy(Vector2.UnitX, MathHelper.Lerp(0f, 6.28318548f, j / 6f), default(Vector2));
                Projectile.NewProjectile(position, vector.RotatedBy(num) * 10, type, damage, knockBack, item.owner, 0f, 0f);
            }
            /*for (int j = 0; j < 3; j++)
            {
                float num = 0.783f;
                double num2 = Math.Atan2(speedX, speedY) - num / 2f;
                double num3 = num / 8f;
                double num4 = num2 + num3 * (j + j * j) / 2.0 + 24f * j;
                Projectile.NewProjectile(position.X, position.Y, (float)(Math.Sin(num4) * 10f), (float)(Math.Cos(num4) * 10f), type, damage, knockBack, player.whoAmI, 0f, 0f);
                Projectile.NewProjectile(position.X, position.Y, -(float)(Math.Sin(num4) * 10f), -(float)(Math.Cos(num4) * 10f), type, damage, knockBack, player.whoAmI, 0f, 0f);
            }*/
            return false;
        }
    }
}