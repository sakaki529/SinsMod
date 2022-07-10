using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Summon
{
    public class BlackCoreStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Black Core Staff");
            Tooltip.SetDefault("Uses 2 minion slots");
		}
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 270;
            item.mana = 20;
            item.summon = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 25;
			item.useAnimation = 25;
			item.knockBack = 0.5f;
            item.shootSpeed = 0f;
            item.shoot = mod.ProjectileType("SmallBlackCore");
            item.value = Item.sellPrice(0, 75, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item90;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
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