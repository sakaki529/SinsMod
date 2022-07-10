using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    public class EternalGear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Gear");
            Tooltip.SetDefault("\nAllows infinitite flight" +
                "\nDecreases movement speed" +
                "\n[c/ff0000:*There is possibility you lose infinite flight when your flight time is controlled by debuffs, npcs and etc.]");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.rare = 7;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.InfinityFlight = true;
            modPlayer.eGear = true;
        }
    }
}
