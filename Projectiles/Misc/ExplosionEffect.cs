using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Misc
{
    public class ExplosionEffect : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 144;
            projectile.height = 144;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.scale = 1f;
            projectile.light = 0.7f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, null, projectile.GetAlpha(drawColor) * 0.33f, -projectile.rotation, new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f), projectile.scale * 2f, 0, 0f);
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, null, projectile.GetAlpha(drawColor), projectile.rotation, new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f), projectile.scale, 0, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
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
            if (projectile.ai[0] == 1)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Origin_purged").WithVolume(1.0f), projectile.Center);
                projectile.ai[0] = -1;
            }
            else if (projectile.ai[0] == 0)
            {
                Main.PlaySound(SoundID.Zombie, (int)projectile.Center.X, (int)projectile.Center.Y, 104, 1f, 0f);
                projectile.ai[0] = -1;
            }
            projectile.velocity = Vector2.Zero;
            projectile.scale += 0.25f;
            if (projectile.timeLeft <= 20)
            {
                projectile.alpha += 13;
            }
            if (Main.netMode != 1 && projectile.timeLeft > 15)
            {
                //Projectile.NewProjectileDirect(projectile.Center, Utils.RotatedByRandom(Vector2.One, 360.0) * (float)Main.rand.Next(3, 8), mod.ProjectileType(""), 0, 0.5f, Main.MyPlayer, 0f, 0f);
            }
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}