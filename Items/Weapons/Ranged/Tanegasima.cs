using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class Tanegasima : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Tanegasima");
            DisplayName.AddTranslation(GameCulture.Chinese, "ŽíŽq“‡");
            Tooltip.SetDefault("");
		}
        public override void SetDefaults()
		{
            item.width = 56;
			item.height = 16;
			item.damage = 1250;
			item.ranged = true;
            item.autoReuse = false;
			item.noMelee = true;
            item.useStyle = 5;
			item.useTime = 25;
			item.useAnimation = 25;
			item.knockBack = 7;
            item.useAmmo = AmmoID.Bullet;
			item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 20f;
			item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item36;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-25, +1);
        }
	}
}