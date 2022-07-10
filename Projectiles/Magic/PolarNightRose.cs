﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class PolarNightRose : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rose");
        }
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.magic = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = 2;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.hide = true;
            projectile.alpha = 255;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0f)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Bloom").WithVolume(0.75f), projectile.Center);
                Vector2 vector = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2();
                float num = Main.rand.Next(5, 9);
                float num2 = Main.rand.Next(12, 17);
                float value = Main.rand.Next(3, 7);
                float num3 = 20f;
                for (float num4 = 0f; num4 < num; num4++)
                {
                    for (int num5 = 0; num5 < 2; num5++)
                    {
                        Vector2 value2 = vector.RotatedBy(((num5 == 0) ? 1f : -1f) * 6.28318548f / (num * 2f));
                        for (float num6 = 0f; num6 < num3; num6++)
                        {
                            Vector2 value3 = Vector2.Lerp(vector, value2, num6 / num3);
                            float scaleFactor = MathHelper.Lerp(num2, value, num6 / num3);
                            int num7 = Dust.NewDust(projectile.Center, 6, 6, 186, 0f, 0f, 100, default, 1.3f);
                            Main.dust[num7].velocity *= 0.1f;
                            Main.dust[num7].noGravity = true;
                            Main.dust[num7].velocity += value3 * scaleFactor;
                            Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                        }
                    }
                    vector = vector.RotatedBy(6.28318548f / num);
                }
                for (float num8 = 0f; num8 < num; num8++)
                {
                    for (int num9 = 0; num9 < 2; num9++)
                    {
                        Vector2 value4 = vector.RotatedBy(((num9 == 0) ? 1f : -1f) * 6.28318548f / (num * 2f));
                        for (float num10 = 0f; num10 < num3; num10++)
                        {
                            Vector2 value5 = Vector2.Lerp(vector, value4, num10 / num3);
                            float scaleFactor2 = MathHelper.Lerp(num2, value, num10 / num3) / 2f;
                            int num11 = Dust.NewDust(projectile.Center, 6, 6, 186, 0f, 0f, 100, default, 1.3f);
                            Main.dust[num11].velocity *= 0.1f;
                            Main.dust[num11].noGravity = true;
                            Main.dust[num11].velocity += value5 * scaleFactor2;
                            Main.dust[num11].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                        }
                    }
                    vector = vector.RotatedBy(6.28318548f / num);
                }
                for (int num12 = 0; num12 < 100; num12++)
                {
                    float num13 = num2;
                    int num14 = Dust.NewDust(projectile.Center, 6, 6, 46, 0f, 0f, 100);
                    float num15 = Main.dust[num14].velocity.X;
                    float num16 = Main.dust[num14].velocity.Y;
                    if (num15 == 0f && num16 == 0f)
                    {
                        num15 = 1f;
                    }
                    float num17 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
                    num17 = num13 / num17;
                    num15 *= num17;
                    num16 *= num17;
                    Main.dust[num14].velocity *= 0.5f;
                    Dust dust = Main.dust[num14];
                    dust.velocity.X += num15;
                    Dust dust2 = Main.dust[num14];
                    dust2.velocity.Y += num16;
                    Main.dust[num14].scale = 1.3f;
                    Main.dust[num14].noGravity = true;
                }
                for (float num18 = 0f; num18 < num; num18++)
                {
                    for (int num19 = 0; num19 < 2; num19++)
                    {
                        Vector2 value4 = vector.RotatedBy(((num19 == 0) ? 1f : -1f) * 6.28318548f / (num * 2f));
                        for (float num20 = 0f; num20 < num3; num20++)
                        {
                            Vector2 value6 = Vector2.Lerp(vector, value4, num20 / num3);
                            float scaleFactor3 = MathHelper.Lerp(num2, value, num20 / num3) / 2f;
                            int num21 = Dust.NewDust(projectile.Center, 6, 6, 245, 0f, 0f, 100, default, 1.3f);
                            Main.dust[num21].velocity *= 0.1f;
                            Main.dust[num21].noGravity = true;
                            Main.dust[num21].velocity += value6 * scaleFactor3;
                            Main.dust[num21].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                        }
                    }
                    vector = vector.RotatedBy(6.28318548f / num);
                }
                projectile.ai[0] = 1f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
    }
}