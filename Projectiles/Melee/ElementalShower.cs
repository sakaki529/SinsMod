using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class ElementalShower : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Shower");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.melee = true;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 120;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 120;
            projectile.netUpdate = true;
        }
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                float x = projectile.velocity.X / 3f * i;
                float y = projectile.velocity.Y / 3f * i;
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("RainbowDust"), 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[d].position.X = projectile.Center.X - x;
                Main.dust[d].position.Y = projectile.Center.Y - y;
                Main.dust[d].velocity *= 0f;
                Main.dust[d].noGravity = true;
            }
            if (projectile.ai[0] < 0)
            {
                return;
            }
            float num = projectile.Center.X;
            float num2 = projectile.Center.Y;
            float num3 = 600f;
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
                float num8 = 48f;
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] < 0)
            {
                return;
            }
            target.AddBuff(mod.BuffType("Chroma"), 360);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.ai[1] < 0)
            {
                return;
            }
            target.AddBuff(mod.BuffType("Chroma"), 360);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.ai[1] < 0)
            {
                return;
            }
            target.AddBuff(mod.BuffType("Chroma"), 360);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
    }
}