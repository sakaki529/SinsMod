using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class GoldRush : ModItem
	{
	    public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("Gold Rush");
        }
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 281;
			item.melee = true;
            item.noMelee = true;
			item.noUseGraphic = true;
            item.autoReuse = true;
            item.channel = true;
			item.useTime = 10;
			item.useAnimation = 10;
            item.useStyle = 5;
            item.knockBack = 0.25f;
            item.shoot = mod.ProjectileType("GoldRush");
            item.shootSpeed = 10f;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
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
        /*public override bool AltFunctionUse(Player player)
        {
            return true;
        }*/
        public override bool CanUseItem(Player player)
        {
            item.shootSpeed = player.altFunctionUse != 2 ? 10 : 27.5f;
            item.reuseDelay = player.altFunctionUse != 2 ? 0 : 60;
            item.channel = player.altFunctionUse != 2 ? false : true;
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, -1f, 0f);
                return false;
            }
            Vector2 rad = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5f));
            speedX = rad.X;
            speedY = rad.Y;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}