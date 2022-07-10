using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class IcicleEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Icicle Edge");
        }
		public override void SetDefaults()
		{
            item.width = 36;
			item.height = 36;
			item.damage = 100;
            item.melee = true;
			item.autoReuse = true;
            item.useTime = 26; 
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 3.14f;
            item.shootSpeed = 18f;
            item.shoot = mod.ProjectileType("IcicleBeam");
			item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item1;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
            if (Main.rand.Next(3) == 0)
			{
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 92);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.4f;
                Main.dust[num].scale = 1.0f;
            }
		}
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TrueNightsEdge);
            recipe.AddIngredient(ItemID.TrueExcalibur);
            recipe.AddIngredient(ItemID.Frostbrand);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}