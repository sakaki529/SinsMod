using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    public class VitalityStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitality Stone");
            Tooltip.SetDefault("Provides life regeneration" +
                "\nIncreases damage and crit chance up to 10% as your current life closer to the maximum life");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 20;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.VitalityStone = true;
            player.lifeRegen += 2;
        }
    }
}