using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
	public class KBTITCandle : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("KBTIT Candle");
            Tooltip.SetDefault("'ArigatoNASU!'" + 
                "\n[c/FE2EF7:<MEME ITEM>]");
            Item.staff[item.type] = true;
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
		public override void SetDefaults()
		{
			item.width = 50;
			item.height = 50;
            item.damage = 76;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 5;
            item.useAnimation = 5;
			item.knockBack = 3f;
            item.useAmmo = AmmoID.Gel;
            item.shoot = mod.ProjectileType("Flames");
            item.shootSpeed = 10f;
            item.value = Item.sellPrice(0, 26, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item20;
            item.glowMask = customGlowMask;
        }
        public override void HoldItem(Player player)
        {
            SinsPlayer.AddGlowMask(mod.ItemType(GetType().Name), "SinsMod/Glow/Item/" + GetType().Name + "_Glow");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 1f * 1.0f; //Replace 45 with whatever spread you want
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 4f;//ProjNum-1
            double offsetAngle;
            for (int i = 0; i < 5; i++) //Replace 2 with number of projectiles
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Flamethrower, 1);
            recipe.AddIngredient(ItemID.EldMelter, 1);
            recipe.AddIngredient(ItemID.IllegalGunParts, 10);
            recipe.AddIngredient(ItemID.GoldenCandelabra, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}