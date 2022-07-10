using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AnimaOrbLust : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Anima Orb");
            Tooltip.SetDefault("Unleash anima of Lust"
                + "\nRight click to");
        }
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 266;
            item.crit += 16;
            item.mana = 15;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.channel = true;
			item.useStyle = 5;
            item.useTime = 3;
			item.useAnimation = 10;
            item.knockBack = 0f;
            item.shoot = mod.ProjectileType("LustPrism");
            item.shootSpeed = 30f;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item117;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.useTime = player.altFunctionUse != 2 ? 10 : 3;
            item.knockBack = player.altFunctionUse != 2 ? 0f : 4f;
            //item.shootSpeed = player.altFunctionUse != 2 ? 30f : 30f;
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("LustPrism") : mod.ProjectileType("SweetDream");
            item.UseSound = player.altFunctionUse != 2 ? SoundID.Item117 : SoundID.Item46;
            item.channel = player.altFunctionUse != 2;
            return base.CanUseItem(player);
        }
        public override void UseStyle(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                
            }
            else
            {
                if (player.channel && player.ownedProjectileCounts[mod.ProjectileType("LustPrismBeam")] <= 0)
                {
                    item.mana = 0;
                }
                else
                {
                    item.mana = 15;
                }
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < Main.rand.Next(2, 5); i++)
                {
                    Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                    float num = item.shootSpeed;
                    int num2 = (int)(damage * 1.0);
                    float num3 = knockBack;
                    float num4 = Main.mouseX + Main.screenPosition.X - vector.X;
                    float num5 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                    float num6 = Main.rand.NextFloat() * 6.28318548f;
                    float value = 30f;
                    float value2 = 90f;
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
                    Vector2 vector3 = Main.MouseWorld - player.Center;
                    Vector2 vector4 = new Vector2(num4, num5).SafeNormalize(Vector2.UnitY) * num;
                    vector3 = vector3.SafeNormalize(vector4) * num;
                    vector3 = Vector2.Lerp(vector3, vector4, 0.25f);
                    Projectile.NewProjectile(vector2, vector3, type, num2, num3, player.whoAmI, Main.rand.Next(3), -1f);
                }
                return false;
            }
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OrbStand");
            recipe.AddIngredient(null, "EssenceOfLust", 30);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}