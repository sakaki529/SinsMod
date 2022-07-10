using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Paintings
{
    public class TheBlackMan : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Black Man");
            //Tooltip.SetDefault("''");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.value = 10000;
            item.rare = 1;
            item.createTile = mod.TileType("TheBlackMan");
        }
    }
}