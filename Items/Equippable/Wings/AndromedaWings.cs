using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    public class AndromedaWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Andromeda Wings");
            Tooltip.SetDefault("Count as wings" +
                "\nHorizontal speed: 12" +
                "\nAcceleration multiplier: 2.75" +
                "\nGood vertical speed" +
                "\nFlight time: 200");
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
            player.wingTimeMax = 200;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.2f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.17f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 12f;
            acceleration *= 2.75f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if(player.velocity.Y != 0)
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
                if (Main.rand.Next(2) == 0)
                {
                    int dust = Dust.NewDust(position, 30, 30, 172, 0, 0, 100, color, 1.1f);
                    Main.dust[dust].noGravity = true;
                }
            }
            return base.WingUpdate(player, inUse);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WingsSolar);
            recipe.AddIngredient(ItemID.FragmentSolar, 14);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "MoonDrip", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}