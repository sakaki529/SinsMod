using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class WhiteCoreStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("White Core Staff");
            Tooltip.SetDefault("Inflict white core curse on hit enemies" +
                "\nDebuff decreases enemy's speed");
        }
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
			item.damage = 400;
            item.mana = 40;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = false;
            item.useStyle = 1;
            item.useTime = 25;
			item.useAnimation = 25;
			item.knockBack = 0f;
            item.shootSpeed = 0f;
            item.shoot = mod.ProjectileType("SmallWhiteCore");
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item90;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.type == mod.ProjectileType("SmallWhiteCore"))
                {
                    projectile.Kill();
                }
            }
            for (int j = 0; j < 5; j++)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("SmallWhiteCore"), damage, knockBack, player.whoAmI, j, 0f);
            }
            return false;
        }
    }
}