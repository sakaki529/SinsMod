using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    [AutoloadEquip(EquipType.Waist)]
    public class NightmareBottle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bottle in a Nightmare");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.value = Item.sellPrice(0, 0, 70, 0);
            item.rare = 7;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.controlJump && player.wingTime > 0f && !player.jumpAgainCloud && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
            {
            }
        }
    }
}