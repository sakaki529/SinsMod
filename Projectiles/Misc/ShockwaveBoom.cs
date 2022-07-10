using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Misc
{
    public class ShockwaveBoom : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.alpha = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 200;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool PreAI()
        {
            float progress = (180f - projectile.timeLeft) / 60f;
            float pulseCount = 1;
            float rippleSize = 1;
            float speed = 50;//20
            if (projectile.ai[0] > 0)
            {
                rippleSize = projectile.ai[0];
            }
            if (projectile.ai[1] > 0)
            {
                speed = projectile.ai[1];
            }
            Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(100f * (1 - progress / 3f));
            projectile.localAI[1]++;
            if (projectile.localAI[1] >= 0 && projectile.localAI[1] <= 60)
            {
                if (!Filters.Scene["Shockwave"].IsActive())
                {
                    Filters.Scene.Activate("Shockwave", projectile.Center).GetShader().UseColor(pulseCount, rippleSize, speed).UseTargetPosition(projectile.Center);
                }
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Filters.Scene["Shockwave"].Deactivate();
        }
    }
}