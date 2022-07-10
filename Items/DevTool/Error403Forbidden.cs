using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.DevTool
{
    public class Error403Forbidden : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("[DATA EXPUNGED]");
			Tooltip.SetDefault("[c/ff0000:DATA REDACTED]");
        }
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
            item.useStyle = 4;
            item.useTime = 1;
            item.useAnimation = 10;
            item.autoReuse = true;
            item.rare = -1;
			item.value = Item.sellPrice(0, 0, 0, 0);
            item.useTurn = true;
            item.GetGlobalItem<SinsItem>().isDevTools = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(255, 0, 0);
                }
            }
        }
        public override bool UseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (modPlayer.Debug)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].friendly && Main.npc[i].type != NPCID.TargetDummy)
                    {
                        Main.npc[i].life = 1;
                        Main.npc[i].StrikeNPCNoInteraction(1 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(1, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.npc[i].lifeMax + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.npc[i].lifeMax, 0f, -Main.npc[i].direction, false, false, false);
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(28, -1, -1, null, 1, Main.npc[i].lifeMax, 0f, -Main.npc[i].direction, 1);
                        }
                    }
                }
            }
            else
            {
                Main.instance = null;
            }
            return true;
        }
        /*public override bool UseItem(Player player)
		{
			while (true) {}
			return true;
		}*/
        //Environment.Exit(-1);
    }
}