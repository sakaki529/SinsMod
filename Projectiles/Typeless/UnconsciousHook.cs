using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Typeless
{
    public class UnconsciousHook : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tentacle");
            Main.projHook[projectile.type] = true;
        }
        public override void SetDefaults()
		{
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = 7;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft *= 10;
            projectile.netImportant = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D chainTexture = mod.GetTexture("Extra/Projectile/UnconsciousHook_Chain");
            Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 center = projectile.Center;
            Vector2 distToProj = playerCenter - projectile.Center;
            float projRotation = distToProj.ToRotation() - 1.57f;
            float distance = distToProj.Length();
            while (distance > 12f && !float.IsNaN(distance))
            {
                distToProj.Normalize();                 //get unit vector
                distToProj *= 12f;                      //speed = 24
                center += distToProj;                   //update draw position
                distToProj = playerCenter - center;    //update distance
                distance = distToProj.Length();
                Color drawColor = lightColor;
                spriteBatch.Draw(chainTexture, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y), new Rectangle(0, 0, chainTexture.Width, chainTexture.Height), drawColor, projRotation, new Vector2(chainTexture.Width * 0.5f, chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override bool? CanUseGrapple(Player player)
        {
            int hooksOut = 0;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == projectile.type && Main.projectile[i].ai[0] != 2f)
                {
                    hooksOut++;
                }
            }
            return hooksOut < 4;// This hook can have 4 hooks out.
        }
        /*public override void UseGrapple(Player player, ref int type)
        {
        	int hooksOut = 0;
        	int oldestHookIndex = -1;
        	int oldestHookTimeLeft = 100000;
        	for (int i = 0; i < 1000; i++)
        	{
        		if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == projectile.type && Main.projectile[i].ai[0] != 2f)
        		{
        			hooksOut++;
        			if (Main.projectile[i].timeLeft < oldestHookTimeLeft)
        			{
        				oldestHookIndex = i;
        				oldestHookTimeLeft = Main.projectile[i].timeLeft;
        			}
        		}
        	}
        	if (hooksOut > 3)
        	{
        		Main.projectile[oldestHookIndex].Kill();
        	}
        }*/
        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            return false;
        }
        public override bool? SingleGrappleHook(Player player)
        {
            return false;
        }
        public override float GrappleRange()
        {
            return 700f;
        }
        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = 4;
        }
        // default is 11, Lunar is 24
        public override void GrappleRetreatSpeed(Player player, ref float speed)//player speed
        {
            speed = 24f;
        }
        public override void GrapplePullSpeed(Player player, ref float speed)//returning hook speed
        {
            speed = 30f;
        }
    }
}