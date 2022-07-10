using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class CharredScarletSaber : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charred Scarlet Saber");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.damage = 72;
            item.melee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 8;
            item.shoot = mod.ProjectileType("ScarletFlame");
            item.shootSpeed = 7.5f;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            knockBack /= 4;
            return true;
        }
    }
}