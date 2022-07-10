using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace SinsMod
{
    public delegate void ExtraAction();
    internal static class SinsUtility
    {
        private static PropertyInfo _steamid;
        public static string SteamID
        {
            get
            {
                return (string)_steamid.GetValue(null, null);
            }
        }
        private static MethodInfo startRain;
        private static MethodInfo stopRain;
        private static void InitReflection()
		{
			try
			{
                _steamid = typeof(ModLoader).GetProperty("SteamID64", BindingFlags.NonPublic | BindingFlags.Static);
                startRain = typeof(Main).GetMethod("StartRain", BindingFlags.Static | BindingFlags.NonPublic);
                stopRain = typeof(Main).GetMethod("StopRain", BindingFlags.Static | BindingFlags.NonPublic);
            }
			catch (Exception exception)
			{
				
			}
		}
        public static void StartRain()
        {
            startRain.Invoke(null, null);
        }
        public static void StopRain()
        {
            stopRain.Invoke(null, null);
        }
        public static Vector2 VelocityToPoint(Vector2 A, Vector2 B, float Speed)
        {
            Vector2 Move = B - A;
            return Move * (Speed / (float)Math.Sqrt(Move.X * Move.X + Move.Y * Move.Y));
        }
        public static void Explode(int index, int sizeX, int sizeY, ExtraAction visualAction = null)
        {
            Projectile projectile = Main.projectile[index];
            if (!projectile.active)
            {
                return;
            }
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.position.X = projectile.position.X + projectile.width / 2;
            projectile.position.Y = projectile.position.Y + projectile.height / 2;
            projectile.width = sizeX;
            projectile.height = sizeY;
            projectile.position.X = projectile.position.X - projectile.width / 2;
            projectile.position.Y = projectile.position.Y - projectile.height / 2;
            projectile.Damage();
            Main.projectileIdentity[projectile.owner, projectile.identity] = -1;
            projectile.position.X = projectile.position.X + projectile.width / 2;
            projectile.position.Y = projectile.position.Y + projectile.height / 2;
            projectile.width = (int)(sizeX / 5.8f);
            projectile.height = (int)(sizeY / 5.8f);
            projectile.position.X = projectile.position.X - projectile.width / 2;
            projectile.position.Y = projectile.position.Y - projectile.height / 2;
            if (visualAction == null)
            {
                for (int i = 0; i < 30; i++)
                {
                    int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num].velocity *= 1.4f;
                }
                for (int j = 0; j < 20; j++)
                {
                    int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 7f;
                    num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num2].velocity *= 3f;
                }
                for (int k = 0; k < 2; k++)
                {
                    float scaleFactor = 0.4f;
                    if (k == 1)
                    {
                        scaleFactor = 0.8f;
                    }
                    int num3 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[num3].velocity *= scaleFactor;
                    Gore gore = Main.gore[num3];
                    gore.velocity.X = gore.velocity.X + 1f;
                    Gore gore2 = Main.gore[num3];
                    gore2.velocity.Y = gore2.velocity.Y + 1f;
                    num3 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[num3].velocity *= scaleFactor;
                    Gore gore3 = Main.gore[num3];
                    gore3.velocity.X = gore3.velocity.X - 1f;
                    Gore gore4 = Main.gore[num3];
                    gore4.velocity.Y = gore4.velocity.Y + 1f;
                    num3 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[num3].velocity *= scaleFactor;
                    Gore gore5 = Main.gore[num3];
                    gore5.velocity.X = gore5.velocity.X + 1f;
                    Gore gore6 = Main.gore[num3];
                    gore6.velocity.Y = gore6.velocity.Y - 1f;
                    num3 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[num3].velocity *= scaleFactor;
                    Gore gore7 = Main.gore[num3];
                    gore7.velocity.X = gore7.velocity.X - 1f;
                    Gore gore8 = Main.gore[num3];
                    gore8.velocity.Y = gore8.velocity.Y - 1f;
                }
                return;
            }
            visualAction();
        }
        public static void DrawAroundOrigin(int index, Color lightColor)
        {
            Projectile projectile = Main.projectile[index];
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2(texture2D.Width * 0.5f, texture2D.Height / Main.projFrames[projectile.type] * 0.5f);
            SpriteEffects effects = (projectile.direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition, texture2D.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame), lightColor, projectile.rotation, origin, projectile.scale, effects, 0f);
        }
        public static NPC FindFirstNPC(Vector2 position, float maxDist, bool ignoreCanHitLine = false, bool ignoreFriendlies = true, bool ignoreDontTakeDamage = false, bool ignoreChaseable = false)
        {
            NPC nPC = null;
            float distNearest = maxDist * maxDist;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                Vector2 npcCenter = npc.Center;
                if (npc.active && (ignoreChaseable || npc.chaseable && npc.lifeMax > 5) && (ignoreDontTakeDamage || !npc.dontTakeDamage) && (!ignoreFriendlies || !npc.friendly) && !npc.immortal)
                {
                    float distCurrent = Vector2.DistanceSquared(position, npcCenter);
                    if (distCurrent < distNearest && (ignoreCanHitLine || Collision.CanHitLine(position, 0, 0, npcCenter, 0, 0)))
                    {
                        nPC = npc;
                        distNearest = distCurrent;
                    }
                }
            }
            return nPC;
        }
        internal static void Despawn(int npc)
        {
            //SinsUtility.Despawn(npc.whoAmI);
            Main.npc[npc] = new NPC
            {
                whoAmI = npc
            };
            if (Main.netMode == 2)
            {
                NetMessage.SendData(23, -1, -1, null, npc, 0f, 0f, 0f, 0, 0, 0);
            }
        }
        public static int CountProjectiles(int type)
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    num++;
                }
            }
            return num;
        }
        public static bool AnyProjectiles(int type)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    return true;
                }
            }
            return false;
        }
        internal static float RotationTo(Vector2 startPos, Vector2 endPos)
        {
            return (float)Math.Atan2(endPos.Y - startPos.Y, endPos.X - startPos.X);
        }
        internal static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
        {
            float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
            float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
            return new Vector2(newPosX, newPosY);
        }
        internal static bool AreUCley()
        {
            try
            {
                if (SteamID != null)
                {
                    if (SteamID == "")
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
    class Particle
    {
        internal Vector2 position;
        internal Vector2 velocity;
        internal float strength;
        internal Particle(Vector2 pos, Vector2 vel)
        {
            position = pos;
            velocity = vel;
            strength = 0.75f;
        }
        internal void Update()
        {
            position += velocity * strength;
            strength -= 0.01f;
        }
        /*internal const int size = 120;
        internal const int particleSize = 12;
        internal IList<Particle> particles = new List<Particle>();
        internal float[,] aura = new float[size, size];*/
        internal static void UpdateParticles(int size, int particleSize, float[,] aura, IList<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                particle.Update();
            }
            Vector2 newPos = new Vector2(Main.rand.Next(3 * size / 8, 5 * size / 8), Main.rand.Next(3 * size / 8, 5 * size / 8));
            double newAngle = 2 * Math.PI * Main.rand.NextDouble();
            Vector2 newVel = new Vector2((float)Math.Cos(newAngle), (float)Math.Sin(newAngle));
            newVel *= 0.5f * (1f + (float)Main.rand.NextDouble());
            particles.Add(new Particle(newPos, newVel));
            if (particles[0].strength <= 0f)
            {
                particles.RemoveAt(0);
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    aura[x, y] *= 0.97f;
                }
            }
            foreach (Particle particle in particles)
            {
                int minX = (int)particle.position.X - particleSize / 2;
                int minY = (int)particle.position.Y - particleSize / 2;
                int maxX = minX + particleSize;
                int maxY = minY + particleSize;
                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        if (x >= 0 && x < size && y >= 0 && y < size)
                        {
                            float strength = particle.strength;
                            float offX = particle.position.X - x;
                            float offY = particle.position.Y - y;
                            strength *= 1f - (float)Math.Sqrt(offX * offX + offY * offY) / particleSize * 2;
                            if (strength < 0f)
                            {
                                strength = 0f;
                            }
                            aura[x, y] = 1f - (1f - aura[x, y]) * (1f - strength);
                        }
                    }
                }
            }
        }
    }
}