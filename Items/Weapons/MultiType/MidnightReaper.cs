using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.MultiType
{
    public class MidnightReaper : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Midnight Reaper");
            Tooltip.SetDefault("Right click for melee damage");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
		public override void SetDefaults()
		{
            item.width = 48;
            item.height = 48;
            item.damage = 280; 
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTime = 12;
			item.useAnimation = 12;
            item.useStyle = 1;
            item.knockBack = 4f;
            item.shootSpeed = 20f;
            item.shoot = mod.ProjectileType("MidnightReaper");
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item18;
            item.glowMask = customGlowMask;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().isMultiType = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.melee = player.altFunctionUse == 2;
            item.thrown = player.altFunctionUse != 2;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, (player.altFunctionUse == 2) ? 1f : 0f, 0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallReaper");
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