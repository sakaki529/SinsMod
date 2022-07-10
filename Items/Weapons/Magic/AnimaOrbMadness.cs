using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AnimaOrbMadness : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Anima Orb");
            Tooltip.SetDefault("Unleash anima of Madness"
                + "\nRight click to shots cursed spirit");
        }
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.damage = 255;
            item.crit += 16;
            item.mana = 20;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 0.5f;
            item.shoot = mod.ProjectileType("CursedSpirit");
            item.shootSpeed = 10;
            item.value = Item.sellPrice(0, 70, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item62;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.mana = player.altFunctionUse != 2 ? 20 : 100;
            item.reuseDelay = player.altFunctionUse != 2 ? 0 : 30;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("CursedSpirit"), (int)((double)damage * 4f), knockBack, player.whoAmI, 1f, 0f);
            }
            else
            {
                position = Main.MouseWorld;
                Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("SpreadShortcut"), damage, knockBack, player.whoAmI, mod.ProjectileType("BlackMatterFriendly"), 0f);
            }
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OrbStand");
            recipe.AddIngredient(null, "EssenceOfMadness", 30);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}