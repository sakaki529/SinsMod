using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    public class ElysianEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elysian Emblem");
            Tooltip.SetDefault("35% increased melee,magic,ranged and minion damage" + 
                "\n12% increased these critical stike chance" + 
                "\nincreases melee knockback" + 
                "\n25% increased minion knockback" + 
                "\n25% inceased melee speed" + 
                "\nincreases pickup range for mana stars");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 9;
            item.accessory = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 13;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.35f;
            player.magicDamage += 0.35f;
            player.rangedDamage += 0.35f;
            player.minionDamage += 0.35f;
            player.meleeCrit += 12;
            player.magicCrit += 12;
            player.rangedCrit += 12;
            player.meleeSpeed += 0.25f;
            player.minionKB += 0.25f;
            player.manaMagnet = true;
            player.kbGlove = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DestroyerEmblem, 1);
            recipe.AddIngredient(ItemID.CelestialEmblem, 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 4);
            recipe.AddIngredient(ItemID.FragmentVortex, 4);
            recipe.AddIngredient(ItemID.FragmentStardust, 4);
            recipe.AddIngredient(ItemID.FragmentNebula, 4);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}