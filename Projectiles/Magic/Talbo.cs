using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class Talbo : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talbo");
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = 0f;
            int num2 = 4;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (projectile.spriteDirection == -1)
            {
                num = 3.14159274f;
            }
            projectile.ai[0] += 1f;
            projectile.ai[1] -= 1f;
            bool flag = false;
            if (projectile.ai[1] <= 0f)
            {
                projectile.ai[1] = num2;
                flag = true;
            }
            bool flag2 = player.channel && !player.noItems && !player.CCed;
            if (projectile.localAI[0] > 0f)
            {
                projectile.localAI[0] -= 1f;
            }
            int num3 = (int)(player.inventory[player.selectedItem].mana * player.manaCost);
            if (projectile.soundDelay <= 0 & flag2)
            {
                if (player.statMana < num3)
                {
                    if (player.manaFlower)
                    {
                        player.QuickMana();
                        if (player.statMana >= num3)
                        {
                            player.manaRegenDelay = (int)player.maxRegenDelay;
                            player.statMana -= num3;
                        }
                        else
                        {
                            projectile.Kill();
                        }
                    }
                    else
                    {
                        projectile.Kill();
                    }
                }
                else if(player.statMana >= num3)
                {
                    player.statMana -= num3;
                    player.manaRegenDelay = (int)player.maxRegenDelay;
                }
                projectile.soundDelay = 7;
                if (projectile.ai[0] != 1f)
                {
                    Main.PlaySound(SoundID.Item47, projectile.Center);
                }
                projectile.localAI[0] = 12f;
            }
            if (flag && Main.myPlayer == projectile.owner)
            {
                int num4 = mod.ProjectileType("RedNote");
                float scaleFactor = 18f;
                int weaponDamage = player.GetWeaponDamage(player.inventory[player.selectedItem]);
                float weaponKnockback = player.inventory[player.selectedItem].knockBack;
                if (flag2)
                {
                    weaponKnockback = player.GetWeaponKnockback(player.inventory[player.selectedItem], weaponKnockback);
                    float scaleFactor2 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 vector2 = vector;
                    Vector2 value = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - vector2;
                    if (player.gravDir == -1f)
                    {
                        value.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector2.Y;
                    }
                    Vector2 vector3 = Vector2.Normalize(value);
                    if (float.IsNaN(vector3.X) || float.IsNaN(vector3.Y))
                    {
                        vector3 = -Vector2.UnitY;
                    }
                    vector3 *= scaleFactor2;
                    if (vector3.X != projectile.velocity.X || vector3.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector3 * 0.55f;
                    Vector2 vector4 = Vector2.Normalize(projectile.velocity) * scaleFactor * (0.6f + Utils.NextFloat(Main.rand) * 0.8f);
                    if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
                    {
                        vector4 = -Vector2.UnitY;
                    }
                    Vector2 vector5 = vector2 + Utils.RandomVector2(Main.rand, -10f, 10f);
                    Projectile.NewProjectile(vector5, vector4.RotatedByRandom(MathHelper.ToRadians(6)), num4, weaponDamage, weaponKnockback, projectile.owner, Main.rand.Next(3), 0f);
                }
                else
                {
                    projectile.Kill();
                }
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
        }
    }
}