using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class SubterraneanRose : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subterranean Rose");
            Main.projFrames[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.magic = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = 300;
            //projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 15;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            if (projectile.ai[0] == 1)
            {
                texture = mod.GetTexture("Extra/Projectile/SubterraneanRose_Alt");
            }
            SpriteEffects spriteEffects = SpriteEffects.None;
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.localAI[1]++;
            if (projectile.localAI[1] > 200)
            {
                projectile.alpha += 25;
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                    projectile.Kill();
                }
            }
            if (projectile.ai[0] != 1)
            {
                Lighting.AddLight(projectile.Center, projectile.frame == 1 ?  0.8f : 0.4f, projectile.frame == 1 ? 0.6f : 0.3f, projectile.frame == 1 ? 0.2f : 0.1f);
            }
            if (projectile.ai[0] == 1)
            {
                Lighting.AddLight(projectile.Center, projectile.frame == 1 ? 0.4f : 0.2f, projectile.frame == 1 ? 0.4f : 0.2f, projectile.frame == 1 ? 0.8f : 0.4f);
            }
            projectile.ai[1] += projectile.ai[0] == 1 ? 1 : -1;
            if (projectile.ai[0] == 1 ? projectile.ai[1] > -10 : projectile.ai[1] < -50)
            {
                projectile.frame = 1;
                if (projectile.localAI[0] == 0)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/kira").WithVolume(0.3f), projectile.Center);
                    projectile.localAI[0] = 1;
                    projectile.position.X = projectile.position.X + (projectile.width / 2);
                    projectile.position.Y = projectile.position.Y + (projectile.height / 2);
                    projectile.width = 96;
                    projectile.height = 96;
                    projectile.position.X = projectile.position.X - (projectile.width / 2);
                    projectile.position.Y = projectile.position.Y - (projectile.height / 2);
                }
                projectile.localNPCHitCooldown = 3;
                if (projectile.ai[0] == 1 ? projectile.ai[1] > 0 : projectile.ai[1] < -60)
                {
                    projectile.ai[1] = projectile.ai[0] == 1 ? -60 : 0;
                    projectile.position.X = projectile.position.X + (projectile.width / 2);
                    projectile.position.Y = projectile.position.Y + (projectile.height / 2);
                    projectile.width = 32;
                    projectile.height = 32;
                    projectile.position.X = projectile.position.X - (projectile.width / 2);
                    projectile.position.Y = projectile.position.Y - (projectile.height / 2);
                }
                return;
            }
            projectile.frame = 0;
            projectile.localAI[0] = 0;
            projectile.localNPCHitCooldown = 15;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            if (projectile.frame == 1 && projectile.ai[0] != 1)
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
            if (projectile.frame == 1 && projectile.ai[0] == 1)
            {
                target.AddBuff(BuffID.Frostburn, 300);
            }

        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.frame == 1)
            {
                damage *= 2;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            if (projectile.frame == 1 && projectile.ai[0] != 1)
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
            if (projectile.frame == 1 && projectile.ai[0] == 1)
            {
                target.AddBuff(BuffID.Frostburn, 300);
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (projectile.frame == 1)
            {
                damage *= 2;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            if (projectile.frame == 1 && projectile.ai[0] != 1)
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
            if (projectile.frame == 1 && projectile.ai[0] == 1)
            {
                target.AddBuff(BuffID.Frostburn, 300);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (projectile.frame == 1)
            {
                damage *= 2;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 255) * (1f - projectile.alpha / 255f);
        }
    }
}