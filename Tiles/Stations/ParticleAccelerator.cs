using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SinsMod.Tiles.Stations
{
    public class ParticleAccelerator : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.EmptyTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.addTile(Type);
            ModTranslation modTranslation = CreateMapEntryName();
            modTranslation.SetDefault("Particle Accelerator");
            AddMapEntry(new Color(200, 255, 200), modTranslation);
            minPick = 0;
            dustType = 226;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 vector = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                vector = Vector2.Zero;
            }
            //Main.spriteBatch.Draw(mod.GetTexture("Glow/Tile/ParticleAccelerator_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + vector, new Rectangle(tile.frameX, tile.frameY, 16, 16), new Color(0, 100 + (int)(Main.DiscoG * 1.0f), 100 + (int)(Main.DiscoG * 1.0f)), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(mod.GetTexture("Glow/Tile/ParticleAccelerator_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + vector, new Rectangle(tile.frameX, tile.frameY, 16, 16), Color.Lerp(Color.Teal, Color.Aquamarine, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.4f + 0.5f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 64, 32, mod.ItemType("ParticleAccelerator"));
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            r = 0.3f;
            g = 0.5f;
            b = 0.3f;
        }
    }
}