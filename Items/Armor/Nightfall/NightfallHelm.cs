using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Nightfall
{
    [AutoloadEquip(EquipType.Head)]
    public class NightfallHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightfall Helm");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 26;
            item.rare = 10;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 24;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("NightfallArmor") && legs.type == mod.ItemType("NightfallLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {

        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setNightfall = true;
        }
        public override void UpdateEquip(Player player)
        {

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WhiteNightHelm");
            recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
            recipe.AddIngredient(ItemID.FragmentSolar, 16);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}