using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss
{
    public class ForbiddenFruit : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Experiment item");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 32;
            item.maxStack = 20;
            item.value = 0;
            item.rare = 11;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool CanUseItem(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Item2, (int)player.position.X, (int)player.position.Y);
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("TartarosHead"));
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}