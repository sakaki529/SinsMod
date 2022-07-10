using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
	public class MidnightStaff : ModItem
	{
        public override string Texture => "SinsMod/Extra/Placeholder/Placeholder";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Rose at Midnight");
            Tooltip.SetDefault("Right click to shot beams");
            Item.staff[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 256;
            item.mana = 8;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 20;
			item.useAnimation = 20;
            item.knockBack = 2f;
            item.shootSpeed = 32;
            item.shoot = mod.ProjectileType("Thorn");
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item43;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.mana = (player.altFunctionUse != 2) ? 40 : 20;
            item.useTime = (player.altFunctionUse != 2) ? 18 : 10;
            item.useAnimation = (player.altFunctionUse != 2) ? 18 : 10;
            item.shoot = (player.altFunctionUse != 2) ? mod.ProjectileType("Thorn") : mod.ProjectileType("MidnightBeam");
            item.shootSpeed = (player.altFunctionUse != 2) ? 32 : 16;
            item.UseSound = (player.altFunctionUse != 2) ? SoundID.Item43 : SoundID.Item72;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < Main.rand.Next(1, 3); i++)
                {
                    Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                    float num = item.shootSpeed;
                    int num2 = (int)(damage * 1.0);
                    float num3 = knockBack;
                    float num4 = Main.mouseX + Main.screenPosition.X - vector.X;
                    float num5 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                    float num6 = Main.rand.NextFloat() * 6.28318548f;
                    float value = 20f;
                    float value2 = 60f;
                    Vector2 vector2 = vector + num6.ToRotationVector2() * MathHelper.Lerp(value, value2, Main.rand.NextFloat());
                    for (int num7 = 0; num7 < 50; num7++)
                    {
                        vector2 = vector + num6.ToRotationVector2() * MathHelper.Lerp(value, value2, Main.rand.NextFloat());
                        if (Collision.CanHit(vector, 0, 0, vector2 + (vector2 - vector).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
                        {
                            break;
                        }
                        num6 = Main.rand.NextFloat() * 6.28318548f;
                    }
                    Vector2 vector3 = Main.MouseWorld - vector2;
                    Vector2 vector4 = new Vector2(num4, num5).SafeNormalize(Vector2.UnitY) * num;
                    vector3 = vector3.SafeNormalize(vector4) * num;
                    vector3 = Vector2.Lerp(vector3, vector4, 0.25f);
                    Projectile.NewProjectile(vector2, vector3, type, num2, num3, player.whoAmI, 0f, 0f);
                }
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, item.type, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallStaff");
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