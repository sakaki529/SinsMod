using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Walls
{
    public class DistortionWoodFence : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(155, 155, 95));
            drop = mod.ItemType("DistortionWoodFence");
            dustType = 32;
        }
    }
}