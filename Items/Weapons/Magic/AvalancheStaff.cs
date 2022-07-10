using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AvalancheStaff : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Avalanche Staff");
            Tooltip.SetDefault("");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.damage = 72;
            item.mana = 10;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 2;
            item.useAnimation = 5;
            item.knockBack = 7f;
			item.shootSpeed = 10f;
			item.shoot = mod.ProjectileType("Avalanche");
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 9;
			item.UseSound = SoundID.Item24;
            item.GetGlobalItem<SinsItem>().CustomRarity = 12;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num = 4;
            for (int num2 = 0; num2 < num; num2++)
            {
                position = new Vector2(player.position.X + player.width * 0.5f + (Main.rand.Next(201) * -(float)player.direction) + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                position.X = (position.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                position.Y -= 100 * num2;
                float num3 = Main.mouseX + Main.screenPosition.X - position.X;
                float num4 = Main.mouseY + Main.screenPosition.Y - position.Y;
                float ai2 = num4 + position.Y;
                if (num4 < 0f)
                {
                    num4 *= -1f;
                }
                if (num4 < 20f)
                {
                    num4 = 20f;
                }
                float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                num5 = item.shootSpeed / num5;
                num3 *= num5;
                num4 *= num5;
                Vector2 vector = new Vector2(num3, num4) / 2f;
                int num6 = Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, ai2);
                Main.projectile[num6].ranged = false;
                Main.projectile[num6].magic = true;
            }
            return false;
        }
    }
}