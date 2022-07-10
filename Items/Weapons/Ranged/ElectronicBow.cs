using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class ElectronicBow : ModItem
	{
        private int Count;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electronic Bow");
            Tooltip.SetDefault("Shots a electrosphere every three times");
        }
        public override void SetDefaults()
		{
			item.width = 22;
			item.height = 22;
            item.damage = 43;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 17;
			item.useAnimation = 17;
            item.knockBack = 1f;
            item.useAmmo = AmmoID.Arrow;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 13f;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.rare = 4;
			item.UseSound = SoundID.Item5;
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Count++;
            if (Count == 3)
            {
                Main.PlaySound(SoundID.Item93, (int)player.position.X, (int)player.position.Y);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Electrosphere, damage, knockBack, player.whoAmI);
                Count = 0;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ElectrosphereLauncher);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}