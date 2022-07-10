using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class FlamingKnife : ModProjectile
	{
        public override string Texture => "SinsMod/Items/Weapons/Thrown/FlamingKnife";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Flaming Knife");
		}
		public override void SetDefaults()
		{
            projectile.width = 10;
            projectile.height = 10;
            projectile.thrown = true;
            projectile.aiStyle = 2;
            aiType = 48;
            projectile.penetrate = 2;
            projectile.timeLeft = 600;
            projectile.friendly = true;
            projectile.noDropItem = false;
        }
        public override void AI()
        {
            int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X, projectile.velocity.Y, 200);
            Main.dust[num].velocity *= 0.4f;
            Main.dust[num].scale = 0.5f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 300);
        }
        public override void OnHitPvp(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 300);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            for (int num = 0; num < 10; num++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 1, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.75f);
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 1.0f);
            }
            if (!projectile.noDropItem && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("FlamingKnife"), 1, false, 0, false, false);
            }
        }
    }
}