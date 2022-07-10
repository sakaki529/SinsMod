using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Blocks
{
    public class NightmareOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightmare Ore");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = Item.buyPrice(0, 0, 30, 0);
            item.rare = 11;
            item.createTile = mod.TileType("NightmareOre");
            item.maxStack = 999;
        }
    }
}