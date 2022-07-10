using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class TartarusWhip : ModItem
	{
        public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("Tartarus");
            Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
			item.damage = 444;
            item.width = 16;
            item.height = 16;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.autoReuse = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 3f;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("TartarusWhip");
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item116;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0, Main.rand.Next(-300, 300) * 0.001f * player.gravDir);
            return false;
        }
    }
}