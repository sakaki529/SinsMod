using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Tools
{
    public class AxionTool : ModItem
    {
        public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("Axion Tool");
            Tooltip.SetDefault("Right click to change tool type");
        }
		public override void SetDefaults()
		{
            item.width = 36;
			item.height = 36;
			item.damage = 297;
			item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
			item.useStyle = 1;
			item.knockBack = 4.5f;
            item.tileBoost += 5;
            item.pick = 700;
            item.axe = 0;
            item.hammer = 0;
            item.value = Item.sellPrice(0, 75, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item66;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.altFunctionUse == 2)
            {
                item.useTime = 16;
                item.useAnimation = 16;
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                item.autoReuse = false;
                if (modPlayer.AxionToolType == 0 && modPlayer.useDelay == 0)
                {
                    modPlayer.AxionToolType = 1;
                    modPlayer.useDelay += 10;
                    item.UseSound = SoundID.Item71;
                }
                if (modPlayer.AxionToolType == 1 && modPlayer.useDelay == 0)
                {
                    modPlayer.AxionToolType = 2;
                    modPlayer.useDelay += 10;
                    item.UseSound = SoundID.Item88;
                }
                if (modPlayer.AxionToolType == 2 && modPlayer.useDelay == 0)
                {
                    modPlayer.AxionToolType = 0;
                    modPlayer.useDelay += 10;
                    item.UseSound = SoundID.Item66;
                }
            }
            else
            {
                item.autoReuse = true;
                if (modPlayer.AxionToolType == 0)
                {
                    item.pick = 700;
                    item.axe = 0;
                    item.hammer = 0;
                    item.useTime = 1;
                    item.useAnimation = 16;
                    item.UseSound = SoundID.Item66;
                }
                if (modPlayer.AxionToolType == 1)
                {
                    item.pick = 0;
                    item.axe = 700 / 5;
                    item.hammer = 0;
                    item.useTime = 3;
                    item.useAnimation = 16;
                    item.UseSound = SoundID.Item71;
                }
                if (modPlayer.AxionToolType == 2)
                {
                    item.pick = 0;
                    item.axe = 0;
                    item.hammer = 175;
                    item.useTime = 5;
                    item.useAnimation = 16;
                    item.UseSound = SoundID.Item88;
                }
            }
            return base.CanUseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(1) == 0)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 112)];
                dust.noGravity = true;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Axion", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}