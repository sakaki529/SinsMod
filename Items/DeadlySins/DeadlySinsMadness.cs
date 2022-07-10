using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.DeadlySins
{
    public class DeadlySinsMadness : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sin of Madness");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 23;
            item.height = 29;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 11;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = SinsColor.Madness;
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.MadnessItem = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}