using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class IcicleBeam : ModProjectile
    {
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Icicle Beam");
        }
		public override void SetDefaults()
		{
            projectile.width = 20;
			projectile.height = 20;
            projectile.friendly = true;
			projectile.melee = true;
			projectile.timeLeft = 300;
			projectile.penetrate = 6;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.alpha = 255;
            projectile.light = 0.5f;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = projectile.velocity.X < 0f ? mod.GetTexture("Extra/Projectile/IcicleBeam_Alt") : Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, texture.Size() / 2f, projectile.scale, 0, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 0.785f;
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item8, projectile.Center);
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.scale -= 0.02f;
                projectile.alpha += 30;
                if (projectile.alpha >= 250)
                {
                    projectile.alpha = 255;
                    projectile.localAI[0] = 1f;
                }
            }
            else if (projectile.localAI[0] == 1f)
            {
                projectile.scale += 0.02f;
                projectile.alpha -= 30;
                if (projectile.alpha <= 0)
                {
                    projectile.alpha = 0;
                    projectile.localAI[0] = 0f;
                }
            }
            /*if (this.type == 157)
            {
                this.rotation += (float)this.direction * 0.4f;
                this.spriteDirection = this.direction;
            }*/
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(BuffID.Frostburn, 300);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Frostburn, 300);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Frostburn, 300);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
		public override void Kill(int timeleft)
		{
			for (int num = 0; num < 25; num++)
            {
                float num2 = projectile.oldVelocity.X * (5f / num);
                float num3 = projectile.oldVelocity.Y * (5f / num);
                int num4 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num2, projectile.oldPosition.Y - num3), projectile.width, projectile.height, 92, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.4f);
                Main.dust[num4].noGravity = true;
                Dust dust = Main.dust[num4];
                dust.velocity *= 0.08f;
                num4 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num2, projectile.oldPosition.Y - num3), projectile.width, projectile.height, 92, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.2f);
                dust = Main.dust[num4];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 50);
        }
    }
}