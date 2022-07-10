using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class DawnStar : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dawn Star");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
			item.damage = 24;
			item.width = 30;
			item.height = 30;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.melee = true;
            item.shoot = mod.ProjectileType("DawnStar");
            item.useTime = 40;
            item.useAnimation = 40;
            item.shootSpeed = 2.2f;
			item.useStyle = 5;
			item.knockBack = 4;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX * 4, speedY * 4, ProjectileID.Starfury, damage, knockBack, player.whoAmI);
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Starfury, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 6);
            recipe.AddIngredient(ItemID.MeteoriteBar, 12);
            recipe.AddIngredient(ItemID.Ruby, 20);
            recipe.AddTile(16);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}