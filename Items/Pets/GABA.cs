using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Pets
{
    public class GABA : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("GABA");
            Tooltip.SetDefault("Summons a Koby");
        }
		public override void SetDefaults()
		{
            item.width = 26;
            item.height = 26;
            item.damage = 0;
            item.noMelee = true;
            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shoot = mod.ProjectileType("KobyPet");
			item.buffType = mod.BuffType("KobyPet");
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