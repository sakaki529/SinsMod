using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class SpectreSword : ModItem
	{
	    public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("Spectre Sword");
        }
		public override void SetDefaults()
		{
            item.width = 40;
			item.height = 40;
			item.damage = 108;
			item.melee = true;
            item.autoReuse = true;
            item.useStyle = 1;
			item.useTime = 22;
			item.useAnimation = 22;
            item.knockBack = 4.5f;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("Spectre");
            item.value = Item.sellPrice(0, 6, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item71;
            item.alpha = 40;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreBar, 18);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}