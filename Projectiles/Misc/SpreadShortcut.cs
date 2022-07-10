using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Misc
{
	public class SpreadShortcut : ModProjectile
	{
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
		{
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool PreAI()
        {
            projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == mod.ProjectileType("BlackMatter"))
            {
                int projNum = Main.rand.Next(6, 8);
                if (Main.expertMode && projectile.ai[1] != 1f)
                {
                    projNum += Main.rand.Next(2, 4);
                }
                if (projectile.ai[1] == 1f)
                {
                    projNum = 3;
                }
                for (int i = 0; i < projNum; i++)
                {
                    float num = 1f + Main.rand.Next(-25, 126) / 100f;
                    double radians = Main.rand.Next(-180, 180) * 3.1415926535897931 / 180.0;
                    Vector2 vector = new Vector2(0f, -5f * num).RotatedBy(radians, default(Vector2));
                    int j = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vector.X, vector.Y, (int)projectile.ai[0], projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                    if (projectile.ai[1] == 2f)
                    {
                        Main.projectile[j].tileCollide = true;
                    }
                }
                return;
            }
            if (projectile.ai[0] == mod.ProjectileType("BlackMatterFriendly"))
            {
                int projNum = Main.rand.Next(7, 9);
                for (int i = 0; i < projNum; i++)
                {
                    float num2 = 1f + Main.rand.Next(-25, 126) / 100f;
                    double radians = Main.rand.Next(-180, 180) * 3.1415926535897931 / 180.0;
                    Vector2 vector = new Vector2(0f, -5f * num2).RotatedBy(radians, default(Vector2));
                    int j = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vector.X, vector.Y, (int)projectile.ai[0], projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                }
                return;
            }
            if (projectile.ai[0] == mod.ProjectileType("CherrySeed"))
            {
                int projNum = Main.rand.Next(0, 4);
                for (int i = 0; i < projNum; i++)
                {
                    float num2 = 1f + Main.rand.Next(-25, 126) / 100f;
                    double radians = Main.rand.Next(-180, 180) * 3.1415926535897931 / 180.0;
                    Vector2 vector = new Vector2(0f, -5f * num2).RotatedBy(radians, default(Vector2));
                    int j = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vector.X * 2, vector.Y * 2, (int)projectile.ai[0], projectile.damage, projectile.knockBack, projectile.owner, 1f, 0f);
                }
                return;
            }
            if (projectile.ai[0] == mod.ProjectileType("TrueNightsBall"))
            {
                int projNum = Main.rand.Next(2, 4);
                for (int i = 0; i < projNum; i++)
                {
                    float num2 = 1f + Main.rand.Next(-25, 126) / 100f;
                    double radians = Main.rand.Next(-180, 180) * 3.1415926535897931 / 180.0;
                    Vector2 vector = new Vector2(0f, -5f * num2).RotatedBy(radians, default(Vector2));
                    int j = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vector.X, vector.Y, (int)projectile.ai[0], projectile.damage, projectile.knockBack, projectile.owner, Main.rand.Next(2), 0f);
                }
                return;
            }
            int value = (int)projectile.ai[1];
            /*if (Main.expertMode)
            {
                num += 3;
            }*/
            for (int i = 0; i < value; i++)
            {
                float num2 = 1f + Main.rand.Next(-25, 126) / 100f;
                double radians = Main.rand.Next(-180, 180) * 3.1415926535897931 / 180.0;
                Vector2 vector = new Vector2(0f, -5f * num2).RotatedBy(radians, default(Vector2));
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vector.X, vector.Y, (int)projectile.ai[0], projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
            }
        }
        /*{
            if (projectile.ai[0] == mod.ProjectileType("BlackMatter"))
            {
                int projNum = Main.rand.Next(7, 8);
                if (Main.expertMode)
                {
                    projNum += Main.rand.Next(3, 4);
                }
                if (projectile.ai[1] == 1f)
                {
                    projNum = Main.rand.Next(3, 4);
                }
                for (int i = 0; i < projNum; i++)
                {
                    float num = 1f + Main.rand.Next(-25, 126) / 100f;
                    double radians = Main.rand.Next(-180, 180) * 3.1415926535897931 / 180.0;
                    Vector2 vector = new Vector2(0f, -5f * num).RotatedBy(radians, default(Vector2));
                    int j = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vector.X, vector.Y, (int)projectile.ai[0], projectile.damage, 1f, Main.myPlayer, 0f, 0f);
                    if (projectile.ai[1] == 2f)
                    {
                        Main.projectile[j].tileCollide = true;
                    }
                }
                return;
            }
            if (projectile.ai[0] == mod.ProjectileType("BlackMatterFriendly"))
            {
                int projNum = Main.rand.Next(7, 9);
                for (int i = 0; i < projNum; i++)
                {
                    float num2 = 1f + Main.rand.Next(-25, 126) / 100f;
                    double radians = Main.rand.Next(-180, 180) * 3.1415926535897931 / 180.0;
                    Vector2 vector = new Vector2(0f, -5f * num2).RotatedBy(radians, default(Vector2));
                    int j = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vector.X, vector.Y, (int)projectile.ai[0], projectile.damage, 1f, projectile.owner, 0f, 0f);
                }
                return;
            }
            /*int num3 = 6;
            if (Main.expertMode)
            {
                num3 += 3;
            }
            for (int i = 0; i < num3; i++)
            {
                float num2 = 1f + Main.rand.Next(-25, 126) / 100f;
                double radians = Main.rand.Next(-180, 180) * 3.1415926535897931 / 180.0;
                Vector2 vector = new Vector2(0f, -5f * num2).RotatedBy(radians, default(Vector2));
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vector.X, vector.Y, (int)projectile.ai[0], projectile.damage, 1f, Main.myPlayer, 0f, 0f);
            }*/
        //}
    }
}