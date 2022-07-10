using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss.BossBag
{
	public class TartarusBag : ModItem
	{
        public override int BossBagNPC => mod.NPCType("TartarusHead");
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = 11;
			item.expert = true;
		}
		public override bool CanRightClick()
		{
			return true;
		}
		public override void OpenBossBag(Player player)
		{
            player.TryGettingDevArmor();
            player.QuickSpawnItem(mod.ItemType("AbyssalFlameRelic"));
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("TartarusMask"));
            }
            player.QuickSpawnItem(mod.ItemType("Axion"), Main.rand.Next(13, 25));
            switch (Main.rand.Next(3))
            {
                case 0:
                    player.QuickSpawnItem(mod.ItemType("TartarusWhip"));
                    break;
                case 1:
                    player.QuickSpawnItem(mod.ItemType("AbyssalFlamethrower"));
                    break;
                case 2:
                    player.QuickSpawnItem(mod.ItemType("AbyssalStaff"));
                    break;
                case 3:
                    player.QuickSpawnItem(mod.ItemType("AbyssalGuardianStaff"));
                    break;
            }
        }
	}
}