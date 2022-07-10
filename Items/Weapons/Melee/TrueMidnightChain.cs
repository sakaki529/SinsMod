using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class TrueMidnightChain : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("True Midnight Chain");
            Tooltip.SetDefault("");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
		public override void SetDefaults()
		{
            item.width = 16;
            item.height = 16;
            item.damage = 444;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.autoReuse = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.knockBack = 0.5f;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("TrueMidnightChain");
            item.rare = 11;
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Item1");
            item.glowMask = customGlowMask;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0, Main.rand.Next(-120, 120) * 0.001f * player.gravDir);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MidnightChain");
            recipe.AddIngredient(null, "Axion", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}