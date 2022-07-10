using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class TheFloatingHorror : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Floating Horror");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 360;
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
            projectile.localNPCHitCooldown = 4;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - i) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.05f * 1.5f, 0.15f * 1.5f, 0.4f * 1.5f);
            Vector2 vector = new Vector2(Main.rand.NextFloat(2f, 3.7f), Main.rand.NextFloat(2f, 3.7f)).RotatedByRandom(MathHelper.ToRadians(360));
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] >= 8f)
            {
                for (int j = 0; j < Main.rand.Next(1, 3); j++)
                {
                    int num = Projectile.NewProjectile(projectile.Center, vector, ProjectileID.ToxicBubble, projectile.damage, projectile.knockBack, projectile.owner);
                    Main.projectile[num].melee = true;
                    Main.projectile[num].ranged = false;
                }
                projectile.localAI[1] = 0f;
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