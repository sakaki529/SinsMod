using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Walls
{
    public class MysticWoodWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(55, 64, 55));
            drop = mod.ItemType("MysticWoodWall");
            dustType = 17;
        }
    }
}