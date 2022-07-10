using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    public class MilkyWayWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Milky Way Wings");
            Tooltip.SetDefault("Count as wings" +
                "\nHorizontal speed: 14" +
                "\nAcceleration multiplier: 2" +
                "\nGood vertical speed" +
                "\nFlight time: 200");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = Item.sellPrice(0, 18, 0, 0);
            item.rare = 10;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 200;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.45f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2f;
            constantAscend = 0.35f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 14f;
            acceleration *= 3.0f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            //player.wingFrameCounter++;
            return base.WingUpdate(player, inUse);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WingsStardust);
            recipe.AddIngredient(ItemID.FragmentStardust, 14);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}