using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss.BossBag
{
	public class PrideBag : ModItem
	{
        public override int BossBagNPC => mod.NPCType("Pride");
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
                player.QuickSpawnItem(mod.ItemType(""));
            }
            player.QuickSpawnItem(mod.ItemType("EssenceOfPride"), Main.rand.Next(6, 13));
            switch (Main.rand.Next(3))
            {
                case 0:
                    player.QuickSpawnItem(mod.ItemType("Proudia"));
                    break;
                case 1:
                    player.QuickSpawnItem(mod.ItemType(""));
                    break;
                case 2:
                    player.QuickSpawnItem(mod.ItemType(""));
                    break;
            }
        }
	}
}