using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class SpiralBeater : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral Beater");
            Main.projFrames[projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = 75;
            projectile.ranged = true;
            //projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glowTexture = mod.GetTexture("Glow/Projectile/SpiralBeater_Glow");
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(glowTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(new Color(255, 255, 255, 127)), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            num = 0f;
            if (projectile.spriteDirection == -1)
            {
                num = 3.14159274f;
            }
            projectile.ai[0] += 1f;
            int num2 = 0;
            if (projectile.ai[0] >= 40f)
            {
                num2++;
            }
            if (projectile.ai[0] >= 80f)
            {
                num2++;
            }
            if (projectile.ai[0] >= 120f)
            {
                num2++;
            }
            int num3 = 5;
            int num4 = 0;
            projectile.ai[1] -= 1f;
            bool flag = false;
            int num5 = -1;
            if (projectile.ai[1] <= 0f)
            {
                projectile.ai[1] = (num3 - num4 * num2);
                flag = true;
                if ((int)projectile.ai[0] / (num3 - num4 * num2) % 7 == 0)
                {
                    num5 = 0;
                }
            }
            projectile.frameCounter += 1 + num2;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.soundDelay <= 0)
            {
                projectile.soundDelay = num3 - num4 * num2;
                if (projectile.ai[0] != 1f)
                {
                    Main.PlaySound(SoundID.Item36, projectile.position);
                }
            }
            if (flag && Main.myPlayer == projectile.owner)
            {
                bool flag2 = player.channel && player.HasAmmo(player.inventory[player.selectedItem], true) && !player.noItems && !player.CCed;
                int num6 = 14;
                float scaleFactor = 14f;
                int weaponDamage = player.GetWeaponDamage(player.inventory[player.selectedItem]);
                float weaponKnockback = player.inventory[player.selectedItem].knockBack;
                if (flag2)
                {
                    player.PickAmmo(player.inventory[player.selectedItem], ref num6, ref scaleFactor, ref flag2, ref weaponDamage, ref weaponKnockback, false);
                    weaponKnockback = player.GetWeaponKnockback(player.inventory[player.selectedItem], weaponKnockback);
                    float scaleFactor2 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 vector2 = vector;
                    Vector2 value = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - vector2;
                    if (player.gravDir == -1f)
                    {
                        value.Y = (Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector2.Y;
                    }
                    Vector2 vector3 = Vector2.Normalize(value);
                    if (float.IsNaN(vector3.X) || float.IsNaN(vector3.Y))
                    {
                        vector3 = -Vector2.UnitY;
                    }
                    vector3 *= scaleFactor2;
                    vector3 = vector3.RotatedBy(Main.rand.NextDouble() * 0.13089969754219055 - 0.065449848771095276, default(Vector2));
                    if (vector3.X != projectile.velocity.X || vector3.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector3;
                    for (int i = 0; i < 1; i++)
                    {
                        Vector2 vector4 = Vector2.Normalize(projectile.velocity) * scaleFactor;
                        vector4 = vector4.RotatedBy(Main.rand.NextDouble() * 0.19634954631328583 - 0.098174773156642914, default(Vector2));
                        if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
                        {
                            vector4 = -Vector2.UnitY;
                        }
                        Projectile.NewProjectile(vector2.X, vector2.Y, vector4.X, vector4.Y, num6, weaponDamage, weaponKnockback, projectile.owner, 0f, 0f);
                    }
                    if (num5 == 0)
                    {
                        num6 = mod.ProjectileType("SpiralRocket");//616
                        scaleFactor = 8f;
                        for (int j = 0; j < 1; j++)
                        {
                            Vector2 vector5 = Vector2.Normalize(projectile.velocity) * scaleFactor;
                            vector5 = vector5.RotatedBy(Main.rand.NextDouble() * 0.39269909262657166 - 0.19634954631328583, default(Vector2));
                            if (float.IsNaN(vector5.X) || float.IsNaN(vector5.Y))
                            {
                                vector5 = -Vector2.UnitY;
                            }
                            Projectile.NewProjectile(vector2.X, vector2.Y, vector5.X, vector5.Y, num6, weaponDamage + 30, weaponKnockback * 1.25f, projectile.owner, 0f, 0f);
                        }
                    }
                }
                else
                {
                    projectile.Kill();
                }
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
        }
    }
}