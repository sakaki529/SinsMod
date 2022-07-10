using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Items.Dyes
{
    public class TechnoDyeLightblue : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightblue Techno Dye");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.dye = (byte)GameShaders.Armor.GetShaderIdFromItemId(item.type);
            item.rare = 3;
            item.value = Item.buyPrice(0, 1, 50, 0);
        }
    }
}