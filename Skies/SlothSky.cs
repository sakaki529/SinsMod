using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class SlothSky : CustomSky
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private static Texture2D RuneTexture;
        private bool isActive;
        private float Intensity;
        private int SlothIndex = -1;
        public override void Update(GameTime gameTime)
        {
            if (isActive && Intensity < 1f)
            {
                Intensity += 0.01f;
                return;
            }
            if (!isActive && Intensity > 0f)
            {
                Intensity -= 0.01f;
            }
        }
        public override void OnLoad()
        {
            RuneTexture = mod.GetTexture("Extra/Rune/SlothRune");
        }
        private float GetIntensity()
        {
            if (UpdatePIndex())
            {
                float x = 0f;
                if (SlothIndex != -1)
                {
                    x = Vector2.Distance(Main.player[Main.myPlayer].Center, Main.npc[SlothIndex].Center);
                }
                return (1f - Utils.SmoothStep(3000f, 6000f, x)) * 0.5f;
            }
            return 0.7f;
        }
        public override Color OnTileColor(Color inColor)
        {
            float num = GetIntensity();
            return new Color(Vector4.Lerp(new Vector4(0.1f, 0.25f, 0.5f, 1f), inColor.ToVector4(), 1f - Intensity));
        }
        private bool UpdatePIndex()
        {
            int num = mod.NPCType("Sloth");
            int num2 = mod.NPCType("Sloth");
            if (SlothIndex >= 0 && Main.npc[SlothIndex].active && (Main.npc[SlothIndex].type == num || Main.npc[SlothIndex].type == num2))
            {
                return true;
            }
            SlothIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2))
                {
                    SlothIndex = i;
                    break;
                }
            }
            return SlothIndex != -1;
        }
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0f && minDepth < 0f)
            {
                float num = GetIntensity();
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                //spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(10, 60, 150) * Intensity);
            }
            if (SinsMod.ClientConfig.DrawBackgroundRune)
            {
                Vector2 vector = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                spriteBatch.Draw(RuneTexture, vector, null, new Color(255, 255, 255, 255) * Intensity, 0f, new Vector2(RuneTexture.Width >> 1, RuneTexture.Height >> 1), 2f, 0, 1f);
            }
        }
        public override float GetCloudAlpha()
        {
            return 1f - Intensity;
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
            return isActive || Intensity > 0f;
        }
    }
}