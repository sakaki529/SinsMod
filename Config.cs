using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace SinsMod
{
    [Label("Config")]
    public class ClientConfiguration : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public override void OnLoaded() => SinsMod.ClientConfig = this;

        [DefaultValue(true)]
        [Label("Fix accessory slot position")]
        [Tooltip("Acc slot position will be fixed when you have seven or more acc slots" +
            "\nDoes not work when acc slot increased by Elemental Unleash (the other mod)")]
        //[ReloadRequired]
        public bool AccessorySlotPositionFix { get; set; }

        [DefaultValue(true)]
        [Label("Show rune texture")]
        [Tooltip("Show rune texture on background when specific boss is active")]
        public bool DrawBackgroundRune { get; set; }
    }
    /*
    public static class Config
    {
        public static bool AccessorySlotPositionFix = true;
        private static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "SinsModConfig.json");
        private static Preferences Configuration = new Preferences(ConfigPath, false, false);
        public static void Load()
        {
            if (!ReadConfig())
            {
                SetDefaults();
                ErrorLogger.Log("SinsMod: Failed to read config file");
                CreateConfig();
                ReadConfig();
            }
        }
        public static void SetDefaults()
        {
            AccessorySlotPositionFix = true;//Require reload to switch
        }
        public static bool ReadConfig()
        {
            if (Configuration.Load())
            {
                Configuration.Get<bool>("AccessorySlotPositionFix", ref AccessorySlotPositionFix);
                return true;
            }
            return false;
        }
        static void CreateConfig()
        {
            Configuration.Clear();
            Configuration.Put("AccessorySlotPositionFix", AccessorySlotPositionFix);
            Configuration.Save(true);
        }
    }
    */
}