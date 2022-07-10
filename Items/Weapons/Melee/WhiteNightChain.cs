using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class WhiteNightChain : ModItem
	{
        public override string Texture => "SinsMod/Extra/Placeholder/Placeholder";
        public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("White Night Chain");
            Tooltip.SetDefault("");
        }
		public override void SetDefaults()
		{
            item.width = 16;
            item.height = 16;
            item.damage = 80;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 0.5f;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("WhiteNightChain");
            item.rare = 8;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Item1");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0, Main.rand.Next(-120, 120) * 0.001f * player.gravDir);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PolarNightChain");
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}