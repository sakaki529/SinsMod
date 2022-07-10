using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class HopelessMode : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hopeless Mode");
            Tooltip.SetDefault("Use this to turn on/off Hopeless Mode." +
                "\nThe player loses the immune frame while this mode is active." +
                "\nDouble the number of NPCs drops.");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.rare = 1;
            item.value = 0;
        }
        public override bool CanUseItem(Player player)
        {
            return !SinsNPC.BossActiveCheck();
        }
        public override bool UseItem(Player player)
        {
            Main.PlaySound(29, (int)player.position.X, (int)player.position.Y, 90, 1f, 0f);
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i] != null && !Main.npc[i].townNPC)
                {
                    Main.npc[i].life = 0;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0);
                    }
                }
            }
            SinsWorld.Hopeless = !SinsWorld.Hopeless;
            string key = "Mods.SinsMod.HopelessMode";
            string text = SinsWorld.Hopeless ? "activated" : "deactivated";
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue(key, text), SinsWorld.Hopeless ? new Color(255, 0, 0) : new Color(255, 255, 255));
            }
            else
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), SinsWorld.Hopeless ? new Color(255, 0, 0) : new Color(255, 255, 255));
            }
            return true;
        }
    }
}