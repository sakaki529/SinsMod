using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
	public class IceBarrageStaff : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Ice Barrage Staff");
            Tooltip.SetDefault("");
            Item.staff[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 70;
            item.mana = 25;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 30;
			item.useAnimation = 30;
            item.knockBack = 6f;
            item.shoot = mod.ProjectileType("FrostShard");
            item.shootSpeed = 16f;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item43;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float num = Main.mouseX + Main.screenPosition.X - vector.X;
            float num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
            float num3 = (float)Math.Sqrt(num * num + num2 * num2);
            if ((float.IsNaN(num) && float.IsNaN(num2)) || (num == 0f && num2 == 0f))
            {
                num = player.direction;
                num2 = 0f;
                num3 = item.shootSpeed;
            }
            else
            {
                num3 = item.shootSpeed / num3;
            }
            num *= num3;
            num2 *= num3;
            int num4 = 4;
            if (Main.rand.Next(3) == 0)
            {
                num4++;
            }
            if (Main.rand.Next(4) == 0)
            {
                num4++;
            }
            if (Main.rand.Next(5) == 0)
            {
                num4++;
            }
            for (int i = 0; i < num4; i++)
            {
                float num5 = num;
                float num6 = num2;
                float num7 = 0.05f * i;
                num5 += Main.rand.Next(-35, 36) * num7;
                num6 += Main.rand.Next(-35, 36) * num7;
                num3 = (float)Math.Sqrt(num5 * num5 + num6 * num6);
                num3 = item.shootSpeed / num3;
                num5 *= num3;
                num6 *= num3;
                Projectile.NewProjectile(vector.X, vector.Y, num5, num6, type, damage, knockBack, player.whoAmI, Main.rand.Next(5), 0f);
            }
            return false;
        }
    }
}