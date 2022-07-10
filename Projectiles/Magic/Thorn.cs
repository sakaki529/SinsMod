using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class Thorn : ModProjectile
    {
        private bool ThornHead;
        private int DebuffTime
        {
            get
            {
                if ((int)projectile.ai[0] == mod.ItemType("BlackLotus"))
                {
                    return 600;
                }
                if ((int)projectile.ai[0] == mod.ItemType("CherryBloom"))
                {
                    return 300;
                }
                if ((int)projectile.ai[0] == mod.ItemType("GardenOfEden"))
                {
                    return 300;
                }
                if ((int)projectile.ai[0] == mod.ItemType("MidnightStaff"))
                {
                    return 180;
                }
                if ((int)projectile.ai[0] == mod.ItemType("NightfallStaff"))
                {
                    return 120;
                }
                if ((int)projectile.ai[0] == mod.ItemType("NightmareStaff"))
                {
                    return 300;
                }
                if ((int)projectile.ai[0] == mod.ItemType("PolarNightStaff"))
                {
                    return 30;
                }
                if ((int)projectile.ai[0] == mod.ItemType("SubterraneanRose"))
                {
                    return 300;
                }
                if ((int)projectile.ai[0] == mod.ItemType("TrueMidnightStaff"))
                {
                    return 240;
                }
                if ((int)projectile.ai[0] == mod.ItemType("WhiteNightStaff"))
                {
                    return 60;
                }
                return 300;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorn");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.magic = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = 320;
            //projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 20;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            if ((int)projectile.ai[0] == mod.ItemType("BlackLotus"))
            {
                texture = mod.GetTexture("Extra/Projectile/LotusThorn");
            }
            if ((int)projectile.ai[0] == mod.ItemType("CherryBloom"))
            {
                texture = mod.GetTexture("Extra/Projectile/CherryThorn");
            }
            if ((int)projectile.ai[0] == mod.ItemType("SubterraneanRose"))
            {
                texture = mod.GetTexture("Extra/Projectile/SubterraneanThorn");
            }
            SpriteEffects spriteEffects = SpriteEffects.None;
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            int maxLength = 20;
            int type = 0;
            double damageMultiplier = 2.0;
            int fadein = 90;
            int fadeout = 4;
            bool usesRandomLength = true;
            int randMaxValue = 7;
            int randMinValue = 1;
            if ((int)projectile.ai[0] == mod.ItemType("BlackLotus"))
            {
                projectile.localNPCHitCooldown = 8;
                maxLength = 16;
                type = mod.ProjectileType("BlackLotus");
                fadein = 90;
                fadeout = 16;
            }
            if ((int)projectile.ai[0] == mod.ItemType("CherryBloom"))
            {
                projectile.localNPCHitCooldown = 10;
                maxLength = 10;
                type = mod.ProjectileType("CherryBloom");
                fadein = 90;
                fadeout = 4;
                randMaxValue = 4;
            }
            if ((int)projectile.ai[0] == mod.ItemType("GardenOfEden"))
            {
                maxLength = 18;
                type = mod.ProjectileType("FlowerOfEden");
                fadein = 90;
                fadeout = 10;
            }
            if ((int)projectile.ai[0] == mod.ItemType("MidnightStaff"))
            {
                type = mod.ProjectileType("MidnightRose");
                maxLength = 18;
            }
            if ((int)projectile.ai[0] == mod.ItemType("NightfallStaff"))
            {
                type = mod.ProjectileType("NightfallRose");
                maxLength = 16;
                randMaxValue = 6;
            }
            if ((int)projectile.ai[0] == mod.ItemType("NightmareStaff"))
            {
                type = mod.ProjectileType("NightmareRose");
            }
            if ((int)projectile.ai[0] == mod.ItemType("PolarNightStaff"))
            {
                type = mod.ProjectileType("PolarNightRose");
                maxLength = 10;
                randMaxValue = 4;
            }
            if ((int)projectile.ai[0] == mod.ItemType("SubterraneanRose"))
            {
                projectile.usesLocalNPCImmunity = false;
                projectile.usesIDStaticNPCImmunity = true;
                projectile.idStaticNPCHitCooldown = 10;
                maxLength = 12;
                type = mod.ProjectileType("SubterraneanRose");
                damageMultiplier = 0.5;
                fadein = 90;
                fadeout = 8;
                usesRandomLength = false;
            }
            if ((int)projectile.ai[0] == mod.ItemType("TrueMidnightStaff"))
            {
                type = mod.ProjectileType("TrueMidnightRose");
            }
            if ((int)projectile.ai[0] == mod.ItemType("WhiteNightStaff"))
            {
                type = mod.ProjectileType("WhiteNightRose");
                maxLength = 14;
                randMaxValue = 5;
            }
            ThornHead = (int)projectile.ai[1] >= maxLength;
            if (ThornHead)
            {
                projectile.frame = 0;
            }
            else
            {
                if (projectile.ai[1] % 2 == 0)
                {
                    projectile.frame = 1;
                }
                else
                {
                    projectile.frame = 2;
                }
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                projectile.alpha -= fadein;
                if (projectile.alpha <= 0)
                {
                    if (ThornHead && (int)projectile.ai[0] == mod.ItemType("SubterraneanRose"))
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/tan").WithVolume(0.4f), projectile.Center);
                        for (int i = 0; i < 30; i++)
                        {
                            Vector2 vector = Utils.RotatedBy(Vector2.UnitX, MathHelper.Lerp(0f, 6.28318548f, i / 30f), default(Vector2));
                            Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y) + vector * 4f, vector * 4f, type, (int)(projectile.damage * damageMultiplier), projectile.knockBack, projectile.owner, ((SinsPlayer)player.GetModPlayer(mod, "SinsPlayer")).subterraneanCount, -i * 2);
                        }
                        ((SinsPlayer)player.GetModPlayer(mod, "SinsPlayer")).subterraneanCount += 1;
                    }
                    else if (ThornHead && type != 0)
                    {
                        Projectile.NewProjectile(projectile.Center, Vector2.Zero, type, (int)(projectile.damage * damageMultiplier), projectile.knockBack, projectile.owner, 0f, 0f);
                    }
                    projectile.alpha = 0;
                    projectile.localAI[0] = 1f;
                    if (projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] += usesRandomLength ? Main.rand.Next(randMinValue, randMaxValue) : 1f;
                        projectile.position += projectile.velocity;
                    }
                    if (projectile.ai[1] < maxLength && Main.myPlayer == projectile.owner)
                    {
                        Vector2 vector = projectile.velocity;
                        int num2 = Projectile.NewProjectile(projectile.Center.X + projectile.velocity.X, projectile.Center.Y + projectile.velocity.Y, vector.X, vector.Y, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, (int)projectile.ai[0]);
                        Main.projectile[num2].ai[1] = projectile.ai[1] + 1f;
                        NetMessage.SendData(27, -1, -1, null, num2, 0f, 0f, 0f, 0);
                        projectile.position -= projectile.velocity;
                        return;
                    }
                }
            }
            else
            {
                if (projectile.alpha < 170 && projectile.alpha + fadeout >= 170)//fadeout == 5
                {
                    if ((int)projectile.ai[0] == mod.ItemType("BlackLotus"))
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Dust.NewDust(projectile.position, projectile.width, projectile.height, 18, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f, 170, default(Color), 1.2f);
                        }
                        Dust.NewDust(projectile.position, projectile.width, projectile.height, 14, 0f, 0f, 170, default(Color), 1.1f);
                    }
                    else if ((int)projectile.ai[0] == mod.ItemType("CherryBloom"))
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 7, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f, 200, default(Color), 1.3f);
                            Main.dust[num].noGravity = true;
                            Dust dust = Main.dust[num];
                            dust.velocity *= 0.5f;
                        }
                    }
                    else if ((int)projectile.ai[0] == mod.ItemType("SubterraneanRose"))
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 97, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f, 200, default(Color), 1.3f);
                            Main.dust[num].noGravity = true;
                            Dust dust = Main.dust[num];
                            dust.velocity *= 0.5f;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 3, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f, 200, default(Color), 1.3f);
                            Main.dust[num].noGravity = true;
                            Dust dust = Main.dust[num];
                            dust.velocity *= 0.5f;
                        }
                    }
                }
                projectile.alpha += fadeout;
                if (projectile.alpha >= 220)
                {
                    projectile.friendly = false;
                }
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                    return;
                }
            }
            projectile.position -= projectile.velocity;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(BuffID.Poisoned, DebuffTime);
            target.AddBuff(BuffID.Venom, DebuffTime);
            target.AddBuff(BuffID.Bleeding, DebuffTime);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, DebuffTime);
            target.AddBuff(BuffID.Venom, DebuffTime);
            target.AddBuff(BuffID.Bleeding, DebuffTime);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(BuffID.Poisoned, DebuffTime);
            target.AddBuff(BuffID.Venom, DebuffTime);
            target.AddBuff(BuffID.Bleeding, DebuffTime);
        }
    }
}