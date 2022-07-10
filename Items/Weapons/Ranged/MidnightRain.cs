using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class MidnightRain : ModItem
	{
        public static short customGlowMask = 0;
        private int Count;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Rain");
            Tooltip.SetDefault("Shots midnight energies every six times");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 260;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = 5;
            item.knockBack = 4;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 15f;
			item.value = Item.sellPrice(0, 40, 0, 0);
			item.rare = 11;
			item.UseSound = SoundID.Item11;
            item.glowMask = customGlowMask;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("Glow/Item/" + GetType().Name + "_Glow");
            spriteBatch.Draw(texture, position, frame, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("MidnightLaser"), damage, knockBack, player.whoAmI);
            int num = Main.rand.Next(2, 4);
            for (int i = 0; i < num; i++)
            {
                float velocityX = speedX + Main.rand.Next(-20, 21) * 0.05f;
                float velocityY = speedY + Main.rand.Next(-20, 21) * 0.05f;
                int num2 = Projectile.NewProjectile(position.X, position.Y, velocityX, velocityY, type, damage, knockBack, player.whoAmI, 0f, 0f);
            }
            Count++;
            if (Count == 6)
            {
                for (int j = 0; j <= 0; j++)
                {
                    float direction = new Vector2(speedX, speedY).ToRotation();
                    Vector2 vector = new Vector2(position.X + (float)Math.Cos(direction + Math.PI / 2) * j * 2, position.Y + (float)Math.Sin(direction + Math.PI / 2) * j * 2);
                    Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, mod.ProjectileType("MidnightEnergy"), damage, knockBack, player.whoAmI);
                }
                Count = 0;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallRain");
            recipe.AddIngredient(null, "EssenceOfEnvy", 12);
            recipe.AddIngredient(null, "EssenceOfGluttony", 12);
            recipe.AddIngredient(null, "EssenceOfGreed", 12);
            recipe.AddIngredient(null, "EssenceOfLust", 12);
            recipe.AddIngredient(null, "EssenceOfPride", 12);
            recipe.AddIngredient(null, "EssenceOfSloth", 12);
            recipe.AddIngredient(null, "EssenceOfWrath", 12);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}