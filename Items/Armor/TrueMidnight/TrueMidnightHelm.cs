using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.TrueMidnight
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueMidnightHelm : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Midnight Helm");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 24;
			item.height = 26;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 48;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool DrawHead()
        {
            return false;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TrueMidnightArmor") && legs.type == mod.ItemType("TrueMidnightLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setTrueMidnight = true;
        }
        public override void UpdateEquip(Player player)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MidnightHelm");
            recipe.AddIngredient(null, "Axion", 9);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}