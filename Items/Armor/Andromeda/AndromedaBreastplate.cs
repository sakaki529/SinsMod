using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Andromeda
{
    [AutoloadEquip(EquipType.Body)]
	public class AndromedaBreastplate : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Andromeda Breastplate");
            Tooltip.SetDefault("27% increased melee and thrown damage" +
                "\nEnemies are more likely to target you");
        }
        public override void SetDefaults()
		{
            item.width = 18;
			item.height = 18;
			item.rare = 10;
            item.defense = 44;
        }
        public override void UpdateEquip(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.andromedaCount += 1;
            player.aggro += 300;
            player.meleeDamage += 0.27f;
            player.thrownDamage += 0.27f;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.andromedaVisibleCount += 1;
        }
        public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            SinsPlayer modPlayer = drawPlayer.GetModPlayer<SinsPlayer>();
            Color lightColor = Color.White * 0.3333333f * modPlayer.andromedaVisibleCount;
            color = new Color(lightColor.R, lightColor.G, lightColor.B, 255) * SinsGlow.DrawOpacity(drawPlayer);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SolarFlareBreastplate);
            recipe.AddIngredient(ItemID.FragmentSolar, 20);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}