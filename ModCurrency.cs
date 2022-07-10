using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;

namespace SinsMod
{
    internal class RuneStoneCurrency : CustomCurrencySingleCoin
    {
        public Color RuneStoneTextColor = Colors.RarityAmber;
        public RuneStoneCurrency(int coinItemID, long currencyCap) : base(coinItemID, currencyCap)
        { }
        public override void GetPriceText(string[] lines, ref int currentLine, int price)
        {
            Color color = RuneStoneTextColor * (Main.mouseTextColor / 255f);
            lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
            {
                    color.R,
                    color.G,
                    color.B,
                    Language.GetTextValue("LegacyTooltip.50"),
                    price,
                    price > 1 ? "Rune Stones" : "Rune Stone"
            });
        }
    }
    internal class BlackCoinCurrency : CustomCurrencySingleCoin
    {
        public Color TextColor = new Color(80, 80, 80);
        public BlackCoinCurrency(int coinItemID, long currencyCap) : base(coinItemID, currencyCap)
        { }
        public override void GetPriceText(string[] lines, ref int currentLine, int price)
        {
            Color color = TextColor * (Main.mouseTextColor / 255f);
            lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
            {
                    color.R,
                    color.G,
                    color.B,
                    Language.GetTextValue("LegacyTooltip.50"),
                    price,
                    price > 1 ? "Black Coins" : "Black Coin"
            });
        }
    }
}