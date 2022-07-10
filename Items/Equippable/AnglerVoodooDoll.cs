using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    public class AnglerVoodooDoll : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angler Voodoo Doll");
            Tooltip.SetDefault("'You are a terrible person.'");
        }
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 28;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 0;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.KillAngler = true;
        }
        public override void PostUpdate()
        {
            if (item.lavaWet)
            {
                for (int i = 0; i < 200; ++i)
                {
                    if ((Main.npc[i].type == NPCID.Angler || Main.npc[i].type == NPCID.SleepingAngler) && Main.npc[i].active)
                    {
                        Player player = Main.player[item.owner];
                        Main.npc[i].StrikeNPCNoInteraction(9999, 10f, -Main.npc[i].direction, false, false, false);
                    }
                }
            }
        }
    }
}