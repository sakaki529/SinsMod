using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class GloriousTrophy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glorious Trophy");
            Tooltip.SetDefault("Use this to turn enable/disable expert mode");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.UseSound = SoundID.Item29;
            item.rare = 9;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override bool CanUseItem(Player player)
        {
            return !SinsNPC.BossActiveCheck();
        }
        public override bool UseItem(Player player)
        {
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
            Main.expertMode = !Main.expertMode;
            string key = "Mods.SinsMod.ExpertMode";
            string text = Main.expertMode ? "enabled" : "disabled";
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue(key, text), Main.expertMode ? Colors.RarityCyan : Colors.RarityLime);
            }
            else
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), Main.expertMode ? Colors.RarityCyan : Colors.RarityLime);
            }
            return true;
        }
    }
}