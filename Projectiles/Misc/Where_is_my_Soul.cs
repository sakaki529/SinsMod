using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Misc
{
    public class Where_is_my_Soul : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
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
            projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, (int)projectile.ai[0], 0, 0f, 0f, 0f, 0f, 255);
        }
    }
}