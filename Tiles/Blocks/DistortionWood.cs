using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Blocks
{
    public class DistortionWood : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(160, 160, 100));
            drop = mod.ItemType("DistortionWood");
            dustType = 32;
            minPick = 0;
        }
    }
}