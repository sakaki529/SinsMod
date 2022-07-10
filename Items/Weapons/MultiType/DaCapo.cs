using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.MultiType
{
    public class DaCapo : ModItem
	{
		public override void SetStaticDefaults()
		{
            Tooltip.SetDefault("Right click to throw scythe");
        }
		public override void SetDefaults()
		{
            item.width = 42;
            item.height = 42;
			item.damage = 63; 
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
			item.useStyle = 1;
            item.useTime = 19;
			item.useAnimation = 19; 
			item.knockBack = 2f;
            item.shootSpeed = 15;
            item.value = Item.sellPrice(0, 20, 20, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().isMultiType = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(Main.DiscoR, 0, 0);
                }
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.melee = player.altFunctionUse != 2 ? true : false;
            item.thrown = player.altFunctionUse != 2 ? false : true;
            item.noMelee = player.altFunctionUse != 2 ? false : true;
            item.noUseGraphic = player.altFunctionUse != 2 ? false : true;
            item.useTurn = player.altFunctionUse != 2 ? true : false;
            item.shoot = player.altFunctionUse != 2 ? 0 : mod.ProjectileType("DaCapoThrown");
            item.UseSound = player.altFunctionUse != 2 ? SoundID.Item1 : SoundID.Item18;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return player.altFunctionUse == 2;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if (player.altFunctionUse != 2)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("DaCapo"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 0);
            }
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            if (player.altFunctionUse != 2)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("DaCapo"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 1);
            }
        }
    }
}