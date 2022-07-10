using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.MultiType
{
    public class PolarNightReaper : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Polar Night Reaper");
            Tooltip.SetDefault("Right click for melee damage");
        }
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.damage = 66; 
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTime = 25;
			item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 2f;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("PolarNightReaper");
            item.value = Item.sellPrice(0, 6, 0, 0);
            item.rare = 4;
			item.UseSound = SoundID.Item18;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().isMultiType = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.melee = player.altFunctionUse == 2;
            item.thrown = player.altFunctionUse != 2;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, (player.altFunctionUse == 2) ? 1f : 0f, 0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightEnergizedBar", 12);
            recipe.AddIngredient(ItemID.SoulofNight, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}