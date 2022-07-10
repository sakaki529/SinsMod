using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Thrown
{
    public class Keravnos : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Keravnos");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 800;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 1;
			item.knockBack = 10;
            item.shoot = mod.ProjectileType("Keravnos");
            item.shootSpeed = 20f;
			item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
    }
}