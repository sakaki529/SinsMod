using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Thrown
{
    public class Sakurahubuki : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Sakurahubuki");
            DisplayName.AddTranslation(GameCulture.Chinese, "桜吹雪");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 30;
			item.height = 30;
			item.damage = 500;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.autoReuse = true;
            item.channel = true;
			item.useTime = 12;
			item.useAnimation = 12;
            item.useStyle = 1;
            item.knockBack = 3.0F;
            item.shoot = mod.ProjectileType("Sakurahubuki");
            item.shootSpeed = 9.0f;
            item.value = Item.sellPrice(0, 70, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item39;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(3, 6);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(50));
                Projectile.NewProjectile(position.X, position.Y, speed.X, speed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
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