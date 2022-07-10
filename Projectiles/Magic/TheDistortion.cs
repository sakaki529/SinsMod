using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class TheDistortion : ModProjectile
	{
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Universe Distortion");
        }
        public override void SetDefaults()
        {
            aiType = ProjectileID.LastPrism;
            projectile.aiStyle = 75;
            projectile.width = 26;
            projectile.height = 24;
            projectile.melee = false;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.hide = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num26 = 30f;
            if (projectile.ai[0] > 90f)
            {
                num26 = 15f;
            }
            if (projectile.ai[0] > 120f)
            {
                num26 = 5f;
            }
            bool flag7 = false;
            if (projectile.ai[0] % num26 == 0f)
            {
                flag7 = true;
            }
            bool flag9 = !flag7 || player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
            if ((player.channel & flag9) && !player.noItems && !player.CCed)
            {
                if (projectile.ai[0] == 1f)
                {
                    Vector2 vector19 = Vector2.Normalize(projectile.velocity);
                    if (float.IsNaN(vector19.X) || float.IsNaN(vector19.Y))
                    {
                        vector19 = -Vector2.UnitY;
                    }
                    int num30 = projectile.damage;
                    for (int l = 0; l < 6; l++)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector19.X, vector19.Y, mod.ProjectileType("Distortion"), projectile.damage, projectile.knockBack, projectile.owner, (float)l, (float)projectile.whoAmI);
                    }
                }
            }
            else
            {
                projectile.Kill();
                ((SinsPlayer)player.GetModPlayer(mod, "SinsPlayer")).chargeTime = 0;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, Main.DiscoR, Main.DiscoR, 255);
        }
        public override bool PreKill(int timeLeft)
        {
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            ((SinsPlayer)player.GetModPlayer(mod, "SinsPlayer")).chargeTime = 0;
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