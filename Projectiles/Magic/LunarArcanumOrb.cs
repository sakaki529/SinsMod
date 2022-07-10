using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class LunarArcanumOrb : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Arcanum");
            Main.projFrames[projectile.type] = 9;
        }
		public override void SetDefaults()
		{
            projectile.width = 24;
            projectile.height = 26;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.alpha = 125;
            projectile.tileCollide = false;
            projectile.light = 0.4f;
            projectile.timeLeft = 2;
            projectile.ignoreWater = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Texture2D glow = mod.GetTexture("Glow/Projectile/LunarArcanumOrb_Glow");
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y = num * projectile.frame;
            Main.spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, y, tex.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(tex.Width / 2f, (float)num / 2f), projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(glow, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, y, tex.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(tex.Width / 2f, (float)num / 2f), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Lighting.AddLight(projectile.Center, 0.15f, 0.35f, 0.15f);
            if (player.GetModPlayer<SinsPlayer>().LunarArcanumOrb)
            {
                projectile.timeLeft = 2;
            }
            projectile.position.Y = Main.player[projectile.owner].Center.Y - 12f;
            if (Main.player[projectile.owner].direction == -1)
            {
                projectile.position.X = Main.player[projectile.owner].Center.X - 12f - 28f;
                return;
            }
            projectile.position.X = Main.player[projectile.owner].Center.X - 12f + 28f;
        }
        public override void PostAI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 9)
            {
                projectile.frame = 0;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * 0.7f;
        }
    }
}