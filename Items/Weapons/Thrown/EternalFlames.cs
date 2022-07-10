using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Thrown
{
    public class EternalFlames : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Eternal Flames");
            Tooltip.SetDefault("'Got it memorized?'");
		}
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 140;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 1;
			item.knockBack = 6;
            item.shoot = mod.ProjectileType("EternalFlames");
            item.shootSpeed = 20f;
			item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item1;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 2;
        }
    }
}