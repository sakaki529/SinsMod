using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AnimaOrbSloth : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Anima Orb");
            Tooltip.SetDefault("Unleash anima of Sloth"
                + "\nRight click to");
        }
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 74;
            item.crit += 16;
            item.mana = 14;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 5f;
            item.shoot = mod.ProjectileType("SlothScythe");
            item.shootSpeed = 0.4f;
			item.rare = 11;
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.UseSound = SoundID.Item62;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.useTime = player.altFunctionUse != 2 ? 15 : 12;
            item.useAnimation = player.altFunctionUse != 2 ? 15 : 12;
            item.shootSpeed = player.altFunctionUse != 2 ? 0.4f : 16f;
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("SlothScythe") : mod.ProjectileType("SlothScythe");
            item.UseSound = player.altFunctionUse != 2 ? SoundID.Item62 : SoundID.Item62;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            for (int i = 0; i < Main.rand.Next(1, 4); i++)
            {
                Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                //speed *= Main.rand.NextFloat(0.33f, 1.0f);
                Projectile.NewProjectile(position.X, position.Y, speed.X, speed.Y, type, damage, knockBack, player.whoAmI, 1f);
            }
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OrbStand");
            recipe.AddIngredient(null, "EssenceOfSloth", 30);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}