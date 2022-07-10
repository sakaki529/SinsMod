using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Eibon
{
	[AutoloadEquip(EquipType.Legs)]
	public class EibonLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eibon's Leggings");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 26;
			item.height = 16;
			item.rare = 11;
            item.value = Item.sellPrice(2, 0, 0, 0);
            item.defense = 50;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 160;
            player.magicCuffs = true;
            player.manaCost *= 0.8f;
        }
    }
}