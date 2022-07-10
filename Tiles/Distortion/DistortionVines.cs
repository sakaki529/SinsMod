using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Distortion
{
    public class DistortionVines : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            //Main.tileBlockLight[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileNoFail[Type] = true;
            AddMapEntry(new Color(130, 125, 60));
            dustType = 32;
            soundType = 6;
            soundStyle = 1;
            drop = mod.ItemType("CautionBlock");
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (WorldGen.genRand.Next(2) == 0 && Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)].cordage)
            {
                Item.NewItem(new Vector2((i * 16) + 8f, (j * 16) + 8f), ItemID.VineRope, 1, false, 0, false, false);
            }
        }
    }
}