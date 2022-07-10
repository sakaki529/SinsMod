using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Summon
{
    public class AbyssalGuardianStaff : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Staff of Abyssal Guardian");
            Tooltip.SetDefault("Summons a abyssal guardian to fight for you");
		}
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 116;
            item.mana = 20;
            item.summon = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 25;
			item.useAnimation = 25;
			item.knockBack = 2.0f;
            item.shootSpeed = 0f;
            item.shoot = mod.ProjectileType("TartarusMinionHead");
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item100;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
                return false;
            }
            float num = 1f;
            float num2 = 0f;
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.minion && projectile.owner == player.whoAmI)
                {
                    num2 += projectile.minionSlots;
                    if (num2 + num > player.maxMinions)
                    {
                        return false;
                    }
                }
            }
            position = Main.MouseWorld;
            int segmentCount = player.ownedProjectileCounts[mod.ProjectileType("TartarusMinionBody")];
            /*damage = (int)(damage * (1.69f + 0.46f * (segmentCount - 1)));
            if (segmentCount > 10)
            {
                segmentCount = 10;
            }*/
            damage = (int)(damage * (player.minionDamage * 5f / 3f + player.minionDamage * 0.46f * (segmentCount - 1)));
            int num3 = -1;
            int num4 = -1;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
                {
                    if (num3 == -1 && Main.projectile[i].type == mod.ProjectileType("TartarusMinionHead"))
                    {
                        num3 = i;
                    }
                    else if (num4 == -1 && Main.projectile[i].type == mod.ProjectileType("TartarusMinionTail"))
                    {
                        num4 = i;
                    }
                    if (num4 != -1 && Main.projectile[i].type == mod.ProjectileType("TartarusMinionTail"))
                    {
                        position = Main.projectile[i].Center;
                    }
                    if (num3 != -1 && num4 != -1)
                    {
                        break;
                    }
                }
            }
            if (num3 == -1 && num4 == -1)
            {
                int num5 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("TartarusMinionHead"), damage, knockBack, player.whoAmI, 0f, 0f);
                int num6 = num5;
                num5 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("TartarusMinionBody"), damage, knockBack, player.whoAmI, num6, 0f);
                num6 = num5;
                num5 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("TartarusMinionBody2"), damage, knockBack, player.whoAmI, num6, 0f);
                Main.projectile[num6].localAI[1] = num5;
                Main.projectile[num6].netUpdate = true;
                num6 = num5;
                num5 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("TartarusMinionTail"), damage, knockBack, player.whoAmI, num6, 0f);
                Main.projectile[num6].localAI[1] = num5;
                Main.projectile[num6].netUpdate = true;
            }
            else
            {
                if (num3 != -1 && num4 != -1)
                {
                    Main.projectile[num4].damage = damage;
                    int num7 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("TartarusMinionBody"), damage, knockBack, player.whoAmI, Main.projectile[num4].ai[0], 0f);
                    int num8 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("TartarusMinionBody2"), damage, knockBack, player.whoAmI, num7, 0f);
                    Main.projectile[num7].localAI[1] = num8;
                    Main.projectile[num7].ai[1] = 1f;
                    Main.projectile[num7].netUpdate = true;
                    Main.projectile[num8].localAI[1] = num4;
                    Main.projectile[num8].netUpdate = true;
                    Main.projectile[num8].ai[1] = 1f;
                    Main.projectile[num4].ai[0] = num8;
                    Main.projectile[num4].netUpdate = true;
                    Main.projectile[num4].ai[1] = 1f;
                }
            }
            return false;
        }
    }
}