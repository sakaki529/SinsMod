using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AnimaOrbOrigin : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Anima Orb");
            Tooltip.SetDefault("Unleash anima of original sin"
                + "\nRight click to");
        }
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.damage = 255;
            item.crit += 16;
            item.mana = 15;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 0.5f;
            item.shoot = mod.ProjectileType("");
            item.shootSpeed = 20;
            item.value = Item.sellPrice(0, 45, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item62;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                
            }
            else
            {
                
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {

            }
            else
            {

            }
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OrbStand");
            recipe.AddIngredient(null, "EssenceOfOrigin", 30);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}