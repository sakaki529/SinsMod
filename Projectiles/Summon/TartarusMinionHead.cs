using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class TartarusMinionHead : ModProjectile
	{
        private int spawn = 3;
        private int cantDetectTail;
        private bool Flame;
        private int chaseCounter;
        private int FlameCounter;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Guardian");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 20;
            projectile.height = 20;
            projectile.minion = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.alpha = 255;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 4;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glow = mod.GetTexture("Glow/Projectile/TartarusMinionHead_Glow");
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, texture.Size() / 2f, projectile.scale, 0, 0f);
            spriteBatch.Draw(glow, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(Color.White), projectile.rotation, texture.Size() / 2f, projectile.scale, 0, 0f);
            return false;
        }
        public override void AI()
        {
            Vector2 value = projectile.Center + (projectile.rotation - 1.57079637f).ToRotationVector2() * 8f;
            Vector2 value2 = projectile.rotation.ToRotationVector2() * 16f;
            Dust dust = Main.dust[Dust.NewDust(value + value2, 0, 0, 21, projectile.velocity.X, projectile.velocity.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 1.5f)];
            dust.noGravity = true;
            dust.noLight = true;
            dust.position -= new Vector2(4f);
            dust.fadeIn = 1f;
            dust.velocity = Vector2.Zero;
            Dust dust2 = Main.dust[Dust.NewDust(value - value2, 0, 0, 21, projectile.velocity.X, projectile.velocity.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 1.5f)];
            dust2.noGravity = true;
            dust2.noLight = true;
            dust2.position -= new Vector2(4f);
            dust2.fadeIn = 1f;
            dust2.velocity = Vector2.Zero;
            if (spawn > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int d = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 16f), projectile.width, projectile.height - 16, 21, 0f, 0f, 0, default(Color), 1.1f);
                    Main.dust[d].velocity *= 2f;
                }
                spawn--;
            }

            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            player.AddBuff(mod.BuffType("TartarusMinion"), 3600, true);
            if (Main.time % 120 == 0)
            {
                projectile.netUpdate = true;
            }
            if (player.dead)
            {
                modPlayer.TartarusMinion = false;
            }
            if (!player.active)
            {
                projectile.active = false;
            }
            if (modPlayer.TartarusMinion)
            {
                projectile.timeLeft = 2;
            }

            Vector2 center = player.Center;
            int num3 = 30;
            float num4 = 1800f;
            float num5 = 2100f;
            int num6 = -1;
            if (projectile.Distance(center) > 2900f)
            {
                projectile.Center = center;
                projectile.netUpdate = true;
            }
            NPC ownerTarget = projectile.OwnerMinionAttackTargetNPC;
            if (ownerTarget != null && ownerTarget.CanBeChasedBy(projectile))
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (ownerTarget.CanBeChasedBy(projectile, false) && player.Distance(ownerTarget.Center) < num5)
                    {
                        float num7 = projectile.Distance(ownerTarget.Center);
                        if (num7 < num4)
                        {
                            if (nPC == ownerTarget)
                            {
                                num6 = i;
                            }
                        }
                    }
                }
            }
            else for (int j = 0; j < 200; j++)
            {
                NPC nPC = Main.npc[j];
                if (nPC.CanBeChasedBy(projectile, false) && player.Distance(nPC.Center) < num5)
                {
                    float num7 = projectile.Distance(nPC.Center);
                    if (num7 < num4)
                    {
                        num6 = j;
                    }
                }
            }
            if (num6 != -1)
            {
                NPC nPC = Main.npc[num6];
                Vector2 vector = nPC.Center - projectile.Center;
                (vector.X > 0f).ToDirectionInt();
                (vector.Y > 0f).ToDirectionInt();
                float num8 = 0.3f;
                if (vector.Length() < 900f)
                {
                    num8 = 0.45f;
                }
                if (vector.Length() < 600f)
                {
                    num8 = 0.6f;
                }
                if (vector.Length() < 300f)
                {
                    num8 = 0.8f;
                }
                if (vector.Length() > nPC.Size.Length() * 0.75f)
                {
                    projectile.velocity += Vector2.Normalize(vector) * num8 * 1.5f;
                    if (Vector2.Dot(projectile.velocity, vector) < 0.25f)
                    {
                        projectile.velocity *= 0.8f;
                    }
                }
                float num9 = 40f;
                if (projectile.velocity.Length() > num9)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num9;
                }
                chaseCounter += 1;
            }
            else
            {
                float num10 = 0.2f;
                Vector2 vector2 = center - projectile.Center;
                if (vector2.Length() < 200f)
                {
                    num10 = 0.12f;
                }
                if (vector2.Length() < 140f)
                {
                    num10 = 0.06f;
                }
                if (vector2.Length() > 100f)
                {
                    if (Math.Abs(center.X - projectile.Center.X) > 20f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num10 * Math.Sign(center.X - projectile.Center.X);
                    }
                    if (Math.Abs(center.Y - projectile.Center.Y) > 10f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num10 * Math.Sign(center.Y - projectile.Center.Y);
                    }
                }
                else
                {
                    if (projectile.velocity.Length() > 2f)
                    {
                        projectile.velocity *= 0.96f;
                    }
                }
                if (Math.Abs(projectile.velocity.Y) < 1f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.1f;
                }
                float num11 = 25f;
                if (projectile.velocity.Length() > num11)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num11;
                }
                if (!Flame)
                {
                    chaseCounter = 0;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int direction = projectile.direction;
            projectile.direction = projectile.spriteDirection = (projectile.velocity.X > 0f) ? 1 : -1;
            if (direction != projectile.direction)
            {
                projectile.netUpdate = true;
            }
            float num12 = MathHelper.Clamp(projectile.localAI[0], 0f, 50f);
            projectile.position = projectile.Center;
            //projectile.scale = 1f + num12 * 0.01f;
            projectile.scale = 1f;
            projectile.width = projectile.height = (int)(num3 * projectile.scale);
            projectile.Center = projectile.position;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            if (chaseCounter >= 10f)
            {
                if (Main.rand.Next(120) == 0)
                {
                    Flame = true;
                }
            }
            if (Flame)
            {
                AbyssalFlame();
            }

            if (player.ownedProjectileCounts[mod.ProjectileType("TartarusMinionTail")] < 1)
            {
                cantDetectTail++;
                if (cantDetectTail > 1)
                {
                    projectile.Kill();
                }
            }
            else
            {
                cantDetectTail = 0;
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
            for (int i = 0; i < 20; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, 0f, 0f, 50, default(Color), 1.2f);
                Main.dust[num].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale = 0.5f;
                    Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
        }
        public void AbyssalFlame()
        {
            if (FlameCounter % 10 == 0)
            {
                if (projectile.soundDelay == 0)
                {
                    projectile.soundDelay = 9;
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20, 1f, 0f);

                    Vector2 vector = new Vector2(projectile.velocity.X, projectile.velocity.Y);
                    //Vector2 vector = nPC.Center - projectile.Center;
                    //float vector = (float)Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y);
                    if (vector == Vector2.Zero)
                    {
                        vector = new Vector2(1f, 0f);
                    }
                    vector.Normalize();
                    vector *= 10f;//vel
                    //Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector.X, vector.Y, mod.ProjectileType("AbyssalFlamesFriendly"), projectile.damage / 4, 1f, projectile.owner);
                    //projectileがひとつなら↑のNewProjectileをアクティブにしてここまで
                    //以下複数
                    float num = MathHelper.ToRadians(2.5f);
                    int num2 = 3;
                    //Vector2 position = projectile.Center + Vector2.Normalize(new Vector2(vector2.X, vector2.Y)) * 45f;
                    //↑こいつなくてもよさそう 使う場合はNewProjectileのprojCenterをこいつに変更
                    for (int i = 0; i < num2; i++)
                    {
                        Vector2 vector2 = Utils.RotatedBy(new Vector2(vector.X, vector.Y), MathHelper.Lerp(-num, num, i / (num2 - 1f)), default(Vector2));
                        int num3 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector2.X, vector2.Y, mod.ProjectileType("AbyssalFlamesFriendly"), projectile.damage / 4, 1f, projectile.owner);
                        Main.projectile[num3].ranged = false;
                        Main.projectile[num3].minion = true;
                        Main.projectile[num3].minionSlots = 0f;
                    }
                }
            }
            if (FlameCounter >= 30)
            {
                Flame = false;
                chaseCounter = 0;
                FlameCounter = 0;
                return;
            }
            FlameCounter += 1;
        }
    }
}