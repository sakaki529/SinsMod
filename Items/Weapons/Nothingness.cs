using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons
{
    public class Nothingness : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nothingness");
			/*Tooltip.SetDefault("I`ll send your souls back to Gaia..." +
                "\nSo..." +
                "\nLet us return to nothingness.");*/
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
        }
		public override void SetDefaults()
		{
			item.width = 45;
			item.height = 45;
            item.damage = 0;
            item.mana = 300;
            item.magic = true;
            item.autoReuse = false;
			item.useTurn = false;
            item.noMelee = true;
			item.noUseGraphic = true;
            item.channel = true;
            item.useStyle = 4;
            item.useAnimation = 30;
            item.useTime = 30;
            item.shootSpeed = 0f;
			item.shoot = mod.ProjectileType("Nothingness");
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.expert = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}