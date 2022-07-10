using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Unconscious
{
	[AutoloadEquip(EquipType.Legs)]
	public class UnconsciousBoots : ModItem
	{
        public override string Texture => "SinsMod/Extra/Placeholder/Placeholder";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unconscious Boots");
            Tooltip.SetDefault("Whose this?");
        }
        public override void SetDefaults()
		{
            item.width = 30;
			item.height = 18;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 60;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override void UpdateEquip(Player player)
        {
            player.aggro -= 120;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PainfulHeart", 12);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}