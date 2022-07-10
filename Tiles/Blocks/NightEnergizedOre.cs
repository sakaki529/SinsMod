using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Blocks
{
    public class NightEnergizedOre : ModTile
    {
        public override void SetDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileShine[Type] = 800;
            Main.tileShine2[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSpelunker[Type] = true;
            ModTranslation modTranslation = CreateMapEntryName();
            modTranslation.SetDefault("Nightenergy Cluster");
            AddMapEntry(new Color(40, 40, 40), modTranslation);
            minPick = 100;
            soundType = SoundID.Tink;
            dustType = 175;
            drop = mod.ItemType("NightEnergizedOre");
            mineResist = 3f;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.2f;
            g = 0.2f;
            b = 0.2f;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}