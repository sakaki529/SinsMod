using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.DevTool
{
    public class Error404NotFound : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("[DATA EXPUNGED]");
			Tooltip.SetDefault("[c/ff0000:DATA REDACTED]");
        }
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
            item.useStyle = 4;
            item.useTime = 30;
            item.useAnimation = 30;
            item.autoReuse = false;
            item.rare = -1;
			item.value = Item.sellPrice(0, 0, 0, 0);
            item.useTurn = true;
            item.GetGlobalItem<SinsItem>().isDevTools = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(255, 0, 0);
                }
            }
        }
        public override bool UseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (modPlayer.Cleyera)
            {
                
            }
            else
            {
                SinsMod.Instance = null;
            }
            return true;
        }
    }
}