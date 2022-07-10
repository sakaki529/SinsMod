using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SinsMod.Tiles.Distortion
{
    public class DistortionHerb : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
            TileObjectData.newTile.AnchorValidTiles = new[]
            {
				mod.TileType("DistortionGrass")
            };
            TileObjectData.newTile.AnchorAlternateTiles = new[]
            {
                (int)TileID.ClayPot,
				//TileID.PlanterBox
            };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(130, 125, 60));
            soundType = 6;
            soundStyle = 1;
        }
        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
        public override bool Drop(int i, int j)
        {
            int stage = Main.tile[i, j].frameX / 18;
            if (stage == 2)
            {
                Item.NewItem(i * 16, j * 16, 0, 0, mod.ItemType("DistortionHerb"));
            }
            return false;
        }
        public override void RandomUpdate(int i, int j)
        {
            if (Main.tile[i, j].frameX == 0)
            {
                Main.tile[i, j].frameX += 18;
            }
            else if (Main.tile[i, j].frameX == 18)
            {
                Main.tile[i, j].frameX += 18;
            }
        }
    }
}