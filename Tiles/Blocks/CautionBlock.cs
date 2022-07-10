using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Blocks
{
    public class CautionBlock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            ModTranslation modTranslation = CreateMapEntryName();
            modTranslation.SetDefault("Caution Block");
            AddMapEntry(new Color(255, 255, 0), modTranslation);
            minPick = 0;
            soundType = SoundID.Tink;
            drop = mod.ItemType("CautionBlock");
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}