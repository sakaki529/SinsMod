using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class StormSpell : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Storm Spell");
            Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 28;
			item.height = 30;
			item.damage = 87;
            item.mana = 8;
            item.magic = true;
            item.noMelee = true;
			item.autoReuse = false;
            item.useStyle = 5;
            item.useTime = 45;
			item.useAnimation = 45;
            item.knockBack = 3.5f;
            item.value = Item.sellPrice(0, 0, 80, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item43;
            item.shootSpeed = 10;
            item.shoot = mod.ProjectileType("StormSpell");
        }
    }
}