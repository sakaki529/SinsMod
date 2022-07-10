using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class NightfallSword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Nightfall Sword");
            Tooltip.SetDefault("Shoot nightfall wave" +
                "\nRight click to shoot homing wave");
        }
		public override void SetDefaults()
		{
            item.width = 40;
			item.height = 40;
			item.damage = 380;
            item.melee = true;
			item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 24; 
			item.useAnimation = 24;
			item.knockBack = 5.0f;
            item.shootSpeed = 13f;
            item.shoot = mod.ProjectileType("NightfallWave");
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("NightfallWave") : mod.ProjectileType("NightfallWaveH");
            item.useTime = player.altFunctionUse != 2 ? 26 : 22;
            item.useAnimation = player.altFunctionUse != 2 ? 26 : 22;
            return base.CanUseItem(player);
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(BuffID.Bleeding, 180);
            target.AddBuff(BuffID.Daybreak, 180);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 180);
            target.AddBuff(BuffID.Daybreak, 180);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
            if (Main.rand.Next(3) == 0)
			{
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 174);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.4f;
                Main.dust[num].scale = 1.0f;
            }
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WhiteNightSword");
            recipe.AddIngredient(null, "IcicleEdge");
            recipe.AddIngredient(ItemID.BreakerBlade);
            recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
            recipe.AddIngredient(ItemID.FragmentSolar, 16);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}