using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SinsMod.Tiles.Misc
{
    public class NightEnergizedBar : ModTile
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
            modTranslation.SetDefault("Nightenergized Bar");
            AddMapEntry(new Color(60, 60, 60), modTranslation);
            dustType = 175;
            drop = mod.ItemType("NightEnergizedBar");
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.2f;
            g = 0.2f;
            b = 0.2f;
        }
    }
}
