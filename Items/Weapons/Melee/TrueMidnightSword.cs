using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class TrueMidnightSword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("True Midnight Sword");
            Tooltip.SetDefault("Shoot midnight wave" +
                "\nRight click to shoot homing wave");
        }
		public override void SetDefaults()
		{
            item.width = 40;
			item.height = 40;
			item.damage = 444;
            item.melee = true;
			item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 18;
			item.useAnimation = 18;
			item.knockBack = 5.0f;
            item.shootSpeed = 18;
            item.shoot = mod.ProjectileType("TrueMidnightWave");
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override void HoldItem(Player player)
        {
            SinsPlayer.AddGlowMask(mod.ItemType(GetType().Name), "SinsMod/Glow/Item/" + GetType().Name + "_Glow");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("TrueMidnightWave") : mod.ProjectileType("TrueMidnightWaveH");
            item.useTime = player.altFunctionUse != 2 ? 16 : 10;
            item.useAnimation = player.altFunctionUse != 2 ? 16 : 10;
            return base.CanUseItem(player);
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(BuffID.Bleeding, 300);
            target.AddBuff(mod.BuffType("AbyssalFlame"), 300);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("TrueMidnightSword"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 0);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 300);
            target.AddBuff(mod.BuffType("AbyssalFlame"), 300);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("TrueMidnightSword"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 1);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
            if (Main.rand.Next(2) == 0)
			{
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 112);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.2f;
            }
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MidnightSword");
            recipe.AddIngredient(null, "Axion", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}