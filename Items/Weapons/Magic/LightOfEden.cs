using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class LightOfEden : ModItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Light of Eden");
			Tooltip.SetDefault("Increases mana cost by using time");
        }
		public override void SetDefaults()
		{
            item.width = 26;
            item.height = 30;
            item.damage = 180;
            item.mana = 20;
            item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useStyle = 5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.shootSpeed = 30.0f;
            item.shoot = mod.ProjectileType("LightOfEden");
            item.rare = 11;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.UseSound = SoundID.Item13;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override void UseStyle(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.chargeTime++;
            if (player.channel && (modPlayer.chargeTime >= 3600 || player.ownedProjectileCounts[mod.ProjectileType("LightOfEdenLaser")] <= 0))
            {
                item.mana = 0;
            }
            else if (modPlayer.chargeTime >= 300)
            {
                item.mana = 40;
            }
            else if (modPlayer.chargeTime >= 120)
            {
                item.mana = 30;
            }
            else
            {
                item.mana = 20;
            }
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LastPrism, 1);
            recipe.AddIngredient(null, "EssenceOfOrigin", 12);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}