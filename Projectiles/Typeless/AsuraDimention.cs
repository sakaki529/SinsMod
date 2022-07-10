using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Typeless
{
    public class AsuraDimention : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Asura Dimention");
        }
		public override void SetDefaults()
		{
            projectile.width = 110;
			projectile.height = 84;
			projectile.timeLeft = 2;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (player.gravDir == -1f)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glow = mod.GetTexture("Glow/Projectile/AsuraDimention_Glow");
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(glow, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(new Color(Main.DiscoR, Main.DiscoR, Main.DiscoR)), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            if (Main.rand.Next(10) == 0)
            {
                int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 21, 0f, 0f, 150, default(Color), 1.3f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].fadeIn = 1.5f;
                Main.dust[dust].scale = 1.4f;
                Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= Main.rand.Next(50, 100) * 0.04f;
                vector.Normalize();
                vector *= 80f;
                Main.dust[dust].position = projectile.Center - vector;
                Vector2 newVelocity = projectile.Center - Main.dust[dust].position;
                Main.dust[dust].velocity = newVelocity * 0.1f;
            }
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            /*if (modPlayer.setA && !player.dead)
            {
                projectile.timeLeft = 2;
            }*/
            projectile.position.X = player.Center.X - projectile.width / 2;
            projectile.position.Y = player.Center.Y - projectile.height / 3 + player.gfxOffY - 60f;
            if (player.gravDir == -1f)
            {
                projectile.position.Y = projectile.position.Y + 92f;
            }
            projectile.position.X = (int)projectile.position.X;
            projectile.position.Y = (int)projectile.position.Y;
            projectile.velocity = Vector2.Zero;
            projectile.alpha -= 5;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.direction == 0)
            {
                projectile.direction = Main.player[projectile.owner].direction;
            }
            if (projectile.alpha == 0 && Main.rand.Next(15) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.Top, 0, 0, 186, 0f, 0f, 100, default(Color), 1f)];
                dust.velocity.X = 0f;
                dust.noGravity = true;
                dust.fadeIn = 1f;
                dust.position = projectile.Center + Vector2.UnitY.RotatedByRandom(6.2831854820251465) * (4f * Main.rand.NextFloat() + 26f);
                dust.scale = 0.5f;
                dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }

            if (projectile.ai[0] != 0f)
            {
                projectile.ai[0] -= 1f;
                return;
            }
            bool flag = false;
            float num3 = projectile.Center.X;
            float num4 = projectile.Center.Y;
            float num5 = 1000f;
            NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(projectile, false))
            {
                float num6 = ownerMinionAttackTargetNPC.position.X + (ownerMinionAttackTargetNPC.width / 2);
                float num7 = ownerMinionAttackTargetNPC.position.Y + (ownerMinionAttackTargetNPC.height / 2);
                float num8 = Math.Abs(projectile.position.X + (projectile.width / 2) - num6) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num7);
                if (num8 < num5 && Collision.CanHit(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                {
                    num5 = num8;
                    num3 = num6;
                    num4 = num7;
                    flag = true;
                }
            }
            if (!flag)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(projectile, false))
                    {
                        float num9 = Main.npc[i].position.X + (Main.npc[i].width / 2);
                        float num10 = Main.npc[i].position.Y + (Main.npc[i].height / 2);
                        float num11 = Math.Abs(projectile.position.X + (projectile.width / 2) - num9) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num10);
                        if (num11 < num5 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                        {
                            num5 = num11;
                            num3 = num9;
                            num4 = num10;
                            flag = true;
                        }
                    }
                }
            }
            if (flag)
            {
                float num12 = num3;
                float num13 = num4;
                num3 -= projectile.Center.X;
                num4 -= projectile.Center.Y;
                if (num3 < 0f)
                {
                    projectile.spriteDirection = 1;
                }
                else
                {
                    projectile.spriteDirection = -1;
                }
                float num14 = Main.rand.Next(16, 22);
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num15 = num12 - vector.X;
                float num16 = num13 - vector.Y;
                float num17 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
                num17 = num14 / num17;
                num15 *= num17;
                num16 *= num17;
                int i = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 10, num15, num16, mod.ProjectileType("AsuraBall"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                if (player.meleeDamage >= player.thrownDamage)
                {
                    Main.projectile[i].melee = true;
                }
                else
                {
                    Main.projectile[i].thrown = true;
                }
                projectile.ai[0] = 8f;
            }
        }
        public override void Kill(int timeLeft)
        {
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 50;
            projectile.height = 50;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            for (int j = 0; j < 20; j++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 186, 0f, 0f, 50, default(Color), 1.2f);
                Main.dust[num].velocity *= 3f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale = 0.5f;
                    Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            for (int k = 0; k < 40; k++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 186, 0f, 0f, 0, default(Color), 1.0f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 5f;
                Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 245, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num2].velocity *= 2f;
                Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
        }
    }
}