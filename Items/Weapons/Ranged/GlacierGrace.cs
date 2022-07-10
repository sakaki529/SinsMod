using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class GlacierGrace : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacier Grace");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
			item.width = 24;
			item.height = 48;
            item.damage = 92;
            item.noMelee = true;
			item.ranged = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 11;
			item.useAnimation = 11;
            item.knockBack = 1f;
            item.useAmmo = AmmoID.Arrow;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 13f;
			item.value = Item.sellPrice(0, 16, 0, 0);
			item.rare = 6;
			item.UseSound = SoundID.Item5;
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}