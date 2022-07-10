using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
	public class PolarNightSword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Polar Night Sword");
            Tooltip.SetDefault("Shoot polar night wave");
        }
		public override void SetDefaults()
		{
            item.width = 32;
			item.height = 32;
			item.damage = 66;
            item.melee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 30;
			item.useAnimation = 30; 
			item.knockBack = 3.0f;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("PolarNightWave");
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 4;
			item.UseSound = SoundID.Item1;
            item.scale = 1.1f;
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(BuffID.Bleeding, 120);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 120);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
            if (Main.rand.Next(3) == 0)
			{
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 186);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.4f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
		}
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.NightsEdge);
            recipe.AddIngredient(null, "NightEnergizedBar", 12);
            recipe.AddIngredient(ItemID.SoulofNight, 6);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}