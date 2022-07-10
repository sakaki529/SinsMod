using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class Yozakura : ModProjectile
    {
        private float DistX;
        private float DistY;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yozakura");
            DisplayName.AddTranslation(GameCulture.Chinese, "ñÈç˜");
            Main.projFrames[mod.ProjectileType("Yozakura")] = 28;
        }
        public override void SetDefaults()
        {
            projectile.width = 68;
            projectile.height = 64;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            //projectile.aiStyle = 75;
            projectile.alpha = 80;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
        }
        public override void AI()
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].hostile && Main.projectile[i].damage > 0 && projectile.Hitbox.Intersects(Main.projectile[i].Hitbox))
                {
                    Main.projectile[i].velocity *= -1;
                    Main.projectile[i].friendly = true;
                    Main.projectile[i].hostile = false;
                    Main.projectile[i].damage *= 10;
                }
            }
			if (Main.myPlayer == projectile.owner)
            {
                Player player = Main.player[projectile.owner];
                float num = 1.57079637f;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                if (projectile.type == mod.ProjectileType("Yozakura"))
                {
                    num = 0f;
                    if (projectile.spriteDirection == -1)
                    {
                        num = 3.14159274f;
                    }
                    if (++projectile.frame >= Main.projFrames[projectile.type])
                    {
                        projectile.frame = 0;
                    }
                    projectile.soundDelay--;
                    if (projectile.soundDelay <= 0)
                    {
                        Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 1);
                        projectile.soundDelay = 6;
                    }
                    if (Main.myPlayer == projectile.owner)
                    {
                        if (player.channel && !player.noItems && !player.CCed)
                        {
                            float scaleFactor6 = 1f;
                            if (player.inventory[player.selectedItem].shoot == projectile.type)
                            {
                                scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                            }
                            Vector2 vector13 = Main.MouseWorld - vector;
                            vector13.Normalize();
                            if (vector13.HasNaNs())
                            {
                                vector13 = Vector2.UnitX * (float)player.direction;
                            }
                            vector13 *= scaleFactor6;
                            if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                            {
                                projectile.netUpdate = true;
                            }
                            projectile.velocity = vector13;
                        }
                        else
                        {
                            projectile.Kill();
                        }
                    }
                    Vector2 vector14 = projectile.Center + projectile.velocity;// * 2f;
                    Lighting.AddLight(vector14, 0.8f, 0.8f, 0.8f);
                    if (Main.rand.Next(3) == 0)
                    {
                        int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, 72, projectile.velocity.X, projectile.velocity.Y, 255, new Color(255, 255, 255), 2f);
                        Main.dust[num30].noGravity = true;
                        Main.dust[num30].position += projectile.velocity * 5;
                        Main.dust[num30].velocity += projectile.velocity * 2;
                    }
                }
                projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 1.0f;
                projectile.rotation = projectile.velocity.ToRotation() + num;
                projectile.spriteDirection = projectile.direction;
                projectile.timeLeft = 2;
                player.ChangeDir(projectile.direction);
                player.heldProj = projectile.whoAmI;
                player.itemTime = 2;
                player.itemAnimation = 2;
                player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);

                //Do net updatey thing. Syncs this projectile.
                projectile.netUpdate = true;
                Vector2 mouse = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
                if (Main.player[projectile.owner].Center.Y < mouse.Y)
                {
                    float Xdis = Main.player[Main.myPlayer].Center.X - mouse.X;  // change myplayer to nearest player in full version
                    float Ydis = Main.player[Main.myPlayer].Center.Y - mouse.Y; // change myplayer to nearest player in full version
                    float Angle = (float)Math.Atan(Xdis / Ydis);
                    float DistXT = (float)(Math.Sin(Angle) * 29);
                    float DistYT = (float)(Math.Cos(Angle) * 29);
                    projectile.position.X = Main.player[projectile.owner].Center.X + DistXT - 33;
                    projectile.position.Y = Main.player[projectile.owner].Center.Y + DistYT - 33;
                }
                if (Main.player[projectile.owner].Center.Y >= mouse.Y)
                {
                    float Xdis = Main.player[Main.myPlayer].Center.X - mouse.X;  // change myplayer to nearest player in full version
                    float Ydis = Main.player[Main.myPlayer].Center.Y - mouse.Y; // change myplayer to nearest player in full version
                    float Angle = (float)Math.Atan(Xdis / Ydis);
                    float DistXT = (float)(Math.Sin(Angle) * 29);
                    float DistYT = (float)(Math.Cos(Angle) * 29);
                    projectile.position.X = Main.player[projectile.owner].Center.X + (0 - DistXT) - 33;
                    projectile.position.Y = Main.player[projectile.owner].Center.Y + (0 - DistYT) - 33;
                }
                float x = player.Center.X + 2f * Main.rand.Next(-650, 651);
                float y = player.Center.Y - 1250f;
                projectile.localAI[0]++;
                if (projectile.localAI[0] > 1)
                {
                    Projectile.NewProjectile(x, y, 0f, 9f, mod.ProjectileType("Yozakura2"), projectile.damage, projectile.knockBack, projectile.owner);
                    projectile.localAI[0] = 0;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type != NPCID.TargetDummy)
            {
                projectile.damage += Main.rand.Next(1, 3);
            }
            if (!Main.player[projectile.owner].moonLeech)
            {
                if (target.type != NPCID.TargetDummy)
                {
                    float num = damage * 0.004f;
                    if ((int)num == 0)
                    {
                        return;
                    }
                    int num2 = projectile.owner;
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SakuraHeal"), 0, 0f, projectile.owner, num2, num);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.damage += 10;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            projectile.damage += 10;
            if (!Main.player[projectile.owner].moonLeech)
            {
                float num = damage * 0.0143f;
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
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}