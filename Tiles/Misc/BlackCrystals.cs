using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Misc
{
    public class BlackCrystals : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSpelunker[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            ModTranslation modTranslation = CreateMapEntryName(null);
            modTranslation.SetDefault("Crystal");
            AddMapEntry(new Color(60, 60, 60), modTranslation);
            soundType = 2;//item
            soundStyle = 27;
            dustType = 67;
            drop = mod.ItemType("BlackCrystalShard");
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            for (int num = 0; num < Main.rand.Next(6, 9); num++)
            {
                int num2 = Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 186, 0f, 0f, 1, default(Color), 1.0f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 2f;
                Main.dust[num2].scale = 0.4f + Utils.NextFloat(Main.rand, 0.15f);
                Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
            return false;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.2f;
            g = 0.2f;
            b = 0.2f;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return SinsWorld.downedSins;
        }
        public override bool CanPlace(int i, int j)
        {
            return WorldGen.SolidTile(i - 1, j) || WorldGen.SolidTile(i + 1, j) || WorldGen.SolidTile(i, j - 1) || WorldGen.SolidTile(i, j + 1);
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            if (Main.tile[i, j + 1].active() && Main.tileSolid[Main.tile[i, j + 1].type] && Main.tile[i, j + 1].slope() == 0 && !Main.tile[i, j + 1].halfBrick())
            {
                Main.tile[i, j].frameY = 0;
            }
            else
            {
                if (Main.tile[i, j - 1].active() && Main.tileSolid[Main.tile[i, j - 1].type] && Main.tile[i, j - 1].slope() == 0 && !Main.tile[i, j - 1].halfBrick())
                {
                    Main.tile[i, j].frameY = 18;
                }
                else
                {
                    if (Main.tile[i + 1, j].active() && Main.tileSolid[Main.tile[i + 1, j].type] && Main.tile[i + 1, j].slope() == 0 && !Main.tile[i + 1, j].halfBrick())
                    {
                        Main.tile[i, j].frameY = 36;
                    }
                    else
                    {
                        if (Main.tile[i - 1, j].active() && Main.tileSolid[Main.tile[i - 1, j].type] && Main.tile[i - 1, j].slope() == 0 && !Main.tile[i - 1, j].halfBrick())
                        {
                            Main.tile[i, j].frameY = 54;
                        }
                    }
                }
            }
            Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(18) * 18);
        }
    }
}