using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class NightfallRain : ModItem
	{
        public static short customGlowMask = 0;
        private int Count;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightfall Rain");
            Tooltip.SetDefault("Shots nightfall energies every six times");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 86;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 8;
			item.useAnimation = 8;
            item.knockBack = 2;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 15f;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.rare = 10;
			item.UseSound = SoundID.Item11;
            item.glowMask = customGlowMask;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("NightfallLaser"), damage, knockBack, player.whoAmI);
            int num = Main.rand.Next(2, 4);
            for (int i = 0; i < num; i++)
            {
                float velocityX = speedX + Main.rand.Next(-20, 21) * 0.05f;
                float velocityY = speedY + Main.rand.Next(-20, 21) * 0.05f;
                int num2 = Projectile.NewProjectile(position.X, position.Y, velocityX, velocityY, type, damage, knockBack, player.whoAmI, 0f, 0f);
            }
            Count++;
            if (Count == 6)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("NightfallEnergy"), damage, knockBack, player.whoAmI);
                Count = 0;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WhiteNightRain");
            recipe.AddIngredient(null, "LaserShark");
            recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
            recipe.AddIngredient(ItemID.FragmentSolar, 16);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}