using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class PlanteraFlail : ModProjectile
	{
        private int count;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flail");
		}
		public override void SetDefaults()
		{
            projectile.width = 26;
			projectile.height = 26;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.alpha = 255;
            projectile.scale = 1.2f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = mod.GetTexture("Extra/Projectile/PlanteraFlail_Chain");
            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num = texture.Height;
            Vector2 vector = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector.Y, vector.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
            {
                flag = false;

            }
            if (float.IsNaN(vector.X) && float.IsNaN(vector.Y))
            {
                flag = false;
            }
            while (flag)
            {
                if (vector.Length() < num + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2 = vector;
                    vector2.Normalize();
                    position += vector2 * num;
                    vector = mountedCenter - position;
                    Color color = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color = projectile.GetAlpha(color);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, null, color, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            texture = Main.projectileTexture[projectile.type];
            SpriteEffects spriteEffects = SpriteEffects.None;
            int num2 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num3 = num2 * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, num3, texture.Width, num2), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num2 / 2f), projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Vector2 vector = Main.player[projectile.owner].Center - projectile.Center;
            projectile.rotation = vector.ToRotation() - 1.57f;
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }
            Main.player[projectile.owner].itemAnimation = 6;
            Main.player[projectile.owner].itemTime = 6;
            if (vector.X < 0f)
            {
                Main.player[projectile.owner].ChangeDir(1);
                projectile.direction = 1;
            }
            else
            {
                Main.player[projectile.owner].ChangeDir(-1);
                projectile.direction = -1;
            }
            Main.player[projectile.owner].itemRotation = (vector * -1f * projectile.direction).ToRotation();
            projectile.spriteDirection = ((vector.X > 0f) ? -1 : 1);
            if (projectile.ai[0] == 0f && vector.Length() > 400f)
            {
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] == 1f || projectile.ai[0] == 2f)
            {
                float num = vector.Length();
                if (num > 1500f)
                {
                    projectile.Kill();
                    return;
                }
                if (num > 600f)
                {
                    projectile.ai[0] = 2f;
                }
                projectile.tileCollide = false;
                float num2 = 24f;
                if (projectile.ai[0] == 2f)
                {
                    num2 = 40f;
                }
                projectile.velocity = Vector2.Normalize(vector) * num2;
                if (vector.Length() < num2)
                {
                    projectile.Kill();
                    return;
                }
            }
            float num3 = projectile.ai[1];
            projectile.ai[1] = num3 + 1f;
            if (projectile.ai[1] > 0f)//5f
            {
                projectile.alpha = 0;
            }
            if ((int)projectile.ai[1] % 4 == 0 && projectile.owner == Main.myPlayer)
            {
                Vector2 vector2 = vector * -1f;
                vector2.Normalize();
                vector2 *= Main.rand.Next(45, 65) * 0.1f;
                vector2 = vector2.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866, default(Vector2));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector2.X, vector2.Y, mod.ProjectileType("PlanteraSpore"), projectile.damage, projectile.knockBack, projectile.owner, -10f, 0f);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            projectile.ai[0] = 1f;
            target.AddBuff(BuffID.Poisoned, 300);
            target.AddBuff(BuffID.Venom, 180);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.ai[0] = 1f;
            target.AddBuff(BuffID.Poisoned, 300);
            target.AddBuff(BuffID.Venom, 180);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            projectile.ai[0] = 1f;
            target.AddBuff(BuffID.Poisoned, 300);
            target.AddBuff(BuffID.Venom, 180);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] = 1f;
            projectile.netUpdate = true;
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            return false;
        }
    }
}