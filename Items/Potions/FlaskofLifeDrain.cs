using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Potions
{
    public class FlaskofLifeDrain : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flask of Life Drain");
            Tooltip.SetDefault("Melee attacks restores your health");
        }
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.width = 14;
            item.height = 24;
            item.maxStack = 30;
            item.consumable = true; 
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 4;
            item.buffType = mod.BuffType("WeaponImbueLifeDrain");
            item.buffTime = 72000;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(null, "EclipseDrip", 3);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.LifeFruit, 2);
            recipe.AddTile(TileID.ImbuingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}