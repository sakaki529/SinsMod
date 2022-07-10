using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class CherrySeed : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cherry Seed");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
		public override void SetDefaults()
		{
            projectile.width = 16;
			projectile.height = 16;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.magic = true;
            projectile.timeLeft = 120;
			projectile.penetrate = 1;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.alpha = 100;
            projectile.scale = 0.5f;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[0] % 2 == 0)
            {
                Color color = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
                SpriteEffects effects = SpriteEffects.None;
                int num1 = 0;
                int num2 = 0;
                float num3 = (Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f + projectile.width * 0.5f;
                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
                {
                    Color alpha = projectile.GetAlpha(color);
                    float num4 = (9 - i) / 9f;
                    alpha.R = (byte)(alpha.R * num4);
                    alpha.G = (byte)(alpha.G * num4);
                    alpha.B = (byte)(alpha.B * num4);
                    alpha.A = (byte)(alpha.A * num4);
                    float num5 = (9 - i) / 9f;
                    Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.oldPos[i].X - Main.screenPosition.X + num3 + num2, projectile.oldPos[i].Y - Main.screenPosition.Y + (projectile.height / 2) + projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height)), alpha, projectile.oldRot[i], new Vector2(num3, (projectile.height / 2 + num1)), num5 * projectile.scale, effects, 0f);
                }
            }
        }
        public override void AI()
        {
            int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 72, projectile.velocity.X, projectile.velocity.Y, 200);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 0.4f;
            Main.dust[d].scale = 0.8f;

            float num = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
            float num2 = projectile.localAI[0];
            if (num2 == 0f)
            {
                projectile.localAI[0] = num;
                num2 = num;
            }
            float num3 = projectile.position.X;
            float num4 = projectile.position.Y;
            float num5 = 600f;
            bool flag = false;
            int num6 = 0;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 10f)
            {
                projectile.ai[0] -= 1f;
                if (projectile.ai[1] == 0f)
                {
                    for (int num7 = 0; num7 < 200; num7++)
                    {
                        if (Main.npc[num7].CanBeChasedBy(this, false) && (projectile.ai[1] == 0f || projectile.ai[1] == num7 + 1))
                        {
                            float num8 = Main.npc[num7].position.X + (Main.npc[num7].width / 2);
                            float num9 = Main.npc[num7].position.Y + (Main.npc[num7].height / 2);
                            float num10 = Math.Abs(projectile.position.X + (projectile.width / 2) - num8) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num9);
                            if (num10 < num5 && Collision.CanHit(new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2)), 1, 1, Main.npc[num7].position, Main.npc[num7].width, Main.npc[num7].height))
                            {
                                num5 = num10;
                                num3 = num8;
                                num4 = num9;
                                flag = true;
                                num6 = num7;
                            }
                        }
                    }
                    if (flag)
                    {
                        projectile.ai[1] = num6 + 1;
                    }
                    flag = false;
                }
                if (projectile.ai[1] != 0f)
                {
                    int num11 = (int)(projectile.ai[1] - 1f);
                    if (Main.npc[num11].active && Main.npc[num11].CanBeChasedBy(projectile, true))
                    {
                        float num12 = Main.npc[num11].position.X + (Main.npc[num11].width / 2);
                        float num13 = Main.npc[num11].position.Y + (Main.npc[num11].height / 2);
                        if (Math.Abs(projectile.position.X + (projectile.width / 2) - num12) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num13) < 1000f)
                        {
                            flag = true;
                            num3 = Main.npc[num11].position.X + (Main.npc[num11].width / 2);
                            num4 = Main.npc[num11].position.Y + (Main.npc[num11].height / 2);
                        }
                    }
                }
                if (!projectile.friendly)
                {
                    flag = false;
                }
                if (flag)
                {
                    float num18 = num2;
                    Vector2 vector2 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float num14 = num3 - vector2.X;
                    float num15 = num4 - vector2.Y;
                    float num16 = (float)Math.Sqrt(num14 * num14 + num15 * num15);
                    num16 = num18 / num16;
                    num14 *= num16;
                    num15 *= num16;
                    int num17 = 8;
                    projectile.velocity.X = (projectile.velocity.X * (num17 - 1) + num14) / num17;
                    projectile.velocity.Y = (projectile.velocity.Y * (num17 - 1) + num15) / num17;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!Main.player[projectile.owner].moonLeech)
            {
                if (target.type != 488)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        float num = damage * 0.05f;
                        if ((int)num == 0)
                        {
                            return;
                        }
                        if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                        {
                            return;
                        }
                        Main.player[Main.myPlayer].lifeSteal -= num;
                        int num2 = projectile.owner;
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SakuraHeal"), 0, 0f, projectile.owner, num2, num);
                    }
                }
            }
            projectile.Kill();
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage *= 5;
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (!Main.player[projectile.owner].moonLeech)
            {
                float num = damage * 0.01f;
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = projectile.owner;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SakuraHeal"), 0, 0f, projectile.owner, num2, num);
                projectile.Kill();
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage *= 5;
            damage += target.statDefense / 2;
        }
        public override void Kill(int timeleft)
		{
			for (int num = 0; num < 3; num++)
			{
				int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 72, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 1.8f;
			}
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("CherryBlossom"), projectile.damage / 3, 0f, projectile.owner, 0f);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}