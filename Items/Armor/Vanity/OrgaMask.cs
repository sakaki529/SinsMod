using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class OrgaMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orga Itsuka Mask");
            Tooltip.SetDefault("'Don`t you ever stop...'");
        }
        public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
			item.rare = 1;
			item.vanity = true;
		}
		public override bool DrawHead()
		{
			return false;
		}
	}
}