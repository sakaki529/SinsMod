using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    public class NightmareSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightmare Soul");
            Tooltip.SetDefault("Count as wings" +
                "\nHorizontal speed: 20" +
                "\nAcceleration multiplier: 4" +
                "\nBrilliant vertical speed" +
                "\nFlight time: Infinity" +
                "\n[c/ff0000:*There is possibility you lose infinite flight when your flight time is controlled by debuffs, npcs and etc.]" +
                "\nAllows flight, fast running" +
                "\nHold DOWN and JUMP to hover" +
                "\nIncreases jump height," +
                "\nProvides the ability to walk on water and lava" +
                "\nProvides light under water and extra mobility on ice" +
                "\nGrants the ability to swim and greatly extends underwater breathing");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(1, 20, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.Hover = true;
            modPlayer.InfinityFlight = true;
            player.wingTimeMax = 32767;
            player.noFallDmg = true;
            player.accRunSpeed += 20f;
            player.runAcceleration *= 1.5f;
            player.moveSpeed += 20f;
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
            ascentWhenFalling = 1f;
            ascentWhenRising = 0.4f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 4f;
            constantAscend = 0.3f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 20f;
            acceleration *= 3f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if (player.velocity.Y != 0f)
            {
                player.wingFrame = 1;
            }
            else
            {
                player.wingFrame = 0;
            }
            if (inUse)
            {
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
                int dust = Dust.NewDust(position, 40, 16, 182, player.velocity.X + (player.direction * -3), player.velocity.Y, 100, default(Color), 0.8f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.05f;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfMadness", 12);
            recipe.AddIngredient(null, "NightmareBar", 24);
            recipe.AddIngredient(null, "MidnightPowerOrb");
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 80) * (1f - item.alpha / 255f);
        }
    }
}