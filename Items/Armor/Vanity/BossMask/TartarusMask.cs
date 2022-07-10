using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Vanity.BossMask
{
	[AutoloadEquip(EquipType.Head)]
	public class TartarusMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian of Tartarus Mask");
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