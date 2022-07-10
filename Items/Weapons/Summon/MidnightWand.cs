using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Summon
{
    public class MidnightWand : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Midnight Wand");
            Tooltip.SetDefault("Uses 2 minion slots");
		}
		public override void SetDefaults()
		{
            item.width = 24;
            item.height = 24;
            item.damage = 260;
            item.mana = 20;
            item.summon = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 1;
			item.knockBack = 0.5f;
            item.shootSpeed = 0f;
            item.shoot = mod.ProjectileType("MidnightProbe");
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item100;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
                return false;
            }
            position = Main.MouseWorld;
            return player.altFunctionUse != 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallWand");
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