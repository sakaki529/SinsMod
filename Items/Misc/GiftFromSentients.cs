using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class GiftFromSentients : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gift from Sentients");
            Tooltip.SetDefault("'Decaying your impurity...'" + 
                "\nPermanently increases the number of accessory slots to 7" + 
                "\nCan only be used if the Demon Heart has been used");
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
                    line.overrideColor = new Color(180, 40, 255);
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            return Main.expertMode && player.extraAccessory && !modPlayer.ExtraAccessory2;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<SinsPlayer>().ExtraAccessory2 = true;
            return true;
        }
    }
}