using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.MultiType
{
    public class Talbo : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talbo");
            Tooltip.SetDefault("Right click for axe");
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 180;
            item.mana = 12;
			item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.channel = true;
            item.useStyle = 5;
            item.useTime = 1;
            item.useAnimation = 10;
			item.knockBack = 10f;
            item.shoot = mod.ProjectileType("Talbo");
            item.shootSpeed = 30f;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item47;
            item.GetGlobalItem<SinsItem>().CustomRarity = 13;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().isMultiType = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.mana = player.altFunctionUse != 2 ? 12 : 0;
            item.melee = player.altFunctionUse != 2 ? false : true;
            item.magic = player.altFunctionUse != 2 ? true : false;
            item.noMelee = player.altFunctionUse != 2 ? true : false;
            item.noUseGraphic = player.altFunctionUse != 2 ? true : false;
            item.channel = player.altFunctionUse != 2 ? true : false;
            item.useStyle = player.altFunctionUse != 2 ? 5 : 1;
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("Talbo") : 0;
            item.axe = player.altFunctionUse != 2 ? 0 : 300 / 5;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ExplosionSound"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 0);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ExplosionSound"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 0);
        }
    }
}