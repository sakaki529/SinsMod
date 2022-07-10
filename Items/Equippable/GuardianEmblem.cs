using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    public class GuardianEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guardian Emblem");
            Tooltip.SetDefault("2% increased damage reduction");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.accessory = true;
            item.defense = 6;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.02f;
        }
    }
}