using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Eibon
{
	[AutoloadEquip(EquipType.Body)]
	public class EibonRobe : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eibon's Robe");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 34;
			item.height = 30;
			item.rare = 11;
            item.value = Item.sellPrice(2, 0, 0, 0);
            item.defense = 50;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool DrawBody()
        {
            return false;
        }
        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.35f;
            player.minionDamage += 0.35f;
            player.minionKB += 0.15f; ;
        }
    }
}