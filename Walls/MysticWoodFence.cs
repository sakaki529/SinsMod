using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Walls
{
    public class MysticWoodFence : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(50, 59, 50));
            drop = mod.ItemType("MysticWoodFence");
            dustType = 17;
        }
    }
}