using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Potions
{
    public class GrandHealingPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grand Healing Potion");
			Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.width = 20;
            item.height = 34;
            item.maxStack = 99;
            item.consumable = true;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 9;
            item.potion = true;
            item.healLife = 300;
        }
    }
}