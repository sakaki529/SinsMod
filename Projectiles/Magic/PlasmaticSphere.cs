using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class PlasmaticSphere : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plasma Sphere");
            Main.projFrames[projectile.type] = 5;
        }
		public override void SetDefaults()
		{
            projectile.width = 38;
			projectile.height = 38;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.penetrate = -1;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.light = 0.5f;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = projectile.velocity.X;
                projectile.ai[1] = projectile.velocity.Y;
            }
            if (projectile.velocity.X > 0f)
            {
                projectile.rotation += (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.001f;
            }
            else
            {
                projectile.rotation -= (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.001f;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            if (Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y) > 2.0)
            {
                projectile.velocity *= 0.98f;
            }
            int num;
            /*for (int i = 0; i < Main.maxProjectiles; i = num + 1)
            {
                if (i != projectile.whoAmI && Main.projectile[i].active && Main.projectile[i].owner == projectile.owner && Main.projectile[i].type == projectile.type && projectile.timeLeft > Main.projectile[i].timeLeft && Main.projectile[i].timeLeft > 30)
                {
                    Main.projectile[i].timeLeft = 30;
                }
                num = i;
            }*/
            int[] array = new int[20];
            int num2 = 0;
            float num3 = 300f;
            bool flag = false;
            for (int i = 0; i < Main.maxNPCs; i = num + 1)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false))
                {
                    float num4 = Main.npc[i].position.X + Main.npc[i].width / 2;
                    float num5 = Main.npc[i].position.Y + Main.npc[i].height / 2;
                    float num6 = Math.Abs(projectile.position.X + projectile.width / 2 - num4) + Math.Abs(projectile.position.Y + projectile.height / 2 - num5);
                    if (num6 < num3 && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                    {
                        if (num2 < 20)
                        {
                            array[num2] = i;
                            num2++;
                        }
                        flag = true;
                    }
                }
                num = i;
            }
            if (projectile.timeLeft < 30)
            {
                flag = false;
            }
            if (flag)
            {
                int num7 = Main.rand.Next(num2);
                num7 = array[num7];
                float num8 = Main.npc[num7].position.X + Main.npc[num7].width / 2;
                float num9 = Main.npc[num7].position.Y + Main.npc[num7].height / 2;
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] > 8f)
                {
                    projectile.localAI[0] = 0f;
                    float num10 = 6f;
                    Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    vector += projectile.velocity * 4f;
                    float num11 = num8 - vector.X;
                    float num12 = num9 - vector.Y;
                    float num13 = (float)Math.Sqrt(num11 * num11 + num12 * num12);
                    num13 = num10 / num13;
                    num11 *= num13;
                    num12 *= num13;
                    Projectile.NewProjectile(vector.X, vector.Y, num11, num12, mod.ProjectileType("PlasmaticBeam"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                }
            }
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] == 20)
            {
                projectile.tileCollide = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.tileCollide = false;
            projectile.velocity = oldVelocity;
            if (projectile.timeLeft > 30)
            {
                projectile.timeLeft = 30;
            }
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.timeLeft < 30)
            {
                float num = projectile.timeLeft / 30f;
                projectile.alpha = (int)(255f - 255f * num);
            }
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 0);
        }
    }
}