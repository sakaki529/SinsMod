using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class TartarusMinionBody2 : ModProjectile
	{
        public override string Texture => "SinsMod/NPCs/Boss/TartarusGuardian/TartarusBody";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Guardian");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 30;
            projectile.height = 30;
            projectile.minion = true;
            projectile.minionSlots = 0.5f;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.alpha = 255;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, texture.Size() / 2f, projectile.scale, 0, 0f);
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
                modPlayer.TartarusMinion = false;
            }
            if (!player.active)
            {
                projectile.active = false;
                return;
            }
            if (modPlayer.TartarusMinion)
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
                float num4 = 26f;
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
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            if (player.slotsMinions + projectile.minionSlots > player.maxMinions && projectile.owner == Main.myPlayer)
            {
                int byUUID = Projectile.GetByUUID(projectile.owner, projectile.ai[0]);
                if (byUUID != -1)
                {
                    Projectile projectile = Main.projectile[byUUID];
                    if (projectile.type != mod.ProjectileType("TartarusMinionHead"))
                    {
                        projectile.localAI[1] = base.projectile.localAI[1];
                    }
                    projectile = Main.projectile[(int)base.projectile.localAI[1]];
                    projectile.ai[0] = base.projectile.ai[0];
                    projectile.ai[1] = 1f;
                    projectile.netUpdate = true;
                }
            }
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 50;
            projectile.height = 50;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            for (int i = 0; i < 20; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, 0f, 0f, 50, default(Color), 1.2f);
                Main.dust[num].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale = 0.5f;
                    Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
        }
    }
}