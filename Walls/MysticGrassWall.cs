using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Walls
{
    public class MysticGrassWall : ModWall
    {
        public override void SetDefaults()
        {
            WallID.Sets.Conversion.Grass[Type] = true;
            AddMapEntry(new Color(52, 83, 106));
            dustType = 13;
            soundType = 6;
            soundStyle = 1;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}