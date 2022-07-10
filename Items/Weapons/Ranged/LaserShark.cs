using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class LaserShark : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Laser Shark");
            Tooltip.SetDefault("");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 36;
            item.damage = 40;
            item.ranged = true;
            item.noMelee = true;
			item.autoReuse = true;
			item.useStyle = 5;
            item.useTime = 6;
			item.useAnimation = 6;
			item.knockBack = 1f;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = mod.ProjectileType("SharkLaser");
            item.shootSpeed = 15;
			item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 5;
			item.UseSound = SoundID.Item33;
            item.glowMask = customGlowMask;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("SharkLaser");
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, -8);
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Megashark, 1);
            recipe.AddIngredient(null, "LaserAntenna", 1);
            recipe.AddIngredient(ItemID.SoulofMight, 30);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}