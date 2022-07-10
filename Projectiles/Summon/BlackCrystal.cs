using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class BlackCrystal : ModProjectile
    {
        public override string Texture => "SinsMod/NPCs/Boss/Madness/BlackCrystalNoMove";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Black Crystal");
            Main.projFrames[projectile.type] = 8;
        }
		public override void SetDefaults()
		{
            projectile.width = 26;
			projectile.height = 48;
            projectile.sentry = true;
			projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Color color = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            Vector2 vector = projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            Rectangle rectangle = texture2D.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            Color alpha = projectile.GetAlpha(color);
            Vector2 vector2 = rectangle.Size() / 2f;
            float num = (float)Math.Cos(6.28318548f * (projectile.localAI[0] / 60f)) + 3f + 3f;
            for (float num2 = 0f; num2 < 4f; num2 += 1f)
            {
                SpriteBatch spriteBatch2 = Main.spriteBatch;
                Texture2D texture2D2 = texture2D;
                Vector2 vector3 = vector;
                Vector2 unitY = Vector2.UnitY;
                double radians = num2 * 1.57079637f;
                spriteBatch2.Draw(texture2D2, vector3 + unitY.RotatedBy(radians, default(Vector2)) * num, rectangle, alpha * 0.75f, projectile.rotation, vector2, projectile.scale, 0, 0f);
            }
            return true;
        }
        public override bool PreAI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            return true;
        }
        public override void AI()
        {
            bool flag = projectile.type == mod.ProjectileType("BlackCrystal");
            Player player = Main.player[projectile.owner];
            projectile.position.X = (int)projectile.position.X;
            projectile.position.Y = (int)projectile.position.Y;
            float num = 1500f;
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
            float[] localAI = projectile.localAI;
            int num2 = 0;
            float num3 = localAI[num2];
            localAI[num2] = num3 + 1f;
            if (projectile.localAI[0] >= 60f)
            {
                projectile.localAI[0] = 0f;
            }
            if (projectile.ai[0] < 0f)
            {
                float[] ai = projectile.ai;
                int num4 = 0;
                float num5 = ai[num4];
                ai[num4] = num5 + 1f;
            }
            if (projectile.ai[0] == 0f)
            {
                int num6 = -1;
                float num7 = num;
                NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(projectile, false))
                {
                    float num8 = projectile.Distance(ownerMinionAttackTargetNPC.Center);
                    if (num8 < num7 && Collision.CanHitLine(projectile.Center, 0, 0, ownerMinionAttackTargetNPC.Center, 0, 0))
                    {
                        num7 = num8;
                        num6 = ownerMinionAttackTargetNPC.whoAmI;
                    }
                }
                if (num6 < 0)
                {
                    int num10;
                    for (int i = 0; i < 200; i = num10 + 1)
                    {
                        NPC nPC = Main.npc[i];
                        if (nPC.CanBeChasedBy(projectile, false))
                        {
                            float num9 = projectile.Distance(nPC.Center);
                            if (num9 < num7 && Collision.CanHitLine(projectile.Center, 0, 0, nPC.Center, 0, 0))
                            {
                                num7 = num9;
                                num6 = i;
                            }
                        }
                        num10 = i;
                    }
                }
                if (num6 != -1)
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = num6;
                    projectile.netUpdate = true;
                    return;
                }
            }
            if (projectile.ai[0] > 0f)
            {
                int num11 = (int)projectile.ai[1];
                if (!Main.npc[num11].CanBeChasedBy(projectile, false))
                {
                    projectile.ai[0] = 0f;
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                    return;
                }
                float[] ai2 = projectile.ai;
                int num12 = 0;
                float num13 = ai2[num12];
                ai2[num12] = num13 + 1f;
                if (projectile.ai[0] >= 5f)
                {
                    Vector2 vec = projectile.DirectionTo(Main.npc[num11].Center);
                    if (vec.HasNaNs())
                    {
                        vec = Vector2.UnitY;
                    }
                    int direction = (vec.X > 0f) ? 1 : -1;
                    projectile.direction = direction;
                    projectile.ai[0] = -20f;
                    projectile.netUpdate = true;
                    if (projectile.owner == Main.myPlayer)
                    {
                        Vector2 vector = Main.npc[num11].position + Main.npc[num11].Size * Utils.RandomVector2(Main.rand, 0f, 1f) - projectile.Center;
                        int num14;
                        for (int j = 0; j < 3; j = num14 + 1)
                        {
                            Vector2 vector2 = projectile.Center + vector;
                            if (j > 0)
                            {
                                vector2 = projectile.Center + vector.RotatedByRandom(0.78539818525314331) * (Main.rand.NextFloat() * 0.5f + 0.75f);
                            }
                            float x = Main.rgbToHsl(new Color(20 + (int)(Main.DiscoR * 1.0f), 0, 20 + (int)(Main.DiscoR * 1.0f))).X;
                            Projectile.NewProjectile(vector2.X, vector2.Y, 0f, 0f, mod.ProjectileType("BlackCrystalExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0, projectile.whoAmI);
                            num14 = j;
                        }
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCKilled").WithVolume(0.8f), (int)projectile.position.X, (int)projectile.position.Y);
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 50;
            projectile.height = 50;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            for (int i = 0; i < 20; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 186, 0f, 0f, 50, default(Color), 1.2f);
                Main.dust[num].velocity *= 3f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale = 0.5f;
                    Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            for (int j = 0; j < 40; j++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 186, 0f, 0f, 0, default(Color), 1.0f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 5f;
                Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 245, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num2].velocity *= 2f;
                Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 127 - projectile.alpha / 2);
        }
    }
}