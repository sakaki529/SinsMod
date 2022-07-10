using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class NightsTrail : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Night's Trail");
            Tooltip.SetDefault("");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 12;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 24;
			item.height = 24;
			item.damage = 38;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 16f;
			item.knockBack = 2.0f;
			item.value = Item.sellPrice(0, 1, 8, 0);
            item.rare = 3;
			item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("NightsTrail");
        }
        public override void AddRecipes()
		{
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CorruptYoyo);
            recipe.AddIngredient(ItemID.JungleYoyo);
            recipe.AddIngredient(ItemID.Valor);
            recipe.AddIngredient(null, "Salamander");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimsonYoyo);
            recipe.AddIngredient(ItemID.JungleYoyo);
            recipe.AddIngredient(ItemID.Valor);
            recipe.AddIngredient(null, "Salamander");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}