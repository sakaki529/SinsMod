using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Armor.Nightmare
{
    [AutoloadEquip(EquipType.Head)]
	public class NightmareHelm : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightmare Helm");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
            item.width = 26;
			item.height = 24;
			item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.defense = 62;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override bool DrawHead()
        {
            return false;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("NightmareArmor") && legs.type == mod.ItemType("NightmareLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
            player.armorEffectDrawOutlinesForbidden = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Greatly increased life regen, and increased more when your life is below 50%" +
                "\nYou will survive fatal damage and will be healed half of your max HP if an attack would have killed you";
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.setNightmare = true;
            player.thorns += 10f;
            player.lavaImmune = true;
            player.crimsonRegen = true;
            player.statLifeMax2 += 200;
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.4f;
            player.magicDamage += 0.4f;
            player.thrownDamage += 0.4f;
            player.rangedDamage += 0.4f;
            player.minionDamage += 0.4f;
            player.meleeCrit += 20;
            player.magicCrit += 20;
            player.rangedCrit += 20;
            player.thrownCrit += 20;
            player.meleeSpeed += 0.2f;
            player.statManaMax2 += 100;
            player.maxMinions += 3;
            player.maxTurrets += 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TrueMidnightHelm");
            recipe.AddIngredient(null, "EssenceOfMadness", 6);
            recipe.AddIngredient(null, "NightmareBar", 9);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}