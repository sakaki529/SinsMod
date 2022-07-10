using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.PolarNight
{
    [AutoloadEquip(EquipType.Head)]
	public class PolarNightHelm : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Polar Night Helm");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 24;
			item.height = 26;
			item.rare = 4;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 16;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("PolarNightArmor") && legs.type == mod.ItemType("PolarNightLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {

        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setPolarNight = true;
        }
        public override void UpdateEquip(Player player)
        {

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightEnergizedBar", 10);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}