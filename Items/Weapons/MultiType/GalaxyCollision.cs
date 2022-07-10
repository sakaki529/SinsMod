using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.MultiType
{
    public class GalaxyCollision : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Galaxy Collision");
            Tooltip.SetDefault("Right click for thrown damage");
        }
		public override void SetDefaults()
		{
            item.width = 30;
            item.height = 30;
            item.damage = 232; 
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useTime = 16;
			item.useAnimation = 16;
            item.useStyle = 1;
            item.knockBack = 8f;
            item.shootSpeed = 15f;
            item.shoot = mod.ProjectileType("GalaxyCollision");
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().isMultiType = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.melee = player.altFunctionUse != 2;
            item.thrown = player.altFunctionUse == 2;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 0f, 0f);
            Main.projectile[num].melee = item.melee;
            Main.projectile[num].thrown = item.thrown;
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DayBreak, 2);
            recipe.AddIngredient(ItemID.FragmentSolar, 8);
            if (SinsMod.Instance.SacredToolsLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("SacredTools").ItemType("FragmentNova"), 8);
            }
            if (SinsMod.Instance.TremorLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("Tremor").ItemType("NovaFragment"), 8);
            }
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}