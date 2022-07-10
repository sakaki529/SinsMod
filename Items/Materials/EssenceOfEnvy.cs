﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
	public class EssenceOfEnvy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Essence of Envy");
			Tooltip.SetDefault("");
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = false;
        }
		public override void SetDefaults()
		{
            item.width = 26;
            item.height = 28;
            item.maxStack = 999;
			item.rare = 11;
			item.value = Item.sellPrice(2, 0, 0, 0);
		}
	}
}