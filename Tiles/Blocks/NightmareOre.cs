using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Tiles.Blocks
{
    public class NightmareOre : ModTile
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
            modTranslation.SetDefault("Nightmare Ore");
            AddMapEntry(new Color(120, 120, 120), modTranslation);
            minPick = 600;
            soundType = SoundID.Tink;
            drop = mod.ItemType("NightmareOre");
            mineResist = 5f;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            int dust = Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 172, 0f, 0f, 1, default(Color), 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1.0f + Utils.NextFloat(Main.rand, 1.0f);
            Main.dust[dust].scale = 0.9f + Utils.NextFloat(Main.rand, 0.2f);
            Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
            return false;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.4f;
            g = 0.4f;
            b = 0.5f;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}