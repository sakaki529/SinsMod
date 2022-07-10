using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace SinsMod.Projectiles.Melee
{
    public class Galaxia : ModProjectile
    {
        private float DistX;
        private float DistY;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galaxia");
            Main.projFrames[projectile.type] = 28;
        }
        public override void SetDefaults()
        {
            projectile.width = 68;
            projectile.height = 64;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.aiStyle = 75;
            projectile.alpha = 180;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override void AI()
        {
			if (Main.myPlayer == projectile.owner)
            {
                Player player = Main.player[projectile.owner];
                float num = 1.57079637f;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                if (projectile.type == mod.ProjectileType("Galaxia"))
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
                        Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 1);
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
                            int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, 57, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 2f);
                            Main.dust[num30].noGravity = true;
                            Main.dust[num30].position += projectile.velocity * 55;
                            Main.dust[num30].velocity += projectile.velocity * 45;
                      }
                }
                 projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 1f;
                 projectile.rotation = projectile.velocity.ToRotation() + num;
                 projectile.spriteDirection = projectile.direction;
                 projectile.timeLeft = 2;
                 player.ChangeDir(projectile.direction);
                 player.heldProj = projectile.whoAmI;
                 player.itemTime = 2;
                 player.itemAnimation = 2;
                 player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));

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
                    projectile.position.X = (Main.player[projectile.owner].Center.X + DistXT) - 34;
                    projectile.position.Y = (Main.player[projectile.owner].Center.Y + DistYT) - 30;
                }
                if (Main.player[projectile.owner].Center.Y >= mouse.Y)
                {
                    float Xdis = Main.player[Main.myPlayer].Center.X - mouse.X;  // change myplayer to nearest player in full version
                    float Ydis = Main.player[Main.myPlayer].Center.Y - mouse.Y; // change myplayer to nearest player in full version
                    float Angle = (float)Math.Atan(Xdis / Ydis);
                    float DistXT = (float)(Math.Sin(Angle) * 29);
                    float DistYT = (float)(Math.Cos(Angle) * 29);
                    projectile.position.X = (Main.player[projectile.owner].Center.X + (0 - DistXT)) - 34;
                    projectile.position.Y = (Main.player[projectile.owner].Center.Y + (0 - DistYT)) - 30;
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
        //public override void AI()
        //{
        //    Player player = Main.player[projectile.owner];
        //    float num = 1.57079637f;
        //    Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
        //    if (projectile.type == mod.ProjectileType("CallandorSlice"))
        //    {
        //        num = 0f;
        //        if (projectile.spriteDirection == -1)
        //        {
        //            num = 3.14159274f;
        //        }
        //        if (++projectile.frame >= Main.projFrames[projectile.type])
        //        {
        //            projectile.frame = 0;
        //        }
        //        projectile.soundDelay--;
        //        if (projectile.soundDelay <= 0)
        //        {
        //            Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 1);
        //            projectile.soundDelay = 12;
        //        }
        //        if (Main.myPlayer == projectile.owner)
        //        {
        //            if (player.channel && !player.noItems && !player.CCed)
        //            {
        //                float scaleFactor6 = 1f;
        //                if (player.inventory[player.selectedItem].shoot == projectile.type)
        //                {
        //                    scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
        //                }
        //                Vector2 vector13 = Main.MouseWorld - vector;
        //                vector13.Normalize();
        //                if (vector13.HasNaNs())
        //                {
        //                    vector13 = Vector2.UnitX * (float)player.direction;
        //                }
        //                vector13 *= scaleFactor6;
        //                if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
        //                {
        //                    projectile.netUpdate = true;
        //                }
        //                projectile.velocity = vector13;
        //            }
        //            else
        //            {
        //                projectile.Kill();
        //            }
        //        }
        //        Vector2 vector14 = projectile.Center + projectile.velocity;// * 2f;
        //        Lighting.AddLight(vector14, 0.8f, 0.8f, 0.8f);
        //        //                if (Main.rand.Next(3) == 0)
        //        //                {
        //        //                    int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, 63, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 2f);
        //        //                    Main.dust[num30].noGravity = true;
        //        //                    Main.dust[num30].position -= projectile.velocity;
        //        //                }
        //    }
        //    projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
        //    projectile.rotation = projectile.velocity.ToRotation() + num;
        //    projectile.spriteDirection = projectile.direction;
        //    projectile.timeLeft = 2;
        //    player.ChangeDir(projectile.direction);
        //    player.heldProj = projectile.whoAmI;
        //    player.itemTime = 2;
        //    player.itemAnimation = 2;
        //    player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
        //}

    }
}