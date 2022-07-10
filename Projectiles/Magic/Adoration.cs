using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class Adoration : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Adoration");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
		public override void SetDefaults()
		{
            projectile.width = 14;
			projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.magic = true;
			projectile.penetrate = 1;
            projectile.tileCollide = true;
			projectile.ignoreWater = false;
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
        public override void AI()
        {
            projectile.velocity.Y += 0.6f;
            int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 33, projectile.velocity.X, projectile.velocity.Y, 0, new Color(255, 0, 226), 1f);
            Main.dust[num].velocity *= 0.1f;
            Main.dust[num].scale = 0.8f;
            Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(45, Main.LocalPlayer);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type != NPCID.TargetDummy && !target.boss && !SinsNPC.BossActiveCheck())
            {
                target.life = 0;
                target.active = false;
                NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, NPCID.Pinky, 0, 0f, 0f, 0f, 0f, 255);
            }
        }
        public override void Kill(int timeleft)
		{
            Main.PlaySound(SoundID.NPCHit13, (int)projectile.position.X, (int)projectile.position.Y);
            for (int num = 0; num < 3; num++)
			{
				int num2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 33, projectile.velocity.X, projectile.velocity.Y, 0, new Color(255, 0, 226), 1f);
                Main.dust[num2].velocity *= -0.1f;
				Main.dust[num2].velocity -= projectile.oldVelocity * 0.1f;
                Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(45, Main.LocalPlayer);
            }
		}
    }
}