using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Typeless
{
    public class Nothingness : ModProjectile
	{
		private const int xRange = 600;
		private const int yRange = 320;
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nothingness");
		}
		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
        public override bool CanDamage()
        {
            return false;
        }
        public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (Main.myPlayer == projectile.owner)
			{
				if (!player.channel || player.noItems || player.CCed)
				{
					projectile.Kill();
				}
			}
			projectile.Center = player.MountedCenter;
			projectile.timeLeft = 2;
			player.itemTime = 2;
			player.itemAnimation = 2;

			projectile.ai[0] += 1f;
			projectile.damage = player.statLife * 10;
			if (projectile.ai[0] >= 1f && Main.myPlayer == projectile.owner)
			{
                int useMana = projectile.damage / 200;
                if (player.statMana < useMana && player.manaFlower)
				{
					player.QuickMana();
				}
				if (player.statMana >= useMana)
				{
					player.statMana -= useMana;
				}
				else
				{
					projectile.Kill();
				}
			}

			if (projectile.localAI[1] == 0f)
			{
				projectile.localAI[1] = 39f;
				projectile.localAI[0] = Main.rand.Next(xRange);
			}
			projectile.localAI[0] = Next(projectile.localAI[0]);
		}
		private float Next(float seed)
		{
			return (seed * projectile.localAI[1] + 97f) % (4 * xRange * yRange);
		}
		/*public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			hitbox.X -= xRange;
			hitbox.Width += 2 * xRange;
			hitbox.Y -= yRange;
			hitbox.Height += 2 * yRange;
		}*/
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.penetrate++;
		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage += target.defense / 2;
		}
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.penetrate++;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            projectile.penetrate++;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawCenter = projectile.Center - Main.screenPosition;
			Texture2D texture = mod.GetTexture("Extra/Placeholder/BlankTex");
			float scale = projectile.ai[0] / 60f;
			if (scale > 1f)
			{
				scale = 1f;
			}
			spriteBatch.Draw(texture, drawCenter, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(1.3f * scale, 1.3f), SpriteEffects.None, 0f);
			texture = mod.GetTexture("Extra/Placeholder/BlankTex");
			Vector2 drawTop = drawCenter + new Vector2(0f, -yRange);
			spriteBatch.Draw(texture, drawTop, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(scale, 1f), SpriteEffects.None, 0f);

			float distance = (projectile.ai[0] - 60f) / 60f;
			if (distance < 0f)
			{
				distance = 0f;
			}
			if (distance > 1f)
			{
				distance = 1f;
			}

			float seed = projectile.localAI[0];
			texture = mod.GetTexture("Extra/Placeholder/BlankTex");
			Vector2 topLeft = drawTop + new Vector2(-xRange, 0f);
			for (int k = (int)projectile.ai[0] - 120; k < (int)projectile.ai[0]; k++)
			{
				if (k > 120)
				{
					spriteBatch.Draw(texture, topLeft + new Vector2(seed % (2 * xRange), seed % (2 * yRange)), null, Color.White, 0.1f * (projectile.ai[0] - k), new Vector2(texture.Width / 2, texture.Height / 2), 2f * (projectile.ai[0] - k) / 120f, SpriteEffects.None, 0f);
				}
				seed = Next(seed);
			}
			return false;
		}
        public override bool PreAI()
        {
            if (Main.rand.Next(60) == 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].friendly &&  Vector2.Distance(projectile.Center, Main.npc[i].Center) < 960f && Main.npc[i].CanBeChasedBy(projectile, false))
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            Projectile.NewProjectile(Main.npc[i].Center.X + (250 * (j == 0 ? -1 : 1)), Main.npc[i].Center.Y, 15 * (j == 0 ? -1 : 1), 0, mod.ProjectileType("Purification"), Main.player[projectile.owner].statLife, 1f, projectile.owner, j == 0 ? 0 : 1, 1f);
                        }
                        break; 
                    }
                }
            }
            if (Main.rand.Next(30) == 0)
            {
                float distance = Main.rand.Next(960);
                projectile.ai[0] %= (float)Math.PI * 2f;
                Vector2 offset = new Vector2((float)Math.Cos(projectile.ai[0]), (float)Math.Sin(projectile.ai[0]));
                Vector2 position = Main.player[projectile.owner].position + distance * offset;
                Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("PurificationRune"), Main.player[projectile.owner].statLife, 1f, projectile.owner, Main.rand.Next(6), 1f);
                projectile.ai[0] += 10;
            }
            return true;
        }
    }
}