using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Pets
{
    public class KobyPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Koby");
			Main.projFrames[projectile.type] = 12;
			Main.projPet[projectile.type] = true;
		}
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Truffle);
			aiType = ProjectileID.Truffle;
            projectile.width = 20;
            projectile.height = 36;
		}
		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.truffle = false;
			return true;
		}
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
			if (player.dead)
			{
				modPlayer.KobyPet = false;
			}
			if (modPlayer.KobyPet)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}