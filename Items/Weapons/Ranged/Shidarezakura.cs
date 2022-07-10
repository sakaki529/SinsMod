using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class Shidarezakura : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shidarezakura");
            DisplayName.AddTranslation(GameCulture.Chinese, "é}êÇç˜");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 50;
            item.height = 50;
            item.damage = 255;
            item.ranged = true;
            item.noMelee = true;
            item.useStyle = 5;
            item.useTime = 18;
			item.useAnimation = 18;
            item.knockBack = 3.0f;
            item.shoot = mod.ProjectileType("Shidarezakura");
            item.shootSpeed = 15f;
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.useAmmo = AmmoID.Arrow;
            item.rare = 11;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -4);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                float num = item.shootSpeed;
                float num2 = Main.mouseX + Main.screenPosition.X - vector2.X;
                float num3 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (player.gravDir == -1f)
                {
                    num3 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
                }
                float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                float num5 = num4;
                if ((float.IsNaN(num2) && float.IsNaN(num3)) || (num2 == 0f && num3 == 0f))
                {
                    num2 = player.direction;
                    num3 = 0f;
                    num4 = 11f;
                }
                else
                {
                    num4 = 11f / num4;
                }
                num2 *= num4;
                num3 *= num4;
                Vector2 vector3 = new Vector2(num2, num3);
                vector3.X = Main.mouseX + Main.screenPosition.X - vector2.X;
                vector3.Y = Main.mouseY + Main.screenPosition.Y - vector2.Y - 1000f;
                player.itemRotation = (float)Math.Atan2(vector3.Y * player.direction, (vector3.X * player.direction));
                NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                NetMessage.SendData(41, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                int num6 = 7;
                if (Main.rand.Next(3) == 0)
                {
                    num6++;
                }
                for (int num7 = 0; num7 < num6; num7++)
                {
                    vector2 = new Vector2(player.position.X + player.width * 0.5f + (Main.rand.Next(201) * -(float)player.direction) + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    vector2.X = (vector2.X * 10f + player.Center.X) / 11f + Main.rand.Next(-100, 101);
                    vector2.Y -= (150 * num7);
                    num2 = Main.mouseX + Main.screenPosition.X - vector2.X;
                    if (i == 1)
                    {
                        num2 *= -1;
                    }
                    num3 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                    if (num3 < 0f)
                    {
                        num3 *= -1f;
                    }
                    if (num3 < 20f)
                    {
                        num3 = 20f;
                    }
                    num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                    num4 = num / num4;
                    num2 *= num4;
                    num3 *= num4;
                    float num8 = num2 + Main.rand.Next(-40, 41) * 0.03f;
                    float speed = num3 + Main.rand.Next(-40, 41) * 0.03f;
                    num8 *= Main.rand.Next(75, 150) * 0.01f;
                    vector2.X += Main.rand.Next(-50, 51);
                    int num9 = Projectile.NewProjectile(vector2.X, vector2.Y, num8, speed, mod.ProjectileType("Shidarezakura"), damage, knockBack, player.whoAmI, 0f, 0f);
                    Main.projectile[num9].noDropItem = true;
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfLust", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}