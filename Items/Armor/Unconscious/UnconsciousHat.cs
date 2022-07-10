using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Unconscious
{
    [AutoloadEquip(EquipType.Head)]
    public class UnconsciousHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unconscious Hat");
            Tooltip.SetDefault("Whose this?");
        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 16;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 9;
            item.defense = 60;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = false;
            drawAltHair = false;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("UnconsciousDress") && legs.type == mod.ItemType("UnconsciousBoots");
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setUnconscious = true;
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