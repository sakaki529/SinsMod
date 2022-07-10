using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Summon
{
    public class Moonlight : ModItem
	{
        public override string Texture => "SinsMod/Extra/Placeholder/Placeholder";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("(need name)");
            Tooltip.SetDefault("Summons a moonlight bit to fight for you");
		}
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.damage = 260;
            item.mana = 20;
            item.summon = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 25;
			item.useAnimation = 25;
			item.knockBack = 3.0f;
            item.shootSpeed = 0f;
            item.shoot = mod.ProjectileType("MoonlightBit");
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item109;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
                return false;
            }
            position = Main.MouseWorld;
            return player.altFunctionUse != 2;
        }
    }
}