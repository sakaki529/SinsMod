using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
	public class SpiralBeater : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral Beater");
            Tooltip.SetDefault("66% chance to not consume ammo");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
        public override void SetDefaults()
		{
            item.width = 20;
			item.height = 12;
			item.damage = 76;
            item.ranged = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 5;
            item.useTime = 6;
			item.useAnimation = 6;
            item.knockBack = 3f;
            item.shoot = mod.ProjectileType("SpiralBeater");
            item.shootSpeed = 20f;
			item.value = Item.sellPrice(0, 20, 0, 0);
			item.useAmmo = AmmoID.Bullet;
			item.rare = 10;
			item.UseSound = SoundID.Item13;
            item.glowMask = customGlowMask;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.Next(3) == 0;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SpiralBeater"), damage, knockBack, player.whoAmI, 5 * Main.rand.Next(0, 20), 0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexBeater);
            recipe.AddIngredient(ItemID.FragmentVortex, 8);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}