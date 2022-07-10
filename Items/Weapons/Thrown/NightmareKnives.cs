using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Thrown
{
    public class NightmareKnives : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Fitful Nightmare");
            Tooltip.SetDefault("Shoot nightmare knives to all direction");
		}
		public override void SetDefaults()
		{
            item.width = 26;
			item.height = 26;
			item.damage = 666;
            item.thrown = true;
			item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 3;
            item.shoot = mod.ProjectileType("NightmareKnives");
            item.shootSpeed = 15f;
			item.value = Item.sellPrice(0, 80, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num = 8 + Main.rand.Next(1, 8);
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
            recipe.AddIngredient(null, "TrueMidnightKnives");
            if (SinsMod.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("EmpyreanKnives"));
            }
            recipe.AddIngredient(null, "EssenceOfMadness", 8);
            recipe.AddIngredient(null, "NightmareBar", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}