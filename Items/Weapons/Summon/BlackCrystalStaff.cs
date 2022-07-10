using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Summon
{
    public class BlackCrystalStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Black Crystal Staff");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 26;
            item.height = 28;
            item.damage = 315;
            item.mana = 40;
            item.summon = true;
            item.sentry = true;
            item.noMelee = true;
            item.autoReuse = false;
            item.useStyle = 1;
            item.useTime = 25;
			item.useAnimation = 25;
			item.knockBack = 3.5f;
            item.shootSpeed = 0f;
            item.shoot = mod.ProjectileType("BlackCrystal");
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item78;
            item.scale = 1.3f;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            player.UpdateMaxTurrets();
            return true;
        }
    }
}