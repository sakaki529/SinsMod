using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SinsMod.Tiles.Trophies
{
    public class Trophy : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            ModTranslation modTranslation = CreateMapEntryName();
            modTranslation.SetDefault("Trophy");
            AddMapEntry(new Color(120, 85, 60), modTranslation);
            dustType = 7;
            disableSmartCursor = true;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int item = 0;
            switch (frameX / 54)
            {
                case 0:
                    item = mod.ItemType("VortexPillarTrophy");
                    break;
                case 1:
                    item = mod.ItemType("StardustPillarTrophy");
                    break;
                case 2:
                    item = mod.ItemType("NebulaPillarTrophy");
                    break;
                case 3:
                    item = mod.ItemType("SolarPillarTrophy");
                    break;
            }
            if (item > 0)
            {
                Item.NewItem(i * 16, j * 16, 48, 48, item);
            }
        }
    }
}