using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class MoonRing : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Ring");
            Tooltip.SetDefault("Left click to rise/over the blood moon." +
                "\nRight click to happen/over a eclipse.");
        }
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
            item.autoReuse = true;
            item.useStyle = 4;
            item.useTime = 60;
            item.useAnimation = 60;
            item.reuseDelay = 120;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 9;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Bell");
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                if (!Main.dayTime)
                {
                    Main.bloodMoon = !Main.bloodMoon;
                    string key = "Mods.SinsMod.BloodMoon";
                    string text = Main.bloodMoon ? "is rising..." : "has over.";
                    if (Main.netMode != 2)
                    {
                        Main.NewText(Language.GetTextValue(key, text), new Color(100, 255, 100));
                    }
                    else
                    {
                        NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                        NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), new Color(100, 255, 100));
                    }
                }
            }
            else
            {
                if (Main.dayTime)
                {
                    Main.eclipse = !Main.eclipse;
                    string key = "Mods.SinsMod.Eclipse";
                    string text = Main.eclipse ? "is happening!" : "has over.";
                    if (Main.netMode != 2)
                    {
                        Main.NewText(Language.GetTextValue(key, text), new Color(100, 255, 100));
                    }
                    else
                    {
                        NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                        NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), new Color(100, 255, 100));
                    }
                }
            }
            return true;
        }
    }
}