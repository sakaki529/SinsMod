using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class TerraWheel : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Terra Wheel");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 21;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 24;
			item.height = 24;
			item.damage = 95;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
			item.knockBack = 3.0f;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("TerraWheel");
			item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TrueInnocence");
            recipe.AddIngredient(null, "TrueNightsTrail");
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}