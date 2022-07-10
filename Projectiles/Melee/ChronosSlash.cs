using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class ChronosSlash : ModProjectile
	{
        private int Count = 6;
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slash");
		}
		public override void SetDefaults()
		{
            projectile.width = 4;
			projectile.height = 4;
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
        public override bool CanDamage()
        {
            return (((int)(projectile.ai[0] - 1f) / Count != 0 || Count >= 3) && projectile.ai[1] >= 5f) || projectile.ai[0] == 0f;
        }
        public override bool PreAI()
        {
            projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.03f;
            if (projectile.ai[0] == 0f)
            {
                projectile.tileCollide = true;
                projectile.localAI[0] += 1f;
                float num3 = 0.01f;
                int num4 = 5;
                int num5 = num4 * 15;
                int num6 = 0;
                if (projectile.localAI[0] > 7f)
                {
                    if (projectile.localAI[1] == 0f)
                    {
                        projectile.scale -= num3;
                        projectile.alpha += num4;
                        if (projectile.alpha > num5)
                        {
                            projectile.alpha = num5;
                            projectile.localAI[1] = 1f;
                        }
                    }
                    else
                    {
                        if (projectile.localAI[1] == 1f)
                        {
                            projectile.scale += num3;
                            projectile.alpha -= num4;
                            if (projectile.alpha <= num6)
                            {
                                projectile.alpha = num6;
                                projectile.localAI[1] = 0f;
                            }
                        }
                    }
                }
            }
            else
            {
                if (projectile.ai[0] >= 1f && projectile.ai[0] < (1 + Count))
                {
                    projectile.tileCollide = false;
                    projectile.alpha += 15;
                    projectile.velocity *= 0.98f;
                    projectile.localAI[0] = 0f;
                    if (projectile.alpha >= 255)
                    {
                        if (projectile.ai[0] == 1f)
                        {
                            projectile.Kill();
                            return false;
                        }
                        int num7 = -1;
                        Vector2 value = projectile.Center;
                        float num8 = 300f;
                        int num10;
                        for (int i = 0; i < 200; i = num10 + 1)
                        {
                            NPC nPC = Main.npc[i];
                            if (nPC.CanBeChasedBy(projectile, false))
                            {
                                Vector2 center = nPC.Center;
                                float num9 = Vector2.Distance(center, projectile.Center);
                                if (num9 < num8)
                                {
                                    num8 = num9;
                                    value = center;
                                    num7 = i;
                                }
                            }
                            num10 = i;
                        }
                        if (num7 >= 0)
                        {
                            projectile.netUpdate = true;
                            projectile.ai[0] += Count;
                            projectile.position = value + Utils.ToRotationVector2((float)Main.rand.NextDouble() * 6.28318548f) * 100f - new Vector2(projectile.width, projectile.height) / 2f;
                            projectile.velocity = Vector2.Normalize(value - projectile.Center) * 18f;
                        }
                        else
                        {
                            projectile.Kill();
                        }
                    }
                }
                else
                {
                    if (projectile.ai[0] >= (1 + Count) && projectile.ai[0] < (1 + Count * 2))
                    {
                        projectile.scale = 0.9f;
                        projectile.tileCollide = false;
                        projectile.ai[1] += 1f;
                        if (projectile.ai[1] >= 15f)
                        {
                            projectile.alpha += 51;
                            projectile.velocity *= 0.8f;
                            if (projectile.alpha >= 255)
                            {
                                projectile.Kill();
                            }
                        }
                        else
                        {
                            projectile.alpha -= 125;
                            if (projectile.alpha < 0)
                            {
                                projectile.alpha = 0;
                            }
                            projectile.velocity *= 0.98f;
                        }
                        projectile.localAI[0] += 1f;
                    }
                }
            }
            float num15 = projectile.alpha / 255f;
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            Main.PlaySound(SoundID.Item71, projectile.position);
            projectile.damage *= 2;
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("SuperSlow"), 180);
            for (int num = 0; num < 1; num++)
            {
                Vector2 source = projectile.Center + new Vector2(Main.rand.NextFloatDirection() * 16f, Main.rand.NextFloatDirection() * 16f);
                Vector2 dir = (target.Center - source).SafeNormalize(Vector2.Zero);
                Dust dust = Dust.NewDustPerfect(target.Center - dir * 30f, mod.DustType("Slash"), dir * 15f, 0, Color.White, crit ? 1.5f : 1f);
                dust.noLight = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
            if (projectile.ai[0] >= (1 + Count) && projectile.ai[0] < (1 + Count * 2))
            {
                projectile.ai[0] = 0f;
            }
            Count--;
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] += Count;
            }
            else
            {
                projectile.ai[0] -= Count + 1;
            }
            projectile.ai[1] = 0f;
            projectile.netUpdate = true;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Main.PlaySound(SoundID.Item71, projectile.position);
            projectile.damage *= 2;
            target.AddBuff(mod.BuffType("SuperSlow"), 180);
            for (int num = 0; num < 1; num++)
            {
                Vector2 source = projectile.Center + new Vector2(Main.rand.NextFloatDirection() * 16f, Main.rand.NextFloatDirection() * 16f);
                Vector2 dir = (target.Center - source).SafeNormalize(Vector2.Zero);
                Dust dust = Dust.NewDustPerfect(target.Center - dir * 30f, mod.DustType("Slash"), dir * 15f, 0, Color.White, crit ? 1.5f : 1f);
                dust.noLight = true;
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
            if (projectile.ai[0] >= (1 + Count) && projectile.ai[0] < (1 + Count * 2))
            {
                projectile.ai[0] = 0f;
            }
            Count--;
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] += Count;
            }
            else
            {
                projectile.ai[0] -= Count + 1;
            }
            projectile.ai[1] = 0f;
            projectile.netUpdate = true;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Main.PlaySound(SoundID.Item71, projectile.position);
            projectile.damage *= 2;
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("SuperSlow"), 180);
            for (int num = 0; num < 1; num++)
            {
                Vector2 source = projectile.Center + new Vector2(Main.rand.NextFloatDirection() * 16f, Main.rand.NextFloatDirection() * 16f);
                Vector2 dir = (target.Center - source).SafeNormalize(Vector2.Zero);
                Dust dust = Dust.NewDustPerfect(target.Center - dir * 30f, mod.DustType("Slash"), dir * 15f, 0, Color.White, crit ? 1.5f : 1f);
                dust.noLight = true;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
            if (projectile.ai[0] >= (1 + Count) && projectile.ai[0] < (1 + Count * 2))
            {
                projectile.ai[0] = 0f;
            }
            Count--;
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] += Count;
            }
            else
            {
                projectile.ai[0] -= Count + 1;
            }
            projectile.ai[1] = 0f;
            projectile.netUpdate = true;
        }
    }
}