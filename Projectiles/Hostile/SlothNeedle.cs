using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class SlothNeedle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Needle");
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override bool PreAI()
        {
            if (projectile.ai[0] == -1)
            {
                projectile.friendly = true;
                projectile.hostile = false;
                projectile.ai[0] = 0;
                projectile.localAI[0] = 30;
            }
            if (projectile.friendly)
            {
                if (projectile.localAI[0] > 0)
                {
                    projectile.velocity *= 0.95f;
                    projectile.localAI[0]--;
                    if (projectile.localAI[0] == 1)
                    {
                        int num = -1;
                        for (int i = 0; i < Main.npc.Length; i++)
                        {
                            if (Main.npc[i] != null && Main.npc[i].CanBeChasedBy(projectile, false))
                            {
                                float dist = (float)Math.Sqrt((Main.npc[i].Center.X - projectile.Center.X) * (Main.npc[i].Center.X - projectile.Center.X) + (Main.npc[i].Center.Y - projectile.Center.Y) * (Main.npc[i].Center.Y - projectile.Center.Y));
                                if (dist <= 400f)//640
                                {
                                    num = i;
                                    break;
                                }
                            }
                        }
                        int num2= 16;
                        float num3 = (float)Math.Atan2(projectile.Center.Y + projectile.velocity.Y - projectile.Center.Y, projectile.Center.X + projectile.velocity.X - projectile.Center.X);
                        Vector2 vector = new Vector2((float)(Math.Cos(num3) * num2), (float)(Math.Sin(num3) * num2));
                        if (num != -1)
                        {
                            num2 = 22;
                            num3 = (float)Math.Atan2(Main.npc[num].Center.Y - projectile.Center.Y, Main.npc[num].Center.X - projectile.Center.X);
                            vector = new Vector2((float)(Math.Cos(num3) * num2), (float)(Math.Sin(num3) * num2));
                            projectile.velocity = vector;
                        }
                        else
                        {
                            projectile.velocity = vector;
                        }
                        projectile.localAI[0] = 0;
                    }
                }
            }
            return base.PreAI();
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 0.785f;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            else if (projectile.alpha > 0)
            {
                projectile.alpha -= 15;
            }
            projectile.velocity *= 1.015f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Slow, 240);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Slow, 240);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Slow, 240);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.damage = (int)((double)projectile.damage * 1.5f);
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int num = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 172, projectile.oldVelocity.X * 0.001f, projectile.oldVelocity.Y * 0.001f, 0, default(Color), 1.2f);
                Main.dust[num].noGravity = true;
            }
        }
    }
}