using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Walls
{
    public class DistortionGrassWall : ModWall
    {
        public override void SetDefaults()
        {
            WallID.Sets.Conversion.Grass[Type] = true;
            AddMapEntry(new Color(130, 125, 60));
            dustType = 32;
            soundType = 6;
            soundStyle = 1;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}