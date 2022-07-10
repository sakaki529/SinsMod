using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class TheCrawlingChaos : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("The Crawling Chaos");
            Tooltip.SetDefault("'The most realistic and horrible nightmare'");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 30;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 24;
            item.height = 24;
            item.damage = 444;
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
            item.shoot = mod.ProjectileType("TheCrawlingChaos");
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TheFacelessGod");
            recipe.AddIngredient(null, "Axion", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}