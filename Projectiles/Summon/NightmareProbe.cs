using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class NightmareProbe : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Probe");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            Main.projPet[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 48;
            projectile.height = 48;
            projectile.minion = true;
            projectile.minionSlots = 2f;
            projectile.penetrate = -1;
			projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            player.AddBuff(mod.BuffType("NightmareProbeMinion"), 3600, true);
            if (player.dead)
            {
                modPlayer.NightmareProbeMinion = false;
            }
            if (modPlayer.NightmareProbeMinion)
            {
                projectile.timeLeft = 2;
            }
            float num = 0.05f;
            for (int num2 = 0; num2 < Main.projectile.Length; num2++)
            {
                if (num2 != projectile.whoAmI && Main.projectile[num2].active && Main.projectile[num2].owner == projectile.owner && Main.projectile[num2].type == mod.ProjectileType("NightmareProbe") && Math.Abs(projectile.position.X - Main.projectile[num2].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num2].position.Y) < projectile.width)
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
            float num6 = 1100f;
            if (flag)
            {
                num6 = 2400f;
            }
            if (Vector2.Distance(player.Center, projectile.Center) > num6)
            {
                projectile.ai[0] = 1f;
                projectile.netUpdate = true;
            }
            if (flag && projectile.ai[0] == 0f)
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
                if (projectile.ai[0] == 1f)
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
                if (num11 < 30f && projectile.ai[0] == 1f)
                {
                    projectile.ai[0] = 0f;
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
                projectile.rotation = (vector - projectile.Center).ToRotation() + 3.14159274f;
            }
            else
            {
                projectile.rotation = projectile.velocity.ToRotation() + 3.14159274f;
            }
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += 1f;
            }
            if (projectile.ai[1] > 15f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                float num12 = 16f;
                if (flag && projectile.ai[1] == 0f)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner)
                    {
                        Vector2 vector4 = vector - projectile.Center;
                        vector4.Normalize();
                        vector4 *= num12;
                        int num13 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector4.X, vector4.Y, mod.ProjectileType("NightmareLaserSummon"), projectile.damage, 0f, projectile.owner, 0f, 0f);
                        Main.projectile[num13].ranged = false;
                        Main.projectile[num13].minion = true;
                        Main.projectile[num13].tileCollide = flag2;
                        projectile.netUpdate = true;
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath14, (int)projectile.position.X, (int)projectile.position.Y);
            int num = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), projectile.scale);
            Main.gore[num].velocity *= 0.4f;
            Gore gore = Main.gore[num];
            gore.velocity.X = gore.velocity.X + 1f;
            Gore gore2 = Main.gore[num];
            gore2.velocity.Y = gore2.velocity.Y + 1f;
            num = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), projectile.scale);
            Main.gore[num].velocity *= 0.4f;
            Gore gore3 = Main.gore[num];
            gore3.velocity.X = gore3.velocity.X - 1f;
            Gore gore4 = Main.gore[num];
            gore4.velocity.Y = gore4.velocity.Y + 1f;
            num = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), projectile.scale);
            Main.gore[num].velocity *= 0.4f;
            Gore gore5 = Main.gore[num];
            gore5.velocity.X = gore5.velocity.X + 1f;
            Gore gore6 = Main.gore[num];
            gore6.velocity.Y = gore6.velocity.Y - 1f;
            num = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), projectile.scale);
            Main.gore[num].velocity *= 0.4f;
            Gore gore7 = Main.gore[num];
            gore7.velocity.X = gore7.velocity.X - 1f;
            Gore gore8 = Main.gore[num];
            gore8.velocity.Y = gore8.velocity.Y - 1f;
        }
    }
}