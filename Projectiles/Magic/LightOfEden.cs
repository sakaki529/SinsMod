using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class LightOfEden : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light of Eden");
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 18;
            projectile.friendly = true;
            //projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Color color = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Vector2 vector = (projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();
            float scaleFactor = (float)Math.Cos(6.28318548f * (projectile.ai[0] / 30f)) * 2f + 2f;
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (projectile.ai[0] > 120f)
            {
                scaleFactor = 6f;
            }
            for (float num3 = 0f; num3 < 4f; num3++)
            {
                Vector2 unitY = Vector2.UnitY;
                double value = num3 * 6.28318548f / 4f;
                Vector2 center = default(Vector2);
                spriteBatch.Draw(texture, vector + unitY.RotatedBy(value, center) * scaleFactor, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(color).MultiplyRGBA(new Color(255, 255, 255, 0)) * 0.03f, projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            }
            Main.spriteBatch.Draw(texture, vector, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float num2 = 30f;
            if (projectile.ai[0] > 90f)
            {
                num2 = 15f;
            }
            if (projectile.ai[0] > 120f)
            {
                num2 = 5f;
            }
            projectile.damage = (int)(player.inventory[player.selectedItem].damage * player.magicDamage);
            projectile.ai[0] += 1f;
            projectile.ai[1] += 1f;
            bool flag = false;
            if (projectile.ai[0] % num2 == 0f)
            {
                flag = true;
            }
            int num3 = 10;
            bool flag2 = false;
            if (projectile.ai[0] % num2 == 0f)
            {
                flag2 = true;
            }
            if (projectile.ai[1] >= 1f)
            {
                projectile.ai[1] = 0f;
                flag2 = true;
                if (Main.myPlayer == projectile.owner)
                {
                    float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 value = vector;
                    Vector2 value2 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - value;
                    if (player.gravDir == -1f)
                    {
                        value2.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - value.Y;
                    }
                    Vector2 vector2 = Vector2.Normalize(value2);
                    if (float.IsNaN(vector2.X) || float.IsNaN(vector2.Y))
                    {
                        vector2 = -Vector2.UnitY;
                    }
                    vector2 = Vector2.Normalize(Vector2.Lerp(vector2, Vector2.Normalize(projectile.velocity), 0.92f));
                    vector2 *= scaleFactor;
                    if (vector2.X != projectile.velocity.X || vector2.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector2;
                }
            }
            projectile.frameCounter++;
            int num4 = (projectile.ai[0] < 120f) ? 4 : 1;
            if (projectile.frameCounter >= num4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.soundDelay <= 0)
            {
                projectile.soundDelay = num3;
                projectile.soundDelay *= 2;
                if (projectile.ai[0] != 1f)
                {
                    Main.PlaySound(SoundID.Item15, projectile.position);
                }
            }
            if (flag2 && Main.myPlayer == projectile.owner)
            {
                bool flag3 = !flag || player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
                bool flag4 = player.channel && flag3 && !player.noItems && !player.CCed;
                if (flag4)
                {
                    if (projectile.ai[0] == 1f)
                    {
                        Vector2 center = projectile.Center;
                        Vector2 vector2_ = Vector2.Normalize(projectile.velocity);
                        if (float.IsNaN(vector2_.X) || float.IsNaN(vector2_.Y))
                        {
                            vector2_ = -Vector2.UnitY;
                        }
                        int num5 = projectile.damage;
                        for (int l = 0; l < 6; l++)
                        {
                            Projectile.NewProjectile(center.X, center.Y, vector2_.X, vector2_.Y, mod.ProjectileType("LightOfEdenLaser"), num5, projectile.knockBack, projectile.owner, l, projectile.whoAmI);
                        }
                        projectile.netUpdate = true;
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            ((SinsPlayer)player.GetModPlayer(mod, "SinsPlayer")).chargeTime = 0;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.White;
            color.A = 125;
            return color;
        }
    }
}