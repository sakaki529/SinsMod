using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class EchoicConfusion : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Echoic Confusion");
            Tooltip.SetDefault("");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.damage = 80;
            item.mana = 12;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.knockBack = 3f;
			item.shootSpeed = 10f;
			item.shoot = mod.ProjectileType("EchoRing");
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 9;
			item.UseSound = SoundID.Item79;
		}
    }
}