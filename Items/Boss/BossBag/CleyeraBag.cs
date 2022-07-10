using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss.BossBag
{
	public class CleyeraBag : ModItem
	{
        public static short customGlowMask = 0;
        //public override int BossBagNPC => mod.NPCType("CleyeraFinal");
        public override bool Autoload(ref string name)
        {
            return false;
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = 11;
			item.expert = true;
            item.glowMask = customGlowMask;
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
            if (Main.rand.Next(1000) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("XBlade"));
            }
            if (Main.rand.Next(4) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(mod.ItemType(""));
                        break;
                    case 1:
                        player.QuickSpawnItem(mod.ItemType(""));
                        break;
                    case 2:
                        player.QuickSpawnItem(mod.ItemType(""));
                        break;
                    case 3:
                        player.QuickSpawnItem(mod.ItemType(""));
                        break;
                    case 4:
                        player.QuickSpawnItem(mod.ItemType(""));
                        break;
                }
            }
            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(mod.ItemType("wa"));
                        break;
                    case 1:
                        player.QuickSpawnItem(mod.ItemType("hm"));
                        break;
                    case 2:
                        player.QuickSpawnItem(mod.ItemType("Cleyera"));
                        break;
                }
            }
        }
	}
}