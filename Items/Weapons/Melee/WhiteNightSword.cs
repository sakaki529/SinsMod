using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class WhiteNightSword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("White Night Sword");
            Tooltip.SetDefault("Shoot white night wave" +
                "\nRight click to shoot homing wave");
        }
		public override void SetDefaults()
		{
            item.width = 40;
			item.height = 40;
			item.damage = 145;
            item.melee = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 29;
			item.useAnimation = 29;
			item.knockBack = 3.0f;
            item.shootSpeed = 12;
            item.shoot = mod.ProjectileType("WhiteNightWave");
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("WhiteNightWave") : mod.ProjectileType("WhiteNightWaveH");
            item.useTime = player.altFunctionUse != 2 ? 29 : 25;
            item.useAnimation = player.altFunctionUse != 2 ? 29 : 25;
            return base.CanUseItem(player);
        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
            if (Main.rand.Next(3) == 0)
			{
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 247);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.2f;
                Main.dust[num].scale = 0.7f;
            }
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PolarNightSword");
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}