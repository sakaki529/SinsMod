using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class MagellanicBlaze : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Magellanic Blaze");
            Tooltip.SetDefault("");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
		public override void SetDefaults()
		{
            item.width = 16;
			item.height = 16;
			item.damage = 164;
            item.mana = 18;
			item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 15;
			item.useAnimation = 15;
			item.knockBack = 0;
            item.shootSpeed = 5.0f;
            item.shoot = mod.ProjectileType("MagellanicBlaze1");
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item20;
            item.glowMask = customGlowMask;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.Next(100) < 20)
            {
                type = mod.ProjectileType("MagellanicBlaze2");
                damage *= 3;
            }
            else
            {
                speedX -= 1f;
                speedY -= 1f;
            }
            float num = (Main.rand.NextFloat() - 0.5f) * 0.7853982f * 0.7f;
            int num2 = 0;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            while (num2 < 10 && !Collision.CanHit(vector, 0, 0, vector + new Vector2(speedX, speedY).RotatedBy(num, default(Vector2)) * 100f, 0, 0))
            {
                num = (Main.rand.NextFloat() - 0.5f) * 0.7853982f * 0.7f;
                num2++;
            }
            Vector2 vector2 = new Vector2(speedX, speedY).RotatedBy(num, default(Vector2)) * (0.95f + Main.rand.NextFloat() * 0.3f);
            Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NebulaBlaze);
            recipe.AddIngredient(ItemID.FragmentNebula, 8);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}