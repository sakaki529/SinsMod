using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class MidnightSword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Midnight Sword");
            Tooltip.SetDefault("Shoot midnight wave" +
                "\nRight click to shoot homing wave");
        }
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 260;
            item.melee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 15;
			item.useAnimation = 15;
			item.knockBack = 5.0f;
            item.shoot = mod.ProjectileType("MidnightWave");
            item.shootSpeed = 18;
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 14;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("MidnightWave") : mod.ProjectileType("MidnightWaveH");
            item.useTime = player.altFunctionUse != 2 ? 16 : 10;
            item.useAnimation = player.altFunctionUse != 2 ? 16 : 10;
            return base.CanUseItem(player);
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(BuffID.Bleeding, 180);
            target.AddBuff(BuffID.ShadowFlame, 180);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("MidnightSword"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 0);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 180);
            target.AddBuff(BuffID.ShadowFlame, 180);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("MidnightSword"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 1);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
            if (Main.rand.Next(3) == 0)
			{
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 27);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.2f;
                Main.dust[num].scale = 1.0f;
                Main.dust[num].alpha = 100;
            }
		}
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightfallSword", 1);
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddIngredient(null, "EssenceOfEnvy", 12);
            recipe.AddIngredient(null, "EssenceOfGluttony", 12);
            recipe.AddIngredient(null, "EssenceOfGreed", 12);
            recipe.AddIngredient(null, "EssenceOfLust", 12);
            recipe.AddIngredient(null, "EssenceOfPride", 12);
            recipe.AddIngredient(null, "EssenceOfSloth", 12);
            recipe.AddIngredient(null, "EssenceOfWrath", 12);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}