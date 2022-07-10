using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Thrown
{
    public class LivingEnd : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Living End");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.noMelee = true;
            item.thrown = true;
            item.damage = 180;
            item.knockBack = 10f;
            item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.value = Item.sellPrice(0, 27, 0, 0);
            item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("LivingEnd");
            item.shootSpeed = 25f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Headstone, 50);
            recipe.AddIngredient(ItemID.SoulofNight, 50);
            recipe.AddIngredient(ItemID.Ectoplasm, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}