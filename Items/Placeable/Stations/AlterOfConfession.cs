using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Stations
{
    public class AlterOfConfession : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alter of Confession");
        }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = Item.buyPrice(0, 0, 0, 7);
            item.rare = 11;
            item.createTile = mod.TileType("AlterOfConfession");
            item.maxStack = 99;
        }
    }
}