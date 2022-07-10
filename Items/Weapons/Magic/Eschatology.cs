using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class Eschatology : ModItem
	{
        public override string Texture => "SinsMod/Extra/Placeholder/Placeholder";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Eschatology");
		}
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.damage = 170;
            item.mana = 25;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 10;
            item.useAnimation = 10;
			item.knockBack = 5.2f;
            item.shootSpeed = 21f;
            item.shoot = mod.ProjectileType("Eschatology");
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item74;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float shootSpeed = item.shootSpeed;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float num = Main.mouseX + Main.screenPosition.X - vector.X;
            float num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
            if (player.gravDir == -1f)
            {
                num2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
            }
            float num3 = (float)Math.Sqrt(num * num + num2 * num2);
            if ((float.IsNaN(num) && float.IsNaN(num2)) || (num == 0f && num2 == 0f))
            {
                num = player.direction;
                num2 = 0f;
                num3 = shootSpeed;
            }
            else
            {
                num3 = shootSpeed / num3;
            }
            num *= num3;
            num2 *= num3;
            int num4 = 2;
            for (int i = 0; i < num4; i++)
            {
                vector = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                vector.X = (vector.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                vector.Y -= 100 * i;
                num = Main.mouseX + Main.screenPosition.X - vector.X + Main.rand.Next(-40, 41) * 0.03f;
                num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                if (num2 < 0f)
                {
                    num2 *= -1f;
                }
                if (num2 < 20f)
                {
                    num2 = 20f;
                }
                num3 = (float)Math.Sqrt(num * num + num2 * num2);
                num3 = shootSpeed / num3;
                num *= num3;
                num2 *= num3;
                float num5 = num;
                float num6 = num2 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(vector.X, vector.Y, num5 * 0.75f, num6 * 0.75f, type, damage, knockBack, player.whoAmI, 0f, 0f);
            }
            for (int j = 0; j < num4; j++)
            {
                vector = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y + 600f);
                vector.X = (vector.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                vector.Y += 100 * j;
                num = Main.mouseX + Main.screenPosition.X - vector.X + Main.rand.Next(-40, 41) * 0.03f;
                num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                if (num2 < 0f)
                {
                    num2 *= -1f;
                }
                if (num2 < 20f)
                {
                    num2 = 20f;
                }
                num3 = (float)Math.Sqrt(num * num + num2 * num2);
                num3 = shootSpeed / num3;
                num *= num3;
                num2 *= num3;
                float num5 = num;
                float num6 = num2 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(vector.X, vector.Y, num5 * 0.75f, -num6 * 0.75f, type, damage, knockBack, player.whoAmI, 0f, 0f);
            }
            return false;
        }
    }
}