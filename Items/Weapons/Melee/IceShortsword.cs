using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class IceShortsword : ModItem
	{
		public override void SetStaticDefaults()
		{
            Tooltip.SetDefault(""); 
		}
		public override void SetDefaults()
		{
            item.width = 24;
			item.height = 24;
			item.damage = 17;
            item.melee = true;
			item.autoReuse = true;
			item.useTime = 55;
			item.useAnimation = 20;
			item.knockBack = 4.75f;
            item.useStyle = 3;
            item.shootSpeed = 9.5f;
            item.shoot = ProjectileID.IceBolt;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedX = item.shootSpeed * player.direction;
            speedY = 0f;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceBlade, 1);
            recipe.AddTile(16);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}