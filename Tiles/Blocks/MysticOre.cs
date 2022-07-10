using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Blocks
{
    public class MysticOre : ModTile
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
            modTranslation.SetDefault("Mystic Ore");
            AddMapEntry(new Color(85, 055, 255), modTranslation);
            minPick = 100;
            soundType = SoundID.Tink;
            drop = mod.ItemType("MysticOre");
            mineResist = 2f;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            int dust = Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 172, 0f, 0f, 1, default(Color), 1.5f);
            Main.dust[dust].noGravity = true;
            return false;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.3f;
            g = 0f;
            b = 0.95f;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}