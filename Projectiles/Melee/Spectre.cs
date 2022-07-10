using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace SinsMod.Projectiles.Melee
{
    public class Spectre : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectre");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            aiType = -1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.width = 20;
            projectile.height = 20;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= Main.projFrames[projectile.type])
            {
                projectile.frame = 0;
            }
            int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180, 0f, 0f, 0, default(Color), 1f);
            Dust dust = Main.dust[num];
            dust.velocity *= 0.1f;
            Main.dust[num].scale = 1.3f;
            Main.dust[num].noGravity = true;
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) - 1.57f;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 6f)
            {
                float num1 = projectile.Center.X;
                float num2 = projectile.Center.Y;
                float num3 = 600f;
                bool flag = false;
                for (int num4 = 0; num4 < 200; num4++)
                {
                    if (Main.npc[num4].CanBeChasedBy(projectile, false)/* && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num4].Center, 1, 1)*/)
                    {
                        float num5 = Main.npc[num4].position.X + (Main.npc[num4].width / 2);
                        float num6 = Main.npc[num4].position.Y + (Main.npc[num4].height / 2);
                        float num7 = Math.Abs(projectile.position.X + (projectile.width / 2) - num5) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num6);
                        if (num7 < num3)
                        {
                            num3 = num7;
                            num1 = num5;
                            num2 = num6;
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    float num8 = 20f;
                    Vector2 vector35 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float num9 = num1 - vector35.X;
                    float num10 = num2 - vector35.Y;
                    float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                    num11 = num8 / num11;
                    num9 *= num11;
                    num10 *= num11;
                    projectile.velocity.X = (projectile.velocity.X * 20f + num9) / 21f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + num10) / 21f;
                    return;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath39, (int)projectile.position.X, (int)projectile.position.Y);
            SinsUtility.Explode(projectile.whoAmI, 180, 180, delegate
            {
                int num = 10;
                for (int i = 0; i < num; i++)
                {
                    Vector2 vector = Vector2.Normalize(projectile.velocity) * new Vector2(projectile.width / 2f, projectile.height) * 0.4f;
                    vector = vector.RotatedBy((i - (num / 2 - 1)) * 6.28318548f / num, default(Vector2)) + projectile.position;
                    Vector2 vector2 = vector - projectile.position;
                    int num2 = Dust.NewDust(vector + vector2, 0, 0, 180, vector2.X * 0.9f, vector2.Y * 0.9f, 180, Color.White, 2.0f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity = vector2;
                }
            });
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 127) * (1f - projectile.alpha / 255f);
        }
    }
}