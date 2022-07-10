using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    public class SpiralBooster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral Booster");
            Tooltip.SetDefault("Count as wings" +
                "\nHorizontal speed: 14" +
                "\nAcceleration multiplier: 2" +
                "\nGood vertical speed" +
                "\nFlight time: 200" +
                "\nHold DOWN and JUMP to hover");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = Item.sellPrice(0, 18, 0, 0);
            item.rare = 10;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.Hover = true;
            player.wingTimeMax = 200;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse || player.controlJump)
            {
                player.wingFrameCounter++;
                if (player.wingFrameCounter >= 6)
                {
                    player.wingFrameCounter = 0;
                }
                player.wingFrame = 1 + player.wingFrameCounter / 2;
                for (int num = 0; num < 4; num++)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        Vector2 value = (-0.745398164f + 0.3926991f * num + 0.03f * num).ToRotationVector2() * new Vector2(-(float)player.direction * 20, 20f);
                        Dust dust = Main.dust[Dust.NewDust(player.Center, 0, 0, 229, 0f, 0f, 100, Color.White, 0.8f)];
                        dust.noGravity = true;
                        dust.position = player.Center + value;
                        dust.velocity = player.DirectionTo(dust.position) * 2f;
                        if (Main.rand.Next(10) != 0)
                        {
                            dust.customData = player;
                        }
                        else
                        {
                            dust.fadeIn = 0.5f;
                        }
                        dust.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                }
                for (int num2 = 0; num2 < 4; num2++)
                {
                    if (Main.rand.Next(8) == 0)
                    {
                        Vector2 value2 = (-0.7053982f + 0.3926991f * num2 + 0.03f * num2).ToRotationVector2() * new Vector2(player.direction * 20, 24f) + new Vector2(-player.direction * 16f, 0f);
                        Dust dust2 = Main.dust[Dust.NewDust(player.Center, 0, 0, 229, 0f, 0f, 100, Color.White, 0.5f)];
                        dust2.noGravity = true;
                        dust2.position = player.Center + value2;
                        dust2.velocity = Vector2.Normalize(dust2.position - player.Center - new Vector2(-player.direction * 16f, 0f)) * 2f;
                        dust2.position += dust2.velocity * 5f;
                        if (Main.rand.Next(10) != 0)
                        {
                            dust2.customData = this;
                        }
                        else
                        {
                            dust2.fadeIn = 0.5f;
                        }
                        dust2.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                }
            }
            if (!player.controlJump)
            {
                player.wingFrame = 0;
            }
            return true;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.45f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2f;
            constantAscend = 0.35f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 14f;
            acceleration *= 3.0f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WingsVortex);
            recipe.AddIngredient(ItemID.FragmentVortex, 14);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}