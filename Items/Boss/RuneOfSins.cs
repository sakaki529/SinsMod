using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss
{
    public class RuneOfSins : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune of Sin");
            Tooltip.SetDefault("Summons original sin" +
                "\n'You feel... your... Sins?'");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.maxStack = 20;
            item.value = 0;
            item.rare = 9;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime && !NPC.AnyNPCs(mod.NPCType("RitualSins")) && !NPC.AnyNPCs(mod.NPCType("Envy")) && !NPC.AnyNPCs(mod.NPCType("Greed")) && !NPC.AnyNPCs(mod.NPCType("Gluttony")) && !NPC.AnyNPCs(mod.NPCType("Lust")) && !NPC.AnyNPCs(mod.NPCType("Pride")) && !NPC.AnyNPCs(mod.NPCType("Sloth")) && !NPC.AnyNPCs(mod.NPCType("Wrath"));
        }
        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Item123, (int)player.position.X, (int)player.position.Y);
            Vector2 vector2 = new Vector2(player.Center.X, player.Center.Y - 268);
            Projectile.NewProjectile(vector2, Vector2.Zero, mod.ProjectileType("Where_is_my_Soul"), 0, 0, Main.myPlayer, mod.NPCType("RitualOrigin"));
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