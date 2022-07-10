using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class BlackBoltH : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Bolt");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(661);
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override void AI()
        {
            projectile.type = ProjectileID.BlackBolt;
            projectile.timeLeft++;
            float num = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
            float num2 = projectile.localAI[0];
            if (num2 == 0f)
            {
                projectile.localAI[0] = num;
                num2 = num;
            }
            float num3 = projectile.position.X;
            float num4 = projectile.position.Y;
            float num5 = 400f;
            bool flag = false;
            int num6 = 0;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 10f)
            {
                projectile.ai[0] -= 1f;
                if (projectile.ai[1] == 0f)
                {
                    for (int num7 = 0; num7 < 200; num7++)
                    {
                        if (Main.npc[num7].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num7 + 1)))
                        {
                            float num8 = Main.npc[num7].position.X + (Main.npc[num7].width / 2);
                            float num9 = Main.npc[num7].position.Y + (Main.npc[num7].height / 2);
                            float num10 = Math.Abs(projectile.position.X + (projectile.width / 2) - num8) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num9);
                            if (num10 < num5 && Collision.CanHit(new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2)), 1, 1, Main.npc[num7].position, Main.npc[num7].width, Main.npc[num7].height))
                            {
                                num5 = num10;
                                num3 = num8;
                                num4 = num9;
                                flag = true;
                                num6 = num7;
                            }
                        }
                    }
                    if (flag)
                    {
                        projectile.ai[1] = num6 + 1;
                    }
                    flag = false;
                }
                if (projectile.ai[1] != 0f)
                {
                    int num11 = (int)(projectile.ai[1] - 1f);
                    if (Main.npc[num11].active && Main.npc[num11].CanBeChasedBy(projectile, true))
                    {
                        float num12 = Main.npc[num11].position.X + (Main.npc[num11].width / 2);
                        float num13 = Main.npc[num11].position.Y + (Main.npc[num11].height / 2);
                        if (Math.Abs(projectile.position.X + (projectile.width / 2) - num12) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num13) < 1000f)
                        {
                            flag = true;
                            num3 = Main.npc[num11].position.X + (Main.npc[num11].width / 2);
                            num4 = Main.npc[num11].position.Y + (Main.npc[num11].height / 2);
                        }
                    }
                }
                if (!projectile.friendly)
                {
                    flag = false;
                }
                if (flag)
                {
                    float num12 = num2;
                    Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float num13 = num3 - vector.X;
                    float num14 = num4 - vector.Y;
                    float num15 = (float)Math.Sqrt(num13 * num13 + num14 * num14);
                    num15 = num12 / num15;
                    num13 *= num15;
                    num14 *= num15;
                    int num16 = 8;
                    projectile.velocity.X = (projectile.velocity.X * (num16 - 1) + num13) / num16;
                    projectile.velocity.Y = (projectile.velocity.Y * (num16 - 1) + num14) / num16;
                }
            }
        }
    }
}