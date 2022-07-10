using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class TitanStamp : ModProjectile
    {
        private bool HitTile;
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titan Stamp");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 5;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = true;
        }
        public override void AI()
        {
            projectile.width = (int)projectile.ai[0];
            projectile.height = (int)projectile.ai[1];
            projectile.velocity = Vector2.Zero;
            if (projectile.localAI[0] == 0f)
            {
                projectile.position.X = projectile.position.X + (projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (projectile.height / 2);
                projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.Item14, projectile.Center);
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 62);
                float dist = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, projectile.Center) / 500000;
                SinsMod.shakeIntensity = Math.Max(SinsMod.shakeIntensity, (int)(10 / dist));
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!HitTile)
            {
                Collision.HitTiles(projectile.position, oldVelocity, projectile.width, projectile.height);
                Collision.HitTiles(projectile.position, oldVelocity, projectile.width, projectile.height);
                HitTile = true;
            }
            projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}