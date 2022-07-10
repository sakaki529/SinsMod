using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Potions
{
    public class ManaElixir : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mana Elixir");
        }
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 999;
            item.consumable = true; 
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 11;
            item.healMana = 100;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            int HealLife = list.FindIndex(line => line.Name == "HealMana");
            list.RemoveAt(HealLife);
            list.Insert(HealLife, new TooltipLine(mod, "HealMana", "Restores your mana to max"));
        }
        public override bool UseItem(Player player)
        {
            item.healMana = player.statManaMax2;
            return true;
        }
    }
}