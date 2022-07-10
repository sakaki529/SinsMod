using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AnimaOrbWrath : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Anima Orb");
            Tooltip.SetDefault("Unleash anima of Wrath"
                + "\nRight click to");
        }
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 130;
            item.crit += 16;
            item.mana = 15;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 5;
            item.useAnimation = 16;
            item.knockBack = 2.0f;
            item.shootSpeed = 10;
            item.shoot = mod.ProjectileType("WrathBallFriendly");
            item.value = Item.sellPrice(0, 22, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item62;
            item.GetGlobalItem<SinsItem>().CustomRarity = 13;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            for (int i = 0; i < Main.rand.Next(2, 4); i++)
            {
                Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                speed *= Main.rand.NextFloat(0.33f, 1.0f);
                Projectile.NewProjectile(position.X, position.Y, speed.X, speed.Y, mod.ProjectileType("WrathBallFriendly"), damage, knockBack, player.whoAmI, 1f);
            }
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OrbStand");
            recipe.AddIngredient(null, "EssenceOfWrath", 30);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}