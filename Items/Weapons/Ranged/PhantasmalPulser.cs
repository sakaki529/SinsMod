using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged
{
    public class PhantasmalPulser : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantasmal Pulser");
            Tooltip.SetDefault("Use Phantasmal Amalgam as ammo.");
        }
        public override void SetDefaults()
		{
            item.width = 30;
			item.height = 38;
            item.damage = 340;
            item.ranged = true;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.channel = true;
			item.useTime = 20;
			item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 0.01f;
            item.useAmmo = mod.ItemType("PhantasmalAmalgam");
            item.shoot = mod.ProjectileType("PhantasmalPulse");
			item.shootSpeed = 20f;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.rare = 10;
			item.UseSound = SoundID.Item69;
            item.GetGlobalItem<SinsItem>().CustomRarity = 12;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("PhantasmalPulser");
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Phantasm);
            recipe.AddIngredient(ItemID.PulseBow);
            recipe.AddIngredient(ItemID.Ectoplasm, 8);
            recipe.AddIngredient(mod.ItemType("EssenceOfPride"), 8);
            recipe.AddIngredient(mod.ItemType("MysticBar"), 20);
            recipe.AddTile(null, "ParticleAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}