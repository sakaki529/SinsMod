using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class MiniVampire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Vampire");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }
        public override void AI()
        {
            if (projectile.wet && !projectile.honeyWet)
            {
                projectile.Kill();
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 50;
            }
            else
            {
                projectile.extraUpdates = 0;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }

            if (projectile.velocity.X > 0f)
            {
                projectile.spriteDirection = -1;
            }
            else
            {
                if (projectile.velocity.X < 0f)
                {
                    projectile.spriteDirection = 1;
                }
            }
            projectile.rotation = projectile.velocity.X * 0.1f;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= Main.projFrames[projectile.type])
            {
                projectile.frame = 0;
            }

            float num373 = projectile.position.X;
            float num374 = projectile.position.Y;
            float num375 = 100000f;
            bool flag10 = false;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 30f)
            {
                projectile.ai[0] = 30f;
                int num3;
                for (int num376 = 0; num376 < 200; num376 = num3 + 1)
                {
                    if (Main.npc[num376].CanBeChasedBy(this, false) && !Main.npc[num376].wet)
                    {
                        float num377 = Main.npc[num376].position.X + (Main.npc[num376].width / 2);
                        float num378 = Main.npc[num376].position.Y + (Main.npc[num376].height / 2);
                        float num379 = Math.Abs(projectile.position.X + (projectile.width / 2) - num377) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num378);
                        if (num379 < 800f && num379 < num375 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num376].position, Main.npc[num376].width, Main.npc[num376].height))
                        {
                            num375 = num379;
                            num373 = num377;
                            num374 = num378;
                            flag10 = true;
                        }
                    }
                    num3 = num376;
                }
            }
            if (!flag10)
            {
                num373 = projectile.position.X + (projectile.width / 2) + projectile.velocity.X * 100f;
                num374 = projectile.position.Y + (projectile.height / 2) + projectile.velocity.Y * 100f;
            }

            float num380 = 15f;
            float num381 = 0.75f;

            Vector2 vector30 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
            float num382 = num373 - vector30.X;
            float num383 = num374 - vector30.Y;
            float num384 = (float)Math.Sqrt(num382 * num382 + num383 * num383);
            num384 = num380 / num384;
            num382 *= num384;
            num383 *= num384;
            if (projectile.velocity.X < num382)
            {
                projectile.velocity.X = projectile.velocity.X + num381;
                if (projectile.velocity.X < 0f && num382 > 0f)
                {
                    projectile.velocity.X = projectile.velocity.X + num381 * 2f;
                }
            }
            else
            {
                if (projectile.velocity.X > num382)
                {
                    projectile.velocity.X = projectile.velocity.X - num381;
                    if (projectile.velocity.X > 0f && num382 < 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X - num381 * 2f;
                    }
                }
            }
            if (projectile.velocity.Y < num383)
            {
                projectile.velocity.Y = projectile.velocity.Y + num381;
                if (projectile.velocity.Y < 0f && num383 > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num381 * 2f;
                    return;
                }
            }
            else
            {
                if (projectile.velocity.Y > num383)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num381;
                    if (projectile.velocity.Y > 0f && num383 < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num381 * 2f;
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 300, true);
            if (!Main.player[projectile.owner].moonLeech && target.type != 488 && Main.rand.Next(5) == 0)
            {
                float num = damage * 0.005f;
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = projectile.owner;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, projectile.owner, (float)num2, num);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 300, true);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 300, true);
            if (!Main.player[projectile.owner].moonLeech)
            {
                float num = damage * 0.005f;
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = projectile.owner;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, projectile.owner, num2, num);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int num = 0; num < 5; num++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 235, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num2].scale = 0.85f;
                Main.dust[num2].noGravity = true;
                Dust dust = Main.dust[num2];
                dust.velocity += projectile.velocity * 0.1f;
            }
        }
    }
}