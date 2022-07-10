using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Blocks
{
    public class MysticStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Stone");
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
            item.value = 0;
            item.rare = 0;
            item.createTile = mod.TileType("MysticStone");
            item.maxStack = 999;
        }
    }
}