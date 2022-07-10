using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Misc
{
    public class Ritual2 : ModProjectile
    {
        public override string Texture => "SinsMod/NPCs/Rituals/Ritual2";
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
            Color color = new Color(255, 255, 255);
            if ((int)projectile.ai[0] == mod.NPCType("RitualEnvy"))
            {
                color = SinsColor.Envy;
            }
            if ((int)projectile.ai[0] == mod.NPCType("RitualGluttony"))
            {
                color = SinsColor.Gluttony;
            }
            if ((int)projectile.ai[0] == mod.NPCType("RitualGreed"))
            {
                color = SinsColor.Greed;
            }
            if ((int)projectile.ai[0] == mod.NPCType("RitualLust"))
            {
                color = SinsColor.Lust;
            }
            if ((int)projectile.ai[0] == mod.NPCType("RitualPride"))
            {
                color = SinsColor.Pride;
            }
            if ((int)projectile.ai[0] == mod.NPCType("RitualSloth"))
            {
                color = SinsColor.Sloth;
            }
            if ((int)projectile.ai[0] == mod.NPCType("RitualWrath"))
            {
                color = SinsColor.Wrath;
            }
            if ((int)projectile.ai[0] == mod.NPCType("RitualOrigin"))
            {
                color = SinsColor.Origin;
            }
            if ((int)projectile.ai[0] == mod.NPCType("RitualMadness"))
            {
                color = SinsColor.Madness;
            }
            color.A = 0;
            return color;
        }
        public override void AI()
        {
            projectile.rotation -= 0.01f;
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
            if (!NPC.AnyNPCs((int)projectile.ai[0]))
            {
                projectile.active = false;
            }
        }
    }
}