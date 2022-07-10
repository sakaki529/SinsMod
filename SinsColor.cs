using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace SinsMod
{
    public class SinsColor
    {
        public static Color SinsModThemeColor => new Color(121, 137, 162, 255);//[c/7989a2:](slate gray)
        public static Color Lime => new Color(50, 205, 50, 255);//[c/32cd32:]
        public static Color Cherry => new Color(255, 50, 255, 255);//[c/ff32ff:]
        public static Color UniqueSkyBlue => new Color(70, 230, 200, 255);//[c/46e6c8:]
        public static Color MediumBlack => new Color(30, 30, 30, 255);//[c/1e1e1e:]
        public static Color MediumWhite => new Color(200, 200, 200, 255);//[c/c8c8c8:]
        public static Color VoidBlue => new Color(80, 80, 255, 255);//[c/5050ff:]
        public static Color VoidGreen => new Color(50, 255, 80, 255);//[c/32ff50:]
        public static Color EdenYellow => new Color(255, 255, Main.DiscoB + 125, 255);
        public static Color DarknessPurple => new Color(20 + (int)(Main.DiscoR * 1.0f), 0, 20 + (int)(Main.DiscoR * 1.0f), 255);
        public static Color BW => Color.Lerp(Color.Black, Color.White, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.4f + 0.5f);//not perfect black(short) and perfect white(long)
        public static Color OriginBW => Color.Lerp(Color.Black, Color.White, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.5f + 0.5f);
        public static Color NightmareRed => Color.Lerp(new Color(160, 8, 34, 255), new Color(224, 51, 103, 255), (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.5f + 0.5f);
        public static Color TrueMidnightPurple => Color.Lerp(new Color(240, 109, 231, 255), new Color(255, 176, 215, 255), (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.5f + 0.5f);
        public static Color MidnightPurple => Color.Lerp(new Color(30, 13, 54, 255), new Color(84, 51, 192, 255), (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.5f + 0.5f);
        public static Color Rarity12 => Color.Aquamarine;//post Acedia & Vain
        public static Color Rarity13 => Color.Lime;//post Wrath
        public static Color Rarity14 => Cherry;//post Lust
        public static Color Rarity15 => Color.Indigo;//post origin
        public static Color Rarity16 => UniqueSkyBlue;//post Tartarus
        public static Color Rarity17 => SinsModThemeColor;//post Madness
        public static Color AltUseableColor => new Color(200, 125, 255, 255);//[c/c87dff:<Right clickable>]
        public static Color MultiTypeColor => new Color(255, 186, 0, 255);//[c/ffb600:<Multi Type Weapon>]
        public static Color MemeColor => new Color(254, 46, 247, 255);//[c/FE2EF7:<MEME ITEM>]
        public static Color mcColor => new Color(125, 125, 255, 255);//[c/7d7dff:]
        public static Color hcColor => new Color(200, 125, 255, 255);//[c/c87dff:]
        public static Color Envy => new Color(0, 200, 0, 255);//[c/00c800:]
        public static Color Envy_F => new Color(0.3f, 0.8f, 0.3f);
        public static Color Gluttony => new Color(255, 140, 0, 255);//[c/ff8c00:]
        public static Color Gluttony_F => new Color(0.7f, 0.3f, 0.1f);
        public static Color Greed => new Color(220, 220, 20, 255);//[c/dcdc14:]
        public static Color Greed_F => new Color(0.9f, 0.9f, 0.3f);
        public static Color Lust => new Color(255, 150, 255, 255);//[c/ff96ff:]
        public static Color Lust_F => new Color(0.8f, 0.35f, 0.8f);
        public static Color Pride => new Color(150, 5, 255, 255);//[c/9605ff:]
        public static Color Pride_F => new Color(0.5f, 0.05f, 0.5f);
        public static Color Sloth => new Color(10, 30, 255, 255);//[c/0a1eff:]
        public static Color Sloth_F => new Color(0.1f, 0.25f, 0.5f);
        public static Color Wrath => new Color(255, 20, 20, 255);//[c/ff1414:]
        public static Color Wrath_F => new Color(0.6f, 0.1f, 0.1f);
        public static Color Origin => new Color(200, 200, 200, 255);//[c/c8c8c8:]
        public static Color Origin_F => new Color(0.5f, 0.5f, 0.5f);
        public static Color Madness => new Color(125, 125, 125, 255);//[c/7d7d7d:]
        public static Color Madness_F => new Color(0.32f, 0.32f, 0.32f);
    }
}