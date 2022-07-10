using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod
{
    public class SinsTile : GlobalTile
    {
        internal static void Reset(int i, int j)
        {

        }
        internal static void Gone(int i, int j)
        {
            if (Main.tile[i, j] != null)
            {
                Main.tile[i, j].sTileHeader = 0;
                Main.tile[i, j].wall = 0;
                Main.tile[i, j].liquid = 0;
                if (WorldGen.InWorld(i, j))
                {
                    Main.Map.Update(i, j, 255);
                }
            }
        }
        public static void Convert(int i, int j, int conversionType, int size = 4)
        {
            Mod mod = SinsMod.Instance;
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < 6)
                    {
                        Tile tile = Main.tile[k, l];
                        int type = Main.tile[k, l].type;
                        int wall = Main.tile[k, l].wall;
                        bool sendNet = false;
                        if (conversionType == 0)//Pure
                        {
                            if (type == (ushort)mod.TileType("MysticGrass") || type == (ushort)mod.TileType("DistortionGrass"))
                            {
                                Main.tile[k, l].type = TileID.Grass;
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (type == (ushort)mod.TileType("MysticStone") || type == (ushort)mod.TileType("DistortionStone"))
                            {
                                Main.tile[k, l].type = TileID.Stone;
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            if (wall == (ushort)mod.WallType("MysticGrassWall") || wall == (ushort)mod.WallType("DistortionGrassWall"))
                            {
                                Main.tile[k, l].wall = WallID.Grass;
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (wall == (ushort)mod.WallType("MysticStoneWall") || wall == (ushort)mod.WallType("DistortionStoneWall"))
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                        }
                        if (sendNet)
                        {
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                    }
                }
            }
        }
    }
}