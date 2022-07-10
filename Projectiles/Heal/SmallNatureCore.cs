using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Heal
{
    public class SmallNatureCore : ModProjectile
	{
        public override string Texture => "SinsMod/NPCs/Boss/Madness/BCCSummonHeal";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Small Nature Core");
            Main.projFrames[projectile.type] = 8;
        }
		public override void SetDefaults()
		{
            projectile.width = 56;
			projectile.height = 56;
            projectile.minion = true;
            projectile.minionSlots = 0f;
            projectile.penetrate = -1;
			projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.scale = 0.5f;
            projectile.light = 0.3f;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
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
            projectile.alpha = 0;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4 * (projectile.extraUpdates + 1))
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.dead)
            {
                projectile.Kill();
            }
            if (modPlayer.SmallNatureCore)
            {
                projectile.timeLeft = 2;
            }
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 180)
            {
                float num = player.statLifeMax2 / 10;
                int num2 = projectile.owner;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("NatureHeal"), 0, 0f, projectile.owner, num2, num);
                projectile.localAI[0] = 0;
            }
            float leashLength = 800f;
            if (Vector2.Distance(player.Center, projectile.Center) > leashLength)
            {
                projectile.ai[0] = 1f;
                projectile.tileCollide = false;
                projectile.netUpdate = true;
            }
            float speed = projectile.ai[0] == 1f ? 15f : 6f;
            Vector2 offset = player.Center - projectile.Center + new Vector2(0f, -60f);
            float distance2 = offset.Length();
            if (distance2 > 50f && speed < 40f)
            {
                speed = 40f;
            }
            if (distance2 < 50f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.ai[0] = 0f;
                projectile.netUpdate = true;
            }
            if (distance2 > 2000f)
            {
                projectile.position = player.Center - new Vector2(projectile.width / 2, projectile.height / 2);
                projectile.netUpdate = true;
            }
            if (distance2 > 50f)
            {
                offset.Normalize();
                offset *= speed;
                projectile.velocity = (projectile.velocity * 40f + offset) / 41f;
            }
            else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
            {
                projectile.velocity = new Vector2(-0.15f, -0.05f);
            }
        }
        public override void Kill(int timeleft)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCCCKilled").WithVolume(0.7f), (int)projectile.position.X, (int)projectile.position.Y);
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 50;
            projectile.height = 50;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            for (int i = 0; i < 20; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 235, 0f, 0f, 100, default(Color), 1.2f * projectile.scale);
                Main.dust[num].velocity *= 3f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale = 0.5f;
                    Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            for (int j = 0; j < 40; j++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 245, 0f, 0f, 100, default(Color), 1.0f * projectile.scale);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 5f;
                num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 245, 0f, 0f, 100, default(Color), 1.2f * projectile.scale);
                Main.dust[num2].velocity *= 2f;
            }
        }
    }
}