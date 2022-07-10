using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Pets
{
    public class CleyeraPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cleyera");
			Main.projFrames[projectile.type] = 4;
			Main.projPet[projectile.type] = true;
		}
		public override void SetDefaults()
		{
            projectile.width = 26;
            projectile.height = 26;
            //projectile.aiStyle = 26;//380
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
			if (player.dead)
			{
				modPlayer.CleyeraPet = false;
			}
			if (modPlayer.CleyeraPet)
			{
				projectile.timeLeft = 2;
			}
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            /*float num = projectile.Center.X - player.Center.X;
            float num2 = projectile.Center.Y - player.Center.Y;
            if ((float)Math.Sqrt(num * num + num2 * num2) > 480f)
            {
                Vector2 vector = player.Center - projectile.Center;
                vector.Normalize();
                projectile.velocity = vector * 16f;
            }*/
            if (projectile.velocity.X < 1f && projectile.velocity.X > -1f)
            {
                projectile.spriteDirection = -player.direction;
            }
            else
            {
                projectile.spriteDirection = -projectile.direction;
            }
            if (player.direction == -1)
            {
                projectile.velocity = (player.Center - new Vector2(-30, 50) - projectile.Center) / 7;
                projectile.tileCollide = false;
            }
            else
            {
                projectile.velocity = (player.Center - new Vector2(30, 50) - projectile.Center) / 7;
                projectile.tileCollide = false;
            }
            projectile.rotation = projectile.velocity.X * 0.01f;
            float num = projectile.position.X - player.Center.X;
            float num2 = projectile.position.Y - player.Center.Y;
            float distance = (float)Math.Sqrt(num * num + num2 * num2);
            /*if (distance > 3200)
            {
                projectile.position = player.Center;
            }*/
            projectile.netUpdate = true;
        }
	}
}