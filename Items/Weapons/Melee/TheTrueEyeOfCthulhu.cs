using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class TheTrueEyeOfCthulhu : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("The True Eye of Cthulhu");
            Tooltip.SetDefault("");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 26;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 24;
			item.height = 24;
			item.damage = 128;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
			item.knockBack = 2.0f;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("TheTrueEyeOfCthulhu");
			item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item1;
            item.expert = true;
        }
    }
}