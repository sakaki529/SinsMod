using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class SmallBlackCore : ModProjectile
	{
        private bool Charging => true;
        public override string Texture => "SinsMod/NPCs/Boss/Madness/BlackCrystalCore";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Small Black Core");
            Main.projFrames[projectile.type] = 8;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 56;
            projectile.height = 56;
            projectile.minion = true;
            projectile.minionSlots = 2f;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.scale = 0.6f;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override bool PreAI()
        {
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
            player.AddBuff(mod.BuffType("SmallBlackCoreMinion"), 3600, true);
            if (player.dead)
            {
                modPlayer.SmallBlackCoreMinion = false;
            }
            if (modPlayer.SmallBlackCoreMinion)
            {
                projectile.timeLeft = 2;
            }

            if (projectile.ai[0] == 2f && Charging)
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
                for (int i = 0; i < 200; i++)
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
                        if (((distance < Vector2.Distance(projectile.Center, targetPos) && distance < targetDist) || !hasTarget))
                        {
                            targetDist = distance;
                            targetPos = npc.Center;
                            hasTarget = true;
                        }
                    }
                }
            }
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
                    offset *= Charging ? 40f : 40f;
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
            if (projectile.ai[1] > 90f && !Charging)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[1] > 20f && Charging)
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
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCKilled").WithVolume(0.7f), (int)projectile.position.X, (int)projectile.position.Y);
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 50;
            projectile.height = 50;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            for (int i = 0; i < 20; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 186, 0f, 0f, 50, default(Color), 1.2f * projectile.scale);
                Main.dust[num].velocity *= 3f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale = 0.5f;
                    Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            for (int j = 0; j < 40; j++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 186, 0f, 0f, 0, default(Color), 1.0f * projectile.scale);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 5f;
                Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 245, 0f, 0f, 100, default(Color), 1.2f * projectile.scale);
                Main.dust[num2].velocity *= 2f;
                Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
        }
    }
}