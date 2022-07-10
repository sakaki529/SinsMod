using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    [AutoloadEquip(EquipType.Waist)]
    public class ElementalFlower : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Flower");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 40;
            item.value = 100000;
            item.rare = 5;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaFlower = true;
            player.lavaRose = true;
            player.manaCost -= 0.08f;
            player.pStone = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaFlower, 1);
            recipe.AddIngredient(ItemID.ObsidianRose, 1);
            recipe.AddIngredient(ItemID.PhilosophersStone, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}