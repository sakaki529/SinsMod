using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class TheDistortion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Distortion");
			Tooltip.SetDefault("Make distortion" +
                "\nIncreased mana cost by using time");
        }
		public override void SetDefaults()
		{
            item.width = 28;
			item.height = 30;
			item.damage = 180;
            item.mana = 100;
            item.magic = true;
			item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.reuseDelay = 5;
			item.useStyle = 5;
			item.useTime = 10;
			item.useAnimation = 10;
			item.shootSpeed = 30.0f;
			item.shoot = mod.ProjectileType("TheDistortion");
			item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item68;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
		public override void UseStyle(Player player)
		{
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.chargeTime++;
            if (player.channel && player.ownedProjectileCounts[mod.ProjectileType("Distortion")] <= 0)
            {
                item.mana = 0;
            }
            else if (modPlayer.chargeTime >= 180)
			{
				item.mana = 200;
			}
			else
			{
				item.mana = 100;
			}
		}
        public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfMadness", 24);
            recipe.AddIngredient(null, "DeepNightmare");
            if (SinsMod.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("YharimsCrystal"));
            }
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}