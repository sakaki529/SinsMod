using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class OnyxBlasterX : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Onyx Blaster Type-X");
            Tooltip.SetDefault("Fires bullets and homing Black Bolt to straight" +
                "\nRight click to change shot type");
        }
		public override void SetDefaults()
		{
            item.width = 56;
			item.height = 28;
			item.damage = 529;
			item.ranged = true;
			item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 7;
			item.useAnimation = 7;
			item.knockBack = 5;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 16f;
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item11;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glow/Item/" + GetType().Name + "_Glow");
            spriteBatch.Draw(texture, new Vector2(item.position.X - Main.screenPosition.X + item.width * 0.5f, item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f), new Rectangle(0, 0, texture.Width, texture.Height), new Color(255 + (int)(Main.DiscoR * 0.2f), 255 + (int)(Main.DiscoG * 0.2f), 255 + (int)(Main.DiscoB * 0.2f)), rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
        }
        public override void HoldItem(Player player)
        {
            SinsPlayer.AddGlowMask(mod.ItemType(GetType().Name), "SinsMod/Glow/Item/" + GetType().Name + "_Glow");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.autoReuse = player.altFunctionUse != 2 ? true : false;
            item.UseSound = player.altFunctionUse != 2 ? SoundID.Item11 : SoundID.Item108;
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.altFunctionUse == 2)
            {
                modPlayer.xBlasterType = (modPlayer.xBlasterType == 0) ? 1 : 0;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("BlackBoltH"), damage, knockBack, player.whoAmI);
                if (modPlayer.xBlasterType == 1)
                {
                    Vector2 speed = new Vector2(speedX, speedY);
                    for (int i = 0; i < 3; i++)
                    {
                        float num = Main.rand.NextFloat(0.15f);
                        float num2 = Main.rand.NextFloat(0.9f, 1.1f);
                        Vector2 vector = speed.RotatedBy(-num, default(Vector2)) * num2;
                        Vector2 vector2 = speed.RotatedBy(num, default(Vector2)) * num2;
                        Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
                        if (i == 2)
                        {
                            break;
                        }
                        Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
                    }
                    return false;
                }
                for (int i = -2; i <= 2; i++)
                {
                    float direction = new Vector2(speedX, speedY).ToRotation();
                    Vector2 vector = new Vector2(position.X + (float)Math.Cos(direction + Math.PI / 2) * i * 2, position.Y + (float)Math.Sin(direction + Math.PI / 2) * i * 2);
                    Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.OnyxBlaster);
            recipe.AddIngredient(ItemID.SDMG);
            recipe.AddIngredient(null, "Axion", 12);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}