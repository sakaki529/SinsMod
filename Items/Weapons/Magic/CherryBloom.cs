using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class CherryBloom : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Cherry Bloom");
            Tooltip.SetDefault("");
            Item.staff[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 280;
            item.mana = 20;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 5;
            item.knockBack = 4f;
            item.shootSpeed = 32;
            item.shoot = mod.ProjectileType("Thorn");
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item43;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, item.type, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfLust", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}