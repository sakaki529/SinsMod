using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class BlazingWheel : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Blazing Wheel");
		}
		public override void SetDefaults()
		{
            item.width = 24;
            item.height = 24;
            item.damage = 27;
            item.mana = 14;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = false;
            item.useStyle = 5;
			item.useTime = 26;
			item.useAnimation = 26;
			item.knockBack = 4.5f;
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType("BlazingWheel");
            item.value = Item.sellPrice(0, 0, 40, 0);
            item.rare = 2;
			item.UseSound = SoundID.Item8;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.LavaBucket);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}