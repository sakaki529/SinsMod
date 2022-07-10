using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
	public class CoinMagnum : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Coin Magnum");
            Tooltip.SetDefault("Three round burst" +
                "\nOnly the first shot consumes coin");
		}
		public override void SetDefaults()
		{item.width = 56;
            item.height = 28;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 3;
            item.useAnimation = 10;
            item.knockBack = 3f;
            item.useAmmo = AmmoID.Coin;
            item.shoot = ProjectileID.PlatinumCoin;
            item.shootSpeed = 15f;
            item.rare = 8;
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.UseSound = SoundID.Item31;
            item.reuseDelay = 5;
            item.GetGlobalItem<SinsItem>().CustomRarity = 12;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, +4);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 2, knockBack, player.whoAmI);
            return false;
        }
        public override bool ConsumeAmmo(Player player)
        {
            // Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
            // We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
            return !(player.itemAnimation < item.useAnimation - 1);
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CoinGun, 1);
            recipe.AddIngredient(null, "EssenceOfGreed", 12);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}