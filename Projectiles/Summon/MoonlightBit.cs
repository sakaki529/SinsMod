using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class MoonlightBit : ModProjectile
	{
        private bool hasTargetNow;
        private bool canRotate = true;
        private int Count;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moonlight Bit");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 16;
            projectile.height = 16;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
			projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            player.AddBuff(mod.BuffType("MoonlightMinion"), 3600, true);
            if (player.dead)
            {
                modPlayer.MoonlightMinion = false;
            }
            if (modPlayer.MoonlightMinion)
            {
                projectile.timeLeft = 2;
            }
            float num = 0.05f;
            for (int num2 = 0; num2 < Main.projectile.Length; num2++)
            {
                if (num2 != projectile.whoAmI && Main.projectile[num2].active && Main.projectile[num2].owner == projectile.owner && Main.projectile[num2].type == projectile.type && Math.Abs(projectile.position.X - Main.projectile[num2].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num2].position.Y) < projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num2].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - num;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + num;
                    }
                    if (projectile.position.Y < Main.projectile[num2].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num;
                    }
                }
            }
            if (canRotate)
            {
                projectile.rotation += projectile.velocity.X * 0.025f * projectile.direction;
            }
            if (Count >= 10 && hasTargetNow)
            {
                ShooterAI(player);
                return;
            }
            else
            {
                canRotate = true;
                ChargerAI(player);
            }
        }
        private void ChargerAI(Player player)
        {
            if (projectile.ai[0] == 2f)
            {
                projectile.ai[1] += 1f;
                projectile.extraUpdates = 3;
                if (projectile.ai[1] > 40f)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] = 0f;
                    projectile.extraUpdates = 0;
                    projectile.numUpdates = 0;
                    projectile.netUpdate = true;
                }
                else
                {
                    return;
                }
            }
            float targetDist = 1000f;
            Vector2 targetPos = projectile.position;
            bool hasTarget = false;
            NPC ownerTarget = projectile.OwnerMinionAttackTargetNPC;
            if (ownerTarget != null && ownerTarget.CanBeChasedBy(projectile))
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    Vector2 newMove = Main.npc[i].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < targetDist)
                    {
                        targetDist = Vector2.Distance(ownerTarget.Center, projectile.Center);
                        targetPos = ownerTarget.Center;
                        hasTarget = true;
                    }
                }
            }
            if (!hasTarget)
            {
                for (int j = 0; j < Main.npc.Length; j++)
                {
                    NPC npc = Main.npc[j];
                    if (npc.CanBeChasedBy(projectile))
                    {
                        float distance = Vector2.Distance(npc.Center, projectile.Center);
                        if ((distance < Vector2.Distance(projectile.Center, targetPos) && distance < targetDist) || !hasTarget)
                        {
                            targetDist = distance;
                            targetPos = npc.Center;
                            hasTarget = true;
                        }
                    }
                }
            }
            hasTargetNow = hasTarget;
            float leashLength = hasTarget ? 2400f/*1200*/ : 800f;//Back to player pos
            if (Vector2.Distance(player.Center, projectile.Center) > leashLength)
            {
                projectile.ai[0] = 1f;
                projectile.tileCollide = false;
                projectile.netUpdate = true;
            }
            if (hasTarget && projectile.ai[0] == 0f)
            {
                Vector2 offset = targetPos - projectile.Center;
                offset.Normalize();
                if (targetDist > 50f)
                {
                    offset *= 40f;
                }
                else
                {
                    offset *= -4f;
                }
                projectile.velocity = (projectile.velocity * 40f + offset) / 41f;
            }
            else
            {
                float speed = projectile.ai[0] == 1f ? 15f : 6f;
                Vector2 offset = player.Center - projectile.Center + new Vector2(0f, -60f);
                float distance = offset.Length();
                if (distance > 50f && speed < 40f)
                {
                    speed = 40f;
                }
                if (distance < 50f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (distance > 2400f)
                {
                    projectile.position = player.Center - new Vector2(projectile.width / 2, projectile.height / 2);
                    projectile.netUpdate = true;
                }
                if (distance > 50f)//recharge
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
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (projectile.ai[1] > 20f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f && projectile.ai[1] == 0f && hasTarget)
            {
                projectile.ai[1] += 1f;
                if (targetDist < 200f)//charge start
                {
                    if (Main.myPlayer == projectile.owner)
                    {
                        projectile.ai[0] = 2f;
                        Vector2 offset = targetPos - projectile.Center;
                        offset.Normalize();
                        projectile.velocity = offset * 6f;
                        projectile.netUpdate = true;
                        Count++;
                    }
                }
            }
        }
        private void ShooterAI(Player player)
        {
            Vector2 vector = projectile.Center;
            float num3 = 700f;
            bool flag = false;
            bool flag2 = true;
            for (int num4 = 0; num4 < Main.maxNPCs; num4++)
            {
                NPC nPC = Main.npc[num4];
                if (projectile.OwnerMinionAttackTargetNPC != null)
                {
                    nPC = projectile.OwnerMinionAttackTargetNPC;
                }
                if (nPC.CanBeChasedBy(projectile, false))
                {
                    float num5 = Vector2.Distance(nPC.Center, projectile.Center);
                    if (((Vector2.Distance(projectile.Center, vector) > num5 && num5 < num3) || !flag)/* && flag2*/)
                    {
                        num3 = num5;
                        vector = nPC.Center;
                        flag = true;
                        flag2 = Collision.CanHitLine(projectile.Center, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height);
                    }
                }
            }
            hasTargetNow = flag;
            canRotate = !flag;
            if (Count >= 36)
            {
                Count = 0;
            }
            float num6 = 1100f;
            if (flag)
            {
                num6 = 2400f;
            }
            if (Vector2.Distance(player.Center, projectile.Center) > num6)
            {
                projectile.localAI[0] = 1f;
                projectile.netUpdate = true;
            }
            if (flag && projectile.localAI[0] == 0f)
            {
                Vector2 vector2 = vector - projectile.Center;
                float num7 = vector2.Length();
                vector2.Normalize();
                if (num7 > 200f)
                {
                    float num8 = 6f;
                    vector2 *= num8;
                    projectile.velocity = (projectile.velocity * 40f + vector2) / 41f;
                }
                else
                {
                    float num9 = 4f;
                    vector2 *= -num9;
                    projectile.velocity = (projectile.velocity * 40f + vector2) / 41f;
                }
            }
            else
            {
                float num10 = 20f;
                if (projectile.localAI[0] == 1f)
                {
                    num10 = 15f;
                }
                Vector2 value = projectile.Center;
                Vector2 vector3 = player.Center - value + new Vector2(0f, -60f);
                float num11 = vector3.Length();
                if (num11 > 200f && num10 < 3f)
                {
                    num10 = 2f;
                }
                if (num11 < 30f && projectile.localAI[0] == 1f)
                {
                    projectile.localAI[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (num11 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - (projectile.width / 2);
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - (projectile.height / 2);
                    projectile.netUpdate = true;
                }
                if (num11 > 10f)
                {
                    vector3.Normalize();
                    vector3 *= num10;
                    projectile.velocity = (projectile.velocity * 40f + vector3) / 41f;
                }
                else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = -0.15f;
                    projectile.velocity.Y = -0.05f;
                }
            }
            if (flag)
            {
                projectile.rotation = (vector - projectile.Center).ToRotation() + 0.785f;
            }
            if (projectile.localAI[1] > 0f)
            {
                projectile.localAI[1] += 1f;
            }
            if (projectile.localAI[1] > 6f)
            {
                projectile.localAI[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.localAI[0] == 0f)
            {
                float num12 = 16f;
                if (flag && projectile.localAI[1] == 0f)
                {
                    projectile.localAI[1] += 1f;
                    if (Main.myPlayer == projectile.owner)
                    {
                        Vector2 vector4 = vector - projectile.Center;
                        vector4.Normalize();
                        vector4 *= num12;
                        int num13 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector4.X, vector4.Y, mod.ProjectileType("MoonlightRay"), projectile.damage, 0f, projectile.owner, 0f, 0f);
                        Main.projectile[num13].tileCollide = flag2;
                        projectile.netUpdate = true;
                        Count++;
                    }
                }
            }
        }
    }
}