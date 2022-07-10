using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.MultiType
{
    public class X : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("X");
            Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
			item.damage = 200;
            item.melee = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.noUseGraphic = true;
			item.width = 32;
			item.height = 28;
            item.useTime = 4;
			item.useAnimation = 4;
			item.useStyle = 5;
			item.knockBack = 1;
			item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().isMultiType = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}