using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss
{
    public class PlanteraBulb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plantera's Bulb");
            Tooltip.SetDefault("Left click to summon plantera."
                + "\nRight click to place plantera's bulb on jungle grass."
                + "\nNot consumable.");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 500;
            item.rare = 6;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 20;
                item.useTime = 20;
                item.useStyle = 1;
                item.createTile = TileID.PlanteraBulb;
            }
            else
            {
                item.useAnimation = 30;
                item.useTime = 30;
                item.useStyle = 4;
                return !NPC.AnyNPCs(NPCID.Plantera);
            }
            return base.CanUseItem(player);
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            else
            {
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("PlanteraBulbSummon"), 20);
            recipe.AddIngredient(mod.ItemType("PlanteraBulbPlaceable"), 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}