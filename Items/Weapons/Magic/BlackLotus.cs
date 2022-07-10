using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
	public class BlackLotus : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Black Lotus");
            Tooltip.SetDefault("");
            Item.staff[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 280;
            item.mana = 30;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 30;
			item.useAnimation = 30;
            item.knockBack = 2f;
            item.shootSpeed = 32;
            item.shoot = mod.ProjectileType("Thorn");
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item43;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, item.type, 0f);
            for (int i = 0; i < Main.rand.Next(0, 3); i++)
            {
                Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, item.type, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfPride", 12);
            if (SinsMod.Instance.AALoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("AAMod").ItemType("TrueTerraRose"));
            }
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}