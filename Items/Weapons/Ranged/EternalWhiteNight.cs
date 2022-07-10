using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class EternalWhiteNight : ModItem
	{
        private int Count;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Eternal White Night");
            Tooltip.SetDefault("Shots white night energies every six times");
        }
		public override void SetDefaults()
		{
            item.width = 26;
            item.height = 48;
            item.damage = 68;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 13;
            item.useAnimation = 13;
            item.knockBack = 5;
            item.useAmmo = AmmoID.Arrow;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 30f;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item5;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num = Main.rand.Next(2, 4);
            for (int i = 0; i < num; i++)
            {
                float velocityX = speedX + Main.rand.Next(-20, 21) * 0.05f;
                float velocityY = speedY + Main.rand.Next(-20, 21) * 0.05f;
                int num2 = Projectile.NewProjectile(position.X, position.Y, velocityX, velocityY, type, damage, knockBack, player.whoAmI, 0f, 0f);
                Main.projectile[num2].noDropItem = true;
            }
            Count++;
            if (Count == 6)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX / 3 * 2, speedY / 3 * 2, mod.ProjectileType("WhiteNightEnergy"), damage, knockBack, player.whoAmI);
                Count = 0;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EternalPolarNight");
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}