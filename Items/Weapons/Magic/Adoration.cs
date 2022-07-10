using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class Adoration : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Adoration");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.width = 24;
            item.height = 24;
            item.damage = 44;
            item.mana = 30;
			item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.knockBack = 4;
            item.scale = 0.7f;
            item.shootSpeed = 15f;
            item.shoot = mod.ProjectileType("Adoration");
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item112;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(Main.DiscoR, 0, 0);
                }
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PinkGel, 150);
            recipe.AddIngredient(ItemID.Mug, 1);
            recipe.AddIngredient(ItemID.FlaskofVenom, 5);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}