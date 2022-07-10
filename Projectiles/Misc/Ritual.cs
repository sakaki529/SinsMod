using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Misc
{
    public class Ritual : ModProjectile
    {
        public override string Texture => "SinsMod/NPCs/Rituals/Ritual";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ritual");
        }
        public override void SetDefaults()
        {
            projectile.width = 408;
            projectile.height = 408;
            projectile.aiStyle = -1;
            projectile.alpha = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.scale = 0f;
            projectile.hide = true;
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCs.Add(index);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale * 0.4175f, SpriteEffects.None, 0f);
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if ((int)projectile.ai[0] == mod.NPCType("Envy"))
            {
                return new Color(0, 200, 0, 0);
            }
            if ((int)projectile.ai[0] == mod.NPCType("Gluttony"))
            {
                return new Color(255, 140, 0, 0);
            }
            if ((int)projectile.ai[0] == mod.NPCType("Greed"))
            {
                return new Color(220, 220, 20, 0);
            }
            if ((int)projectile.ai[0] == mod.NPCType("Lust"))
            {
                return new Color(255, 70, 255, 0);
            }
            if ((int)projectile.ai[0] == mod.NPCType("Pride"))
            {
                return new Color(150, 5, 255, 0);
            }
            if ((int)projectile.ai[0] == mod.NPCType("Sloth"))
            {
                return new Color(10, 30, 255, 0);
            }
            if ((int)projectile.ai[0] == mod.NPCType("Wrath") || (int)projectile.ai[0] == mod.NPCType("Indignationist"))
            {
                return new Color(255, 20, 20, 0);
            }
            if ((int)projectile.ai[0] == mod.NPCType("BlackCrystalNoMove"))
            {
                return new Color(200, 200, 200, 0);
            }
            return new Color(255, 255, 255, 0);
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0)
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("Ritual2"), 0, 0f, Main.myPlayer, projectile.type);
            }
            projectile.rotation += 0.01f;
            projectile.velocity.Y = 0f;
            projectile.velocity.X = 0f;
            projectile.ai[1] += 1f;
            if (projectile.ai[1] > 180f)
            {
                projectile.scale -= 0.05f;
                if (projectile.scale < 0.1f)
                {
                    projectile.Kill();
                }
            }
            else
            {
                if (projectile.scale < 1.0f)
                {
                    projectile.scale += 0.025f;
                }
                if (projectile.scale > 1.0f)
                {
                    projectile.scale = 1.0f;
                }
            }
            if (projectile.ai[1] == 180f)
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("Where_is_my_Soul"), 0, 0, Main.myPlayer, (int)projectile.ai[0]);
            }
            Lighting.AddLight(projectile.Center, 1.0f, 1.0f, 1.0f);

            int num1 = 228;
            int num2 = 228;
            if ((int)projectile.ai[0] == mod.NPCType("Envy"))
            {
                num1 = 228;
            }
            if ((int)projectile.ai[0] == mod.NPCType("Gluttony"))
            {
                num1 = 228;
            }
            if ((int)projectile.ai[0] == mod.NPCType("Greed"))
            {
                num1 = 228;
            }
            if ((int)projectile.ai[0] == mod.NPCType("Lust"))
            {
                num1 = 228;
            }
            if ((int)projectile.ai[0] == mod.NPCType("Pride"))
            {
                num1 = 228;
            }
            if ((int)projectile.ai[0] == mod.NPCType("Sloth"))
            {
                num1 = 228;
            }
            if ((int)projectile.ai[0] == mod.NPCType("Wrath") || (int)projectile.ai[0] == mod.NPCType("Indignationist"))
            {
                num1 = 228;
            }
            if ((int)projectile.ai[0] == mod.NPCType("BlackCrystalNoMove"))
            {
                num1 = 228;
            }
            if (projectile.alpha == 0)
            {
                int num3;
                for (int num874 = 0; num874 < 2; num874 = num3 + 1)
                {
                    float num875 = (float)Main.rand.Next(2, 4);
                    float num876 = projectile.scale;
                    if (num874 == 1)
                    {
                        num876 *= 0.42f;
                        num875 *= -0.75f;
                    }
                    Vector2 value41 = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
                    value41.Normalize();
                    int num877 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 43, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num877].noGravity = true;
                    Main.dust[num877].noLight = true;
                    Main.dust[num877].position = projectile.Center + value41 * 204f * num876;
                    if (Main.rand.Next(8) == 0)
                    {
                        Main.dust[num877].velocity = value41 * -num875 * 2f;
                        Dust dust = Main.dust[num877];
                        dust.scale += 0.5f;
                    }
                    else
                    {
                        Main.dust[num877].velocity = value41 * -num875;
                    }
                    num3 = num874;
                }
            }
            if (projectile.ai[1] >= 60f)
            {
                int num878 = (int)(projectile.ai[1] - 0f) / 60;
                float num879 = projectile.scale * 0.4f;
                int num3;
                for (int num880 = 0; num880 < 1; num880 = num3 + 1)
                {
                    float scaleFactor8 = (float)Main.rand.Next(1, 3);
                    Vector2 value42 = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
                    value42.Normalize();
                    int num881 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 43, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num881].noGravity = true;
                    Main.dust[num881].noLight = true;
                    Main.dust[num881].position = projectile.Center;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num881].velocity = value42 * scaleFactor8 * 2f;
                        Dust dust = Main.dust[num881];
                        dust.scale += 0.5f;
                    }
                    else
                    {
                        Main.dust[num881].velocity = value42 * scaleFactor8;
                    }
                    Main.dust[num881].fadeIn = 2f;
                    num3 = num880;
                }
            }
        }
    }
}