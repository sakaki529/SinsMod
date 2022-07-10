using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Pets
{
    public class CleyeraLightPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shining Cleyera");
			Main.projFrames[projectile.type] = 4;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.LightPet[projectile.type] = true;
		}
		public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.SuspiciousTentacle);
            aiType = ProjectileID.SuspiciousTentacle;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.suspiciouslookingTentacle = false;
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (!player.active)
            {
                projectile.active = false;
                return;
            }
            if (player.dead)
            {
                modPlayer.CleyeraLightPet = false;
            }
            if (modPlayer.CleyeraLightPet)
            {
                projectile.timeLeft = 2;
            }

        }
	}
}