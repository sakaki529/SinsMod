using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class Galaxia : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galaxia");
			Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 30;
			item.height = 30;
            item.damage = 334;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = false;
            item.channel = true;
            item.useTime = 30;
			item.useAnimation = 50;
            item.useStyle = 5;
			item.knockBack = 3;
            item.shoot = mod.ProjectileType("Galaxia");
			item.shootSpeed = 0.2f;
			item.value = Item.sellPrice(0, 80, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
        }
	}
}