using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SinsMod.Tiles.Misc
{
    public class NightmareBar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileShine[Type] = 1000;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            ModTranslation modTranslation = CreateMapEntryName();
            modTranslation.SetDefault("Nightmare Bar");
            AddMapEntry(new Color(150, 150, 150), modTranslation);
            drop = mod.ItemType("NightmareBar");
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            int dust = Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 172, 0f, 0f, 1, default(Color), 1.0f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 0.9f + Utils.NextFloat(Main.rand, 0.2f);
            Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
            return false;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.2f;
            g = 0.2f;
            b = 0.25f;
        }
    }
}
