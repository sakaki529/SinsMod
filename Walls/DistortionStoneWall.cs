using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Walls
{
    public class DistortionStoneWall : ModWall
    {
        public override void SetDefaults()
        {
            WallID.Sets.Conversion.Stone[Type] = true;
            AddMapEntry(new Color(140, 150, 130));
            dustType = 11;
            soundType = 21;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}