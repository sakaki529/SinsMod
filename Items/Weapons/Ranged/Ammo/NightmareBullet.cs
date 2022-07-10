using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged.Ammo
{
	public class NightmareBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Nightmare Bullet");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 16;
            item.ranged = true;
			item.damage = 36;
			item.knockBack = 1f;
            item.maxStack = 999;
            item.rare = 11;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.consumable = true;
			item.shoot = mod.ProjectileType("NightmareBullet");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Bullet;
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