using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class ElementalGreatBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Elemental Great Blade");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 58;
			item.height = 58;
			item.damage = 777;
			item.melee = true;
            item.autoReuse = true;
            item.useStyle = 1;
			item.useTime = 12;
			item.useAnimation = 12;
			item.knockBack = 11;
            item.shootSpeed = 28f;
            item.shoot = mod.ProjectileType("ElementalBeam");
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.scale = 1.0f;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = Main.DiscoColor;
                }
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, Main.rand.Next(9), 0);
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Chroma"), 360);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ElementalBlade"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 0);
            if (target.type != NPCID.TargetDummy)
            {
                //Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 88, 1f, 0f);
                Main.PlaySound(SoundID.Item60, (int)player.position.X, (int)player.position.Y);
                float shootSpeed = item.shootSpeed;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float num = Main.mouseX + Main.screenPosition.X - vector.X;
                float num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                if (player.gravDir == -1f)
                {
                    num2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
                }
                float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                if ((float.IsNaN(num) && float.IsNaN(num2)) || (num == 0f && num2 == 0f))
                {
                    num = player.direction;
                    num2 = 0f;
                    num3 = shootSpeed;
                }
                else
                {
                    num3 = shootSpeed / num3;
                }
                num *= num3;
                num2 *= num3;
                int num4 = 3;
                for (int i = 0; i < num4; i++)
                {
                    vector = new Vector2(player.position.X + player.width * 0.5f + (float)Main.rand.Next(201) * -player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    vector.X = (vector.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                    vector.Y -= 100 * i;
                    num = Main.mouseX + Main.screenPosition.X - vector.X + Main.rand.Next(-40, 41) * 0.03f;
                    num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                    if (num2 < 0f)
                    {
                        num2 *= -1f;
                    }
                    if (num2 < 20f)
                    {
                        num2 = 20f;
                    }
                    num3 = (float)Math.Sqrt(num * num + num2 * num2);
                    num3 = shootSpeed / num3;
                    num *= num3;
                    num2 *= num3;
                    float speedX = num;
                    float speedY = num2 + Main.rand.Next(-80, 81) * 0.02f;
                    Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, mod.ProjectileType("ElementalShower"), (int)((double)item.damage * player.meleeDamage / 5), knockback, player.whoAmI, 0f, 0f);
                }
            }
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Chroma"), 360);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ElementalBlade"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 1);
            Main.PlaySound(SoundID.Item60, (int)player.position.X, (int)player.position.Y);
            float shootSpeed = item.shootSpeed;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float num = Main.mouseX + Main.screenPosition.X - vector.X;
            float num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
            if (player.gravDir == -1f)
            {
                num2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
            }
            float num3 = (float)Math.Sqrt(num * num + num2 * num2);
            if ((float.IsNaN(num) && float.IsNaN(num2)) || (num == 0f && num2 == 0f))
            {
                num = player.direction;
                num2 = 0f;
                num3 = shootSpeed;
            }
            else
            {
                num3 = shootSpeed / num3;
            }
            num *= num3;
            num2 *= num3;
            int num4 = 3;
            for (int i = 0; i < num4; i++)
            {
                vector = new Vector2(player.position.X + player.width * 0.5f + (float)Main.rand.Next(201) * -player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                vector.X = (vector.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                vector.Y -= 100 * i;
                num = Main.mouseX + Main.screenPosition.X - vector.X + Main.rand.Next(-40, 41) * 0.03f;
                num2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                if (num2 < 0f)
                {
                    num2 *= -1f;
                }
                if (num2 < 20f)
                {
                    num2 = 20f;
                }
                num3 = (float)Math.Sqrt(num * num + num2 * num2);
                num3 = shootSpeed / num3;
                num *= num3;
                num2 *= num3;
                float speedX = num;
                float speedY = num2 + Main.rand.Next(-80, 81) * 0.02f;
                Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, mod.ProjectileType("ElementalShower"), (int)((double)item.damage * player.meleeDamage / 5), 0, player.whoAmI, 0f, 0f);
            }
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(2) == 0)
            {
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 267);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.05f;
                Main.dust[num].scale = 1.5f;
                Main.dust[num].color = Main.DiscoColor;
                Main.dust[num].alpha = 0;
            }
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalBlade");
            recipe.AddIngredient(null, "EssenceOfGluttony", 12);
            recipe.AddIngredient(null, "EssenceOfGreed", 12);
            recipe.AddIngredient(null, "EssenceOfLust", 12);
            recipe.AddIngredient(null, "EssenceOfPride", 12);
            recipe.AddIngredient(null, "EssenceOfSloth", 12);
            recipe.AddIngredient(null, "EssenceOfWrath", 12);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}