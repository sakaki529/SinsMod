using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Blocks
{
    public class MysticOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Ore");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = Item.buyPrice(0, 0, 15, 0);
            item.rare = 9;
            item.createTile = mod.TileType("MysticOre");
            item.consumable = true;
            item.maxStack = 999;
        }
    }
}