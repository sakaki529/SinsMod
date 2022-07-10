using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class DeathMirror : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hades' Hand Mirror");
            Tooltip.SetDefault("Teleports you to your last death location");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.useStyle = 4;
            item.useTime = 30;
            item.useAnimation = 30;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 9;
            //item.UseSound = SoundID.Item29;
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SinsPlayer>().FinalDeadPosition.HasValue;
        }
        public override bool UseItem(Player player)
        {
            SinsPlayer sinsPlayer = player.GetModPlayer<SinsPlayer>();
            Vector2 vector = new Vector2(Main.spawnTileX, Main.spawnTileY);
            if (sinsPlayer.FinalDeadPosition.HasValue)
            {
                vector = sinsPlayer.FinalDeadPosition.Value;
                if (player.GetModPlayer<SinsPlayer>().teleportDelay <= 0)
                {
                    player.GetModPlayer<SinsPlayer>().Teleport(vector, 3);
                    player.GetModPlayer<SinsPlayer>().teleportDelay = 30;
                }
            }
            return true;
        }
    }
}