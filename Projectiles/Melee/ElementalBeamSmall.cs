using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class ElementalBeamSmall : ModProjectile
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
                        return mod.BuffType("Chroma");
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
        public override string Texture => "SinsMod/Projectiles/Melee/ElementalBeam";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Beam");
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.melee = true;
            projectile.timeLeft = 180;
            projectile.penetrate = 5;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 3;
            projectile.scale = 0.8f;
            projectile.netUpdate = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D bodyTexture = mod.GetTexture("Extra/Projectile/ElementalBeam_Extra");
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, num2, texture.Width, num), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(bodyTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, num2, texture.Width, num), projectile.GetAlpha(Main.DiscoColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 0.785f;
            int dustType = 267;
            if (projectile.ai[0] == 0 || projectile.ai[0] > 8)
            {
                dustType = 66;
            }
            int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 0, Main.DiscoColor, projectile.scale * 1.6f);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 0.4f;
            Main.dust[d].noLight = true;
            Color color = GetColor();
            Lighting.AddLight(projectile.Center, color.R / 255, color.G / 255, color.B / 255);
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 40)
            {
                projectile.scale -= 0.02f;
            }
            if (projectile.scale < 0.01f)
            {
                projectile.Kill();
            }
            if (projectile.localAI[1] == 0f)
            {
                projectile.scale -= 0.02f;
                projectile.alpha += 30;
                if (projectile.alpha >= 250)
                {
                    projectile.alpha = 255;
                    projectile.localAI[1] = 1f;
                }
            }
            else if (projectile.localAI[1] == 1f)
            {
                projectile.scale += 0.02f;
                projectile.alpha -= 30;
                if (projectile.alpha <= 0)
                {
                    projectile.alpha = 0;
                    projectile.localAI[1] = 0f;
                }
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
                    canExplode = true;
                    canHeal = true;
                    canPenetrate = true;
                    canThroughTile = true;
                    canSpawnPro = true;
                    break;
            };
            if (canHoming)
            {
                projectile.localNPCHitCooldown = 0;
                float num = projectile.Center.X;
                float num2 = projectile.Center.Y;
                float num3 = 600;
                bool flag = false;
                for (int num4 = 0; num4 < Main.npc.Length; num4++)
                {
                    if (Main.npc[num4].CanBeChasedBy(projectile, false))
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
                    float num8 = 35f;
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
                    projectile.localNPCHitCooldown = 1;
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
            target.AddBuff(DebuffType, 300);
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
            target.AddBuff(DebuffType, 300);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(DebuffType, 300);
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
            for (int num = 0; num < 15; num++)
            {
                int dustType = 267;
                if (projectile.ai[0] == 0 || projectile.ai[0] > 8)
                {
                    dustType = 66;
                }
                float num2 = projectile.oldVelocity.X * (5f / num);
                float num3 = projectile.oldVelocity.Y * (5f / num);
                int num4 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num2, projectile.oldPosition.Y - num3), projectile.width, projectile.height, dustType, projectile.oldVelocity.X, projectile.oldVelocity.Y, 0, default(Color), 1.2f);
                Main.dust[num4].noGravity = true;
                Dust dust = Main.dust[num4];
                dust.velocity *= 0.08f;
                num4 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num2, projectile.oldPosition.Y - num3), projectile.width, projectile.height, dustType, projectile.oldVelocity.X, projectile.oldVelocity.Y, 0, default(Color), 1.0f);
                dust = Main.dust[num4];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
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
            Color color = GetColor();
            color.A = 127;
            return GetColor();
        }
    }
}