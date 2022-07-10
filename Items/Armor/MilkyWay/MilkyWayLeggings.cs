using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.MilkyWay
{
    [AutoloadEquip(EquipType.Legs)]
	public class MilkyWayLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Milky Way Leggings");
            Tooltip.SetDefault("Increases your max number of minions" +
                "Increases minion damage by 26%");
        }
        public override void SetDefaults()
		{
            item.width = 18;
			item.height = 18;
			item.rare = 10;
            item.defense = 18;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Lighting.AddLight(item.position, 0.2f, 0.2f, 0.2f);
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 2;
            player.minionDamage += 0.26f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StardustLeggings);
            recipe.AddIngredient(ItemID.FragmentStardust, 15);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}