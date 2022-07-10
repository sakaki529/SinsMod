using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class SpiralWeaver : ModProjectile
	{
        public override string Texture => "SinsMod/Items/Weapons/Ranged/SpiralWeaver";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral Weaver");
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
            projectile.hide = true;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glow = mod.GetTexture("Glow/Item/SpiralWeaver_Glow");
            Texture2D extra = mod.GetTexture("Extra/Projectile/SpiralWeaver_Extra");
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(glow, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(new Color(255, 255, 255, 127)), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            if (projectile.localAI[0] > 0f)
            {
                int frameY = 6 - (int)(projectile.localAI[0] / 1f);
                Vector2 vector = (projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();
                Main.spriteBatch.Draw(extra, vector + Vector2.Normalize(projectile.velocity) * 2f, new Microsoft.Xna.Framework.Rectangle?(extra.Frame(1, 6, 0, frameY)), new Color(255, 255, 255, 127) * Main.player[projectile.owner].stealth, projectile.rotation, new Vector2((float)(spriteEffects.HasFlag(SpriteEffects.FlipHorizontally) ? extra.Width : 0), (float)Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] / 2f - 2f), projectile.scale, spriteEffects, 0f);
            }
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
            int num3 = 24;
            int num4 = 2;
            projectile.ai[1] -= 1f;
            bool flag = false;
            if (projectile.ai[1] <= 0f)
            {
                projectile.ai[1] = num3 - num4 * num2;
                flag = true;
                int value = (int)projectile.ai[0] / (num3 - num4 * num2);
            }
            bool flag2 = player.channel && player.HasAmmo(player.inventory[player.selectedItem], true) && !player.noItems && !player.CCed;
            if (projectile.localAI[0] > 0f)
            {
                projectile.localAI[0] -= 1f;
            }
            if (projectile.soundDelay <= 0 & flag2)
            {
                projectile.soundDelay = num3 - num4 * num2;
                if (projectile.ai[0] != 1f)
                {
                    Main.PlaySound(SoundID.Item5, projectile.position);
                }
                projectile.localAI[0] = 12f;
            }
            //player.phantasmTime = 2;
            player.GetModPlayer<SinsPlayer>().spiralTime = 2;
            if (flag && Main.myPlayer == projectile.owner)
            {
                int num5 = 14;
                float scaleFactor = 14f;
                int weaponDamage = player.GetWeaponDamage(player.inventory[player.selectedItem]);
                float weaponKnockback = player.inventory[player.selectedItem].knockBack;
                if (flag2)
                {
                    player.PickAmmo(player.inventory[player.selectedItem], ref num5, ref scaleFactor, ref flag2, ref weaponDamage, ref weaponKnockback, false);
                    weaponKnockback = player.GetWeaponKnockback(player.inventory[player.selectedItem], weaponKnockback);
                    float scaleFactor12 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 vector2 = vector;
                    Vector2 value2 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - vector2;
                    if (player.gravDir == -1f)
                    {
                        value2.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector2.Y;
                    }
                    Vector2 vector3 = Vector2.Normalize(value2);
                    if (float.IsNaN(vector3.X) || float.IsNaN(vector3.Y))
                    {
                        vector3 = -Vector2.UnitY;
                    }
                    vector3 *= scaleFactor12;
                    if (vector3.X != projectile.velocity.X || vector3.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector3 * 0.55f;
                    for (int num6 = 0; num6 < 4; num6++)
                    {
                        Vector2 vector4 = Vector2.Normalize(projectile.velocity) * scaleFactor * (0.6f + Main.rand.NextFloat() * 0.8f);
                        if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
                        {
                            vector4 = -Vector2.UnitY;
                        }
                        Vector2 vector5 = vector2 + Utils.RandomVector2(Main.rand, -15f, 15f);
                        int num7 = Projectile.NewProjectile(vector5.X, vector5.Y, vector4.X, vector4.Y, num5, weaponDamage, weaponKnockback, projectile.owner, 0f, 0f);
                        Main.projectile[num7].noDropItem = true;
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