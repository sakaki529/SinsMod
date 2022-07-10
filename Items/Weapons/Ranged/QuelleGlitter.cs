using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class QuelleGlitter : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Quelle Glitter");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            
			item.width = 50;
			item.height = 50;
            item.damage = 290;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
			item.useStyle = 5;
			item.useTime = 11;
			item.useAnimation = 11;
            item.knockBack = 4.6f;
            item.shoot = ProjectileID.WoodenArrowFriendly;
			item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 20f;
			item.value = Item.sellPrice(0, 50, 0, 0);
			item.rare = 10;
			item.UseSound = SoundID.Item5;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX * 2.5f, speedY * 2.5f, mod.ProjectileType("BlessedArrow"), damage, knockBack, player.whoAmI);
            for (int i = 0; i < 3; i++)
            {
                int num = Projectile.NewProjectile(position.X, position.Y, speedX * (i + 1), speedY * (i + 1), type, damage, knockBack, player.whoAmI);
                Main.projectile[num].noDropItem = true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfOrigin", 12);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}