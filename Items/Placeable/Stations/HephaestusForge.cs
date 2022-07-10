using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Stations
{
    public class HephaestusForge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hephaestus Forge");
        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 34;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 9;
            item.createTile = mod.TileType("HephaestusForge");
            item.maxStack = 99;
        }
    }
}