using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss.BossBag
{
	public class LunarEyeBag : ModItem
	{
        public override int BossBagNPC => mod.NPCType("LunarEye");
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
            player.QuickSpawnItem(mod.ItemType("TheTrueEyeOfCthulhu"));
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("LunarEyeMask"));
            }
            player.QuickSpawnItem(mod.ItemType("MoonDrip"), Main.rand.Next(3, 7));
            player.QuickSpawnItem(ItemID.FragmentVortex, Main.rand.Next(30, 48));
            player.QuickSpawnItem(ItemID.FragmentNebula, Main.rand.Next(30, 48));
            player.QuickSpawnItem(ItemID.FragmentSolar, Main.rand.Next(30, 48));
            player.QuickSpawnItem(ItemID.FragmentStardust, Main.rand.Next(30, 48));
            if (NPC.downedMoonlord)
            {
                player.QuickSpawnItem(ItemID.LunarOre, Main.rand.Next(40, 61));
            }
        }
	}
}