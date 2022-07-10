using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class PlanteraFlail : ModItem
	{
        public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("Flailera");
            Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 16;
            item.height = 16;
            item.damage = 76;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.knockBack = 8f;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("PlanteraFlail");
            item.rare = 7;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.UseSound = SoundID.Item1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector = Main.MouseWorld - player.RotatedRelativePoint(player.MountedCenter, true);
            if (vector != Vector2.Zero)
            {
                vector.Normalize();
            }
            position += vector;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}