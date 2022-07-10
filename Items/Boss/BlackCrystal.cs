using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss
{
    public class BlackCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Crystal");
            Tooltip.SetDefault("You feel madness");
        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 66;
            item.maxStack = 1;
            item.value = 0;
            item.rare = 11;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("RitualMadness")) && !NPC.AnyNPCs(mod.NPCType("BlackCrystalNoMove")) && !NPC.AnyNPCs(mod.NPCType("BlackCrystal")) && !NPC.AnyNPCs(mod.NPCType("BlackCrystalCore"));
        }
        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Item123, (int)player.position.X, (int)player.position.Y);
            Vector2 vector2 = new Vector2(player.Center.X, player.Center.Y - 268);
            Projectile.NewProjectile(vector2, Vector2.Zero, mod.ProjectileType("Where_is_my_Soul"), 0, 0, Main.myPlayer, mod.NPCType("RitualMadness"));
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RuneOfSins"));
            recipe.AddIngredient(mod.ItemType("Axion"), 10);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return SinsColor.DarknessPurple;
        }
    }
}