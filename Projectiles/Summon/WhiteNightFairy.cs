using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class WhiteNightFairy : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("White Night Fairy");
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 20;
            projectile.height = 20;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.light = 1.0f;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            player.AddBuff(mod.BuffType("WhiteNightFairyMinion"), 3600, true);
            if (player.dead)
            {
                modPlayer.WhiteNightFairyMinion = false;
            }
            if (modPlayer.WhiteNightFairyMinion)
            {
                projectile.timeLeft = 2;
            }
            int num;
            float accel = 0.12f;
            for (int num2 = 0; num2 < 1000; num2 = num + 1)
            {
                if (num2 != projectile.whoAmI && Main.projectile[num2].active && Main.projectile[num2].owner == projectile.owner && Main.projectile[num2].type == projectile.type && Math.Abs(projectile.position.X - Main.projectile[num2].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num2].position.Y) < projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num2].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - accel;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + accel;
                    }
                    if (projectile.position.Y < Main.projectile[num2].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - accel;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + accel;
                    }
                }
                num = num2;
            }
            float num3 = projectile.position.X;
            float num4 = projectile.position.Y;
            float num5 = 900f;
            bool flag = false;
            int num6 = 400;
            if (projectile.ai[1] != 0f || projectile.friendly)
            {
                num6 = 1400;
            }
            if (Math.Abs(projectile.Center.X - Main.player[projectile.owner].Center.X) + Math.Abs(projectile.Center.Y - Main.player[projectile.owner].Center.Y) > num6)
            {
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.tileCollide = true;
                NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(projectile, false))
                {
                    float num7 = ownerMinionAttackTargetNPC.position.X + (ownerMinionAttackTargetNPC.width / 2);
                    float num8 = ownerMinionAttackTargetNPC.position.Y + (ownerMinionAttackTargetNPC.height / 2);
                    float num9 = Math.Abs(projectile.position.X + (projectile.width / 2) - num7) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num8);
                    if (num9 < num5 && Collision.CanHit(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                    {
                        num5 = num9;
                        num3 = num7;
                        num4 = num8;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    for (int num10 = 0; num10 < Main.maxNPCs; num10 = num + 1)
                    {
                        if (Main.npc[num10].CanBeChasedBy(projectile, false))
                        {
                            float num11 = Main.npc[num10].position.X + (Main.npc[num10].width / 2);
                            float num12 = Main.npc[num10].position.Y + (Main.npc[num10].height / 2);
                            float num13 = Math.Abs(projectile.position.X + (projectile.width / 2) - num11) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num12);
                            if (num13 < num5 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num10].position, Main.npc[num10].width, Main.npc[num10].height))
                            {
                                num5 = num13;
                                num3 = num11;
                                num4 = num12;
                                flag = true;
                            }
                        }
                        num = num10;
                    }
                }
            }
            else
            {
                projectile.tileCollide = false;
            }
            projectile.rotation = projectile.velocity.X * 0.05f;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
            }
            if (projectile.frame >= Main.projFrames[projectile.type])
            {
                projectile.frame = 0;
            }
            if (!flag)
            {
                projectile.friendly = true;
                float num14 = 14f;
                if (projectile.ai[0] == 1f)
                {
                    num14 = 18f;
                }
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num15 = Main.player[projectile.owner].Center.X - vector.X;
                float num16 = Main.player[projectile.owner].Center.Y - vector.Y - 60f;
                float num17 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
                if (num17 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                }
                if (num17 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - (projectile.width / 2);
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - (projectile.width / 2);
                }
                if (num17 > 70f)
                {
                    num17 = num14 / num17;
                    num15 *= num17;
                    num16 *= num17;
                    projectile.velocity.X = (projectile.velocity.X * 20f + num15) / 21f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + num16) / 21f;
                }
                else
                {
                    if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                    {
                        projectile.velocity.X = -0.15f;
                        projectile.velocity.Y = -0.05f;
                    }
                    projectile.velocity *= 1.01f;
                }
                //projectile.friendly = false;
                if (Math.Abs(projectile.velocity.X) > 0.2)
                {
                    projectile.spriteDirection = -projectile.direction;
                    return;
                }
            }
            else
            {
                if (projectile.ai[1] == -1f)
                {
                    projectile.ai[1] = 17f;
                }
                if (projectile.ai[1] > 0f)
                {
                    projectile.ai[1] -= 1f;
                }
                if (projectile.ai[1] == 0f)
                {
                    //projectile.friendly = true;
                    float num18 = 14f;
                    Vector2 vector2 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float num19 = num3 - vector2.X;
                    float num20 = num4 - vector2.Y;
                    float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
                    if (num21 < 100f)
                    {
                        num18 = 18f;
                    }
                    num21 = num18 / num21;
                    num19 *= num21;
                    num20 *= num21;
                    projectile.velocity.X = (projectile.velocity.X * 14f + num19) / 15f;
                    projectile.velocity.Y = (projectile.velocity.Y * 14f + num20) / 15f;
                }
                else
                {
                    //projectile.friendly = false;
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 10f)
                    {
                        projectile.velocity *= 1.05f;
                    }
                }
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                if (Math.Abs(projectile.velocity.X) > 0.2)
                {
                    projectile.spriteDirection = -projectile.direction;
                    return;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[1] = -1f;
            projectile.netUpdate = true;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            //projectile.ai[1] = -1f;
            projectile.netUpdate = true;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            //projectile.ai[1] = -1f;
            projectile.netUpdate = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * -0.6f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * -0.6f;
            }
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255);
        }
    }
}