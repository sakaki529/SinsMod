using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace SinsMod.Projectiles.Magic
{
    public class Distortion : ModProjectile
	{
        public float Distance
        {
            get { return projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Universe Distortion");
        }
		public override void SetDefaults()
		{
            projectile.width = 18;
			projectile.height = 18;
            projectile.magic = true;
			projectile.friendly = true;
            projectile.hostile = false;
			//projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
            projectile.hide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == 488)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool CanHitPlayer(Player target)
        {
            return true;
        }
        public override void AI()
		{
            projectile.timeLeft++;
            bool prism = true;
            Vector2? vector78 = null;
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            if (prism == true)
            {
                Player player = Main.player[projectile.owner];
                if (!player.channel)
                {
                    projectile.Kill();
                }
                if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
                {
                    projectile.velocity = -Vector2.UnitY;
                }
                float num796 = (float)((int)projectile.ai[0]) - 2.5f;
                Vector2 value27 = Vector2.Normalize(Main.projectile[(int)projectile.ai[1]].velocity);
                Projectile projectile2 = Main.projectile[(int)projectile.ai[1]];
                float num797 = num796 * 0.5235988f;
                Vector2 value28 = Vector2.Zero;
                float num798;
                float y;
                float num799;
                float scaleFactor6;
                if (projectile2.ai[0] < 180f)
                {
                    if (!player.channel)
                    {
                        projectile.Kill();
                    }

                    num798 = 1f - projectile2.ai[0] / 180f;
                    y = 20f - projectile2.ai[0] / 180f * 14f;
                    if (projectile2.ai[0] < 120f)
                    {
                        num799 = 20f - 4f * (projectile2.ai[0] / 120f);
                        projectile2.Opacity = projectile2.ai[0] / 120f * 0.4f;
                    }
                    else
                    {
                        num799 = 16f - 10f * ((projectile2.ai[0] - 120f) / 60f);
                        projectile.Opacity = 0.4f + (projectile2.ai[0] - 120f) / 60f * 0.6f;
                    }
                    scaleFactor6 = -22f + projectile2.ai[0] / 180f * 20f;
                }
                else
                {
                    if (!player.channel)
                    {
                        projectile.Kill();
                    }

                    num798 = 0f;
                    num799 = 1.75f;
                    y = 6f;
                    projectile.Opacity = 1f;
                    scaleFactor6 = -2f;
                }
                float num800 = (projectile2.ai[0] + num796 * num799) / (num799 * 6f) * 6.28318548f;
                num797 = Vector2.UnitY.RotatedBy((double)num800, default(Vector2)).Y * 0.5235988f * num798;
                value28 = (Vector2.UnitY.RotatedBy((double)num800, default(Vector2)) * new Vector2(4f, y)).RotatedBy((double)projectile2.velocity.ToRotation(), default(Vector2));
                projectile.position = projectile2.Center + value27 * 16f - projectile.Size / 2f + new Vector2(0f, -Main.projectile[(int)projectile.ai[1]].gfxOffY);
                projectile.position += projectile2.velocity.ToRotation().ToRotationVector2() * scaleFactor6;
                projectile.position += value28;
                projectile.velocity = Vector2.Normalize(projectile2.velocity).RotatedBy((double)num797, default(Vector2));
                projectile.scale = 1.4f * (1f - num798);
                projectile.damage = projectile2.damage;
                if (projectile2.ai[0] >= 180f)
                {
                    if (!player.channel)
                    {
                        projectile.Kill();
                    }

                    projectile.damage *= 4;
                    vector78 = new Vector2?(projectile2.Center);
                }
                if (!Collision.CanHitLine(Main.player[projectile.owner].Center, 0, 0, projectile2.Center, 0, 0))
                {
                    vector78 = new Vector2?(Main.player[projectile.owner].Center);
                }
                projectile.friendly = (projectile2.ai[0] > 30f);


                int num3 = 0;
                float num805 = 0f;
                float num807 = 0f;
                num805 = 2f;
                num807 /= num805;
                float amount = 0.5f;
                amount = 0.75f;
                projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num807, amount);
                if (Math.Abs(projectile.localAI[1] - num807) < 100f && projectile.scale > 0.15f)
                {
                    float prismHue = projectile.GetPrismHue(projectile.ai[0]);
                    Color color = Main.hslToRgb(prismHue, 1f, 0.5f);
                    color.A = 0;
                    Vector2 vector87 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14.5f * projectile.scale);
                    float x = Main.rgbToHsl(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)).X;
                    for (int num829 = 0; num829 < 2; num829 = num3 + 1)
                    {
                        float num830 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                        float num831 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector88 = new Vector2((float)Math.Cos((double)num830) * num831, (float)Math.Sin((double)num830) * num831);
                        int num832 = Dust.NewDust(vector87, 0, 0, 112, vector88.X, vector88.Y, 0, default(Color), 1f);
                        Main.dust[num832].color = color;
                        Main.dust[num832].scale = 1.2f;
                        Main.dust[num832].shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                        if (projectile.scale > 1f)
                        {
                            Dust dust = Main.dust[num832];
                            dust.velocity *= projectile.scale;
                            dust = Main.dust[num832];
                            dust.scale *= projectile.scale;
                        }
                        Main.dust[num832].noGravity = true;
                        if (projectile.scale != 1.4f)
                        {
                            Dust dust10 = Dust.CloneDust(num832);
                            dust10.color = Color.White;
                            Dust dust = dust10;
                            dust.scale /= 2f;
                        }
                        float hue = (x + Main.rand.NextFloat() * 0.4f) % 1f;
                        Main.dust[num832].color = Color.Lerp(color, Main.hslToRgb(hue, 1f, 0.75f), projectile.scale / 1.4f);
                        num3 = num829;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        /*Vector2 value33 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)projectile.width;
                        int num833 = Dust.NewDust(vector87 + value33 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                        Dust dust = Main.dust[num833];
                        dust.velocity *= 0.5f;
                        Main.dust[num833].velocity.Y = -Math.Abs(Main.dust[num833].velocity.Y);*/
                    }
                    DelegateMethods.v3_1 = color.ToVector3() * 0.3f;
                    float value34 = 0.1f * (float)Math.Sin((double)(Main.GlobalTime * 20f));
                    Vector2 vector89 = new Vector2(projectile.velocity.Length() * projectile.localAI[1], (float)projectile.width * projectile.scale);
                    float num834 = projectile.velocity.ToRotation();
                    if (Main.netMode != 2)
                    {
                        ((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(projectile.position + new Vector2(vector89.X * 0.5f, 0f).RotatedBy((double)num834, default(Vector2)), new Color(0.5f, 0.1f * (float)Math.Sign(value34) + 0.5f, 0f, 1f) * Math.Abs(value34), vector89, RippleShape.Square, num834);
                    }
                    Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], (float)projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
                    return;
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
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
        public override bool ShouldUpdatePosition()
        {
            return true;
        }
        public override bool? CanCutTiles()
        {
            return true;
        }
        public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = projectile.velocity;
			Utils.PlotTileLine(projectile.Center, projectile.Center + unit * Distance, (projectile.width + 16) * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[projectile.owner];
            Vector2 unit = projectile.velocity;
            float point = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, player.Center + unit * Distance, 22, ref point))
            {
                return true;
            }
            return true;
        }
    }
}