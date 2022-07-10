using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.DeadlySins
{
    public class DeadlySinsThanks : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Deadly Sins");
			Tooltip.SetDefault("Thanks for playing this mod!" +
                "\nBlushiemagic: Some cords from blushie's mods" +
                "\nJeff Carlisle: Design of Sins Rune" +
                "\n[c/e4b400:-Sprite-]" +
                "\nCalming" +
                "\nNorcle");
        }
		public override void SetDefaults()
		{
			item.width = 23;
			item.height = 29;
			item.rare = -12;
			item.value = Item.sellPrice(0, 0, 0, 0);
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = Main.DiscoColor;
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.SinsThanksItem = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor;
        }
    }
}