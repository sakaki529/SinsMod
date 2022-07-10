using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class CherryBlossom : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cherry Blossom");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
		public override void SetDefaults()
		{
            projectile.width = 22;
			projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.timeLeft = 60;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 100;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor * 0.35f) * ((projectile.oldPos.Length - k) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, projectile.velocity.X < 0f ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation += 10.0f;
            if (projectile.ai[0] == 1f)
            {
                projectile.ignoreWater = true;
                projectile.tileCollide = false;
                int num7 = 5 * projectile.MaxUpdates;
                bool flag = false;
                projectile.localAI[0] += 1f;
                int num8 = (int)projectile.ai[1];
                if (projectile.localAI[0] >= 60 * num7)
                {
                    flag = true;
                }
                else
                {
                    if (num8 < 0 || num8 >= 200)
                    {
                        flag = true;
                    }
                    else
                    {
                        if (Main.npc[num8].active && !Main.npc[num8].dontTakeDamage)
                        {
                            projectile.Center = Main.npc[num8].Center - projectile.velocity * 2f;
                            projectile.gfxOffY = Main.npc[num8].gfxOffY;
                            if (projectile.localAI[0] % 30f == 0f)
                            {
                                Main.npc[num8].HitEffect(0, 1.0);
                            }
                            if (!Main.npc[num8].active || Main.npc[num8].life <= 0)
                            {
                                projectile.ai[0] = 0f;
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    projectile.ai[0] = 0f;
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
            projectile.ai[0] = 1f;
            projectile.ai[1] = target.whoAmI;
            if (target.life < 0)
            {
                projectile.ai[1] = -1f;
            }
            projectile.velocity = (target.Center - projectile.Center) * 0.75f;
            projectile.netUpdate = true;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage *= 4;
            damage += target.statDefense / 2;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage *= 4;
            damage += target.statDefense / 2;
        }
        public override void Kill(int timeleft)
		{
			for (int num = 0; num < 3; num++)
			{
				int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 72, 0f, 0f, 100, default(Color), 1.0f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 1.2f;
				Main.dust[num2].velocity -= projectile.oldVelocity * 0.3f;
			}
            Vector2 vel1 = new Vector2(0, 0);
            vel1 *= 0f;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}