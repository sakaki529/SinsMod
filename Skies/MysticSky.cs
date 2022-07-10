using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class MysticSky : CustomSky
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private static Texture2D SkyTexture;
        private bool isActive;
        private float opacity;
        public override void Update(GameTime gameTime)
        {
            if (isActive && opacity < 1f)
            {
                opacity += 0.01f;
                return;
            }
            if (!isActive && opacity > 0f)
            {
                opacity -= 0.01f;
            }
        }
        public override void OnLoad()
        {
            SkyTexture = mod.GetTexture("Skies/MysticSky");
        }
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(SkyTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Main.bgColor * opacity);
                if (Main.netMode != 2)
                {
                    for (int i = 0; i < Main.star.Length; i++)
                    {
                        Star star = Main.star[i];
                        if (star != null)
                        {
                            Texture2D texture2D = Main.starTexture[star.type];
                            Vector2 vector = new Vector2(texture2D.Width * 0.5f, texture2D.Height * 0.5f);
                            int num = (int)(-Main.screenPosition.Y / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                            float num2 = star.position.X * (Main.screenWidth / 800f);
                            float num3 = star.position.Y * (Main.screenHeight / 600f);
                            Vector2 vector2 = new Vector2(num2 + vector.X, num3 + vector.Y + num);
                            spriteBatch.Draw(texture2D, vector2, new Rectangle?(new Rectangle(0, 0, texture2D.Width, texture2D.Height)), Color.White * star.twinkle * 0.952f * opacity, star.rotation, vector, star.scale * star.twinkle, 0, 0f);
                        }
                    }
                }
            }
        }
        public override float GetCloudAlpha()
        {
            return (1f - opacity) * 0.97f + 0.03f;
        }
        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }
        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }
        public override void Reset()
        {
            isActive = false;
        }
        public override bool IsActive()
        {
            return isActive || opacity > 0f;
        }
    }
}