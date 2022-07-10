using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class PlanteraSpore : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spore");
		}
		public override void SetDefaults()
		{
            projectile.width = 18;
			projectile.height = 22;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.penetrate = 1;
            projectile.timeLeft = 150;
            projectile.tileCollide = true;
			projectile.ignoreWater = false;
            projectile.noEnchantments = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            projectile.alpha = 255;
			projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0f)
            {
                float num = 500f;
                int num2 = -1;
                int num3;
                for (int num4 = 0; num4 < Main.maxNPCs; num4 = num3 + 1)
                {
                    NPC nPC = Main.npc[num4];
                    if (nPC.CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
                    {
                        float num5 = (nPC.Center - projectile.Center).Length();
                        if (num5 < num)
                        {
                            num2 = num4;
                            num = num5;
                        }
                    }
                    num3 = num4;
                }
                projectile.ai[0] = num2 + 1;
                if (projectile.ai[0] == 0f)
                {
                    projectile.ai[0] = -15f;
                }
                if (projectile.ai[0] > 0f)
                {
                    float scaleFactor = Main.rand.Next(35, 75) / 30f;
                    projectile.velocity = (projectile.velocity * 20f + Vector2.Normalize(Main.npc[(int)projectile.ai[0] - 1].Center - projectile.Center + new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101))) * scaleFactor) / 21f;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                if (projectile.ai[0] > 0f)
                {
                    Vector2 value = Vector2.Normalize(Main.npc[(int)projectile.ai[0] - 1].Center - projectile.Center);
                    projectile.velocity = (projectile.velocity * 40f + value * 12f) / 41f;
                }
                else
                {
                    float num6 = projectile.ai[0];
                    projectile.ai[0] = num6 + 1f;
                    projectile.alpha -= 25;
                    if (projectile.alpha < 0)
                    {
                        projectile.alpha = 0;
                    }
                    projectile.velocity *= 0.95f;
                }
            }
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = Main.rand.Next(80, 101) / 100f;
                projectile.netUpdate = true;
            }
            projectile.scale = projectile.ai[1];
            projectile.rotation = projectile.velocity.X * 0.2f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300);
            target.AddBuff(BuffID.Venom, 180);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300);
            target.AddBuff(BuffID.Venom, 180);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300);
            target.AddBuff(BuffID.Venom, 180);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                if (Main.rand.Next(3) != 0)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 166, projectile.direction, -1f, 0, default(Color), projectile.scale);
                }
                else
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 167, projectile.direction, -1f, 0, default(Color), projectile.scale);
                }
            }
        }
    }
}