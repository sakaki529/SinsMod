using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class Lunatic : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunatic");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 64;
            projectile.height = 64;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
			projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 5;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glow = mod.GetTexture("Glow/Projectile/Lunatic_Glow");
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, texture.Size() / 2f, projectile.scale, 0, 0f);
            if (!Main.dayTime)
            {
                spriteBatch.Draw(glow, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(Color.White), projectile.rotation, texture.Size() / 2f, projectile.scale, 0, 0f);
            }
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            player.AddBuff(mod.BuffType("LunaticMinion"), 3600, true);
            if (player.dead)
            {
                modPlayer.LunaticMinion = false;
            }
            if (modPlayer.LunaticMinion)
            {
                projectile.timeLeft = 2;
            }
            if (!Main.dayTime)
            {
                Lighting.AddLight(projectile.Center, 0.3f, 0.5f, 0.7f);
            }

            float num626 = 1500f;
            float num627 = 900f;
            float num628 = 1500f;
            float num629 = 750f;

            float num630 = 0.05f;
            for (int i = 0; i < 1000; i++)
            {
                bool flag22 = Main.projectile[i].type == mod.ProjectileType("Lunatic");
                if (((i != projectile.whoAmI && Main.projectile[i].active && Main.projectile[i].owner == projectile.owner) & flag22) && Math.Abs(projectile.position.X - Main.projectile[i].position.X) + Math.Abs(projectile.position.Y - Main.projectile[i].position.Y) < projectile.width)
                {
                    if (projectile.position.X < Main.projectile[i].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - num630;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + num630;
                    }
                    if (projectile.position.Y < Main.projectile[i].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num630;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num630;
                    }
                }
            }
            bool flag23 = false;
            if (projectile.ai[0] >= 3f && projectile.ai[0] <= 5f)
            {
                int num632 = 2;
                flag23 = true;
                projectile.velocity *= 0.9f;
                float num73 = projectile.ai[1];
                projectile.ai[1] = num73 + 1f;
                if (projectile.ai[1] > (num632 * 8))
                {
                    projectile.ai[0] -= 3f;
                    projectile.ai[1] = 0f;
                }
            }
            if (projectile.ai[0] >= 6f && projectile.ai[0] <= 8f)
            {
                float num73 = projectile.ai[1];
                projectile.ai[1] = num73 + 1f;
                projectile.MaxUpdates = 2;
                if (projectile.ai[0] == 7f)
                {
                    projectile.rotation = projectile.velocity.ToRotation() + 3.14159274f;
                }
                else
                {
                    projectile.rotation += 0.5235988f;
                }
                int num634 = 0;
                switch ((int)projectile.ai[0])
                {
                    case 6:
                        num634 = 40;
                        break;
                    case 7:
                        num634 = 30;
                        break;
                    case 8:
                        num634 = 30;
                        break;
                }
                if (projectile.ai[1] > num634)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] -= 6f;
                    num73 = projectile.localAI[0];
                    projectile.localAI[0] = num73 + 1f;
                    projectile.extraUpdates = 0;
                    projectile.numUpdates = 0;
                    projectile.netUpdate = true;
                }
                else
                {
                    flag23 = true;
                }
                if (projectile.ai[0] == 8f)
                {
                    /*for (int num635 = 0; num635 < 4; num635 = num3 + 1)
                    {
                        int num636 = Utils.SelectRandom<int>(Main.rand, new int[]
                        {
                            226,
                            228,
                            75
                        });
                        int num637 = Dust.NewDust(projectile.Center, 0, 0, num636, 0f, 0f, 0, default(Color), 1f);
                        Dust dust3 = Main.dust[num637];
                        Vector2 value12 = Vector2.One.RotatedBy((double)((float)num635 * 1.57079637f), default(Vector2)).RotatedBy((double)projectile.rotation, default(Vector2));
                        dust3.position = projectile.Center + value12 * 10f;
                        dust3.velocity = value12 * 1f;
                        dust3.scale = 0.6f + Main.rand.NextFloat() * 0.5f;
                        dust3.noGravity = true;
                        num3 = num635;
                    }*/
                }
            }
            if (flag23)
            {
                return;
            }
            Vector2 vector = projectile.position;
            bool flag24 = false;
            if (projectile.ai[0] < 9f)
            {
                projectile.tileCollide = true;
            }
            if (projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16)))
            {
                projectile.tileCollide = false;
            }
            NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(projectile, false))
            {
                float num638 = Vector2.Distance(ownerMinionAttackTargetNPC.Center, projectile.Center);
                if (((Vector2.Distance(projectile.Center, vector) > num638 && num638 < num626) || !flag24) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                {
                    num626 = num638;
                    vector = ownerMinionAttackTargetNPC.Center;
                    flag24 = true;
                }
            }
            if (!flag24)
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.CanBeChasedBy(projectile, false))
                    {
                        float num640 = Vector2.Distance(nPC.Center, projectile.Center);
                        if (((Vector2.Distance(projectile.Center, vector) > num640 && num640 < num626) || !flag24) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
                        {
                            num626 = num640;
                            vector = nPC.Center;
                            flag24 = true;
                        }
                    }
                }
            }
            float num641 = num627;
            if (flag24)
            {
                num641 = num628;
            }
            Player player2 = Main.player[projectile.owner];
            if (Vector2.Distance(player2.Center, projectile.Center) > num641)
            {
                if (projectile.ai[0] < 9f)
                {
                    projectile.ai[0] += 3 * (3 - (int)(projectile.ai[0] / 3f));
                }
                projectile.tileCollide = false;
                projectile.netUpdate = true;
            }

            bool flag25 = false;
            if (!flag25)
            {
                flag25 = projectile.ai[0] >= 9f;
            }
            float num644 = 12f * 1.5f;//vel near player
            if (flag25)
            {
                num644 = 15f * 1.5f;
            }
            Vector2 center2 = projectile.Center;
            Vector2 vector52 = player2.Center - center2 + new Vector2(0f, -60f);
            float num645 = vector52.Length();
            if (num645 > 200f && num644 < 8f * 1.5f)
            {
                num644 = 8f * 1.5f;
            }
            if ((num645 < num629 & flag25) && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.ai[0] -= 9f;
                projectile.netUpdate = true;
            }
            if (num645 > 2000f)
            {
                projectile.position.X = Main.player[projectile.owner].Center.X - (projectile.width / 2);
                projectile.position.Y = Main.player[projectile.owner].Center.Y - (projectile.height / 2);
                projectile.netUpdate = true;
            }
            if (num645 > 70f)
            {
                vector52.Normalize();
                vector52 *= num644;
                projectile.velocity = (projectile.velocity * 40f + vector52) / 41f;
            }
            else
            {
                if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = -0.15f;
                    projectile.velocity.Y = -0.05f;
                }
            }

            if (projectile.ai[0] < 3f || projectile.ai[0] >= 9f)
            {
                projectile.rotation += projectile.velocity.X * 0.04f;
            }

            if (projectile.ai[1] > 0f)
            {
                float num73 = projectile.ai[1];
                projectile.ai[1] = num73 + 1f;
                int num650 = 10;
                if (projectile.ai[1] > num650)
                {
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
            }

            if (projectile.ai[0] < 3f)
            {
                int num653 = 0;
                switch ((int)projectile.ai[0])//dist
                {
                    case 0:
                    case 3:
                    case 6:
                        num653 = 400 * 2;
                        break;
                    case 1:
                    case 4:
                    case 7:
                        num653 = 400 * 2;
                        break;
                    case 2:
                    case 5:
                    case 8:
                        num653 = 600 * 2;
                        break;
                }
                if ((projectile.ai[1] == 0f & flag24) && num626 < num653)
                {
                    float num73 = projectile.ai[1];
                    projectile.ai[1] = num73 + 1f;
                    if (Main.myPlayer == projectile.owner)
                    {
                        if (projectile.localAI[0] >= 3f)
                        {
                            projectile.ai[0] += 4f;
                            if (projectile.ai[0] == 6f)
                            {
                                projectile.ai[0] = 3f;
                            }
                            projectile.localAI[0] = 0f;
                            return;
                        }
                        projectile.ai[0] += 6f;
                        Vector2 value15 = vector - projectile.Center;
                        value15.Normalize();
                        float scaleFactor4 = (projectile.ai[0] == 8f) ? 12f : 10f;//attack vel
                        projectile.velocity = value15 * scaleFactor4;
                        projectile.netUpdate = true;
                        return;
                    }
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (!Main.dayTime)
            {
                damage = (int)(damage * 1.5);
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (!Main.dayTime)
            {
                damage = (int)(damage * 1.5);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (!Main.dayTime)
            {
                damage = (int)(damage * 1.5);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bool flag = false;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * -1;
                flag = true;
            }
            if (projectile.velocity.Y != oldVelocity.Y || projectile.velocity.Y == 0f)
            {
                projectile.velocity.Y = oldVelocity.Y * -1 * 0.5f;
                flag = true;
            }
            if (flag)
            {
                float num12 = oldVelocity.Length() / projectile.velocity.Length();
                if (num12 == 0f)
                {
                    num12 = 1f;
                }
                projectile.velocity /= num12;
                if (projectile.ai[0] == 7f && projectile.velocity.Y < -0.1)
                {
                    projectile.velocity.Y = projectile.velocity.Y + 0.1f;
                }
                if (projectile.ai[0] >= 6f && projectile.ai[0] < 9f)
                {
                    Collision.HitTiles(projectile.position, oldVelocity, projectile.width, projectile.height);
                }
            }
            return false;
        }
    }
}