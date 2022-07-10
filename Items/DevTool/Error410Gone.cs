using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

namespace SinsMod.Items.DevTool
{
    public class Error410Gone : ModItem
	{
        private bool EndDelItems;
        private bool EndDelTiles;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("[DATA EXPUNGED]");
			Tooltip.SetDefault("[c/ff0000:DATA REDACTED]");
        }
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
            item.autoReuse = false;
            item.useTurn = true;
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
        public override bool CanUseItem(Player player)
        {
            return false;
        }
        public override bool UseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (modPlayer.Debug)
            {
                
            }
            else
            {
                for (int i = 0; i < 22; i++)
                {
                    player.buffTime[i] = 0;
                }
                for (int i = 0; i < 59; i++)
                {
                    if (i < player.inventory.Length)
                    {
                        player.inventory[i].stack = 0;
                    }
                    if (i < player.armor.Length)
                    {
                        player.armor[i].type = 0;
                    }
                    if (i < player.dye.Length)
                    {
                        player.dye[i].stack = 0;
                    }
                    if (i < player.miscEquips.Length)
                    {
                        player.miscEquips[i].stack = 0;
                    }
                    if (i < player.miscDyes.Length)
                    {
                        player.miscDyes[i].stack = 0;
                    }
                    if (i < player.bank.item.Length)
                    {
                        player.bank.item[i].stack = 0;
                    }
                    if (i < player.bank2.item.Length)
                    {
                        player.bank2.item[i].stack = 0;
                    }
                    if (i < player.bank3.item.Length)
                    {
                        player.bank3.item[i].stack = 0;
                    }
                    if (i == 58)
                    {
                        EndDelItems = true;
                    }
                }
                player.head = -1;
                player.body = -1;
                player.legs = -1;
                player.handon = -1;
                player.handoff = -1;
                player.back = -1;
                player.front = -1;
                player.shoe = -1;
                player.waist = -1;
                player.shield = -1;
                player.neck = -1;
                player.face = -1;
                player.balloon = -1;
                player.wings = -1;
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        SinsTile.Gone(i, j);
                        if (WorldGen.InWorld(i, j))
                        {
                            Main.Map.Update(i, j, 255);
                        }
                        if (i == Main.maxTilesX - 1 && j == Main.maxTilesY - 1)
                        {
                            EndDelTiles = true;
                        }
                    }
                }
                if (EndDelItems && EndDelTiles)
                {
                    WorldFile.saveWorld();
                    player.KillMeForGood();
                }
                /*for (int i = 0; i < 40; i++)
                {
                    player.bank.item[i].stack = 0;
                    player.bank2.item[i].stack = 0;
                    player.bank3.item[i].stack = 0;
                }
                for (int i = 0; i < 58; i++)
                {
                    player.inventory[i].stack = 0;
                    if (i == 57)
                    {
                        EndDelItems = true;
                    }
                }*/
                /*for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        WorldGen.KillTile(i, j, false, false, true);
                        WorldGen.KillWall(i, j, false);
                        Main.tile[i, j].liquid = 0;
                        if (i == Main.maxTilesX - 1 && j == Main.maxTilesY - 1)
                        {
                            WorldFile.saveWorld();
                        }
                    }
                }
                Main.refreshMap = true;*/
            }
            return true;
        }
    }
}