using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Blocks
{
    public class CautionBlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Caution Block");
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
            item.rare = 1;
            item.createTile = mod.TileType("CautionBlock");
            item.maxStack = 999;
        }
    }
}