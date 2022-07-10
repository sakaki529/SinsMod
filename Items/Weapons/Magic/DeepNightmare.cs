using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class DeepNightmare : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Deep Nightmare");
			Tooltip.SetDefault("More and more deeply..." +
                "\nIncreases mana cost by using time");
        }
		public override void SetDefaults()
		{
            item.width = 26;
            item.height = 30;
            item.damage = 100;
            item.mana = 60;
            item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useStyle = 5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.shootSpeed = 27.0f;
            item.shoot = mod.ProjectileType("DeepNightmare");
            item.rare = 11;
            item.value = Item.sellPrice(2, 0, 0, 0);
            item.UseSound = SoundID.Item13;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void UseStyle(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.chargeTime++;
            if (player.channel && (modPlayer.chargeTime >= 3600 || player.ownedProjectileCounts[mod.ProjectileType("DeepNightmareRay")] <= 0))
            {
                item.mana = 0;
            }
            else if (modPlayer.chargeTime >= 300)
            {
                item.mana = 120;
            }
            else if (modPlayer.chargeTime >= 120)
            {
                item.mana = 90;
            }
            else
            {
                item.mana = 60;
            }
        }
        /*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfMadness", 12);
            recipe.AddIngredient(null, "NightmareBar", 12);
            recipe.AddIngredient(ItemID.LastPrism, 1);
            recipe.AddIngredient(ItemID.BlackLens, 6);
            recipe.AddIngredient(ItemID.SoulofNight, 500);
			recipe.AddIngredient(ItemID.Ectoplasm, 200);
            if (SinsMod.Instance.CalamityLoaded)
			{
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("DarksunFragment"), 80);
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("Phantoplasm"), 100);
            }
			if (SinsMod.Instance.ThoriumLoaded)
			{
			    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("OceanEssence"), 200);
			    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("DeathEssence"), 200);
			    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("InfernoEssence"), 200);
			}
            if (SinsMod.Instance.AALoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("AAMod").ItemType("Spectrum"));
            }
            if (SinsMod.Instance.SacredToolsLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("SacredTools").ItemType("HyperPrism"));
            }
            if (SinsMod.Instance.AntiarisLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("Antiaris").ItemType("PocketBlackhole"));
            }
            if (SinsMod.Instance.UltraconyxLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("Ultraconyx").ItemType("OriginPrism"));
            }
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
	}
}