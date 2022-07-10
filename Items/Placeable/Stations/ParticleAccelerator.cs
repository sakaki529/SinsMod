using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Stations
{
    public class ParticleAccelerator : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Particle Accelerator");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 9;
            item.createTile = mod.TileType("ParticleAccelerator");
            item.maxStack = 99;
        }
    }
}