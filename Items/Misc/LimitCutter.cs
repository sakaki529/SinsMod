using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class LimitCutter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Limit Cutter");
            Tooltip.SetDefault("Use this to turn on/off Limit Cut mode." +
                "\nCan only be used in expert mode.");//halves crit chance
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
            item.UseSound = SinsWorld.LimitCut ? SoundID.Item105 : SoundID.Item101;
            return !SinsNPC.BossActiveCheck() && Main.expertMode;
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
            SinsWorld.LimitCut = !SinsWorld.LimitCut;
            string key = "Mods.SinsMod.LimitCut";
            string text = SinsWorld.LimitCut ? "activated" : "deactivated";
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue(key, text), SinsWorld.LimitCut ? SinsColor.UniqueSkyBlue : new Color(255, 255, 255));
            }
            else
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), SinsWorld.LimitCut ? SinsColor.UniqueSkyBlue : new Color(255, 255, 255));
            }
            return true;
        }
    }
}