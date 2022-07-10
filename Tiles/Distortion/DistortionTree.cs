using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Distortion
{
    public class DistortionTree : ModTree
    {
        private Mod mod => ModLoader.GetMod("SinsMod");

        public override int CreateDust()
        {
            return 17;
        }
        public override int GrowthFXGore()
        {
            return mod.GetGoreSlot("Gores/DistortionTreeFX");
        }
        public override int DropWood()
        {
            return mod.ItemType("DistortionWood");
        }
        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/Distortion/DistortionTree");
        }
        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            return mod.GetTexture("Tiles/Distortion/DistortionTree_Tops");
        }
        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            return mod.GetTexture("Tiles/Distortion/DistortionTree_Branches");
        }
    }
}