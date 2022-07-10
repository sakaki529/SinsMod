using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class Proudia : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Proudia");
        }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.damage = 115;
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 15;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item1;
        }
    }
}