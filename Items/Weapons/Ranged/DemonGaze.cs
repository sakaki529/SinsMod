using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class DemonGaze : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Gaze");
            Tooltip.SetDefault("");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
        public override void SetDefaults()
		{
            item.width = 58;
			item.height = 116;
            item.damage = 58;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
			item.useStyle = 5;
			item.useTime = 20;
			item.useAnimation = 20;
            item.knockBack = 10f;
            item.useAmmo = AmmoID.Arrow;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 13f;
			item.value = Item.sellPrice(0, 4, 0, 0);
			item.rare = 6;
			item.UseSound = SoundID.Item5;
            item.glowMask = customGlowMask;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return true;
        }
    }
}