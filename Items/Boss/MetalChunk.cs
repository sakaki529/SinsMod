using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss
{
    public class MetalChunk : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal Chunk");
            Tooltip.SetDefault("Summons Metal King.");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.maxStack = 20;
            item.value = 600;
            item.rare = 6;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("KingMetalSlime"));
        }
        public override bool UseItem(Player player)
        {
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("KingMetalSlime"));
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MetalNugget", 10);
            recipe.AddIngredient(ItemID.Gel, 200);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}