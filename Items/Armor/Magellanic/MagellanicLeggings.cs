using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Magellanic
{
    [AutoloadEquip(EquipType.Legs)]
	public class MagellanicLeggings : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magellanic Leggings");
            Tooltip.SetDefault("12% increased magic damage" +
                "\n14% increased movement speed");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
        public override void SetDefaults()
		{
            item.width = 18;
			item.height = 18;
			item.rare = 10;
            item.defense = 22;
            item.glowMask = customGlowMask;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.12f;
            player.magicDamage += 0.14f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NebulaLeggings);
            recipe.AddIngredient(ItemID.FragmentNebula, 15);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}