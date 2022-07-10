using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class AbyssalWave : ModProjectile
    {
        public float offSet;
        public Vector2 vector = Vector2.Zero;
        private int Bounce = 10;
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Wave");
        }
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 240;
            projectile.extraUpdates = 60;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        /*public override bool PreAI()
        {
            projectile.localAI[0]++;
            if (projectile.ai[1] == 1 || projectile.ai[1] == 2)
            {
                projectile.velocity = Vector2.Zero;
                projectile.Center = projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].Center : Main.player[(int)projectile.ai[0]].Center;
                if (projectile.localAI[0] > 4)
                {
                    projectile.Kill();
                }
                return false;
            }
            return base.PreAI();
        }*/
        public override void AI()
        {
            /*projectile.localNPCHitCooldown = 1;
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            float interpolate = projectile.MaxUpdates * 4;
            Vector2 diffPos = (projectile.Center + projectile.velocity * projectile.extraUpdates - new Vector2(projectile.oldPosition.X + projectile.width / 2, projectile.oldPosition.Y + projectile.height / 2)) / interpolate;
            Vector2 position;
            if (projectile.localAI[0] > 5)
            {
                for (int i = 0; i < interpolate; i++)
                {
                    position = new Vector2(projectile.oldPosition.X + projectile.width / 2, projectile.oldPosition.Y + projectile.height / 2) + diffPos * i;

                    Vector2 DustTopPos = position + new Vector2((float)(15 * Math.Sin(i * 3.141f / interpolate) * Math.Cos(projectile.rotation)), (float)(15 * Math.Sin(i * 3.141f / interpolate) * Math.Sin(projectile.rotation)));
                    Vector2 DustBotPos = position + new Vector2((float)(-15 * Math.Sin(i * 3.141f / interpolate) * Math.Cos(projectile.rotation)), (float)(-15 * Math.Sin(i * 3.141f / interpolate) * Math.Sin(projectile.rotation)));

                    int sineTop = Dust.NewDust(DustTopPos, 0, 0, 226, 0, 0, 100, default(Color), 0.75f);
                    Main.dust[sineTop].noGravity = true;
                    Main.dust[sineTop].velocity = new Vector2(0, 0);
                    int sineBot = Dust.NewDust(DustBotPos, 0, 0, 272, 0, 0, 100, default(Color), 0.75f);
                    Main.dust[sineBot].noGravity = true;
                    Main.dust[sineBot].velocity = new Vector2(0, 0);
                }
            }*/
            projectile.localAI[0]++;
            if (vector == Vector2.Zero)
            {
                vector = projectile.velocity;
            }
            if ((projectile.ai[0] == 1 || projectile.ai[0] == 2) && projectile.localAI[0] > 10)
            {
                int num = Dust.NewDust(new Vector2(projectile.Center.X - 1f, projectile.Center.Y - 1f) - projectile.velocity, 2, 2, projectile.ai[0] == 1 ? 272 : 226, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num].velocity *= 0f;
                Main.dust[num].noLight = false;
                Main.dust[num].noGravity = true;
                if (projectile.ai[1] == 1)
                {
                    offSet -= 0.14f;
                    if (offSet <= -1.0f)
                    {
                        offSet = -1.0f;
                        projectile.ai[1] = 2;
                    }
                }
                else
                {
                    offSet += 0.14f;
                    if (offSet >= 1.0f)
                    {
                        offSet = 1.0f;
                        projectile.ai[1] = 1;
                    }
                }
                float num2 = SinsUtility.RotationTo(projectile.Center, projectile.Center + vector);
                projectile.velocity = SinsUtility.RotateVector(default(Vector2), new Vector2(projectile.velocity.Length(), 0f), num2 + offSet * 0.5f);
            }
            projectile.rotation = SinsUtility.RotationTo(projectile.Center, projectile.Center + projectile.velocity) + 1.57f - 0.7853982f;
            projectile.spriteDirection = 1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 1 || projectile.ai[0] == 2)
            {
                int type = projectile.ai[0] == 1 ? mod.BuffType("AbyssalFlame") : BuffID.Frostburn;
                target.AddBuff(type, 180);
                target.AddBuff(BuffID.Frozen, 120);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.ai[0] == 1 || projectile.ai[0] == 2)
            {
                int type = projectile.ai[0] == 1 ? mod.BuffType("AbyssalFlame") : BuffID.Frostburn;
                target.AddBuff(type, 180);
                target.AddBuff(BuffID.Frozen, 120);
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.ai[0] == 1 || projectile.ai[0] == 2)
            {
                int type = projectile.ai[0] == 1 ? mod.BuffType("AbyssalFlame") : BuffID.Frostburn;
                target.AddBuff(type, 180);
                target.AddBuff(BuffID.Frozen, 120);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            /*if (projectile.ai[0] == 1 || projectile.ai[0] == 2)
            {
                Bounce--;
                if (Bounce <= 0)
                {
                    projectile.Kill();
                }
                else
                {
                    if (projectile.velocity.X != oldVelocity.X)
                    {
                        projectile.velocity.X = -oldVelocity.X;
                    }
                    if (projectile.velocity.Y != oldVelocity.Y)
                    {
                        projectile.velocity.Y = -oldVelocity.Y;
                    }
                }
                return false;
            }*/
            return base.OnTileCollide(oldVelocity);
        }
    }
}