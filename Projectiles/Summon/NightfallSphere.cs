using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class NightfallSphere : ModProjectile
    {
        private bool charging;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightfall Sphere");
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 30;
			projectile.height = 30;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glowTexture = mod.GetTexture("Glow/Projectile/NightfallSphere_Glow");
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(glowTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(Color.White), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            for (int i = 6; i >= 0; i--)
            {
                Color color = Color.White;
                color.R = (byte)(255 * (10 - i) / 20);
                color.G = (byte)(255 * (10 - i) / 20);
                color.B = (byte)(255 * (10 - i) / 20);
                color.A = 0;
                Main.spriteBatch.Draw(glowTexture, projectile.oldPos[i] + new Vector2(projectile.width / 2, projectile.height / 2) - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(color), projectile.oldRot[i], new Vector2(texture.Width / 2f, num / 2f), MathHelper.Lerp(0.75f, 1.2f, (10f - i) / 10f), spriteEffects, 0f);
            }
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.5f, 0.4f, 0.2f);
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            player.AddBuff(mod.BuffType("NightfallMinion"), 3600, true);
            if (player.dead)
            {
                modPlayer.NightfallMinion = false;
            }
            if (modPlayer.NightfallMinion)
            {
                projectile.timeLeft = 2;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            int num = (int)projectile.position.X + projectile.width / 2;
            int num2 = (int)projectile.position.Y + projectile.height / 2;
            int num3 = 16;
            if (!WorldGen.SolidTile(num / num3, num2 / 16))
            {
                //Lighting.AddLight((int)(((double)projectile.position.X + (projectile.width / 2)) / 16.0), (int)(((double)projectile.position.Y + (projectile.height / 2)) / 16.0), 0.82f, 0.82f, 0.41f);
            }
            if (charging)
            {
                if (projectile.velocity.X >= 0f)
                {
                    projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
                }
                if (projectile.velocity.X < 0f)
                {
                    projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 3.14f;
                }
                for (int i = 0; i < 4; i++)
                {
                    int num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 174, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num4].noGravity = true;
                }
            }
            else
            {
                projectile.rotation = 0f;
            }
            projectile.spriteDirection = projectile.velocity.X < 0f ? 1 : -1;//reverse
            float speed = 32f;
            float accele = 0.8f;
            int target = -1;
            float dist = 640f;
            float maxDist = 720f;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (projectile.OwnerMinionAttackTargetNPC != null)
                {
                    nPC = projectile.OwnerMinionAttackTargetNPC;
                }
                float num4 = nPC.position.X + (nPC.width / 2);
                float num5 = nPC.position.Y + (nPC.height / 2);
                float num6 = Math.Abs(projectile.position.X + (projectile.width / 2) - num4) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num5);
                if (nPC.active && nPC.CanBeChasedBy(projectile, true) && /*Collision.CanHit(player.position, player.width, player.height, nPC.position, nPC.width, nPC.height) &&*/ nPC.active && num6 < dist && !nPC.dontTakeDamage)
                {
                    target = nPC.whoAmI;
                }
            }
            if (target != -1)
            {
                if (!Main.npc[target].active)
                {
                    target = -1;
                    return;
                }
                float num7 = Math.Abs(projectile.position.X + (projectile.width / 2) - Main.npc[target].Center.X) + Math.Abs(projectile.position.Y + (projectile.height / 2) - Main.npc[target].Center.Y);
                if (num7 < 80f)
                {
                    if (!charging)
                    {
                        Vector2 vector = Main.npc[target].Center - projectile.Center;
                        vector.Normalize();
                        vector.X *= 20f;
                        vector.Y *= 20f;
                        projectile.velocity = vector;
                        charging = true;
                    }
                }
                else
                {
                    charging = false;
                    if (projectile.velocity.Y > speed || projectile.velocity.Y < speed * -1f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y * 0.95f;
                    }
                    if (projectile.velocity.X > speed || projectile.velocity.X < speed * -1f)
                    {
                        projectile.velocity.X = projectile.velocity.X * 0.95f;
                    }
                    float num8 = projectile.position.X + (projectile.width / 2) - Main.npc[target].position.X - (Main.npc[target].width / 2);
                    Math.Atan2(projectile.position.Y + projectile.height - 59f - Main.npc[target].position.Y - (Main.npc[target].height / 2), num8);
                    Vector2 vector2 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float num9 = Main.npc[target].position.X + (Main.npc[target].width / 2) - vector2.X;
                    float num10 = Main.npc[target].position.Y + (Main.npc[target].height / 2) - vector2.Y;
                    float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                    num11 = speed / num11;
                    num9 *= num11;
                    num10 *= num11;
                    if (projectile.velocity.X < num9)
                    {
                        projectile.velocity.X = projectile.velocity.X + accele;
                        if (projectile.velocity.X < 0f && num9 > 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X + accele;
                        }
                    }
                    else if (projectile.velocity.X > num9)
                    {
                        projectile.velocity.X = projectile.velocity.X - accele;
                        if (projectile.velocity.X > 0f && num9 < 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X - accele;
                        }
                    }
                    if (projectile.velocity.Y < num10)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + accele;
                        if (projectile.velocity.Y < 0f && num10 > 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y + accele;
                        }
                    }
                    else if (projectile.velocity.Y > num10)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - accele;
                        if (projectile.velocity.Y > 0f && num10 < 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y - accele;
                        }
                    }
                }
                if (!Main.npc[target].CanBeChasedBy(projectile, true) || /*!Collision.CanHit(player.position, player.width, player.height, Main.npc[target].position, Main.npc[target].width, Main.npc[target].height) ||*/ num7 > maxDist || !Main.npc[target].active || Main.npc[target].dontTakeDamage)
                {
                    target = -1;
                    charging = false;
                    return;
                }
            }
            else
            {
                charging = false;
                if (projectile.velocity.Y > speed || projectile.velocity.Y < speed * -1f)
                {
                    projectile.velocity.Y = projectile.velocity.Y * 0.95f;
                }
                if (projectile.velocity.X > speed || projectile.velocity.X < speed * -1f)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.95f;
                }
                float value = Math.Abs(projectile.position.X + (projectile.width / 2) - player.Center.X) + Math.Abs(projectile.position.Y + (projectile.height / 2) - player.Center.Y);
                float num12 = projectile.position.X + (projectile.width / 2) - player.position.X - (player.width / 2);
                Math.Atan2(projectile.position.Y + projectile.height - 59f - player.position.Y - (player.height / 2), num12);
                Vector2 vector3 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num13 = player.position.X + (player.width / 2) - vector3.X;
                float num14 = player.position.Y + (player.height / 2) - vector3.Y;
                float num15 = (float)Math.Sqrt(num13 * num13 + num14 * num14);
                num15 = speed / num15;
                num13 *= num15;
                num14 *= num15;
                if (value > 96f)
                {
                    if (projectile.velocity.X < num13)
                    {
                        projectile.velocity.X = projectile.velocity.X + accele;
                        if (projectile.velocity.X < 0f && num13 > 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X + accele;
                        }
                    }
                    else if (projectile.velocity.X > num13)
                    {
                        projectile.velocity.X = projectile.velocity.X - accele;
                        if (projectile.velocity.X > 0f && num13 < 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X - accele;
                        }
                    }
                    if (projectile.velocity.Y < num14)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + accele;
                        if (projectile.velocity.Y < 0f && num14 > 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y + accele;
                            return;
                        }
                    }
                    else if (projectile.velocity.Y > num14)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - accele;
                        if (projectile.velocity.Y > 0f && num14 < 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y - accele;
                        }
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 240);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 240);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 240);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = true;
            return true;
        }
    }
}