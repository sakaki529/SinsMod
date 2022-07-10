using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Vanity.BossMask
{
	[AutoloadEquip(EquipType.Head)]
	public class KingMetalSlimeMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal King Mask");
        }
        public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.rare = 1;
			item.vanity = true;
		}
		public override bool DrawHead()
		{
			return false;
		}
	}
}