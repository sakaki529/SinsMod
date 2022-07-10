using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    [AutoloadEquip(EquipType.Waist)]
    public class UnconsciousRose : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unconscious Rose");
            Tooltip.SetDefault("8% reduced mana usage" +
                "\nAutomatically use mana potions when needed");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 9;
            item.accessory = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaFlower = true;
            //player.manaCost *= 0.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PainfulHeart", 8);
            recipe.AddIngredient(ItemID.ManaFlower);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}