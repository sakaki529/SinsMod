using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged.Ammo
{
    public class DistortionSolution : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Distortion Solution");
			Tooltip.SetDefault("Used by the Clentaminator" +
                "\nSpreads the Distortion");
        }
        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("DistortionSpray") - ProjectileID.PureSpray;
            item.ammo = AmmoID.Solution;
            item.width = 10;
            item.height = 12;
            item.value = Item.buyPrice(0, 0, 25, 0);
            item.rare = 3;
            item.maxStack = 999;
            item.consumable = true;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return player.itemAnimation >= player.HeldItem.useAnimation - 3;
        }
    }
}