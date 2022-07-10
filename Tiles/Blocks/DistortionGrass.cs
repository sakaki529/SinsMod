using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Blocks
{
    public class DistortionGrass : ModTile
    {
        public override void SetDefaults()
        {
            TileID.Sets.Conversion.Grass[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(130, 125, 60));
            drop = ItemID.DirtBlock;
            dustType = 32;
            minPick = 0;
            SetModTree(new Distortion.DistortionTree());
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail && !effectOnly)
            {
                Main.tile[i, j].type = TileID.Dirt;
            }
        }
        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("DistortionSapling");
        }
    }
}