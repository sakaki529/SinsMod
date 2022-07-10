using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Thrown
{
    public class PolarNightKnives : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Polar Night Knives");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 26;
			item.height = 26;
			item.damage = 30;
            item.thrown = true;
			item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 3;
            item.shoot = mod.ProjectileType("PolarNightKnives");
            item.shootSpeed = 8f;
			item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 4;
			item.UseSound = SoundID.Item1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num = 2;
            if (Main.rand.Next(2) == 0)
            {
                num++;
            }
            if (Main.rand.Next(4) == 0)
            {
                num++;
            }
            if (Main.rand.Next(8) == 0)
            {
                num++;
            }
            if (Main.rand.Next(16) == 0)
            {
                num++;
            }
            for (int num2 = 0; num2 < num; num2++)
            {
                float num3 = speedX;
                float num4 = speedY;
                float num5 = 0.025f * num2;
                num3 += Main.rand.Next(-35, 36) * num5;
                num4 += Main.rand.Next(-35, 36) * num5;
                float num6 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                num6 = item.shootSpeed / num6;
                num3 *= num6;
                num4 *= num6;
                Projectile.NewProjectile(position.X, position.Y, num3, num4, type, damage, knockBack, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            if (SinsMod.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("LightAnguish"));
            }
            recipe.AddIngredient(null, "NightEnergizedBar", 12);
            recipe.AddIngredient(ItemID.SoulofNight, 6);
			recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}