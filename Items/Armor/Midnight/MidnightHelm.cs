using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Midnight
{
    [AutoloadEquip(EquipType.Head)]
	public class MidnightHelm : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Helm");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 24;
			item.height = 26;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 42;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
        }
        public override bool DrawHead()
        {
            return false;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MidnightArmor") && legs.type == mod.ItemType("MidnightLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setMidnight = true;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallHelm");
            recipe.AddIngredient(null, "EssenceOfEnvy", 8);
            recipe.AddIngredient(null, "EssenceOfGluttony", 8);
            recipe.AddIngredient(null, "EssenceOfGreed", 8);
            recipe.AddIngredient(null, "EssenceOfLust", 8);
            recipe.AddIngredient(null, "EssenceOfPride", 8);
            recipe.AddIngredient(null, "EssenceOfSloth", 8);
            recipe.AddIngredient(null, "EssenceOfWrath", 8);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}