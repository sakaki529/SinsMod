using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class VortexPillarMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 28;
			item.rare = 1;
			item.vanity = true;
		}
		public override bool DrawHead()
		{
			return false;
		}
	}
}