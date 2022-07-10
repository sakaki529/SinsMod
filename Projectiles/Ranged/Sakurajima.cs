using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class Sakurajima : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sakurajima");
            DisplayName.AddTranslation(GameCulture.Chinese, "����");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
            projectile.width = 8;
			projectile.height = 8;
            projectile.aiStyle = 1;
            aiType = 14;
            projectile.friendly = true;
			projectile.melee = false;
            projectile.ranged = true;
			projectile.timeLeft = 300;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            projectile.alpha = 100;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor * 0.35f) * ((float)(projectile.oldPos.Length - i) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, projectile.velocity.X < 0f ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("Sakurajima2"), projectile.damage, 0, projectile.owner, target.whoAmI, 0);
            if (!Main.player[projectile.owner].moonLeech)
			{
				if (target.type != 488)
				{
                    float num = damage * 0.0005f;
                    if ((int)num == 0)
                    {
                        return;
                    }
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SakuraHeal"), 0, 0f, projectile.owner, projectile.owner, num);
                }
			}
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("Sakurajima2"), projectile.damage, 0, projectile.owner, target.whoAmI, 1);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("Sakurajima2"), projectile.damage, 0, projectile.owner, target.whoAmI, 1);
            if (!Main.player[projectile.owner].moonLeech)
            {
                float num = damage * 0.0005f;
                if ((int)num == 0)
                {
                    return;
                }
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SakuraHeal"), 0, 0f, projectile.owner, projectile.owner, num);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 72, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 0.8f);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 0.4f;
            Main.dust[d].scale = 0.8f;
        }
		public override void Kill(int timeleft)
		{
			for (int num = 0; num < 3; num++)
			{
				int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 72, 0f, 0f, 100, default(Color), 0.8f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 1.2f;
				Main.dust[num2].velocity -= projectile.oldVelocity * 0.3f;
			}
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}