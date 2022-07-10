using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Cleyera
{
	[AutoloadEquip(EquipType.Legs)]
	public class CleyeraLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cleyera Leggings");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 30;
			item.height = 18;
			item.rare = 11;
            item.value = Item.sellPrice(5, 2, 9, 0);
            item.defense = 40;
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