using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.WhiteNight
{
	[AutoloadEquip(EquipType.Body)]
	public class WhiteNightArmor : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Night Armor");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 44;
			item.rare = 8;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 24;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PolarNightArmor");
            recipe.AddIngredient(ItemID.SpectreBar, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}