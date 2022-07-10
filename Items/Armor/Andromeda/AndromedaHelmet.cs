using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Andromeda
{
    [AutoloadEquip(EquipType.Head)]
	public class AndromedaHelmet : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Andromeda Helmet");
            Tooltip.SetDefault("25% increased melee and thrown critical strike chance" +
                "Enemies are more likely to target you");
        }
        public override void SetDefaults()
		{
            item.width = 18;
			item.height = 18;
			item.rare = 10;
            item.defense = 34;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("AndromedaBreastplate") && legs.type == mod.ItemType("AndromedaLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Andromeda shields generate over time protecting you," +
                "\nconsume the shield power to dash, damaging enemies";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setAndromeda = true;
            bool flag = modPlayer.andromedaDashing && player.dashDelay < 0;
            if (modPlayer.andromedaShields > 0 | flag)
            {
                modPlayer.ModDash = 2;
            }
        }
        public override void UpdateEquip(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.andromedaCount += 1;
            player.aggro += 300;
            player.meleeCrit += 25;
            player.thrownCrit += 25;
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
            recipe.AddIngredient(ItemID.SolarFlareHelmet);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}