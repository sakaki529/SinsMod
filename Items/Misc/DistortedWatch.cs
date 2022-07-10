using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class DistortedWatch : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Distorted Watch");
			Tooltip.SetDefault("Changes day to night, night to day.");
        }
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 50;
            item.useStyle = 4;
            item.useAnimation = 60;
            item.useTime = 60;
            item.autoReuse = true;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SnapFingers");
            item.rare = 9;
			item.value = Item.sellPrice(0, 1, 0, 0);
		}
        public override bool UseItem(Player player)
        {
            /*if (Main.dayTime)
            {
                Main.dayTime = !Main.dayTime;
                Main.time = 0.0;
                Main.NewText("Night comes on.", new Color(100, 255, 100));
            }
            else
            {
                Main.time = 54000.0;
                Main.NewText("Morning comes on.", new Color(100, 255, 100));
            }*/
            Main.dayTime = !Main.dayTime;
            Main.time = 0.0;
            if (Main.dayTime && ++Main.moonPhase >= 8)
            {
                Main.moonPhase = 0;
            }
            CultistRitual.delay = 0;
            CultistRitual.recheck = 0;
            string key = "Mods.SinsMod.Time";
            string text = Main.dayTime ? "Morning" : "Night";
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue(key, text), new Color(100, 255, 100));
            }
            else
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), new Color(100, 255, 100));
            }
            return true;
        }
    }
}