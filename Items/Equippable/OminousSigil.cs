using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    public class OminousSigil : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ominous Sigil");
            Tooltip.SetDefault("");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 1;
            item.accessory = true;
            item.glowMask = customGlowMask;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

        }
    }
}