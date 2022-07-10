using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class Purification : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("祓");
            Main.projFrames[projectile.type] = 7;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 128;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 240;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor * 0.35f) * ((float)(projectile.oldPos.Length - i) / projectile.oldPos.Length);
                int num = texture.Height / Main.projFrames[projectile.type];
                int num2 = num * projectile.frame;
                if (projectile.ai[0] == 0)
                {
                    spriteBatch.Draw(texture, drawPos, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(texture, drawPos, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.FlipHorizontally, 0f);
                }
            }
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.friendly)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool PreAI()
        {
            if ((int)projectile.ai[1] == 1)
            {
                projectile.friendly = true;
                projectile.hostile = false;
            }
            if (projectile.friendly)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].friendly && projectile.Hitbox.Intersects(Main.npc[i].Hitbox) && !Main.npc[i].dontTakeDamage)
                    {
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                    }
                }
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 2)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            float accel = 0.3f;
            projectile.spriteDirection = projectile.ai[0] == 0 ? 1 : - 1;
            projectile.velocity.X += projectile.ai[0] == 0 ? accel : -accel;
            if (projectile.ai[0] == 0)
            {
                if (projectile.velocity.X > 60f)
                {
                    projectile.velocity.X = 60f;
                }
            }
            else
            {
                if (projectile.velocity.X < -60f)
                {
                    projectile.velocity.X = -60f;
                }
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Nothingness"), 20);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.GetModPlayer<SinsPlayer>().nothingnessTime = 20;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.GetModPlayer<SinsPlayer>().nothingnessTime = 20;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}