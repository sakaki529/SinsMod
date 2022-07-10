using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Unconscious
{
	[AutoloadEquip(EquipType.Body)]
	public class UnconsciousDress : ModItem
	{
        public override string Texture => "SinsMod/Extra/Placeholder/Placeholder";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unconscious Dress");
            Tooltip.SetDefault("Whose this?");
        }
        public override void SetDefaults()
		{
            item.width = 32;
			item.height = 44;
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