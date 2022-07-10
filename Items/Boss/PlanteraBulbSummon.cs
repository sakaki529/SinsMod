using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss
{
    public class PlanteraBulbSummon : ModItem
    {
        public override string Texture
        {
            get
            {
                return "SinsMod/Items/Boss/PlanteraBulb";
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plantera's Bulb");
            Tooltip.SetDefault("Summons plantera.");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 20;
            item.value = 500;
            item.rare = 6;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCID.Plantera);
        }
        public override bool UseItem(Player player)
        {
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("PlanteraBulbPlaceable"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}