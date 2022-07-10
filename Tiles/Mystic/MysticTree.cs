using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Mystic
{
    public class MysticTree : ModTree
    {
        private Mod mod => ModLoader.GetMod("SinsMod");

        public override int CreateDust()
        {
            return 17;
        }
        public override int GrowthFXGore()
        {
            return mod.GetGoreSlot("Gores/MysticTreeFX");
        }
        public override int DropWood()
        {
            return mod.ItemType("MysticWood");
        }
        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/Mystic/MysticTree");
        }
        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            frameWidth = 114;
            frameHeight = 98;
            yOffset += 2;
            xOffsetLeft += 16;
            return mod.GetTexture("Tiles/Mystic/MysticTree_Tops");
        }
        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            return mod.GetTexture("Tiles/Mystic/MysticTree_Branches");
        }
    }
}