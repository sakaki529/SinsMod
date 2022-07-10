using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Blocks
{
    public class MysticWood : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(66, 75, 66));
            drop = mod.ItemType("MysticWood");
            dustType = 17;
            minPick = 0;
        }
    }
}