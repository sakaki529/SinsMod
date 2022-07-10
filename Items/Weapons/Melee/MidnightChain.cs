using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class MidnightChain : ModItem
	{
        public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("Midnight Chain");
            Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 16;
            item.height = 16;
            item.damage = 280;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.autoReuse = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.knockBack = 0.5f;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("MidnightChain");
            item.rare = 11;
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Item1");
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0, Main.rand.Next(-120, 120) * 0.001f * player.gravDir);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallChain");
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