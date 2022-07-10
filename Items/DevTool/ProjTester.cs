using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.DevTool
{
    public class ProjTester : ModItem
	{
        public override string Texture => "SinsMod/Items/DevTool/Book";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Projectiles Tester");
			Tooltip.SetDefault("[c/ff0000:Dev Tool]");
        }
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
            item.useStyle = 4;
            item.useTime = 60;
            item.useAnimation = 60;
            item.shoot = mod.ProjectileType("SpreadShortcut");
            item.autoReuse = false;
            item.rare = -1;
			item.value = Item.sellPrice(0, 0, 0, 0);
            item.useTurn = true;
            item.shootSpeed = 16f;
            item.GetGlobalItem<SinsItem>().isDevTools = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(140, 140, 255);
                }
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (modPlayer.Debug)
            { }
            else
            {
                throw new Exception("This is Dev tool. You should not use this.");
            }
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (modPlayer.Debug)
            {
                if (player.altFunctionUse == 2)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Hellheat"), 200, 0f, player.whoAmI, 0f, 0f);
                }
                else
                {
                    position.X = Main.MouseWorld.X;
                    position.Y = Main.MouseWorld.Y;
                    Projectile.NewProjectile(position.X, position.Y, 0f, 0f, mod.ProjectileType("RingEffect"), 0, 0f, player.whoAmI, 1f, 0f);
                }
            }
            return false;
        }
    }
}