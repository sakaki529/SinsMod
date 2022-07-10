using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.DevTool
{
    public class MeteorTester : ModItem
	{
        public override string Texture => "SinsMod/Items/DevTool/Book";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Meteor Tester");
			Tooltip.SetDefault("[c/ff0000:Dev Tool]");
        }
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
            item.autoReuse = false;
            item.useTurn = true;
            item.useStyle = 4;
            item.useTime = 60;
            item.useAnimation = 60;
			item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = -1;
            item.GetGlobalItem<SinsItem>().isDevTools = true;
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(140, 140, 255);
                }
            }
        }
        public override bool UseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (modPlayer.Debug)
            {
                SinsWorld.MeteorOreDrop(mod.TileType("NightEnergizedOre"));
            }
            else
            {
                throw new Exception("This is Dev tool. You should not use this.");
            }
            return true;
        }
    }
}