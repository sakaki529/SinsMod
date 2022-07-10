using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Typeless
{
    public class AndromedaCounter : ModProjectile
	{
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Explosion");
		}
		public override void SetDefaults()
		{
			projectile.width = 160;
			projectile.height = 160;
			projectile.melee = true;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.hide = true;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            crit = false;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            crit = false;
        }
        public override void Kill(int timeLeft)
        {
            projectile.penetrate = 1;
            projectile.Damage();
            Main.PlaySound(SoundID.Item14, projectile.position);
            int num;
            for (int num2 = 0; num2 < 4; num2 = num + 1)
            {
                int num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num3].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                num = num2;
            }
            for (int num4 = 0; num4 < 30; num4 = num + 1)
            {
                int num5 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 172, 0f, 0f, 200, default(Color), 3.7f);
                Main.dust[num5].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                Main.dust[num5].noGravity = true;
                Dust dust = Main.dust[num5];
                dust.velocity *= 3f;
                Main.dust[num5].shader = GameShaders.Armor.GetSecondaryShader(Main.player[projectile.owner].ArmorSetDye(), Main.player[projectile.owner]);
                num5 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 172, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num5].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                dust = Main.dust[num5];
                dust.velocity *= 2f;
                Main.dust[num5].noGravity = true;
                Main.dust[num5].fadeIn = 2.5f;
                Main.dust[num5].shader = GameShaders.Armor.GetSecondaryShader(Main.player[projectile.owner].ArmorSetDye(), Main.player[projectile.owner]);
                num = num4;
            }
            for (int num6 = 0; num6 < 10; num6 = num + 1)
            {
                int num7 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 172, 0f, 0f, 0, default(Color), 2.7f);
                Main.dust[num7].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation(), default(Vector2)) * projectile.width / 2f;
                Main.dust[num7].noGravity = true;
                Dust dust = Main.dust[num7];
                dust.velocity *= 3f;
                Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Main.player[projectile.owner].ArmorSetDye(), Main.player[projectile.owner]);
                num = num6;
            }
            for (int num8 = 0; num8 < 10; num8 = num + 1)
            {
                int num9 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num9].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation(), default(Vector2)) * projectile.width / 2f;
                Main.dust[num9].noGravity = true;
                Dust dust = Main.dust[num9];
                dust.velocity *= 3f;
                num = num8;
            }
            for (int num10 = 0; num10 < 2; num10 = num + 1)
            {
                int num11 = Gore.NewGore(projectile.position + new Vector2(projectile.width * Main.rand.Next(100) / 100f, projectile.height * Main.rand.Next(100) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num11].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                Gore gore = Main.gore[num11];
                gore.velocity *= 0.3f;
                Gore gore2 = Main.gore[num11];
                gore2.velocity.X = gore2.velocity.X + Main.rand.Next(-10, 11) * 0.05f;
                Gore gore3 = Main.gore[num11];
                gore3.velocity.Y = gore3.velocity.Y + Main.rand.Next(-10, 11) * 0.05f;
                num = num10;
            }
        }
    }
}