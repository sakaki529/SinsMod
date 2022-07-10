using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class ElementalShot : ModProjectile
    {
        private bool canHoming;
        private bool canExplode;
        private bool canx;
        private bool canHeal;
        private bool canPenetrate;
        private bool canBounce;
        private bool canThroughTile;
        private bool canSpawnPro;
        private int DebuffType
        {
            get
            {
                switch ((int)projectile.ai[0])
                {
                    case 1:
                        return BuffID.OnFire;
                    case 2:
                        return BuffID.Daybreak;
                    case 3:
                        return BuffID.Ichor;
                    case 4:
                        return BuffID.CursedInferno;
                    case 5:
                        return BuffID.Frostburn;
                    case 6:
                        return BuffID.Wet;
                    case 7:
                        return BuffID.ShadowFlame;
                    case 8:
                        return BuffID.Venom;
                    default:
                        return 0;
                };
            }
        }
        private Color GetColor()
        {
            Color color = Main.DiscoColor;
            switch ((int)projectile.ai[0])
            {
                case 1:
                    color = Color.Red;
                    break;
                case 2:
                    color = Color.Orange;
                    break;
                case 3:
                    color = Color.Yellow;
                    break;
                case 4:
                    color = Color.Green;
                    break;
                case 5:
                    color = Color.Turquoise;
                    break;
                case 6:
                    color = Color.Blue;
                    break;
                case 7:
                    color = Color.Indigo;
                    break;
                case 8:
                    color = Color.Magenta;
                    break;
                default:
                    break;
            };
            return color;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Shot");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 240;
            projectile.penetrate = 2;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = lightColor;
            color = projectile.GetAlpha(color);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                Color color2 = color;
                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value = projectile.oldPos[i];
                float num3 = projectile.oldRot[i];
                float num4 = (10 - i) / 10f;
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, color2, num3, origin, projectile.scale * num4, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int dustType = 267;
            if (projectile.ai[0] == 0 || projectile.ai[0] > 8)
            {
                dustType = 66;
            }
            int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 0, GetColor(), projectile.scale * 1.6f);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 0.4f;
            Main.dust[d].noLight = true;
            Color color = GetColor();
            Lighting.AddLight(projectile.Center, color.R / 255, color.G / 255, color.B / 255);
            if (projectile.localAI[0] == 0f)
            {
                Main.PlaySound(SoundID.Item60, projectile.position);
                projectile.localAI[0] = 1f;
            }
            if (projectile.alpha < 25)
            {
                projectile.alpha = 25;
            }
            else
            {
                projectile.alpha -= 20;
            }
            switch ((int)projectile.ai[0])
            {
                case 1:
                    canHoming = true;
                    break;
                case 2:
                    canExplode = true;
                    break;
                case 3:
                    break;
                case 4:
                    canHeal = true;
                    break;
                case 5:
                    canPenetrate = true;
                    break;
                case 6:
                    canBounce = true;
                    break;
                case 7:
                    canThroughTile = true;
                    break;
                case 8:
                    canSpawnPro = true;
                    break;
                default:
                    canHoming = projectile.ai[1] == 1;
                    canPenetrate = projectile.ai[1] == 0;
                    canExplode = true;
                    canHeal = true;
                    canThroughTile = true;
                    canSpawnPro = true;
                    projectile.localNPCHitCooldown = 15;
                    break;
            };
            if (canHoming)
            {
                float num = projectile.Center.X;
                float num2 = projectile.Center.Y;
                float num3 = 960f;
                bool flag = false;
                for (int num4 = 0; num4 < Main.npc.Length; num4++)
                {
                    bool canhit = true;
                    if ((int)projectile.ai[0] == 1)
                    {
                        canhit = Collision.CanHit(projectile.Center, 1, 1, Main.npc[num4].Center, 1, 1);
                    }
                    if (Main.npc[num4].CanBeChasedBy(projectile, false) && canhit)
                    {
                        float num5 = Main.npc[num4].position.X + (Main.npc[num4].width / 2);
                        float num6 = Main.npc[num4].position.Y + (Main.npc[num4].height / 2);
                        float num7 = Math.Abs(projectile.position.X + (projectile.width / 2) - num5) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num6);
                        if (num7 < num3)
                        {
                            num3 = num7;
                            num = num5;
                            num2 = num6;
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    float num8 = 24f;
                    Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float num9 = num - vector.X;
                    float num10 = num2 - vector.Y;
                    float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                    num11 = num8 / num11;
                    num9 *= num11;
                    num10 *= num11;
                    projectile.velocity.X = (projectile.velocity.X * 14f + num9) / 15f;
                    projectile.velocity.Y = (projectile.velocity.Y * 14f + num10) / 15f;
                }
            }
            if (canPenetrate)
            {
                projectile.penetrate = -1;
                if ((int)projectile.ai[0] == 5)
                {
                    projectile.localNPCHitCooldown = 3;
                }
            }
            if (canBounce)
            {
                projectile.tileCollide = true;
            }
            if (canThroughTile)
            {
                projectile.tileCollide = false;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (canSpawnPro && projectile.ai[1] == 0)
            {
                float shootSpeed = 16f;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float num = Main.mouseX + Main.screenPosition.X - vector.X;
                float num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                if (player.gravDir == -1f)
                {
                    num2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
                }
                float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                if ((float.IsNaN(num) && float.IsNaN(num2)) || (num == 0f && num2 == 0f))
                {
                    num = player.direction;
                    num2 = 0f;
                    num3 = shootSpeed;
                }
                else
                {
                    num3 = shootSpeed / num3;
                }
                num *= num3;
                num2 *= num3;
                for (int i = 0; i < 2; i++)
                {
                    vector = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    vector.X = (vector.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                    vector.Y -= 100 * i;
                    Vector2 vector2 = target.Center - vector;
                    vector2.Normalize();
                    float speedX = vector2.X * 16;
                    float speedY = vector2.Y * 16 + Main.rand.Next(-40, 41) * 0.02f;
                    int num7 = Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, (int)projectile.ai[0] == 8 ? Main.rand.Next(9) : 0, 1f);
                    Main.projectile[num7].tileCollide = false;
                }
            }
            switch ((int)projectile.ai[0])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    target.AddBuff(DebuffType, 300);
                    break;
                default:
                    target.AddBuff(BuffID.OnFire, 120);
                    target.AddBuff(BuffID.Daybreak, 120);
                    target.AddBuff(BuffID.Ichor, 120);
                    target.AddBuff(BuffID.CursedInferno, 120);
                    target.AddBuff(BuffID.Frostburn, 120);
                    target.AddBuff(BuffID.Wet, 120);
                    target.AddBuff(BuffID.ShadowFlame, 120);
                    target.AddBuff(BuffID.Venom, 120);
                    break;
            };
            if (canHeal)
            {
                if (!Main.player[projectile.owner].moonLeech)
                {
                    if (target.type != NPCID.TargetDummy)
                    {
                        float num = damage * 0.004f;
                        if ((int)num <= 0)
                        {
                            return;
                        }
                        Main.player[Main.myPlayer].statLife += (int)num;
                        Main.player[Main.myPlayer].HealEffect((int)num);
                    }
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (canSpawnPro && projectile.ai[1] == 0)
            {
                float shootSpeed = 16f;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float num = Main.mouseX + Main.screenPosition.X - vector.X;
                float num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                if (player.gravDir == -1f)
                {
                    num2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
                }
                float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                if ((float.IsNaN(num) && float.IsNaN(num2)) || (num == 0f && num2 == 0f))
                {
                    num = player.direction;
                    num2 = 0f;
                    num3 = shootSpeed;
                }
                else
                {
                    num3 = shootSpeed / num3;
                }
                num *= num3;
                num2 *= num3;
                for (int i = 0; i < 2; i++)
                {
                    vector = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    vector.X = (vector.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                    vector.Y -= 100 * i;
                    Vector2 vector2 = target.Center - vector;
                    vector2.Normalize();
                    float speedX = vector2.X * 16;
                    float speedY = vector2.Y * 16 + Main.rand.Next(-40, 41) * 0.02f;
                    int num7 = Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, (int)projectile.ai[0] == 8 ? Main.rand.Next(9) : 0, 1f);
                    Main.projectile[num7].tileCollide = false;
                }
            }
            switch ((int)projectile.ai[0])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    target.AddBuff(DebuffType, 300);
                    break;
                default:
                    target.AddBuff(BuffID.OnFire, 120);
                    target.AddBuff(BuffID.Daybreak, 120);
                    target.AddBuff(BuffID.Ichor, 120);
                    target.AddBuff(BuffID.CursedInferno, 120);
                    target.AddBuff(BuffID.Frostburn, 120);
                    target.AddBuff(BuffID.Wet, 120);
                    target.AddBuff(BuffID.ShadowFlame, 120);
                    target.AddBuff(BuffID.Venom, 120);
                    break;
            };
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (canSpawnPro && projectile.ai[1] == 0)
            {
                float shootSpeed = 16f;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float num = Main.mouseX + Main.screenPosition.X - vector.X;
                float num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                if (player.gravDir == -1f)
                {
                    num2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
                }
                float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                if ((float.IsNaN(num) && float.IsNaN(num2)) || (num == 0f && num2 == 0f))
                {
                    num = player.direction;
                    num2 = 0f;
                    num3 = shootSpeed;
                }
                else
                {
                    num3 = shootSpeed / num3;
                }
                num *= num3;
                num2 *= num3;
                for (int i = 0; i < 2; i++)
                {
                    vector = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    vector.X = (vector.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                    vector.Y -= 100 * i;
                    Vector2 vector2 = target.Center - vector;
                    vector2.Normalize();
                    float speedX = vector2.X * 16;
                    float speedY = vector2.Y * 16 + Main.rand.Next(-40, 41) * 0.02f;
                    int num7 = Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, (int)projectile.ai[0] == 8 ? Main.rand.Next(9) : 0, 1f);
                    Main.projectile[num7].tileCollide = false;
                }
            }
            switch ((int)projectile.ai[0])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    target.AddBuff(DebuffType, 300);
                    break;
                default:
                    target.AddBuff(BuffID.OnFire, 120);
                    target.AddBuff(BuffID.Daybreak, 120);
                    target.AddBuff(BuffID.Ichor, 120);
                    target.AddBuff(BuffID.CursedInferno, 120);
                    target.AddBuff(BuffID.Frostburn, 120);
                    target.AddBuff(BuffID.Wet, 120);
                    target.AddBuff(BuffID.ShadowFlame, 120);
                    target.AddBuff(BuffID.Venom, 120);
                    break;
            };
            if (canHeal)
            {
                if (!Main.player[projectile.owner].moonLeech)
                {
                    float num = damage * 0.004f;
                    if ((int)num <= 0)
                    {
                        return;
                    }
                    Main.player[Main.myPlayer].statLife += (int)num;
                    Main.player[Main.myPlayer].HealEffect((int)num);
                }
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (canBounce)
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                return false;
            }
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
            {
                float num = Main.rand.Next(-100, 101);
                float num2 = Main.rand.Next(-100, 101);
                float num3 = Main.rand.Next(10, 16);
                float num4 = (float)Math.Sqrt(num * num + num2 * num2);
                num4 = num3 / num4;
                num *= num4;
                num2 *= num4;
                int dustType = 267;
                if (projectile.ai[0] == 0 || projectile.ai[0] > 8)
                {
                    dustType = 66;
                }
                int NewDust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, 0f, 0f, 0, GetColor(), 1.0f);
                Main.dust[NewDust].noGravity = true;
                Main.dust[NewDust].position.X = projectile.Center.X;
                Main.dust[NewDust].position.Y = projectile.Center.Y;
                Dust dust = Main.dust[NewDust];
                dust.position.X = dust.position.X + Main.rand.Next(-7, 8);
                Dust dust2 = Main.dust[NewDust];
                dust2.position.Y = dust2.position.Y + Main.rand.Next(-7, 8);
                Main.dust[NewDust].velocity.X = num;
                Main.dust[NewDust].velocity.Y = num2;
                Dust dust3 = Main.dust[NewDust];
                dust3.velocity.X = dust3.velocity.X * 0.7f;
                Dust dust4 = Main.dust[NewDust];
                dust4.velocity.Y = dust4.velocity.Y * 0.7f;
            }
            if (canExplode)
            {
                Main.PlaySound(SoundID.Item14, projectile.Center);
                projectile.penetrate = -1;
                projectile.localNPCHitCooldown = 0;
                projectile.position = projectile.Center;
                projectile.width = 92;
                projectile.height = 92;
                projectile.position.X = projectile.position.X - projectile.width / 2;
                projectile.position.Y = projectile.position.Y - projectile.height / 2;
                int num;
                for (int num2 = 0; num2 < 30; num2 = num + 1)
                {
                    int num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                    Dust dust = Main.dust[num3];
                    dust.velocity *= 1.4f;
                    num = num2;
                }
                for (int num4 = 0; num4 < 20; num4 = num + 1)
                {
                    int num5 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
                    Main.dust[num5].noGravity = true;
                    Dust dust = Main.dust[num5];
                    dust.velocity *= 7f;
                    num5 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                    dust = Main.dust[num5];
                    dust.velocity *= 3f;
                    num = num4;
                }
                for (int num6 = 0; num6 < 2; num6 = num + 1)
                {
                    float scaleFactor = 0.4f;
                    if (num6 == 1)
                    {
                        scaleFactor = 0.8f;
                    }
                    int num7 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Gore gore = Main.gore[num7];
                    gore.velocity *= scaleFactor;
                    Gore gore2 = Main.gore[num7];
                    gore2.velocity.X = gore2.velocity.X + 1f;
                    Gore gore3 = Main.gore[num7];
                    gore3.velocity.Y = gore3.velocity.Y + 1f;
                    num7 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    gore = Main.gore[num7];
                    gore.velocity *= scaleFactor;
                    Gore gore4 = Main.gore[num7];
                    gore4.velocity.X = gore4.velocity.X - 1f;
                    Gore gore5 = Main.gore[num7];
                    gore5.velocity.Y = gore5.velocity.Y + 1f;
                    num7 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    gore = Main.gore[num7];
                    gore.velocity *= scaleFactor;
                    Gore gore6 = Main.gore[num7];
                    gore6.velocity.X = gore6.velocity.X + 1f;
                    Gore gore7 = Main.gore[num7];
                    gore7.velocity.Y = gore7.velocity.Y - 1f;
                    num7 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    gore = Main.gore[num7];
                    gore.velocity *= scaleFactor;
                    Gore gore8 = Main.gore[num7];
                    gore8.velocity.X = gore8.velocity.X - 1f;
                    Gore gore9 = Main.gore[num7];
                    gore9.velocity.Y = gore9.velocity.Y - 1f;
                    num = num6;
                }
                projectile.Damage();
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return GetColor() * (1f - projectile.alpha / 255f);
        }
    }
}