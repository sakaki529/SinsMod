using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class VampireWand : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampire Wand");
            Tooltip.SetDefault("Shoot mini vampires");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
            item.damage = 200;
            item.mana = 6;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 9;
            item.useAnimation = 9;
            item.shootSpeed = 15;
			item.shoot = mod.ProjectileType("MiniVampire");
            item.value = Item.sellPrice(0, 35, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item32;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(3, 6);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 Speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(12));
                Projectile.NewProjectile(position.X, position.Y, Speed.X, Speed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BatScepter, 1);
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddIngredient(null, "EssenceOfLust", 6);
            recipe.AddTile(null, "ParticleAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}