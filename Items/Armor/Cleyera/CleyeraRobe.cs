using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Cleyera
{
	[AutoloadEquip(EquipType.Body)]
	public class CleyeraRobe : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cleyera Robe");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 44;
			item.rare = 11;
            item.value = Item.sellPrice(5, 2, 9, 0);
            item.defense = 40;
        }
        public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
            robes = true;
            equipSlot = mod.GetEquipSlot("CleyeraRobe_Legs", EquipType.Legs);
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
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = false;
        }
    }
}