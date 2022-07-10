using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class GateOfBabylon : ModProjectile
    {
        private bool getTex;
        private int itemType;
        private static MethodInfo _hitNPCMethod;
        private static MethodInfo _hitPlayerHeadMethod;
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gate of Babylon");
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 240;
            projectile.alpha = 255;
            projectile.extraUpdates = 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (!getTex || itemType == 0)
            {
                itemType = SinsMod.swordList[Main.rand.Next(SinsMod.swordList.Count)];
                getTex = true;
                return false;
            }
            Texture2D texture = Main.itemTexture[itemType];
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(texture, new Vector2(projectile.position.X - Main.screenPosition.X + (projectile.width / 2), projectile.position.Y - Main.screenPosition.Y + (projectile.height / 2)), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width, 0f), projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 1f, 0f);
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 0.785f;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 75;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.localAI[0] == 0)
            {
                float num = 30f;
                int num2 = 0;
                while (num2 < num)
                {
                    Vector2 vector = Vector2.UnitX * 0f;
                    vector += -Vector2.UnitY.RotatedBy(num2 * (6.28318548f / num), default(Vector2)) * new Vector2(4f, 12f);
                    vector = vector.RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                    int num3 = Dust.NewDust(projectile.Center, 0, 0, 246, 0f, 0f, 0, default(Color), 1.2f);
                    Main.dust[num3].noGravity = true;
                    Main.dust[num3].position = projectile.Center + vector;
                    Main.dust[num3].velocity = projectile.velocity * 0f + vector.SafeNormalize(Vector2.UnitY) * 1f;
                    int num4 = num2;
                    num2 = num4 + 1;
                }
                projectile.localAI[0] = 1;
            }
            if (!getTex || itemType == 0)
            {
                return;
            }
            Item item = new Item();
            item.SetDefaults(itemType, false);
            projectile.knockBack = item.knockBack;
        }
        /*public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            try
            {
                _hitNPCMethod = Main.instance.GetType().GetMethod("StatusNPC", BindingFlags.NonPublic | BindingFlags.Instance);
                _hitNPCMethod.Invoke(Main.instance, new object[] { itemType, target.whoAmI });
            }
            catch { }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            try
            {
                _hitPlayerHeadMethod = Main.instance.GetType().GetMethod("StatusPvP", new Type[] { typeof(int), typeof(int) });
                _hitPlayerHeadMethod.Invoke(Main.instance, new object[] { itemType, target.whoAmI });
            }
            catch { }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            try
            {
                _hitPlayerHeadMethod = Main.instance.GetType().GetMethod("StatusPvP", new Type[] { typeof(int), typeof(int) });
                _hitPlayerHeadMethod.Invoke(Main.instance, new object[] { itemType, target.whoAmI });
            }
            catch { }
        }*/
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            SpawnProjectile();
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            SpawnProjectile();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SpawnProjectile();
            return base.OnTileCollide(oldVelocity);
        }
        private void SpawnProjectile()
        {
            projectile.Kill();
            Item item = new Item();
            item.SetDefaults(itemType, false);
            Main.PlaySound(item.UseSound, projectile.Center);
            for (int i = 0; i < 10; i++)
            {
                float num = projectile.oldVelocity.X * (5f / i);
                float num2 = projectile.oldVelocity.Y * (5f / i);
                int num3 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num, projectile.oldPosition.Y - num2), projectile.width, projectile.height, 246, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.2f);
                Main.dust[num3].noGravity = true;
                Dust dust = Main.dust[num3];
                dust.velocity *= 0.08f;
                num3 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num, projectile.oldPosition.Y - num2), projectile.width, projectile.height, 246, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.0f);
                dust = Main.dust[num3];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
            if (SinsMod.Instance.BluemagicLoaded)
            {
                if (item.type == ModLoader.GetMod("Bluemagic").ItemType("PhantomBlade"))
                {
                    return;
                }
            }
            if (item.shoot != 0)
            {
                int num = Main.rand.Next(1, 4);
                double num2 = Main.rand.NextDouble();
                double num3 = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - (num2 / 2f);
                double num4 = num2 / num;
                for (int i = 0; i < num; i++)
                {
                    double num5 = num3 + num4 * (i + i * i) / 2.0 + (32f * i);
                    int num6 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(num5) * item.shootSpeed), (float)(Math.Cos(num5) * item.shootSpeed), item.shoot, (int)((double)projectile.damage * 0.5f), projectile.knockBack, projectile.owner, 0f, 0f);
                    Main.projectile[num6].melee = false;
                    Main.projectile[num6].magic = true;
                    num6 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(num5) * item.shootSpeed), (float)(-Math.Cos(num5) * item.shootSpeed), item.shoot, (int)((double)projectile.damage * 0.5f), projectile.knockBack, projectile.owner, 0f, 0f);
                    Main.projectile[num6].melee = false;
                    Main.projectile[num6].magic = true;
                }
            }
        }
    }
}