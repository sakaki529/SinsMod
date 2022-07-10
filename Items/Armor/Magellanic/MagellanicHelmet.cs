using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Magellanic
{
    [AutoloadEquip(EquipType.Head)]
    public class MagellanicHelmet : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magellanic Helmet");
            Tooltip.SetDefault("Increases maximum mana by 80 and reduces mana usage by 18%" +
                "\n9% increased magic damage and critical strike chance");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = 10;
            item.defense = 20;
            item.glowMask = customGlowMask;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MagellanicBreastplate") && legs.type == mod.ItemType("MagellanicLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("ArmorSetBonus.Nebula") +
                "\n";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setMagellanic = true;
            if (player.nebulaCD > 0)
            {
                player.nebulaCD--;
            }
            player.setNebula = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 80;
            player.manaCost -= 0.15f;
            player.magicDamage += 0.9f;
            player.magicCrit += 9;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NebulaHelmet);
            recipe.AddIngredient(ItemID.FragmentNebula, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}