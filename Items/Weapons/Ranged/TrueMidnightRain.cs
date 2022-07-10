using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class TrueMidnightRain : ModItem
	{
        public static short customGlowMask = 0;
        private int Count;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Midnight Rain");
            Tooltip.SetDefault("Shots true midnight energies every six times");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 444;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 7;
			item.useAnimation = 7;
            item.knockBack = 4;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 15f;
			item.value = Item.sellPrice(0, 60, 0, 0);
			item.rare = 11;
			item.UseSound = SoundID.Item11;
            item.glowMask = customGlowMask;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
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
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("TrueMidnightLaser"), damage, knockBack, player.whoAmI);
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
                for (int j = -1; j <= 1; j++)
                {
                    float direction = new Vector2(speedX, speedY).ToRotation();
                    Vector2 vector = new Vector2(position.X + (float)Math.Cos(direction + Math.PI / 2) * j * 2, position.Y + (float)Math.Sin(direction + Math.PI / 2) * j * 2);
                    Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, mod.ProjectileType("TrueMidnightEnergy"), damage, knockBack, player.whoAmI);
                }
                Count = 0;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MidnightRain");
            recipe.AddIngredient(null, "Axion", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}