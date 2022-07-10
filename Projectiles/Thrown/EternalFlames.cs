using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class EternalFlames : ModProjectile
	{
        private bool collide;
        public override string Texture => "SinsMod/Items/Weapons/Thrown/EternalFlames";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Eternal Flames");
        }
		public override void SetDefaults()
		{
            projectile.width = 32;
			projectile.height = 32;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = Utils.NextFloat(Main.rand, 0.2f, 0.5f) * projectile.velocity;
            Main.dust[dust].scale = 0.9f + Utils.NextFloat(Main.rand, 0.3f);
            projectile.alpha -= 20;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 8;
                Main.PlaySound(SoundID.Item7, projectile.position);
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[1] += 1f;
                if (projectile.localAI[1] >= 10f || projectile.numHits >= 5 || collide)
                {
                    projectile.localAI[0] = 1f;
                    projectile.localAI[1] = 0f;
                    projectile.netUpdate = true;
                    projectile.tileCollide = false;
                }
            }
            else
            {
                float num = 22f;
                float num2 = 2f;
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num3 = Main.player[projectile.owner].position.X + (player.width / 2) - vector.X;
                float num4 = Main.player[projectile.owner].position.Y + (player.height / 2) - vector.Y;
                float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                if (num5 > 2000f)
                {
                    projectile.Kill();
                }
                num5 = num / num5;
                num3 *= num5;
                num4 *= num5;
                if (projectile.velocity.X < num3)
                {
                    projectile.velocity.X = projectile.velocity.X + num2;
                    if (projectile.velocity.X < 0f && num3 > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num2;
                    }
                }
                else
                {
                    if (projectile.velocity.X > num3)
                    {
                        projectile.velocity.X = projectile.velocity.X - num2;
                        if (projectile.velocity.X > 0f && num3 < 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X - num2;
                        }
                    }
                }
                if (projectile.velocity.Y < num4)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num2;
                    if (projectile.velocity.Y < 0f && num4 > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num2;
                    }
                }
                else
                {
                    if (projectile.velocity.Y > num4)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num2;
                        if (projectile.velocity.Y > 0f && num4 < 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y - num2;
                        }
                    }
                }
                if (Main.myPlayer == projectile.owner && projectile.Hitbox.Intersects(player.Hitbox))
                {
                    projectile.Kill();
                }
            }
            float dir = (projectile.direction <= 0) ? -1f : 1f;
            projectile.rotation += dir * (0.4f / (projectile.extraUpdates + 1));
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            collide = true;
            return false;
        }
    }
}