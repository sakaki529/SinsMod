using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
	public class TerraMachinegun : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Terra Machinegun");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 36;
			item.height = 36;
			item.damage = 70;
			item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
			item.useStyle = 5;
			item.useTime = 7;
			item.useAnimation = 7;
			item.knockBack = 4;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 16f;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item11;
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, + 4);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (SinsMod.Instance.CalamityLoaded)
            {
                if (type == ProjectileID.Bullet)
                {
                    type = ModLoader.GetMod("CalamityMod").ProjectileType("TerraBulletMain");
                }
            }
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TerraBlade, 1);
            recipe.AddIngredient(ItemID.Uzi, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
