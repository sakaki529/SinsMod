using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Summon
{
    public class NightmareWand : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Nightmare Wand");
            Tooltip.SetDefault("Uses 2 minion slots");
		}
		public override void SetDefaults()
		{
            item.width = 24;
            item.height = 24;
            item.damage = 666;
            item.mana = 20;
            item.summon = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 25;
			item.useAnimation = 25;
			item.knockBack = 0.5f;
            item.shootSpeed = 0f;
            item.shoot = mod.ProjectileType("NightmareProbe");
            item.value = Item.sellPrice(0, 80, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item100;
            item.scale = 1.1f;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
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
            recipe.AddIngredient(null, "TrueMidnightWand");
            recipe.AddIngredient(null, "EssenceOfMadness", 8);
            recipe.AddIngredient(null, "NightmareBar", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}