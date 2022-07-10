using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class NightmareSword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Nightmare Sword");
            Tooltip.SetDefault("Shoot nightmare wave" +
                "\nRight click to shoot homing wave");
		}
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 666;
            item.melee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 15;
			item.useAnimation = 15;
            item.knockBack = 5.0f;
            item.shootSpeed = 18;
            item.shoot = mod.ProjectileType("NightmareWave");
            item.value = Item.sellPrice(0, 80, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("NightmareWave") : mod.ProjectileType("NightmareWaveH");
            item.useTime = player.altFunctionUse != 2 ? 15 : 9;
            item.useAnimation = player.altFunctionUse != 2 ? 15 : 9;
            return base.CanUseItem(player);
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(mod.BuffType("Nightmare"), 300);
            target.AddBuff(BuffID.Bleeding, 300);
            target.AddBuff(BuffID.Ichor, 300);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NightmareSword"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 0);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 300);
            target.AddBuff(BuffID.Bleeding, 300);
            target.AddBuff(BuffID.Ichor, 300);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NightmareSword"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 1);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
            if (Main.rand.Next(2) == 0)
			{
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 172);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.05f;
                Main.dust[num].scale = 1.0f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                Main.dust[num].color = new Color(255, 255, 255);
                Main.dust[num].alpha = 100;
            }
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TrueMidnightSword");
            if (SinsMod.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("Phantoplasm"), 50);
            }
            if (SinsMod.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TerrariansKnife"));
            }
            if (SinsMod.Instance.SacredToolsLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("SacredTools").ItemType("AsthralBlade"));
            }
            recipe.AddIngredient(null, "EssenceOfMadness", 8);
            recipe.AddIngredient(null, "NightmareBar", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}