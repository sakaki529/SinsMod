using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Misc
{
    public class DistortionHerbSeeds : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Distortion Seed");
        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 14;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.value = Item.buyPrice(0, 0, 0, 80);
            item.maxStack = 99;
            item.placeStyle = 0;
            item.createTile = mod.TileType("DistortionHerb");
        }
    }
}