using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.MultiType
{
    public class AbyssalStaff : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Staff");
            Tooltip.SetDefault("Right click to summons an abyssal sphere centry");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
		{
            item.width = 40;
			item.height = 40;
			item.damage = 490;
            item.mana = 20;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 3;
            item.useAnimation = 10;
			item.knockBack = 6f;
            item.shootSpeed = 8f;
            item.shoot = mod.ProjectileType("AbyssalWave");
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item72;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().isMultiType = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.magic = player.altFunctionUse != 2 ? true : false;
            item.summon = player.altFunctionUse != 2 ? false : true;
            item.useTime = player.altFunctionUse != 2 ? 4 : 20;
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("AbyssalWave") : mod.ProjectileType("AbyssalSphereSentry");
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                for (int i = 1; i < 3; i++)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 2, knockBack, player.whoAmI, i, i);
                }
                return false;
            }
            speedX = speedY = 0f;
            position = Main.MouseWorld;
            player.UpdateMaxTurrets();
            return true;
        }
    }
}