using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class EternalPolarNight : ModItem
	{
        private int Count;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Eternal Polar Night");
            Tooltip.SetDefault("Shots polar night energies every six times");
        }
		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 22;
            item.damage = 42;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.knockBack = 5;
            item.shoot = 1;
            item.shootSpeed = 30f;
            item.useAmmo = AmmoID.Arrow;
            item.value = Item.sellPrice(0, 6, 0, 0);
            item.rare = 4;
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
                Projectile.NewProjectile(position.X, position.Y, speedX / 3 * 2, speedY / 3 * 2, mod.ProjectileType("PolarNightEnergy"), damage, knockBack, player.whoAmI);
                Count = 0;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            if (SinsMod.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("EternalNight"));
            }
            recipe.AddIngredient(null, "NightEnergizedBar", 12);
            recipe.AddIngredient(ItemID.SoulofNight, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}