using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class DarkOne : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Dark One");
            Tooltip.SetDefault("");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 14;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 24;
            item.height = 24;
            item.damage = 46;
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
            item.shoot = mod.ProjectileType("DarkOne");
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 4;
			item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightsTrail");
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}