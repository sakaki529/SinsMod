using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Eibon
{
    [AutoloadEquip(EquipType.Head)]
	public class EibonMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eibon's Mask");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 26;
			item.height = 23;
			item.rare = 11;
            item.value = Item.sellPrice(2, 0, 0, 0);
            item.defense = 50;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("EibonRobe") && legs.type == mod.ItemType("EibonLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setEibon = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 7;
            player.maxMinions += 2;
            player.maxTurrets += 1;
        }
    }
}