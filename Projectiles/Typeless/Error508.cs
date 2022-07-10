using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Typeless
{
    public class Error508 : ModProjectile
    {
        public override string Texture => "SinsMod/Projectiles/Typeless/Boulder";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = 2;
            projectile.timeLeft = 240;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.penetrate = -1;
            projectile.netUpdate = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            aiType = 48;
        }
        public override void AI()
        {
            if (projectile.ai[1] == 1f)
            {
                projectile.localNPCHitCooldown = -1;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] == 1f)
            {
                target.life = 1;
                return;
            }
            projectile.Kill();
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.ai[1] == 1f)
            {
                target.KillMe(PlayerDeathReason.ByProjectile(1, 1), 10, 0, false);
                return;
            }
            SinsMod.Instance = null;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.ai[1] == 1f)
            {
                target.KillMe(PlayerDeathReason.ByProjectile(1, 1), 10, 0, false);
                return;
            }
            SinsMod.Instance = null;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.owner == Main.myPlayer)
            {
                projectile.penetrate = -1;
                projectile.position.X = projectile.position.X + (projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (projectile.height / 2);
                projectile.width = 120;
                projectile.height = 120;
                projectile.position.X = projectile.position.X - (projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (projectile.height / 2);
                projectile.Damage();
            }
            int num;
            for (int num2 = 0; num2 < 10; num2 = num + 1)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                num = num2;
            }
            for (int num3 = 0; num3 < 5; num3 = num + 1)
            {
                int num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 172, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num4].noGravity = true;
                Main.dust[num4].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                Dust dust = Main.dust[num4];
                dust.velocity *= 3f;
                num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 172, 0f, 0f, 100, default(Color), 1.0f);
                Main.dust[num4].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                dust = Main.dust[num4];
                dust.velocity *= 2f;
                num = num3;
            }
        }
    }
}