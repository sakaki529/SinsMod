using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class LastNemesis : ModItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Last Nemesis");
			Tooltip.SetDefault("");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }
		public override void SetDefaults()
		{
            item.width = 26;
            item.height = 30;
            item.damage = 100;
            item.mana = 16;
            item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useStyle = 5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.shootSpeed = 30.0f;
            item.shoot = mod.ProjectileType("LastNemesis");
            item.rare = 10;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = SoundID.Item13;
        }
        public override void UseStyle(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.chargeTime++;
            if (player.channel && (modPlayer.chargeTime >= 3600 || player.ownedProjectileCounts[mod.ProjectileType("LastNemesisLaser")] <= 0))
            {
                item.mana = 0;
            }
            else
            {
                item.mana = 12;
            }
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LastPrism);
            recipe.AddIngredient(ItemID.FragmentNebula, 8);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}