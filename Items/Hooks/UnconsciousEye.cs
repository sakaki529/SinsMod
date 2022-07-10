using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Hooks
{
    public class UnconsciousEye : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Closed Third Eye");
            Tooltip.SetDefault("Count as hook");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.damage = 0;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = 5;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shootSpeed = 32f;  // how quickly the hook is shot.
            item.shoot = mod.ProjectileType("UnconsciousHook");
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool CanUseItem(Player player)
        {
            return false;
        }
    }
}