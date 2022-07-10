using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class StickyRainStone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sticky Tlaloc's Eye");
            Tooltip.SetDefault("Left click to start/stop the rain." + 
                "\nRight click to start/stop slime rain.");
        }
		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 28;
            item.autoReuse = true;
            item.useStyle = 4;
            item.useTime = 60;
            item.useAnimation = 60;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 9;
			item.UseSound = SoundID.Item44;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                if (Main.raining)
                {
                    Main.rainTime = 0;
                    Main.raining = false;
                    Main.maxRaining = 0f;
                }
                else
                {
                    Main.StopSlimeRain(true);
                    int num = 86400;
                    int num2 = num / 24;
                    Main.rainTime = Main.rand.Next(num2 * 8, num);
                    if (Main.rand.Next(3) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num2);
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num2 * 2);
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num2 * 2);
                    }
                    if (Main.rand.Next(6) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num2 * 3);
                    }
                    if (Main.rand.Next(7) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num2 * 4);
                    }
                    if (Main.rand.Next(8) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num2 * 5);
                    }
                    float num3 = 1f;
                    if (Main.rand.Next(2) == 0)
                    {
                        num3 += 0.05f;
                    }
                    if (Main.rand.Next(3) == 0)
                    {
                        num3 += 0.1f;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        num3 += 0.15f;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        num3 += 0.2f;
                    }
                    Main.rainTime = (int)(Main.rainTime * num3);
                    ChangeRain();
                    Main.raining = true;
                }
                string key = "Mods.SinsMod.Rain";
                string text = Main.raining ? "started" : "stopped";
                if (Main.netMode != 2)
                {
                    Main.NewText(Language.GetTextValue(key, text), new Color(100, 255, 100));
                }
                else
                {
                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), new Color(100, 255, 100));
                }
            }
            else
            {
                if (Main.slimeRain)
                {
                    Main.StopSlimeRain(true);
                }
                else
                {
                    Main.StartSlimeRain(true);
                }
                string key = "Mods.SinsMod.SlimeRain";
                string text = Main.slimeRain ? "started" : "stopped";
                if (Main.netMode != 2)
                {
                    Main.NewText(Language.GetTextValue(key, text), new Color(100, 255, 100));
                }
                else
                {
                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), new Color(100, 255, 100));
                }
            }
            return true;
        }
        private static void ChangeRain()
        {
            if (Main.cloudBGActive >= 1f || Main.numClouds > 150.0)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Main.maxRaining = Main.rand.Next(20, 90) * 0.01f;
                    return;
                }
                Main.maxRaining = Main.rand.Next(40, 90) * 0.01f;
                return;
            }
            else if (Main.numClouds > 100.0)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Main.maxRaining = Main.rand.Next(10, 70) * 0.01f;
                    return;
                }
                Main.maxRaining = Main.rand.Next(20, 60) * 0.01f;
                return;
            }
            else
            {
                if (Main.rand.Next(3) == 0)
                {
                    Main.maxRaining = Main.rand.Next(5, 40) * 0.01f;
                    return;
                }
                Main.maxRaining = Main.rand.Next(5, 30) * 0.01f;
                return;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RainStone");
            recipe.AddIngredient(ItemID.SlimeStaff);
            recipe.AddIngredient(ItemID.SlimeCrown, 3);
            recipe.AddIngredient(ItemID.Gel, 300);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}