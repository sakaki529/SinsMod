using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class SandStoneCross : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sand Stone Cross");
            Tooltip.SetDefault("Starts/Stops the sand storm.");
        }
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 26;
            item.useStyle = 4;
            item.useAnimation = 60;
            item.useTime = 60;
            item.autoReuse = true;
            item.UseSound = SoundID.Item44;
            item.rare = 9;
			item.value = Item.sellPrice(0, 1, 0, 0);
		}
        public override bool UseItem(Player player)
        {
            Sandstorm.Happening = !Sandstorm.Happening;
            if (Sandstorm.Happening)
            {
                Sandstorm.TimeLeft = (int)(3600f * (8f + Main.rand.NextFloat() * 16f));
                Sandstorm.IntendedSeverity = 0.4f + Main.rand.NextFloat();
                if (Main.netMode != 1)
                {
                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
            string key = "Mods.SinsMod.Sandstorm";
            string text = Sandstorm.Happening ? "happened" : "stopped";
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