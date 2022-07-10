using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class ElectricalDischarge : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discharge");
        }
        public override void SetDefaults()
        {
            projectile.width = 420;
            projectile.height = 420;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 3;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0 && projectile.ai[0] == 1)
            {
                Main.PlaySound(SoundID.Item93);
                projectile.localAI[0] = 1;
            }
            for (int i = 0; i < 16; i++)
            {
                int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 222, 0, 0, 0, default(Color), 1f);
                Main.dust[num].noGravity = false;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Electrified, Main.expertMode ? 360 : 180);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, Main.expertMode ? 360 : 180);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, Main.expertMode ? 360 : 180);
        }
    }
}