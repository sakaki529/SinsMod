using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Magellanic
{
    [AutoloadEquip(EquipType.Body)]
	public class MagellanicBreastplate : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magellanic Breastplate");
            Tooltip.SetDefault("12% increased magic damage and critical strike chance");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
        public override void SetDefaults()
		{
            item.width = 18;
			item.height = 18;
			item.rare = 10;
            item.defense = 24;
            item.glowMask = customGlowMask;
        }
        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.12f;
            player.magicCrit += 12;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NebulaBreastplate);
            recipe.AddIngredient(ItemID.FragmentNebula, 20);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}