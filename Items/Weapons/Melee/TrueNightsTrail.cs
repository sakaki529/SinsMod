using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class TrueNightsTrail : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("True Night's Trail");
            Tooltip.SetDefault("");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 16;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 24;
            item.height = 24;
            item.damage = 72;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
			item.knockBack = 4.5f;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("TrueNightsTrail");
			item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightsTrail");
            recipe.AddIngredient(null, "BrokenHeroYoyo");
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}