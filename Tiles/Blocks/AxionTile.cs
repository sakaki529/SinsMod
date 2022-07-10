using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Blocks
{
    public class AxionTile : ModTile
    {
        public override void SetDefaults()
        {
            //TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileShine[Type] = 200;
            Main.tileShine2[Type] = true;
            Main.tileBlockLight[Type] = true;
            //Main.tileSpelunker[Type] = true;
            ModTranslation modTranslation = CreateMapEntryName();
            modTranslation.SetDefault("Axion");
            AddMapEntry(new Color(30, 10, 45), modTranslation);
            minPick = 1200;
            soundType = SoundID.Tink;
            dustType = 27;
            drop = mod.ItemType("Axion");
            mineResist = 50f;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.1f;
            g = 0.0f;
            b = 0.15f;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}