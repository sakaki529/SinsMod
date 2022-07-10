using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AnimaOrbEnvy : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Anima Orb");
            Tooltip.SetDefault("Unleash anima of Envy"
                + "\nRight click to");
        }
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 375;
            item.crit += 16;
            item.mana = 13;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.reuseDelay = 10;
			item.useStyle = 5;
            item.useTime = 1;
            item.useAnimation = 10;
            item.knockBack = 4.5f;
            item.shoot = mod.ProjectileType("EnvyHand");
            item.shootSpeed = 20f;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item103;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.mana = player.altFunctionUse != 2 ? 13 : 13;
            item.useTime = player.altFunctionUse != 2 ? 1 : 1;
            item.useAnimation = player.altFunctionUse != 2 ? 10 : 10;
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("EnvyHand") : 0;
            item.UseSound = player.altFunctionUse != 2 ? SoundID.Item103 : SoundID.Item103;
            item.reuseDelay = player.altFunctionUse != 2 ? 10 : 0;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {

            }
            else
            {
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float num = Main.mouseX + Main.screenPosition.X - vector.X;
                float num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                Vector2 speed = new Vector2(num, num2);
                speed.Normalize();
                Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector2.Normalize();
                speed = speed * 6f + vector2;
                speed.Normalize();
                speed *= item.shootSpeed;
                float num3 = Main.rand.Next(10, 50) * 0.001f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 *= -1f;
                }
                float num4 = Main.rand.Next(10, 50) * 0.001f;
                if (Main.rand.Next(2) == 0)
                {
                    num4 *= -1f;
                }
                Projectile.NewProjectile(vector.X, vector.Y, speed.X, speed.Y, type, damage, knockBack, player.whoAmI, num4, num3);
            }
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OrbStand");
            recipe.AddIngredient(null, "EssenceOfEnvy", 30);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}