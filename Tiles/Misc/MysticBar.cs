using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SinsMod.Tiles.Misc
{
    public class MysticBar : ModTile
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
            modTranslation.SetDefault("Mystic Bar");
            AddMapEntry(new Color(85, 055, 255), modTranslation);
            drop = mod.ItemType("MysticBar");
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            int dust = Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 172, 0f, 0f, 1, default(Color), 1.5f);
            Main.dust[dust].noGravity = true;
            return false;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.15f;
            g = 0f;
            b = 0.45f;
        }
    }
}
