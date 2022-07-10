using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class Yozakura : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Yozakura");
            DisplayName.AddTranslation(GameCulture.Chinese, "ñÈç˜");
            Tooltip.SetDefault("Can only be used during the night" + 
                "\nIncreased damage by hit of slashes(except dummy)");
        }
        public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 1;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.autoReuse = false;
            item.useTime = 30;
			item.useAnimation = 50;
            item.useStyle = 5;
            item.knockBack = 3;
            item.shoot = mod.ProjectileType("Yozakura");
			item.shootSpeed = 5.0f;
			item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfLust", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}