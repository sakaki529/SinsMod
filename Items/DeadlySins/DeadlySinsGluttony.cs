using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.DeadlySins
{
    public class DeadlySinsGluttony : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sin of Gluttony");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 23;
            item.height = 29;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 11;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = SinsColor.Gluttony;
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.GluttonyItem = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}