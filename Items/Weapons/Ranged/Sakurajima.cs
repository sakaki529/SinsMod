using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class Sakurajima : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Sakurajima");
            DisplayName.AddTranslation(GameCulture.Chinese, "ç˜ìá");
            Tooltip.SetDefault("");
		}
        public override void SetDefaults()
		{
            item.width = 56;
			item.height = 16;
			item.damage = 3000;
			item.ranged = true;
            item.noMelee = true;
            item.autoReuse = false;
            item.useStyle = 5;
			item.useTime = 25;
			item.useAnimation = 25;
			item.knockBack = 4;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = mod.ProjectileType("Sakurajima");
			item.shootSpeed = 16f;
			item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item36;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-25, +1);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("Sakurajima");
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
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