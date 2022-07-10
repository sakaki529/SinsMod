using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Thrown
{
    public class FlamingKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Flaming Knife");
		}
		public override void SetDefaults()
		{
            item.width = 18;
            item.height = 20;
            item.damage = 10;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;
            item.useTime = 15;
			item.useAnimation = 15;
			item.knockBack = 2.0f;
            item.shoot = mod.ProjectileType("FlamingKnife");
            item.shootSpeed = 10f;
			item.value = Item.sellPrice(0, 0, 0, 10);
            item.rare = 0;
			item.UseSound = SoundID.Item1;
            item.consumable = true;
            item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ThrowingKnife, 20);
            recipe.AddIngredient(ItemID.Torch, 5);
            recipe.SetResult(this, 20);
            recipe.AddRecipe();
        }
    }
}