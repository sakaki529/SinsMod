using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    public class MagellanicMantle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magellanic Mantle");
            Tooltip.SetDefault("Count as wings" +
                "\nHorizontal speed: 12" +
                "\nAcceleration multiplier: 2.75" +
                "\nGood vertical speed" +
                "\nFlight time: 200" +
                "\nHold DOWN and JUMP to hover");
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
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.Hover = true;
            player.wingTimeMax = 200;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.2f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.17f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 12f;
            acceleration *= 2.75f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WingsNebula);
            recipe.AddIngredient(ItemID.FragmentNebula, 14);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}