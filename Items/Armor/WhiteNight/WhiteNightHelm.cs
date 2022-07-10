using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.WhiteNight
{
    [AutoloadEquip(EquipType.Head)]
	public class WhiteNightHelm : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Night Helm");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 24;
			item.height = 26;
			item.rare = 8;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 20;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("WhiteNightArmor") && legs.type == mod.ItemType("WhiteNightLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {

        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setWhiteNight = true;
        }
        public override void UpdateEquip(Player player)
        {

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PolarNightHelm");
            recipe.AddIngredient(ItemID.SpectreBar, 9);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}