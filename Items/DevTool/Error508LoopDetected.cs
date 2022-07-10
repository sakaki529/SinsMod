using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.DevTool
{
    public class Error508LoopDetected : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("[DATA EXPUNGED]");
			Tooltip.SetDefault("[c/ff0000:DATA REDACTED]");
        }
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
            item.autoReuse = false;
            item.useStyle = 5;
            item.useTime = 30;
            item.useAnimation = 30;
            item.shoot = mod.ProjectileType("Error508");
            item.shootSpeed = 11f;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = -1;
            item.GetGlobalItem<SinsItem>().isDevTools = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(255, 0, 0);
                }
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (modPlayer.Debug)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, 1, knockBack, player.whoAmI, 0f, 1f);
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, 1, knockBack, player.whoAmI, 0f, 0f);
            }
            return false;
        }
    }
}