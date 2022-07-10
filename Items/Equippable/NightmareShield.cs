using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    [AutoloadEquip(EquipType.Shield)]
    public class NightmareShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightmare Shield");
            Tooltip.SetDefault("\nGrants immunity to knockback and chaos state debuff" +
                "\n20% increased damage reduction" + 
                "\nGives werewolf buff at night and merfolk buff when entering water" +
                "\nAllows the ability to invincible dash" +
                "\nDouble tap a direction" +
                "\nWhen below 25% life you become invincible");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(1, 20, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.defense = 30;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.NightmareShield = true;
            modPlayer.ModDash = 1;
            player.buffImmune[BuffID.ChaosState] = true;
            player.noKnockback = true;
            player.accMerman = true;
            player.wolfAcc = true;
            player.hideMerman = true;
            player.hideWolf = true;
            player.endurance += 0.2f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssalFlameRelic");
            recipe.AddIngredient(null, "EssenceOfMadness", 12);
            recipe.AddIngredient(null, "NightmareBar", 24);
            if (SinsMod.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("CosmiliteBar"), 24);
            }
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}