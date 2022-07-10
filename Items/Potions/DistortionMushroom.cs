using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Potions
{
    public class DistortionMushroom : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Distortion Mushroom");
			Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item2;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.consumable = true;
            item.value = Item.sellPrice(0, 0, 0, 4);
            item.rare = 1;
            item.healMana = 20;
            item.buffType = mod.BuffType("DistortionSeeing");
            item.buffTime = 2700;
        }
    }
}