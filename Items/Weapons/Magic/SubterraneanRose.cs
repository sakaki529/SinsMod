using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class SubterraneanRose : ModItem
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Subterranean Rose");
            Tooltip.SetDefault("");
            Item.staff[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.damage = 514;
            item.mana = 30;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 30;
			item.useAnimation = 30;
            item.knockBack = 1f;
            item.shootSpeed = 32;
            item.shoot = mod.ProjectileType("Thorn");
            item.value = Item.sellPrice(0, 51, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item20;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            mult = modPlayer.setUnconscious ? 0 : 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, item.type, 0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PainfulHeart", 12);
            if (SinsMod.Instance.AALoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("AAMod").ItemType("TrueTerraRose"));
            }
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}