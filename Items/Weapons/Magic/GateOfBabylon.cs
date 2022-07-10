using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
	public class GateOfBabylon : ModItem
	{
        public override string Texture => "SinsMod/Extra/Placeholder/Placeholder";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Gate of Babylon");
            Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.damage = 340;
            item.mana = 16;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 4;
            item.useAnimation = 9;
            item.knockBack = 8f;
            item.shootSpeed = 30;
            item.shoot = mod.ProjectileType("GateOfBabylon");
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item46;
            item.GetGlobalItem<SinsItem>().CustomRarity = 12;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
                float value = 50f;
                float value2 = 150f;
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
    }
}