using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.MilkyWay
{
    [AutoloadEquip(EquipType.Head)]
	public class MilkyWayHelmet : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Milky Way Helmet");
            Tooltip.SetDefault("Increases your max number of minions" +
                "Increases minion damage by 26%");
        }
        public override void SetDefaults()
		{
            item.width = 18;
			item.height = 18;
			item.rare = 10;
            item.defense = 16;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Lighting.AddLight(item.position, 0.2f, 0.2f, 0.2f);
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MilkyWayPlate") && legs.type == mod.ItemType("MilkyWayLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "" +
                "\n";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setMilkyWay = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 2;
            player.minionDamage += 0.26f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StardustHelmet);
            recipe.AddIngredient(ItemID.FragmentStardust, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}