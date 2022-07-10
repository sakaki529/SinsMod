using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class BookOfEibon : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Book of Eibon");
            Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 28;
			item.height = 30;
			item.damage = 300;
            item.mana = 20;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
			item.useTime = 9;
			item.useAnimation = 9;
			item.useStyle = 5;
			item.knockBack = 7f;
            item.shootSpeed = 11;
            item.shoot = mod.ProjectileType("DemonOfEibon");
			item.UseSound = SoundID.NPCDeath52;
            item.rare = 11;
            item.value = Item.sellPrice(0, 85, 0, 0);
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < Main.rand.Next(1, 2); i++)
            {
                Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI);
            }
            for (int i = 0; i < Main.rand.Next(0, 2); i++)
            {
                Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, mod.ProjectileType("DemonOfEibonH"), damage, knockBack, player.whoAmI);
            }
            switch (Main.rand.Next(7))
            {
                case 0://Envy
                    Main.PlaySound(SoundID.Item103, (int)player.position.X, (int)player.position.Y);
                    for (int useTime = 0; useTime < 10; useTime++)
                    {
                        Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                        float num = Main.mouseX + Main.screenPosition.X - vector.X;
                        float num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                        Vector2 speed = new Vector2(num, num2);
                        speed.Normalize();
                        Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                        vector2.Normalize();
                        speed = speed * 3f + vector2;
                        speed.Normalize();
                        speed *= item.shootSpeed;
                        float num3 = Main.rand.Next(10, 50) * 0.001f;
                        if (Main.rand.Next(2) == 0)
                        {
                            num3 *= -1f;
                        }
                        float num4 = Main.rand.Next(10, 50) * 0.001f;
                        if (Main.rand.Next(2) == 0)
                        {
                            num4 *= -1f;
                        }
                        Projectile.NewProjectile(vector.X, vector.Y, speed.X * 3f, speed.Y * 3f, mod.ProjectileType("EibonTome"), damage, knockBack, player.whoAmI, num4, num3);
                    }
                    break;
                case 1://Gluttony
                    break;
                case 2://Greed
                    float spread = 1f / 3.0f; //Replace 45 with whatever spread you want
                    float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                    double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
                    double deltaAngle = spread / 2f;
                    double offsetAngle;
                    for (int i = 0; i < 3; i++)
                    {
                        offsetAngle = startAngle + deltaAngle * i;
                        Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("StormSpell"), damage, knockBack, item.owner, item.type);
                    }
                    break;
                case 3://Lust
                    for (int useTime = 0; useTime < 3; useTime++)
                    {
                        for (int i = 0; i < Main.rand.Next(3, 6); i++)
                        {
                            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                            float num = item.shootSpeed;
                            int num2 = (int)(damage * 1.0);
                            float num3 = knockBack;
                            float num4 = Main.mouseX + Main.screenPosition.X - vector.X;
                            float num5 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                            float num6 = Main.rand.NextFloat() * 6.28318548f;
                            float value = 20f;
                            float value2 = 60f;
                            Vector2 vector2 = vector + num6.ToRotationVector2() * MathHelper.Lerp(value, value2, Main.rand.NextFloat());
                            for (int num7 = 0; num7 < 50; num7++)
                            {
                                vector2 = vector + num6.ToRotationVector2() * MathHelper.Lerp(value, value2, Main.rand.NextFloat());
                                if (Collision.CanHit(vector, 0, 0, vector2 + (vector2 - vector).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
                                {
                                    break;
                                }
                                num6 = Main.rand.NextFloat() * 6.28318548f;
                            }
                            Vector2 vector3 = Main.MouseWorld - player.Center;
                            Vector2 vector4 = new Vector2(num4, num5).SafeNormalize(Vector2.UnitY) * num;
                            vector3 = vector3.SafeNormalize(vector4) * num;
                            vector3 = Vector2.Lerp(vector3, vector4, 0.25f);
                            Projectile.NewProjectile(vector2, vector3, mod.ProjectileType("SweetDream"), num2, num3, player.whoAmI, Main.rand.Next(3), -1f);
                        }
                    }
                    break;
                case 4://Pride
                    break;
                case 5://Sloth
                    break;
                case 6://Wrath
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < Main.rand.Next(2, 4); j++)
                        {
                            Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                            speed *= Main.rand.NextFloat(0.33f, 1.0f);
                            Projectile.NewProjectile(position.X, position.Y, speed.X, speed.Y, mod.ProjectileType("EibonBall"), damage, knockBack, player.whoAmI);
                        }
                    }
                    break;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(null, "EssenceOfEnvy", 7);
            recipe.AddIngredient(null, "EssenceOfGluttony", 7);
            recipe.AddIngredient(null, "EssenceOfGreed", 7);
            recipe.AddIngredient(null, "EssenceOfLust", 7);
            recipe.AddIngredient(null, "EssenceOfPride", 7);
            recipe.AddIngredient(null, "EssenceOfSloth", 7);
            recipe.AddIngredient(null, "EssenceOfWrath", 7);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}