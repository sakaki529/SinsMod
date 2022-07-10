using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class TheBloodyTongue : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Bloody Tongue");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 480;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 17.0f;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.melee = true;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            //projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }
        public override void AI()
        {
            Vector2 vector = Vector2.Zero;
            bool flag = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false))
                {
                    if (Vector2.Distance(Main.npc[i].Center, projectile.Center) < 180f)
                    {
                        flag = true;
                        Vector2 vector2 = Main.npc[i].Center - projectile.Center;
                        vector2.Normalize();
                        vector2 *= Main.rand.NextFloat(5f, 7f);
                        vector = vector2;
                        break;
                    }
                    else
                    {
                        flag = false;
                        vector = Vector2.Zero;
                    }
                }
            }
            if (flag)
            {
                projectile.localAI[1] += 1f;
                if (projectile.localAI[1] >= 12f)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        float num = Main.rand.Next(10, 50) * 0.001f;
                        if (Main.rand.Next(2) == 0)
                        {
                            num *= -1f;
                        }
                        float num2 = Main.rand.Next(10, 50) * 0.001f;
                        if (Main.rand.Next(2) == 0)
                        {
                            num2 *= -1f;
                        }
                        Projectile.NewProjectile(projectile.Center, vector, mod.ProjectileType("BloodyTongue"), projectile.damage, projectile.knockBack, projectile.owner, num, num2);
                    }
                    projectile.localAI[1] = 0f;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}