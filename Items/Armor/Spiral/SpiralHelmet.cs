using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Spiral
{
    [AutoloadEquip(EquipType.Head)]
    public class SpiralHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral Helmet");
            Tooltip.SetDefault("20% increased ranged damage" +
                "\n10% increased ranged critical strike chance");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = 10;
            item.defense = 18;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("SpiralBreastplate") && legs.type == mod.ItemType("SpiralLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("ArmorSetBonus.Vortex", Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN")) +
                "\n";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setSpiral = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 10;
            player.rangedDamage += 0.20f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexHelmet);
            recipe.AddIngredient(ItemID.FragmentVortex, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}