using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Thrown
{
    public class MidnightKnives : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Midnight Knives");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 26;
			item.height = 26;
			item.damage = 300;
            item.thrown = true;
			item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 3;
            item.shoot = mod.ProjectileType("MidnightKnives");
            item.shootSpeed = 15f;
			item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num = 6 + Main.rand.Next(0, 4);
            double num2 = Main.rand.NextDouble();
            double num3 = Math.Atan2(speedX, speedY) - (num2 / 2f);
            double num4 = num2 / num;
            for (int i = 0; i < num; i++)
            {
                double num5 = num3 + num4 * (i + i * i) / 2.0 + (32f * i);
                Projectile.NewProjectile(position.X, position.Y, (float)(Math.Sin(num5) * 14.0), (float)(Math.Cos(num5) * 14.0), type, damage, knockBack, player.whoAmI, 0f, 0f);
                Projectile.NewProjectile(position.X, position.Y, (float)(-(float)Math.Sin(num5) * 14.0), (float)(-(float)Math.Cos(num5) * 14.0), type, damage, knockBack, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallKnives");
            recipe.AddIngredient(null, "EssenceOfEnvy", 12);
            recipe.AddIngredient(null, "EssenceOfGluttony", 12);
            recipe.AddIngredient(null, "EssenceOfGreed", 12);
            recipe.AddIngredient(null, "EssenceOfLust", 12);
            recipe.AddIngredient(null, "EssenceOfPride", 12);
            recipe.AddIngredient(null, "EssenceOfSloth", 12);
            recipe.AddIngredient(null, "EssenceOfWrath", 12);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}