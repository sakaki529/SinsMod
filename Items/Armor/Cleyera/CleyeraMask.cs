using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Cleyera
{
    [AutoloadEquip(EquipType.Head)]
	public class CleyeraMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cleyera Mask");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 24;
			item.height = 26;
			item.rare = 11;
            item.value = Item.sellPrice(5, 2, 9, 0);
            item.defense = 40;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("CleyeraRobe") && legs.type == mod.ItemType("CleyeraLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";
        }
        public override void UpdateEquip(Player player)
        {
            if (player.name == "Cleyera")
            {
                item.defense = 529;
            }
            else
            {
                item.defense = 40;
            }
        }
	}
}