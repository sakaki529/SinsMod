using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Misc
{
    public class RingEffect : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 10;
            projectile.scale = 0.2f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            if (projectile.ai[0] == 1f)
            {
                texture = mod.GetTexture("Extra/Projectile/RingSolar");
            }
            else if (projectile.ai[0] == 2f)
            {
                texture = mod.GetTexture("Extra/Projectile/RingVortex");
            }
            else if (projectile.ai[0] == 3f)
            {
                texture = mod.GetTexture("Extra/Projectile/RingNebula");
            }
            else if (projectile.ai[0] == 4f)
            {
                texture = mod.GetTexture("Extra/Projectile/RingQuasar");
            }
            else if (projectile.ai[0] == 5f)
            {
                texture = mod.GetTexture("Extra/Projectile/RingStardust");
            }
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(Color.White), projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), projectile.scale, SpriteEffects.None, 0f);
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
            projectile.velocity = Vector2.Zero;
            projectile.alpha += 7;
            projectile.scale *= 1.2f;
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
            if (projectile.localAI[0] == 0)
            {
                if (projectile.ai[0] >= 1 && projectile.ai[0] <= 5)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/ChangeResist").WithVolume(0.7f), projectile.position);
                }
                projectile.localAI[0] = 1;
            }
            return false;
        }
    }
}