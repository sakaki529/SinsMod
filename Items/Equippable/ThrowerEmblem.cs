using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    public class ThrowerEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thrower Emblem");
            Tooltip.SetDefault("15% increased thrown damage");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.thrownDamage += 0.15f;
        }
    }
}