using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class StormSpell : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Spell");
        }
		public override void SetDefaults()
		{
            projectile.width = 1;
            projectile.height = 1;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 240;
        }
        public override void AI()
        {
            if (projectile.ai[0] == mod.ItemType("BookOfEibon"))
            {
                for (int num = 0; num < 5; num++)
                {
                    int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, 21, 0f, 0f, 0, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].alpha = 20;
                }
                return;
            }
            for (int num = 0; num < 20; num++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, 91, 0f, 0f, 0, new Color(255, 176, 0), 0.8f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].alpha = 20;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == mod.ItemType("BookOfEibon"))
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("StormOfEibon"), projectile.damage, projectile.knockBack, projectile.owner, 1f, projectile.ai[0]);
                return;
            }
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("Storm"), projectile.damage, projectile.knockBack, projectile.owner, 1f, 0f);
        }
    }
}