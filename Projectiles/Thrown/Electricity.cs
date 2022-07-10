using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class Electricity : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mjolnir");
		}
        public override void SetDefaults()
       	{
            projectile.width = 8;
            projectile.height = 8;
            projectile.thrown = true;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 3;
            projectile.penetrate = 10;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override void AI()
        {
            projectile.velocity = Vector2.Zero;
            projectile.localAI[0] += 0.01f;
            projectile.scale = projectile.localAI[0];
            int num = 0;
            if (num == 0)
            {
                Main.PlaySound(SoundID.Item14, projectile.position);
            }
            num++;
            Lighting.AddLight(projectile.Center, 0.9f, 0.8f, 0.6f);
            projectile.width = projectile.height = (int)(52f * projectile.scale);
            projectile.Damage();
            for (int num2 = 0; num2 < 4; num2 = num + 1)
            {
                int num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num3].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                num = num2;
            }
            for (int num4 = 0; num4 < 10; num4 = num + 1)
            {
                int num5 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 222, 0f, 0f, 200, default(Color), 0.7f);
                Main.dust[num5].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                Main.dust[num5].noGravity = false;
                Dust dust = Main.dust[num5];
                dust.velocity *= 2f;
                num5 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 222, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num5].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                dust = Main.dust[num5];
                dust.velocity *= 1.5f;
                Main.dust[num5].noGravity = false;
                num = num4;
            }
            for (int num6 = 0; num6 < 10; num6 = num + 1)
            {
                int num7 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num7].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation(), default(Vector2)) * projectile.width / 2f;
                Main.dust[num7].noGravity = true;
                Dust dust = Main.dust[num7];
                dust.velocity *= 3f;
                num = num6;
            }
            if (projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].active && Main.npc[(int)projectile.ai[0]].life > 0 : Main.player[(int)projectile.ai[0]].active && !Main.player[(int)projectile.ai[0]].dead)
            {
                projectile.Center = projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].Center : Main.player[(int)projectile.ai[0]].Center;
                if (projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].life <= 0 || !Main.npc[(int)projectile.ai[0]].active : Main.player[(int)projectile.ai[0]].dead || !Main.player[(int)projectile.ai[0]].active)
                {
                    projectile.ai[1] = -1;
                }
                return;
            }
            projectile.ai[1] = -1;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
	}
}