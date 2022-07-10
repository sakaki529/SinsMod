using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Blocks
{
    public class NightEnergizedOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightenergized Ore");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 14;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = Item.buyPrice(0, 0, 5, 0);
            item.rare = 4;
            item.createTile = mod.TileType("NightEnergizedOre");
            item.consumable = true;
            item.maxStack = 999;
        }
    }
}