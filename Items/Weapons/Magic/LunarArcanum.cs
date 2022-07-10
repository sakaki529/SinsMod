using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class LunarArcanum : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Lunar Arcanum");
            Tooltip.SetDefault("");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
		public override void SetDefaults()
		{
            item.width = 28;
			item.height = 26;
			item.damage = 110;
            item.mana = 40;
			item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useStyle = 5;
			item.useTime = 30;
			item.useAnimation = 30;
			item.knockBack = 8;
			item.shootSpeed = 7.5f;
            item.shoot = mod.ProjectileType("LunarArcanum");
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item117;
            item.holdStyle = 3;
            item.glowMask = customGlowMask;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<SinsPlayer>().LunarArcanumOrb = true;
            if (player.ownedProjectileCounts[mod.ProjectileType("LunarArcanumOrb")] < 1 && !player.dead)
            {
                Projectile.NewProjectile(player.position.X, player.position.Y, 0f, 0f, mod.ProjectileType("LunarArcanumOrb"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NebulaArcanum);
            recipe.AddIngredient(ItemID.FragmentNebula, 8);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}