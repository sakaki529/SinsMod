using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss.BossBag
{
	public class KingMetalSlimeBag : ModItem
	{
        public override int BossBagNPC => mod.NPCType("KingMetalSlime");
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
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("KingMetalSlimeMask"));
            }
            player.QuickSpawnItem(ItemID.Gel, Main.rand.Next(25, 36));
            player.QuickSpawnItem(ItemID.IronBar, Main.rand.Next(15, 26));
            player.QuickSpawnItem(ItemID.HallowedBar, Main.rand.Next(20, 36));
            player.QuickSpawnItem(ItemID.SoulofFright, Main.rand.Next(8, 17));
            player.QuickSpawnItem(ItemID.SoulofMight, Main.rand.Next(8, 17));
            player.QuickSpawnItem(ItemID.SoulofSight, Main.rand.Next(8, 17));
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(ItemID.MetalDetector);
            }
        }
	}
}