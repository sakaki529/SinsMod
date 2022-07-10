using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class TrueEternalMidnight : ModItem
	{
        public static short customGlowMask = 0;
        private int Count;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("True Eternal Midnight");
            Tooltip.SetDefault("Shots true midnight energies every six times");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
		public override void SetDefaults()
		{
            item.width = 42;
            item.height = 42;
            item.damage = 444;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 8;
            item.useAnimation = 8;
            item.knockBack = 5;
            item.shoot = 1;
            item.shootSpeed = 50f;
            item.useAmmo = AmmoID.Arrow;
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item5;
            item.glowMask = customGlowMask;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("TrueEternalMidnightArrow"), damage, knockBack, player.whoAmI, 0f, 0f);
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
                for (int j = -1; j <= 1; j++)
                {
                    float direction = new Vector2(speedX, speedY).ToRotation();
                    Vector2 vector = new Vector2(position.X + (float)Math.Cos(direction + Math.PI / 2) * j * 2, position.Y + (float)Math.Sin(direction + Math.PI / 2) * j * 2);
                    Projectile.NewProjectile(vector.X, vector.Y, speedX * 0.4f, speedY * 0.4f, mod.ProjectileType("TrueMidnightEnergy"), damage, knockBack, player.whoAmI);
                }
                Count = 0;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EternalMidnight");
            recipe.AddIngredient(null, "Axion", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}