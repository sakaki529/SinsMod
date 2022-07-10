using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class PhantasmDragonTail : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantasm Dragon");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 30;
            projectile.height = 30;
            projectile.minion = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.alpha = 255;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, texture.Size() / 2f, projectile.scale, (projectile.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (Main.time % 120 == 0)
            {
                projectile.netUpdate = true;
            }
            if (player.dead)
            {
                modPlayer.PhantasmDragonMinion = false;
            }
            if (!player.active)
            {
                projectile.active = false;
                return;
            }
            if (modPlayer.PhantasmDragonMinion)
            {
                projectile.timeLeft = 2;
            }
            int num = 10;
            Vector2 vector = Vector2.Zero;
            if (projectile.ai[1] == 1f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            int num2 = (int)projectile.ai[0];
            if (num2 >= 0 && Main.projectile[num2].active)
            {
                Main.projectile[num2].damage = projectile.damage;
                vector = Main.projectile[num2].Center;
                Vector2 vel = Main.projectile[num2].velocity;
                float rotation = Main.projectile[num2].rotation;
                float num3 = MathHelper.Clamp(Main.projectile[num2].scale, 0f, 50f);
                float num4 = 54f;
                Main.projectile[num2].localAI[0] = projectile.localAI[0] + 1f;
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                projectile.velocity = Vector2.Zero;
                Vector2 vector2 = vector - projectile.Center;
                if (rotation != projectile.rotation)
                {
                    float num5 = MathHelper.WrapAngle(rotation - projectile.rotation);
                    vector2 = vector2.RotatedBy(num5 * 0.1f, default(Vector2));
                }
                projectile.rotation = vector2.ToRotation() + 1.57079637f;
                projectile.position = projectile.Center;
                //projectile.scale = num3;
                //projectile.width = projectile.height = (int)(num * projectile.scale);
                projectile.Center = projectile.position;
                if (vector2 != Vector2.Zero)
                {
                    projectile.Center = vector - Vector2.Normalize(vector2) * num4 * num3;
                }
                projectile.spriteDirection = ((vector2.X > 0f) ? 1 : -1);
                return;
            }
            projectile.Kill();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
        }
        public override void Kill(int timeLeft)
        {
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 50;
            projectile.height = 50;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            for (int i = 0; i < 10; i++)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 16, 0f, 0f, 0, default(Color), 1.5f);
                Dust dust = Main.dust[d];
                dust.velocity *= 2f;
                Main.dust[d].noGravity = true;
            }
            int num = Main.rand.Next(1, 4);
            Vector2 vector = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
            for (int i = 0; i < num; i++)
            {
                int g = Gore.NewGore(projectile.position - vector * 5f, Vector2.Zero, Main.rand.Next(11, 14), projectile.scale);
                Gore gore = Main.gore[g];
                gore.velocity *= 0.8f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            int num = lightColor.A - projectile.alpha;
            lightColor = Color.Lerp(lightColor, Color.White, 0.4f);
            lightColor.A = 150;
            lightColor *= num / 255f;
            return lightColor;
        }
    }
}