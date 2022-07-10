using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Walls
{
    public class DistortionWoodWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(160, 160, 100));
            drop = mod.ItemType("DistortionWoodWall");
            dustType = 32;
        }
    }
}