using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class Innocence : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("The Innocence");
            Tooltip.SetDefault("");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 24;
			item.height = 24;
			item.damage = 52;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
			item.knockBack = 2.0f;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("Innocence");
            item.value = Item.sellPrice(0, 4, 60, 0);
            item.rare = 5;
			item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}