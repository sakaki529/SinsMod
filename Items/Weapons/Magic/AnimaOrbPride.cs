using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AnimaOrbPride : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Anima Orb");
            Tooltip.SetDefault("Unleash anima of Pride"
                + "\nRight click to");
        }
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 400;
            item.crit += 16;
            item.mana = 15;
            item.magic = true;
            item.noMelee = true;
			item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 3;
			item.useAnimation = 6;
            item.knockBack = 0.5f;
            item.shootSpeed = 20;
            item.shoot = mod.ProjectileType("");
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item62;
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
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OrbStand");
            recipe.AddIngredient(null, "EssenceOfPride", 30);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}