using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SinsMod.Tiles.Misc
{
    public class BlackCoinPile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileShine[Type] = 1000;
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tilePile[Type] = true;
            Main.tileMerge[Type][TileID.CopperCoinPile] = true;
            Main.tileMerge[Type][TileID.SilverCoinPile] = true;
            Main.tileMerge[Type][TileID.GoldCoinPile] = true;
            Main.tileMerge[Type][TileID.PlatinumCoinPile] = true;
            TileID.Sets.Falling[Type] = true;
            TileObjectData.addTile(Type);
            drop = mod.ItemType("BlackCoin");
            dustType = 11;
            soundStyle = 0;
            AddMapEntry(new Color(15, 15, 15));
        }
        /*public override bool CanPlace(int i, int j)
		{
			return Main.tile[i, j + 1].slope() == 0 && !Main.tile[i, j + 1].halfBrick();
		}*/
        public override bool KillSound(int i, int j)
        {
            Main.PlaySound(18, i, j, 10, 1f, 0f);
            return base.KillSound(i, j);
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            Main.PlaySound(18, i * 16, j * 16, 1, 1f, 0f);
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if (i > 5 && j > 5 && i < Main.maxTilesX - 5 && j < Main.maxTilesY - 5 && Main.tile[i, j] != null)
            {
                Tile tile = Main.tile[i, j];
                if (!WorldGen.noTileActions)
                {
                    if (Main.netMode == 0)
                    {
                        if (j < Main.maxTilesY && !Main.tile[i, j + 1].active())
                        {
                            Main.tile[i, j].active(false);
                            Main.tile[i, j].ClearTile();
                            int num = Projectile.NewProjectile(i * 16 + 8, j * 16 + 8, 0f, 2.5f, mod.ProjectileType("BlackCoinsFalling"), 200, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num].ai[0] = 1f;
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                    }
                    else if (Main.netMode == 2)
                    {
                        tile.active(false);
                        bool flag = false;
                        for (int k = 0; k < 1000; k++)
                        {
                            if (Main.projectile[k].active && Main.projectile[k].owner == Main.myPlayer && Main.projectile[k].type == mod.ProjectileType("BlackCoinsFalling") && Math.Abs(Main.projectile[k].timeLeft - 3600) < 60 && Main.projectile[k].Distance(new Vector2(i * 16 + 8, j * 16 + 10)) < 4f)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            int num2 = Projectile.NewProjectile(i * 16 + 8, j * 16 + 8, 0f, 2.5f, mod.ProjectileType("BlackCoinsFalling"), 200, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num2].velocity.Y = 0.5f;
                            Projectile proj = Main.projectile[num2];
                            proj.position.Y = proj.position.Y + 2f;
                            Main.projectile[num2].netUpdate = true;
                        }
                        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                        WorldGen.SquareTileFrame(i, j, true);
                    }
                }
            }
            return base.TileFrame(i, j, ref resetFrame, ref noBreak);
        }
    }
}