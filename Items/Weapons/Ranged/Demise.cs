using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class Demise : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demise");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 200;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 3;
			item.useAnimation = 3;
			item.knockBack = 5;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 70f;
			item.value = Item.sellPrice(0, 60, 0, 0);
			item.useAmmo = AmmoID.Arrow;
			item.rare = 11;
			item.UseSound = SoundID.Item5;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float num = 7;
            float num2 = MathHelper.ToRadians(5.4f);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            for (int i = 0; i < num; i++)
            {
                Vector2 vector = Utils.RotatedBy(new Vector2(speedX, speedY), MathHelper.Lerp(-num2, num2, i / (num - 1f)), default(Vector2)) * 0.2f;
                int num3 = Projectile.NewProjectile(position, vector, type, damage, knockBack, player.whoAmI, 0f, 0f);
                Main.projectile[num3].noDropItem = true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Axion", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}