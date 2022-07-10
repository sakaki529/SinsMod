using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.DevTool
{
    public class Error418ImaTeapot : ModItem
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
            item.useTurn = true;
            item.autoReuse = false;
            item.useStyle = 4;
            item.useTime = 30;
            item.useAnimation = 30;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = -1;
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
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active && !Main.projectile[i].minion)
                    {
                        Main.projectile[i].friendly = !Main.projectile[i].friendly;
                        Main.projectile[i].hostile = !Main.projectile[i].hostile;
                        Main.projectile[i].velocity *= -1;
                        Main.projectile[i].owner = player.whoAmI;
                    }
                }
            }
            else
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        for (int i = 0; i < 200; i++)
                        {
                            Main.npc[i].type = NPCID.Guide;
                        }
                        break;
                    case 1:
                        for (int i = 0; i < 200; i++)
                        {
                            Main.npc[i].type = NPCID.Bunny;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 200; i++)
                        {
                            Main.npc[i].type = NPCID.BlueSlime;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 200; i++)
                        {
                            Main.npc[i].type = NPCID.WyvernHead;
                        }
                        break;
                    case 4:
                        for (int i = 0; i < 200; i++)
                        {
                            Main.npc[i].type = NPCID.DungeonGuardian;
                        }
                        break;
                }
            }
            return true;
        }
    }
}