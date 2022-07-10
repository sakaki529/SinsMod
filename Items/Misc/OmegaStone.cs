using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class OmegaStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Omega Stone");
            Tooltip.SetDefault("'You faced to madness bravely...'" + 
                "\nPermanently allow to use extra accessory also out of expert mode" + 
                "\nCan only be used if the Gift from Sentients has been used");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 4;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.consumable = true;
            item.expert = true;
            item.UseSound = SoundID.Item122;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(255, 255, 10);
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            return Main.expertMode && player.extraAccessory && modPlayer.ExtraAccessory2 && !modPlayer.ExtraAccessoryLimitBreak;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<SinsPlayer>().ExtraAccessoryLimitBreak = true;
            return true;
        }
    }
}