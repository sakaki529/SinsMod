using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Materials
{
    public class BlackCrystalShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Black Crystal Shard");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 15;
            item.maxStack = 99;
            item.value = Item.sellPrice(0, 0, 48, 0);
			item.rare = 9;
            item.createTile = mod.TileType("BlackCrystals");
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
	}
}