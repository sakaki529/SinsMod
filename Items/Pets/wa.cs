using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Pets
{
    public class wa : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("wa");
            Tooltip.SetDefault("Summons a Cleyera pet");
        }
		public override void SetDefaults()
		{
            item.width = 30;
            item.height = 30;
            item.damage = 0;
            item.noMelee = true;
            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shoot = mod.ProjectileType("CleyeraPet");
            item.buffType = mod.BuffType("CleyeraPet");
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item2;
        }
		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}