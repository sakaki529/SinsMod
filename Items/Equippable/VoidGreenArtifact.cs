using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    public class VoidGreenArtifact : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Green Artifact");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 24;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.SmallNatureCore = true;
            if (player.ownedProjectileCounts[mod.ProjectileType("SmallNatureCore")] < 1 && !player.dead)
            {
                Projectile.NewProjectile(player.position.X, player.position.Y, 0f, 0f, mod.ProjectileType("SmallNatureCore"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}