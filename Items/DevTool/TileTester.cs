using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.DevTool
{
    public class TileTester : ModItem
    {
        public override string Texture => "SinsMod/Items/DevTool/Book";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tile Tester");
            Tooltip.SetDefault("[c/ff0000:Dev Tool]");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = 0;
            item.rare = -1;
            item.createTile = mod.TileType("BlackCoinPile");
            item.maxStack = 999;
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
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (modPlayer.Debug)
            {
                if (player.altFunctionUse == 2)
                {
                    item.createTile = 0;
                }
            }
            else
            {
                throw new Exception("This is Dev tool. You should not use this.");
            }
            return true;
        }
    }
}