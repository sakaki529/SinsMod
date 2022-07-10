using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged.Ammo
{
    public class NightmareArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Arrow");
			Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 14;
            item.height = 36;
            item.ranged = true;
            item.damage = 28;
            item.knockBack = 1f;
            item.maxStack = 999;
			item.rare = 11;
			item.value = Item.sellPrice(0, 1, 0, 0);
            item.consumable = true;
            item.shoot = mod.ProjectileType("NightmareArrow");
            item.shootSpeed = 10f;
            item.ammo = AmmoID.Arrow;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightmareBar");
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this, 333);
            recipe.AddRecipe();
        }
    }
}