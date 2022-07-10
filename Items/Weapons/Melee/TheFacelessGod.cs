using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class TheFacelessGod : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("The Faceless God");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 28;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 24;
            item.height = 24;
            item.damage = 300;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
			item.knockBack = 1.5f;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("TheFacelessGod");
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TheBloodyTongue");
            recipe.AddIngredient(null, "EssenceOfEnvy", 12);
            recipe.AddIngredient(null, "EssenceOfGluttony", 12);
            recipe.AddIngredient(null, "EssenceOfGreed", 12);
            recipe.AddIngredient(null, "EssenceOfLust", 12);
            recipe.AddIngredient(null, "EssenceOfPride", 12);
            recipe.AddIngredient(null, "EssenceOfSloth", 12);
            recipe.AddIngredient(null, "EssenceOfWrath", 12);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}