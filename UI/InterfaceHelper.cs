using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.UI;

namespace SinsMod.UI
{
    //from Bluemagic
    public static class InterfaceHelper
    {
        private static FieldInfo mHInfo;
        private static FieldInfo _itemIconCacheTimeInfo;
        public static void Initialize()
        {
            mHInfo = typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static);
            _itemIconCacheTimeInfo = typeof(Main).GetField("_itemIconCacheTime", BindingFlags.NonPublic | BindingFlags.Static);
        }
        public static int GetMH()
        {
            return (int)mHInfo.GetValue(null);
        }
        public static void SetMH(int height)
        {
            mHInfo.SetValue(null, height);
        }
        public static void HideItemIconCache()
        {
            _itemIconCacheTimeInfo.SetValue(null, 0);
        }
        public static void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (SinsMod.ClientConfig.AccessorySlotPositionFix)
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    if (layers[i].Name == "Vanilla: Inventory")
                    {
                        layers.Insert(i, new LegacyGameInterfaceLayer("SinsMod: Accessory Slot Fix", FixAccessorySlots, InterfaceScaleType.None));
                        i++;
                    }
                }
            }
        }
        private static bool FixAccessorySlots()
        {
            Player player = Main.player[Main.myPlayer];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (Main.mapEnabled && !Main.mapFullscreen && Main.mapStyle == 1 && modPlayer.ExtraAccessory2 && modPlayer.ExtraAccessorySlotsCount >= 2 && !modPlayer.BluemagicExAcc)
            {
                SetMH(GetMH() - 50/* * (modPlayer.ExtraAccessorySlotsCount -1)*/);
            }
            return true;
        }
    }
}