using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Summon
{
    public class PhantasmDragonStaff : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Phantasm Dragon Staff");
            Tooltip.SetDefault("Summons a phantasm dragon to fight for you");
		}
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 60;
            item.mana = 10;
            item.summon = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 30;
			item.useAnimation = 30;
			item.knockBack = 2.0f;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("PhantasmDragonHead");
            item.value = Item.sellPrice(0, 17, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item44;
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
            int segmentCount = player.ownedProjectileCounts[mod.ProjectileType("PhantasmDragonBody")];
            damage = (int)(damage * (1.69f + 0.46f * (segmentCount - 1)));
            int num3 = -1;
            int num4 = -1;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
                {
                    if (num3 == -1 && Main.projectile[i].type == mod.ProjectileType("PhantasmDragonHead"))
                    {
                        num3 = i;
                    }
                    else if (num4 == -1 && Main.projectile[i].type == mod.ProjectileType("PhantasmDragonTail"))
                    {
                        num4 = i;
                    }
                    if (Main.projectile[i].type == mod.ProjectileType("PhantasmDragonTail"))
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
                int num5 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("PhantasmDragonHead"), damage, knockBack, player.whoAmI, 0f, 0f);
                int num6 = num5;
                num5 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("PhantasmDragonBody"), damage, knockBack, player.whoAmI, num6, 0f);
                num6 = num5;
                num5 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("PhantasmDragonBody2"), damage, knockBack, player.whoAmI, num6, 0f);
                Main.projectile[num6].localAI[1] = num5;
                Main.projectile[num6].netUpdate = true;
                num6 = num5;
                num5 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("PhantasmDragonTail"), damage, knockBack, player.whoAmI, num6, 0f);
                Main.projectile[num6].localAI[1] = num5;
                Main.projectile[num6].netUpdate = true;
            }
            else
            {
                if (num3 != -1 && num4 != -1)
                {
                    Main.projectile[num4].damage = damage;
                    int num7 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("PhantasmDragonBody"), damage, knockBack, player.whoAmI, Main.projectile[num4].ai[0], 0f);
                    int num8 = Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("PhantasmDragonBody2"), damage, knockBack, player.whoAmI, num7, 0f);
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
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StardustDragonStaff);
            recipe.AddIngredient(ItemID.FragmentStardust, 8);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}