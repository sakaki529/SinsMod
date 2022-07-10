using Terraria.ModLoader;

namespace SinsMod.Items.Placeable.Trophies
{
    public class EyeOfSatanTrophy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of Satan Trophy");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 50000;
            item.rare = 1;
            item.createTile = mod.TileType("EyeOfSatanTrophy");
        }
    }
}