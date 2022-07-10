using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Dyes
{
    public class InvisibleDye : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invisible Dye");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            byte dye = item.dye;
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.dye = dye;
            item.rare = 1;
            item.value = Item.buyPrice(0, 1, 0, 0);
        }
    }
}