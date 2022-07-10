using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    public class MidnightPowerOrb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Power Orb");
            Tooltip.SetDefault("Function as wings" + 
                "\nAllows flight, fast running" + 
                "\nIncreases jump height," + 
                "\nProvides the ability to walk on water and lava" + 
                "\nProvides light under water and extra mobility on ice" + 
                "\nGrants the ability to swim and greatly extends underwater breathing");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            player.wingTimeMax = 3600;
            player.accRunSpeed += 20f;
            player.moveSpeed += player.moveSpeed + 20f;
            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            player.jumpBoost = true;
            player.ignoreWater = true;
            if (SinsMod.Instance.CalamityLoaded)
            {
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ExtremeGravity")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("WeakPetrification")] = true;
            }
            if (SinsMod.Instance.FargoSoulsLoaded)
            {
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("ClippedWings")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Crippled")] = true;
            }
            if (SinsMod.Instance.TERRARIATALELoaded)
            {
                player.buffImmune[ModLoader.GetMod("TERRARIATALE").BuffType("CantFly")] = true;
            }
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.45f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3.5f;
            constantAscend = 0.35f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 15f;
            acceleration *= 2.7f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                Color color = new Color();
                float positionX;
                if (player.direction > 0)
                {
                    positionX = player.position.X + player.direction - 42;
                }
                else
                {
                    positionX = player.position.X + player.direction + 16;
                }
                Vector2 position = new Vector2(positionX, player.position.Y + 16);
                if (player.gravDir == -1f)
                {
                    position = new Vector2(positionX, player.position.Y + 8);
                }
                int dust = Dust.NewDust(position, 40, 16, 173, player.velocity.X + (player.direction * -3), player.velocity.Y, 100, color, 1.1f);
                Main.dust[dust].noGravity = true;
            }
            return base.WingUpdate(player, inUse);
        }
    }
}