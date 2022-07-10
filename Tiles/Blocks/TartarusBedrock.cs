using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Blocks
{
    public class TartarusBedrock : ModTile
    {
        public override void SetDefaults()
        {
            TileID.Sets.HellSpecial[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Ash][Type] = true;
            AddMapEntry(new Color(10, 0, 15));
            drop = ItemID.Obsidian;
            dustType = 0;
            minPick = 50000;
            soundType = SoundID.Tink;
            mineResist = 50f;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Main.tile[i, j].actuator(false);
            Tile tile = Main.tile[i, j];
            Vector2 vector = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                vector = Vector2.Zero;
            }
            Main.spriteBatch.Draw(mod.GetTexture("Glow/Tile/TartarusBedrock_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + vector, new Rectangle(tile.frameX, tile.frameY, 16, 16), SinsColor.BW, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = SinsColor.BW.R * 0.2f;
            g = 0f;
            b = SinsColor.BW.B * 0.3f;
        }
        public override bool Slope(int i, int j)
        {
            return Main.tile[i, j - 1].type != mod.TileType("AlterOfConfession");
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            //return SinsWorld.downedTartarus;
            return false;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void RandomUpdate(int i, int j)
        {
            if (Main.rand.Next(40) == 0 && SinsWorld.downedSins)
            {
                int num = WorldGen.genRand.Next(4);
                if (num == 0)
                {
                    i++;
                }
                if (num == 1)
                {
                    i--;
                }
                if (num == 2)
                {
                    j++;
                }
                if (num == 3)
                {
                    j--;
                }
                if (Main.tile[i, j] != null && !Main.tile[i, j].active() && !Main.tile[i, j].lava() && Main.tile[i, j].slope() == 0 && !Main.tile[i, j].halfBrick())
                {
                    Main.tile[i, j].type = (ushort)mod.TileType("BlackCrystals");
                    Main.tile[i, j].active(true);
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
                    WorldGen.SquareTileFrame(i, j, true);
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                    }
                }
            }
        }
    }
}