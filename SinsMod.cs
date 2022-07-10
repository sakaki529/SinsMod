using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using SinsMod.Skies;
using SinsMod.UI;
using SinsMod.Items.Misc;
using SinsMod.Items.Dyes;
using SinsMod.Items.Materials;

namespace SinsMod
{
    class SinsMod : Mod
    {
        internal static SinsMod Instance;
        internal static ClientConfiguration ClientConfig;
        public static Effect SinsEffect;
        public static int BlackCoinCurrencyID;
        public static int RuneStoneCurrencyID;
        //List
        internal static IList<string> DevList;
        public static List<int> swordList;
        //public static IList<string> DonaterList;
        //public static IList<string> ContributerList;
        public static List<int> AutoReusableItemList;
        // HotKey
        public static ModHotKey AllDebuffImmunity;
        public static ModHotKey NightmareToolDestructMode;
        // Load
        internal bool SinsMusicLoaded;
        internal bool AntisocialLoaded;
        internal bool CheatSheetLoaded;
        internal bool HEROsLoaded;
        internal bool AchievementLoaded;
        internal bool BossAssistLoaded;
        internal bool BossHealthBarLoaded;
        internal bool BossListLoaded;
        internal bool CensusLoaded;
        internal bool LargeWorldEnablerLoaded;
        internal bool MinionCritLoaded;
        internal bool AALoaded;
        internal bool AlchemistNPCLoaded;
        internal bool AntiarisLoaded;
        internal bool BluemagicLoaded;
        internal bool CalamityLoaded;
        internal bool ChaoticUprisingLoaded;
        internal bool DBZLoaded;
        internal bool EALoaded;
        internal bool FargoLoaded;
        internal bool FargoSoulsLoaded;
        internal bool GRealmLoaded;
        internal bool LaugicalityLoaded;
        internal bool PinkyLoaded;
        internal bool PumpkingLoaded;
        internal bool RedemptionLoaded;
        internal bool SacredToolsLoaded;
        internal bool SpiritLoaded;
        internal bool TERRARIATALELoaded;
        internal bool ThoriumLoaded;
        internal bool TremorLoaded;
        internal bool UltraconyxLoaded;
        //Misc
        public static int shakeIntensity;
        private static int shakeTick;

        public SinsMod()
        {
            Instance = this;
            Properties = new ModProperties()
            {
            	Autoload = true,
            	AutoloadGores = true,
            	AutoloadSounds = true,
            	AutoloadBackgrounds = true
            };
        }
        public override void Load()
        {
            InterfaceHelper.Initialize();
            BlackCoinCurrencyID = CustomCurrencyManager.RegisterCurrency(new BlackCoinCurrency(ModContent.ItemType<BlackCoin>(), 999L));
            RuneStoneCurrencyID = CustomCurrencyManager.RegisterCurrency(new RuneStoneCurrency(ModContent.ItemType<RuneStone>(), 999L));
            if (!Main.dedServ)
            {
                AddEquipTexture(null, EquipType.Legs, "CleyeraRobe_Legs", "SinsMod/Extra/Placeholder/BlankTex");
                GameShaders.Misc["SinsMod:DeathAnimation"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/ExampleEffectDeath")), "DeathAnimation").UseImage("Images/Misc/Perlin");
                Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/Shockwave"));
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();
                Ref<Effect> @ref = new Ref<Effect>();
                SinsEffect = GetEffect("Effects/ExampleEffectDeath");
                @ref.Value = SinsEffect;
                GameShaders.Armor.BindShader(ItemType("GrayscaleDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorVortex")).UseImage("SinsMod/Backgrounds/CHRONOS").UseColor(1f, 1f, 1f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
                GameShaders.Armor.BindShader(ItemType("InvisibleDye"), new ArmorShaderData(@ref, "DeathAnimation").UseOpacity(-1f));
                GameShaders.Armor.BindShader(ItemType("TechnoDyeLightblue"), new ArmorShaderData(Main.PixelShaderRef, "ArmorVortex")).UseImage("Images/Misc/noise").UseColor(0.1f, 0.1f, 0.1f).UseSecondaryColor(0.5f, 100, 100).UseSaturation(1f);
                GameShaders.Armor.BindShader(ItemType("TechnoDyeRed"), new ArmorShaderData(Main.PixelShaderRef, "ArmorVortex")).UseImage("Images/Misc/noise").UseColor(0.1f, 0.1f, 0.1f).UseSecondaryColor(90, 0.2f, 0.2f).UseSaturation(1f);
                
                /*Filters.Scene["SinsMod:Envy"] = new Filter(new EnvyScreenShaderData("FilterMiniTower").UseColor(Color.Black).UseOpacity(1.0f), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Envy"] = new EnvySky();*/
                Filters.Scene["SinsMod:Envy"] = new Filter(new EnvyScreenShaderData("FilterMiniTower").UseColor(SinsColor.Envy_F).UseOpacity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Envy"] = new EnvySky();
                Filters.Scene["SinsMod:Gluttony"] = new Filter(new GluttonyScreenShaderData("FilterMiniTower").UseColor(SinsColor.Gluttony_F).UseOpacity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Gluttony"] = new GluttonySky();
                Filters.Scene["SinsMod:Greed"] = new Filter(new GreedScreenShaderData("FilterMiniTower").UseColor(SinsColor.Greed_F).UseOpacity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Greed"] = new GreedSky();
                Filters.Scene["SinsMod:Lust"] = new Filter(new LustScreenShaderData("FilterMiniTower").UseColor(SinsColor.Lust_F).UseOpacity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Lust"] = new LustSky();
                Filters.Scene["SinsMod:Pride"] = new Filter(new PrideScreenShaderData("FilterMiniTower").UseColor(SinsColor.Pride_F).UseOpacity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Pride"] = new PrideSky();
                Filters.Scene["SinsMod:Sloth"] = new Filter(new SlothScreenShaderData("FilterMiniTower").UseColor(SinsColor.Sloth_F).UseOpacity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Sloth"] = new SlothSky();
                Filters.Scene["SinsMod:Wrath"] = new Filter(new WrathScreenShaderData("FilterMiniTower").UseColor(SinsColor.Wrath_F).UseOpacity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Wrath"] = new WrathSky();
                Filters.Scene["SinsMod:Sins"] = new Filter(new SinsScreenShaderData("FilterMiniTower").UseColor(SinsColor.Origin_F).UseOpacity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Sins"] = new SinsSky();
                Filters.Scene["SinsMod:Nothingness"] = new Filter(new NothingnessScreenShaderData("FilterMoonLord"), EffectPriority.VeryHigh);
                SkyManager.Instance["SinsMod:Nothingness"] = new NothingnessSky();
                //Biome
                Filters.Scene["SinsMod:Mystic"] = new Filter(new MysticScreenShaderData("FilterMoonLord"), EffectPriority.High);
                SkyManager.Instance["SinsMod:Mystic"] = new MysticSky();
                Filters.Scene["SinsMod:Distortion"] = new Filter(new DistortionScreenShaderData("FilterMoonLord"), EffectPriority.High);
                SkyManager.Instance["SinsMod:Distortion"] = new DistortionSky();
            }
            AllDebuffImmunity = RegisterHotKey("All Debuff Immunity toggle", "I");
            NightmareToolDestructMode = RegisterHotKey("Nightmare Tool: Destruct Mode", "V");
            SetupLists();
            //ConfigClient.Load();
            #region message
            ModTranslation text = CreateTranslation("Free");
            text.SetDefault("{0}");
            AddTranslation(text);
            text = CreateTranslation("MeteorLanded");
            text.SetDefault("A {0} meteor has landed!");
            AddTranslation(text);
            text = CreateTranslation("NightEnergyGen");
            text.SetDefault("A nightenergy cluster has generated.");
            AddTranslation(text);
            text = CreateTranslation("LimitCut");
            text.SetDefault("Limit cut {0}");
            AddTranslation(text);
            text = CreateTranslation("HopelessMode");
            text.SetDefault("Hopeless mode {0}");
            AddTranslation(text);
            text = CreateTranslation("ExpertMode");
            text.SetDefault("Expert mode {0}");
            AddTranslation(text);
            text = CreateTranslation("Time");
            text.SetDefault("{0} comes on.");
            AddTranslation(text);
            text = CreateTranslation("Rain");
            text.SetDefault("Rain has been {0}");
            AddTranslation(text);
            text = CreateTranslation("SlimeRain");
            text.SetDefault("Slime rain has been {0}");
            AddTranslation(text);
            text = CreateTranslation("Sandstorm");
            text.SetDefault("A sand storm has been {0}");
            AddTranslation(text);
            text = CreateTranslation("BloodMoon");
            text.SetDefault("The Blood Moon {0}");
            AddTranslation(text);
            text = CreateTranslation("Eclipse");
            text.SetDefault("A solar eclipse {0}");
            AddTranslation(text);
            #region dummyCommands
            text = CreateTranslation("DefenceCommand");
            text.SetDefault("Sets instant dummy's defence to {0}.");
            AddTranslation(text);
            text = CreateTranslation("DamegeMultiplierCommand");
            text.SetDefault("Sets instant dummy's damege multiplier to {0}f({1}%)");
            AddTranslation(text);
            text = CreateTranslation("BuffImmunityCommand");
            text.SetDefault("{0} instant dummy's buff immunity");
            AddTranslation(text);
            text = CreateTranslation("ScaleCommand");
            text.SetDefault("Sets instant dummy's scale to {0}f({1}%)");
            AddTranslation(text);
            text = CreateTranslation("ResetCommand");
            text.SetDefault("Resets instant dummy's status");
            AddTranslation(text);
            #endregion
            #endregion
        }
        public override void Unload()
        {
            Instance = null;
            SinsNPC.InstantDummyDefence = 0;
            SinsNPC.InstantDummyDamegeMultiplier = 1.0f;
            SinsNPC.InstantDummyBuffImmunity = false;
            SinsNPC.InstantDummyScale = 1.0f;
            DevList = null;
            swordList = null;
            ClientConfig = null;
            SinsEffect = null;
            AllDebuffImmunity = null;
            NightmareToolDestructMode = null;
        }
        internal static void SetupLists()
        {
            if (Instance != null)
            {
                DevList = new List<string>
                {
                    "Cleyera","sakaki529","Не"
                };
                AutoReusableItemList = new List<int>
                {
                    ItemID.PhoenixBlaster,
                    ItemID.NightsEdge,
                    ItemID.TrueExcalibur,
                    ItemID.TrueNightsEdge,
                    ItemID.BeamSword,
                    ItemID.ChlorophyteClaymore,
                    ItemID.VenusMagnum,
                    ItemID.IceSickle,
                    ItemID.PaladinsHammer,
                    ItemID.ChristmasTreeSword,
                    ItemID.OnyxBlaster,
                    ItemID.MonkStaffT2
                };
            }
        }
        public override void PostSetupContent()
        {
            try
            {
                SinsMusicLoaded = ModLoader.GetMod("SinsModMusic") != null;
                AntisocialLoaded = ModLoader.GetMod("Antisocial") != null;
                CheatSheetLoaded = ModLoader.GetMod("CheatSheet") != null;
                HEROsLoaded = ModLoader.GetMod("HEROsMod") != null;
                AchievementLoaded = ModLoader.GetMod("AchievementLibs") != null;
                BossAssistLoaded = ModLoader.GetMod("BossAssist") != null;
                BossHealthBarLoaded = ModLoader.GetMod("FKBossHealthBar") != null;
                BossListLoaded = ModLoader.GetMod("BossChecklist") != null;
                CensusLoaded = ModLoader.GetMod("Census") != null;
                LargeWorldEnablerLoaded = ModLoader.GetMod("LargeWorldEnabler") != null;
                MinionCritLoaded = ModLoader.GetMod("dotSKN_MinionCriticalHits") != null;
                AALoaded = ModLoader.GetMod("AAMod") != null;
                AlchemistNPCLoaded = ModLoader.GetMod("AlchemistNPC") != null;
                AntiarisLoaded = ModLoader.GetMod("Antiaris") != null;
                BluemagicLoaded = ModLoader.GetMod("Bluemagic") != null;
                CalamityLoaded = ModLoader.GetMod("CalamityMod") != null;
                ChaoticUprisingLoaded = ModLoader.GetMod("ChaoticUprising") != null;
                DBZLoaded = ModLoader.GetMod("DBZMOD") != null;
                EALoaded = ModLoader.GetMod("ElementsAwoken") != null;
                FargoLoaded = ModLoader.GetMod("Fargowiltas") != null;
                FargoSoulsLoaded = ModLoader.GetMod("FargowiltasSouls") != null;
                GRealmLoaded = ModLoader.GetMod("GRealm") != null;
                LaugicalityLoaded = ModLoader.GetMod("Laugicality") != null;
                PinkyLoaded = ModLoader.GetMod("pinkymod") != null;
                PumpkingLoaded = ModLoader.GetMod("Pumpking") != null;
                RedemptionLoaded = ModLoader.GetMod("Redemption") != null;
                SacredToolsLoaded = ModLoader.GetMod("SacredTools") != null;
                SpiritLoaded = ModLoader.GetMod("SpiritMod") != null;
                TERRARIATALELoaded = ModLoader.GetMod("TERRARIATALE") != null;
                ThoriumLoaded = ModLoader.GetMod("ThoriumMod") != null;
                TremorLoaded = ModLoader.GetMod("Tremor") != null;
                UltraconyxLoaded = ModLoader.GetMod("Ultraconyx") != null;
            }
            catch (Exception error)
            {
                throw new Exception("SinsMod: PostSetupContent Error: " + error.StackTrace + error.Message);
                //ErrorLogger.Log("SinsMod: PostSetupContent Error:" + error.StackTrace + error.Message);
            }
            Mod bossAssist = ModLoader.GetMod("BossAssist");
            Mod bossHealthBar = ModLoader.GetMod("FKBossHealthBar");
            Mod bossList = ModLoader.GetMod("BossChecklist");
            Mod census = ModLoader.GetMod("Census");
            Mod achievement = ModLoader.GetMod("AchievementLibs");
            if (bossAssist != null)
            {
                #region Greed
                List<int> GreedBossCollection = new List<int>()
                {
                    ItemType("DesertKingMask"),
                    ItemType("DesertKingTrophy"),
                    ItemID.MusicBoxBoss1
                };
                List<int> GreedBossLoot = new List<int>()
                {
                    ItemType("GreedBag"),
                    ItemType("EssenceOfGreed")
                };
                bossAssist.Call("AddStatPage", 2.1f, NPCType("Greed"), Name, "Sin of Greed", (Func<bool>)(() => SinsWorld.downedGreed), ItemType("RuneOfGreed"), GreedBossCollection, GreedBossLoot, GetTexture("NPCs/Boss/Sins/Greed/DesertKing"));
                #endregion
                #region KingMetalSlime
                List<int> KingMetalSlimeBossCollection = new List<int>()
                {
                    ItemType("KingMetalSlimeMask"),
                    ItemType("KingMetalSlimeTrophy"),
                    ItemID.MusicBoxBoss3
                };
                List<int> KingMetalSlimeBossLoot = new List<int>()
                {
                    ItemType("KingMetalSlimeBag"),
                    ItemID.Gel,
                    ItemID.IronBar,
                    ItemID.HallowedBar,
                    ItemID.SoulofFright,
                    ItemID.SoulofMight,
                    ItemID.SoulofSight,
                    ItemID.MetalDetector
                };
                bossAssist.Call("AddStatPage", 9.1f, NPCType("KingMetalSlime"), Name, "Metal King", (Func<bool>)(() => SinsWorld.downedKingMetalSlime), ItemType("MetalChunk"), KingMetalSlimeBossCollection, KingMetalSlimeBossLoot, GetTexture("BossAssist/KingMetalSlime"));
                #endregion
                #region LunarEye
                List<int> LunarEyeBossCollection = new List<int>()
                {
                    ItemType("LunarEyeMask"),
                    ItemType("LunarEyeTrophy"),
                    ItemID.MusicBoxTowers
                };
                List<int> LunarEyeBossLoot = new List<int>()
                {
                    ItemType("LunarEyeBag"),
                    ItemType("TheTrueEyeOfCthulhu"),
                    ItemType("MoonDrip"),
                    ItemID.FragmentVortex,
                    ItemID.FragmentNebula,
                    ItemID.FragmentSolar,
                    ItemID.FragmentStardust
                };
                if (NPC.downedMoonlord)
                {
                    LunarEyeBossLoot.Add(ItemID.LunarOre);
                }
                bossAssist.Call("AddStatPage", 13.999f, NPCType("LunarEye"), Name, "True Eye of Cthulhu", (Func<bool>)(() => SinsWorld.downedLunarEye), ItemType("SuspiciousGlowingEye"), LunarEyeBossCollection, LunarEyeBossLoot, GetTexture("BossAssist/LunarEye"));
                #endregion
                #region Envy
                List<int> EnvyBossCollection = new List<int>()
                {
                    ItemType("EnvyMask"),
                    ItemType("EnvyTrophy"),
                    ItemID.MusicBoxBoss1
                };
                List<int> EnvyBossLoot = new List<int>()
                {
                    ItemType("EnvyBag"),
                    //ItemType("GoldRush"),
                    ItemType("EssenceOfEnvy")
                };
                bossAssist.Call("AddStatPage", 14.11f, NPCType("Envy"), Name, "Sin of Envy", (Func<bool>)(() => SinsWorld.downedEnvy), ItemType("RuneOfEnvy"), EnvyBossCollection, EnvyBossLoot, GetTexture("NPCs/BossAssist/Sins/Leviathan"));
                #endregion
                #region Gluttony
                List<int> GluttonyBossCollection = new List<int>()
                {
                    ItemType("GluttonyMask"),
                    ItemType("GluttonyTrophy"),
                    ItemID.MusicBoxBoss1
                };
                List<int> GluttonyBossLoot = new List<int>()
                {
                    ItemType("GluttonyBag"),
                    //ItemType("GoldRush"),
                    ItemType("EssenceOfGluttony")
                };
                bossAssist.Call("AddStatPage", 14.12f, NPCType("Gluttony"), Name, "Sin of Gluttony", (Func<bool>)(() => SinsWorld.downedGreed), ItemType("RuneOfGluttony"), GluttonyBossCollection, GluttonyBossLoot, GetTexture("NPCs/Boss/Sins/Gluttony/Gluttony"));
                #endregion
                #region Lust
                List<int> LustBossCollection = new List<int>()
                {
                    ItemType("LustMask"),
                    ItemType("LustTrophy"),
                    ItemID.MusicBoxBoss1
                };
                List<int> LustBossLoot = new List<int>()
                {
                    ItemType("LustBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfLust")
                };
                bossAssist.Call("AddStatPage", 14.14f, NPCType("Lust"), Name, "Sin of Lust", (Func<bool>)(() => SinsWorld.downedLust), ItemType("RuneOfLust"), LustBossCollection, LustBossLoot, GetTexture("BossAssist/Asmodeus"));
                #endregion
                #region Pride
                List<int> PrideBossCollection = new List<int>()
                {
                    ItemType("PrideMask"),
                    ItemType("PrideTrophy"),
                    ItemID.MusicBoxBoss1
                };
                List<int> PrideBossLoot = new List<int>()
                {
                    ItemType("PrideBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfPride"),
                    ItemType("Proudia")
                };
                bossAssist.Call("AddStatPage", 14.15f, NPCType("Pride"), Name, "Sin of Pride", (Func<bool>)(() => SinsWorld.downedSloth), ItemType("RuneOfPride"), PrideBossCollection, PrideBossLoot, GetTexture("NPCs/Boss/Sins/Pride/Pride"));
                #endregion
                #region Sloth
                List<int> SlothBossCollection = new List<int>()
                {
                    ItemType("SlothMask"),
                    ItemType("SlothTrophy"),
                    ItemID.MusicBoxBoss1
                };
                List<int> SlothBossLoot = new List<int>()
                {
                    ItemType("SlothBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfSloth")
                };
                bossAssist.Call("AddStatPage", 14.16f, NPCType("Sloth"), Name, "Sin of Sloth", (Func<bool>)(() => SinsWorld.downedSloth), ItemType("RuneOfSloth"), SlothBossCollection, SlothBossLoot, GetTexture("NPCs/Boss/Sins/Sloth/Sloth"));
                #endregion
                #region Wrath
                List<int> WrathBossCollection = new List<int>()
                {
                    ItemType("WrathMask"),
                    ItemType("EyeOfSatanTrophy"),
                    ItemID.MusicBoxBoss1
                };
                List<int> WrathBossLoot = new List<int>()
                {
                    ItemType("WrathBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfWrath")
                };
                bossAssist.Call("AddStatPage", 14.17f, NPCType("Wrath"), Name, "Sin of Wrath", (Func<bool>)(() => SinsWorld.downedWrath), ItemType("RuneOfWrath"), WrathBossCollection, WrathBossLoot, GetTexture("BossAssist/EyeOfSatan"));
                #endregion
                #region Origin
                List<int> OriginBossCollection = new List<int>()
                {
                    ItemType("OriginMask"),
                    ItemType("OriginTrophy"),
                    ItemID.MusicBoxLunarBoss
                };
                List<int> OriginBossLoot = new List<int>()
                {
                    ItemType("OriginBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfOrigin"),
                    ItemType("GardenOfEden")
                };
                bossAssist.Call("AddStatPage", 14.18f, NPCType("Eden"), Name, "Original Sin", (Func<bool>)(() => SinsWorld.downedOrigin), ItemType("RuneOfSins"), OriginBossCollection, OriginBossLoot, GetTexture("BossAssist/Origin"));
                #endregion
                #region Tartarus
                List<int> TartarusBossCollection = new List<int>()
                {
                    ItemType("TartarusMask"),
                    ItemType("TartarusTrophy"),
                    ItemID.MusicBoxBoss3
                };
                List<int> TartarusBossLoot = new List<int>()
                {
                    ItemType("TartarusBag"),
                    ItemType("AbyssalFlameRelic"),
                    ItemType("Axion"),
                    ItemType("TartarusWhip"),
                    ItemType("AbyssalFlamethrower"),
                    ItemType("AbyssalStaff"),
                    ItemType("AbyssalGuardianStaff"),
                };
                bossAssist.Call("AddStatPage", 14.54f, NPCType("TartarusHead"), Name, "The Guardian of Tartarus", (Func<bool>)(() => SinsWorld.downedTartarus), ItemType("TartarusMeat"), TartarusBossCollection, TartarusBossLoot, GetTexture("BossAssist/Tartarus"));
                #endregion
                #region Madness
                List<int> MadnessBossCollection = new List<int>()
                {
                    ItemType("BlackCrystalMask"),
                    ItemType("BlackCrystalTrophy"),
                    ItemID.MusicBoxBoss2
                };
                List<int> MadnessBossLoot = new List<int>()
                {
                    ItemType("MadnessBag"),
                    ItemType("OmegaStone"),
                    ItemType("VoidGreenArtifact"),
                    ItemType("EssenceOfMadness"),
                    ItemType("BlackCrystalStaff"),
                    ItemType("BlackCoreStaff"),
                    ItemType("WhiteCoreStaff"),
                };
                bossAssist.Call("AddStatPage", 14.55f, NPCType("BlackCrystalCore"), Name, "Sin of Madness", (Func<bool>)(() => SinsWorld.downedMadness), ItemType("BlackCrystal"), MadnessBossCollection, MadnessBossLoot, GetTexture("NPCs/Boss/Madness/BlackCrystal"));
                #endregion
            }
            if (bossHealthBar != null)
            {
                #region KingMetalSlime
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", bossHealthBar.GetTexture("UI/MechBarStart"), bossHealthBar.GetTexture("UI/MechBarMiddle"), bossHealthBar.GetTexture("UI/MechBarEnd"), null);
                bossHealthBar.Call("hbSetMidBarOffset", -14, 2);
                bossHealthBar.Call("hbSetBossHeadCentre", 30, 28);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("KingMetalSlime"));
                #endregion
                #region LunarEye
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", bossHealthBar.GetTexture("UI/MoonLordBarStart"), null, bossHealthBar.GetTexture("UI/MoonLordBarEnd"), null);
                bossHealthBar.Call("hbSetTextureExpert", bossHealthBar.GetTexture("UI/MoonLordBarStart_Exp"), null, bossHealthBar.GetTexture("UI/MoonLordBarEnd_Exp"), null);
                bossHealthBar.Call("hbSetColours", new Color(0f, 1f, 0.6f), new Color(0.8f, 1f, 0f), new Color(1f, 0f, 0f));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("LunarEye"));
                #endregion
                #region Envy
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/EnvyBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Envy, SinsColor.Envy, SinsColor.Envy);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("LeviathanHead"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/EnvyBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Envy, SinsColor.Envy, SinsColor.Envy);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Envy"));
                #endregion
                #region Gluttony
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/GluttonyBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Gluttony, SinsColor.Gluttony, SinsColor.Gluttony);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Gluttony"));
                #endregion
                #region Greed
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/GreedBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Greed, SinsColor.Greed, SinsColor.Greed);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("DesertKing"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/GreedBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Greed, SinsColor.Greed, SinsColor.Greed);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Greed"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbForceSmall", true);
                bossHealthBar.Call("hbSetTextureSmall", GetTexture("HealthBars/UniqueBarStartSmall"), GetTexture("HealthBars/UniqueBarBodySmall"), GetTexture("HealthBars/UniqueBarEndSmall"), null);
                bossHealthBar.Call("hbSetColours", SinsColor.Greed, SinsColor.Greed, SinsColor.Greed);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("DesertKingHand"));
                #endregion
                #region Lust
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/LustBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Lust, SinsColor.Lust, SinsColor.Lust);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Asmodeus"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/LustBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Lust, SinsColor.Lust, SinsColor.Lust);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Lust"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbForceSmall", true);
                bossHealthBar.Call("hbSetTextureSmall", GetTexture("HealthBars/UniqueBarStartSmall"), GetTexture("HealthBars/UniqueBarBodySmall"), GetTexture("HealthBars/UniqueBarEndSmall"), null);
                bossHealthBar.Call("hbSetColours", SinsColor.Lust, SinsColor.Lust, SinsColor.Lust);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("AsmodeusSerpentHead"));
                #endregion
                #region Pride
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/PrideBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Pride, SinsColor.Pride, SinsColor.Pride);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Pride"));
                #endregion
                #region Sloth
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/SlothBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Sloth, SinsColor.Sloth, SinsColor.Sloth);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Sloth"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbForceSmall", true);
                bossHealthBar.Call("hbSetTextureSmall", GetTexture("HealthBars/UniqueBarStartSmall"), GetTexture("HealthBars/UniqueBarBodySmall"), GetTexture("HealthBars/UniqueBarEndSmall"), null);
                bossHealthBar.Call("hbSetColours", SinsColor.Sloth, SinsColor.Sloth, SinsColor.Sloth);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Sphere"));
                #endregion
                #region Wrath
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/WrathBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Wrath, SinsColor.Wrath, SinsColor.Wrath);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("EyeOfSatan"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/WrathBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Wrath, SinsColor.Wrath, SinsColor.Wrath);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Wrath"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbForceSmall", true);
                bossHealthBar.Call("hbSetTextureSmall", GetTexture("HealthBars/UniqueBarStartSmall"), GetTexture("HealthBars/UniqueBarBodySmall"), GetTexture("HealthBars/UniqueBarEndSmall"), null);
                bossHealthBar.Call("hbSetColours", SinsColor.Wrath, SinsColor.Wrath, SinsColor.Wrath);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("SatansServant"));
                #endregion
                #region Origin
                /*bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/SinsBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.MediumWhite, SinsColor.MediumWhite, SinsColor.MediumWhite);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Eve"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/SinsBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.MediumBlack, SinsColor.MediumBlack, SinsColor.MediumBlack);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Adam"));*/

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/SinsBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.Origin, SinsColor.Origin, SinsColor.Origin);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("Eden"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/SinsBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.MediumWhite, SinsColor.MediumWhite, SinsColor.MediumWhite);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("OriginWhite"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/SinsBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.MediumBlack, SinsColor.MediumBlack, SinsColor.MediumBlack);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("OriginBlack"));
                #endregion
                #region Tartarus
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/UniqueBarStart"), GetTexture("HealthBars/UniqueBarBody"), GetTexture("HealthBars/UniqueBarEnd"), GetTexture("HealthBars/UniqueHealthBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.UniqueSkyBlue, SinsColor.UniqueSkyBlue, SinsColor.UniqueSkyBlue);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 14);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("TartarusHead"));
                #endregion
                #region Madness
                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/BCBarStart"), GetTexture("HealthBars/BCBarBody"), GetTexture("HealthBars/BCBarEnd"), GetTexture("HealthBars/BCBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.MediumBlack, SinsColor.MediumBlack, SinsColor.MediumBlack);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 10);
                bossHealthBar.Call("hbSetBossHeadCentre", 54, 32);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("BlackCrystalNoMove"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/BCBarStart"), GetTexture("HealthBars/BCBarBody"), GetTexture("HealthBars/BCBarEnd"), GetTexture("HealthBars/BCBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.MediumBlack, SinsColor.MediumBlack, SinsColor.MediumBlack);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 10);
                bossHealthBar.Call("hbSetBossHeadCentre", 54, 32);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/Madness/BlackCrystalSmall"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("BlackCrystal"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbForceSmall", true);
                bossHealthBar.Call("hbSetTextureSmall", GetTexture("HealthBars/UniqueBarStartSmall"), GetTexture("HealthBars/UniqueBarBodySmall"), GetTexture("HealthBars/UniqueBarEndSmall"), null);
                bossHealthBar.Call("hbSetColours", SinsColor.MediumBlack, SinsColor.MediumBlack, SinsColor.MediumBlack);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("BlackCrystalSmall"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbSetTexture", GetTexture("HealthBars/BCCBarStart"), GetTexture("HealthBars/BCCBarBody"), GetTexture("HealthBars/BCCBarEnd"), GetTexture("HealthBars/SinsBarFill"));
                bossHealthBar.Call("hbSetColours", SinsColor.MediumBlack, SinsColor.MediumBlack, SinsColor.MediumBlack);
                bossHealthBar.Call("hbSetMidBarOffset", -30, 10);
                bossHealthBar.Call("hbSetBossHeadCentre", 48, 30);
                bossHealthBar.Call("hbSetFillDecoOffsetSmall", 16);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("BlackCrystalCore"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbForceSmall", true);
                bossHealthBar.Call("hbSetTextureSmall", GetTexture("HealthBars/BCCCBarStart"), GetTexture("HealthBars/BCCCBarBody"), GetTexture("HealthBars/BCCCBarEnd"), null);
                bossHealthBar.Call("hbSetColours", SinsColor.MediumWhite, SinsColor.MediumWhite, SinsColor.MediumWhite);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("BlackCrystalCoreClone"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbForceSmall", true);
                bossHealthBar.Call("hbSetTextureSmall", GetTexture("HealthBars/BCCSABarStart"), GetTexture("HealthBars/BCCSABarBody"), GetTexture("HealthBars/BCCSABarEnd"), null);
                bossHealthBar.Call("hbSetColours", SinsColor.VoidBlue, SinsColor.VoidBlue, SinsColor.VoidBlue);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("BCCSummonAttack"));

                bossHealthBar.Call("hbStart");
                bossHealthBar.Call("hbForceSmall", true);
                bossHealthBar.Call("hbSetTextureSmall", GetTexture("HealthBars/BCCSHBarStart"), GetTexture("HealthBars/BCCSHBarBody"), GetTexture("HealthBars/BCCSHBarEnd"), null);
                bossHealthBar.Call("hbSetColours", SinsColor.VoidGreen, SinsColor.VoidGreen, SinsColor.VoidGreen);
                bossHealthBar.Call("hbSetBossHeadTexture", GetTexture("Extra/Placeholder/BlankTex"));
                bossHealthBar.Call("hbFinishSingle", Instance.NPCType("BCCSummonHeal"));
                #endregion
            }
            if (bossList != null)
            {
                //SlimeKing = 1f;
                //EyeOfCthulhu = 2f;
                //EaterOfWorlds = 3f;
                //QueenBee = 4f;
                //Skeletron = 5f;
                //WallOfFlesh = 6f;
                //TheTwins = 7f;
                //TheDestroyer = 8f;
                //SkeletronPrime = 9f;
                //Plantera = 10f;
                //Golem = 11f;
                //DukeFishron = 12f;
                //LunaticCultist = 13f;
                //Moonlord = 14f;
                #region OldCaller
                /*bossList.Call("AddBossWithInfo", "Sin of Greed", 2.1f, (Func<bool>)(() => SinsWorld.downedGreed), string.Format("Use a [i:{0}] in the Desert at night", ItemType("RuneOfGreed")));
                bossList.Call("AddBossWithInfo", "Metal King", 9.1f, (Func<bool>)(() => SinsWorld.downedKingMetalSlime), string.Format("Use a [i:{0}]", ItemType("MetalChunk")));
                bossList.Call("AddBossWithInfo", "True Eye of Cthulhu", 13.999f, (Func<bool>)(() => SinsWorld.downedLunarEye), string.Format("Use a [i:{0}]", ItemType("SuspiciousGlowingEye")));
                bossList.Call("AddBossWithInfo", "Leviathan", 14.11f, (Func<bool>)(() => SinsWorld.downedEnvy), string.Format("Use a [i:{0}] at night", ItemType("RuneOfEnvy")));
                bossList.Call("AddBossWithInfo", "Sin of Gluttony", 14.12f, (Func<bool>)(() => SinsWorld.downedGluttony), string.Format("Use a [i:{0}] at night", ItemType("RuneOfGluttony")));
                bossList.Call("AddBossWithInfo", "Asmodeus", 14.14f, (Func<bool>)(() => SinsWorld.downedLust), string.Format("Use a [i:{0}] at night", ItemType("RuneOfLust")));
                bossList.Call("AddBossWithInfo", "Sin of Pride", 14.15f, (Func<bool>)(() => SinsWorld.downedPride), string.Format("Use a [i:{0}] at night", ItemType("RuneOfPride")));
                bossList.Call("AddBossWithInfo", "Sin of Sloth", 14.16f, (Func<bool>)(() => SinsWorld.downedSloth), string.Format("Use a [i:{0}] at night", ItemType("RuneOfSloth")));
                bossList.Call("AddBossWithInfo", "Sin of Wrath", 14.17f, (Func<bool>)(() => SinsWorld.downedWrath), string.Format("Use a [i:{0}] at night", ItemType("RuneOfWrath")));
                bossList.Call("AddBossWithInfo", "Adam and Eve", 14.18f, (Func<bool>)(() => SinsWorld.downedOrigin), string.Format("Use a [i:{0}] at night", ItemType("RuneOfSins")));
                bossList.Call("AddBossWithInfo", "The Guardian of Tartarus", 14.54f, (Func<bool>)(() => SinsWorld.downedTartarus), string.Format("Use a [i:{0}] in the Underworld", ItemType("KeyOfTartarus")));
                bossList.Call("AddBossWithInfo", "Sin of Madness", 14.55f, (Func<bool>)(() => SinsWorld.downedMadness), string.Format("Use a [i:{0}]", ItemType("BlackCrystal")));
                bossList.Call("AddBossWithInfo", "Eibon", 14.6f, (Func<bool>)(() => SinsWorld.downed), string.Format("Use a [i:{0}]", ItemType("")));
                bossList.Call("AddBossWithInfo", "Memory of X", 14.5f, (Func<bool>)(() => SinsWorld.downedX), string.Format("Use a [i:{0}]", ItemType("")));*/
                #endregion
                #region Greed
                List<int> GreedBossCollection = new List<int>()
                {
                    ItemType("DesertKingTrophy"),
                    ItemType("DesertKingMask"),
                    ItemID.MusicBoxBoss1
                };
                List<int> GreedBossLoot = new List<int>()
                {
                    ItemType("GreedBag"),
                    ItemType("EssenceOfGreed")
                };
                bossList.Call("AddBoss", 2.1f, NPCType("Greed"), this, "Sin of Greed", (Func<bool>)(() => SinsWorld.downedGreed), ItemType("RuneOfGreed"), GreedBossCollection, GreedBossLoot, "Use a [i:" + ItemType("RuneOfGreed") + "] in the Desert at night", "", "SinsMod/NPCs/Boss/Sins/Greed/DesertKing");
                #endregion
                #region KingMetalSlime
                List<int> KingMetalSlimeBossCollection = new List<int>()
                {
                    ItemType("KingMetalSlimeTrophy"),
                    ItemType("KingMetalSlimeMask"),
                    ItemID.MusicBoxBoss3
                };
                List<int> KingMetalSlimeBossLoot = new List<int>()
                {
                    ItemType("KingMetalSlimeBag"),
                    ItemID.Gel,
                    ItemID.IronBar,
                    ItemID.HallowedBar,
                    ItemID.SoulofFright,
                    ItemID.SoulofMight,
                    ItemID.SoulofSight,
                    ItemID.MetalDetector
                };
                bossList.Call("AddBoss", 9.1f, NPCType("KingMetalSlime"), this, "Metal King", (Func<bool>)(() => SinsWorld.downedKingMetalSlime), ItemType("MetalChunk"), KingMetalSlimeBossCollection, KingMetalSlimeBossLoot, "Use a [i:" + ItemType("MetalChunk") + "]", "", "SinsMod/BossAssist/KingMetalSlime");
                #endregion
                #region LunarEye
                List<int> LunarEyeBossCollection = new List<int>()
                {
                    ItemType("LunarEyeTrophy"),
                    ItemType("LunarEyeMask"),
                    ItemID.MusicBoxTowers
                };
                List<int> LunarEyeBossLoot = new List<int>()
                {
                    ItemType("LunarEyeBag"),
                    ItemType("TheTrueEyeOfCthulhu"),
                    ItemType("MoonDrip"),
                    ItemID.FragmentVortex,
                    ItemID.FragmentNebula,
                    ItemID.FragmentSolar,
                    ItemID.FragmentStardust
                };
                if (NPC.downedMoonlord)
                {
                    LunarEyeBossLoot.Add(ItemID.LunarOre);
                }
                bossList.Call("AddBoss", 13.999f, NPCType("LunarEye"), this, "True Eye of Cthulhu", (Func<bool>)(() => SinsWorld.downedLunarEye), ItemType("SuspiciousGlowingEye"), LunarEyeBossCollection, LunarEyeBossLoot, "Use a [i:" + ItemType("SuspiciousGlowingEye") + "]", "", "SinsMod/BossAssist/LunarEye");
                #endregion
                #region Envy
                List<int> EnvyBossCollection = new List<int>()
                {
                    ItemType("EnvyTrophy"),
                    ItemType("EnvyMask"),
                    ItemID.MusicBoxBoss1
                };
                List<int> EnvyBossLoot = new List<int>()
                {
                    ItemType("EnvyBag"),
                    //ItemType("GoldRush"),
                    ItemType("EssenceOfEnvy")
                };
                bossList.Call("AddBoss", 14.11f, NPCType("Envy"), this, "Sin of Envy", (Func<bool>)(() => SinsWorld.downedEnvy), ItemType("RuneOfEnvy"), EnvyBossCollection, EnvyBossLoot, "Use a [i:" + ItemType("RuneOfEnvy") + "] at night", "", "SinsMod/BossAssist/Leviathan");
                #endregion
                #region Gluttony
                List<int> GluttonyBossCollection = new List<int>()
                {
                    ItemType("GluttonyTrophy"),
                    ItemType("GluttonyMask"),
                    ItemID.MusicBoxBoss1
                };
                List<int> GluttonyBossLoot = new List<int>()
                {
                    ItemType("GluttonyBag"),
                    ItemType("EssenceOfGluttony")
                };
                bossList.Call("AddBoss", 14.12f, NPCType("Gluttony"), this, "Sin of Gluttony", (Func<bool>)(() => SinsWorld.downedGreed), ItemType("RuneOfGluttony"), GluttonyBossCollection, GluttonyBossLoot, "Use a [i:" + ItemType("RuneOfGluttony") + "] at night", "", "SinsMod/NPCs/Boss/Sins/Gluttony/Gluttony");
                #endregion
                #region Lust
                List<int> LustBossCollection = new List<int>()
                {
                    ItemType("LustTrophy"),
                    ItemType("LustMask"),
                    ItemID.MusicBoxBoss1
                };
                List<int> LustBossLoot = new List<int>()
                {
                    ItemType("LustBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfLust")
                };
                bossList.Call("AddBoss", 14.14f, NPCType("Lust"), this, "Sin of Lust", (Func<bool>)(() => SinsWorld.downedLust), ItemType("RuneOfLust"), LustBossCollection, LustBossLoot, "Use a [i:" + ItemType("RuneOfLust") + "] at night", "", "SinsMod/BossAssist/Asmodeus");
                #endregion
                #region Pride
                List<int> PrideBossCollection = new List<int>()
                {
                    ItemType("PrideTrophy"),
                    ItemType("PrideMask"),
                    ItemID.MusicBoxBoss1
                };
                List<int> PrideBossLoot = new List<int>()
                {
                    ItemType("PrideBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfPride"),
                    ItemType("Proudia")
                };
                bossList.Call("AddBoss", 14.15f, NPCType("Pride"), this, "Sin of Pride", (Func<bool>)(() => SinsWorld.downedSloth), ItemType("RuneOfPride"), PrideBossCollection, PrideBossLoot, "Use a [i:" + ItemType("RuneOfPride") + "] at night", "", "SinsMod/NPCs/Boss/Sins/Pride/Pride");
                #endregion
                #region Sloth
                List<int> SlothBossCollection = new List<int>()
                {
                    ItemType("SlothTrophy"),
                    ItemType("SlothMask"),
                    ItemID.MusicBoxBoss1
                };
                List<int> SlothBossLoot = new List<int>()
                {
                    ItemType("SlothBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfSloth")
                };
                bossList.Call("AddBoss", 14.16f, NPCType("Sloth"), this, "Sin of Sloth", (Func<bool>)(() => SinsWorld.downedSloth), ItemType("RuneOfSloth"), SlothBossCollection, SlothBossLoot, "Use a [i:" + ItemType("RuneOfSloth") + "] at night", "", GetTexture("NPCs/Boss/Sins/Sloth/Sloth"));
                #endregion
                #region Wrath
                List<int> WrathBossCollection = new List<int>()
                {
                    ItemType("EyeOfSatanTrophy"),
                    ItemType("WrathMask"),
                    ItemID.MusicBoxBoss1
                };
                List<int> WrathBossLoot = new List<int>()
                {
                    ItemType("WrathBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfWrath")
                };
                bossList.Call("AddBoss", 14.17f, NPCType("Wrath"), this, "Sin of Wrath", (Func<bool>)(() => SinsWorld.downedWrath), ItemType("RuneOfWrath"), WrathBossCollection, WrathBossLoot, "Use a [i:" + ItemType("RuneOfWrath") + "] at night", "", "SinsMod/BossAssist/EyeOfSatan");
                #endregion
                #region Origin
                List<int> OriginBossCollection = new List<int>()
                {
                    ItemType("OriginTrophy"),
                    ItemType("OriginMask"),
                    ItemID.MusicBoxLunarBoss
                };
                List<int> OriginBossLoot = new List<int>()
                {
                    ItemType("OriginBag"),
                    //ItemType("BossExpertItem"),
                    ItemType("EssenceOfOrigin"),
                    ItemType("GardenOfEden")
                };
                bossList.Call("AddBoss", 14.18f, NPCType("Eden"), this, "Original Sin", (Func<bool>)(() => SinsWorld.downedOrigin), ItemType("RuneOfSins"), OriginBossCollection, OriginBossLoot, "Use a [i:" + ItemType("RuneOfSins") + "] at night", "", "SinsMod/BossAssist/Origin");
                #endregion
                #region Tartarus
                List<int> TartarusBossCollection = new List<int>()
                {
                    ItemType("TartarusTrophy"),
                    ItemType("TartarusMask"),
                    ItemID.MusicBoxBoss3
                };
                List<int> TartarusBossLoot = new List<int>()
                {
                    ItemType("TartarusBag"),
                    ItemType("AbyssalFlameRelic"),
                    ItemType("Axion"),
                    ItemType("TartarusWhip"),
                    ItemType("AbyssalFlamethrower"),
                    ItemType("AbyssalStaff"),
                    ItemType("AbyssalGuardianStaff"),
                };
                bossList.Call("AddBoss", 14.54f, NPCType("TartarusHead"), this, "The Guardian of Tartarus", (Func<bool>)(() => SinsWorld.downedTartarus), ItemType("TartarusMeat"), TartarusBossCollection, TartarusBossLoot, "Use a [i:" + ItemType("TartarusMeat") + "] in the Tartarus", "", "SinsMod/BossAssist/Tartarus");
                #endregion
                #region Madness
                List<int> MadnessBossCollection = new List<int>()
                {
                    ItemType("BlackCrystalTrophy"),
                    ItemType("BlackCrystalMask"),
                    ItemID.MusicBoxBoss2
                };
                List<int> MadnessBossLoot = new List<int>()
                {
                    ItemType("MadnessBag"),
                    ItemType("OmegaStone"),
                    ItemType("VoidGreenArtifact"),
                    ItemType("EssenceOfMadness"),
                    ItemType("BlackCrystalStaff"),
                    ItemType("BlackCoreStaff"),
                    ItemType("WhiteCoreStaff"),
                };
                bossList.Call("AddBoss", 14.55f, NPCType("BlackCrystalCore"), this, "Sin of Madness", (Func<bool>)(() => SinsWorld.downedMadness), ItemType("BlackCrystal"), MadnessBossCollection, MadnessBossLoot, "Use a [i:" + ItemType("BlackCrystal") + "]", "", "SinsMod/NPCs/Boss/Madness/BlackCrystal");
                #endregion
            }
            if (census != null)
            {
                //Census.Call("TownNPCCondition", NPCType("PumpkinHead"), "Defeat a pumpking");
                census.Call("TownNPCCondition", NPCType("RuneStoneDealer"), "Have [i:" + ItemType("RuneStone") + "] in your inventory");
            }
            if (achievement != null)
            {
                //achievement.Call("AddAchievementWithoutReward", Instance, "You Will Know Our Names", "Defeat all Unique Monsters", Instance.GetTexture("Extra/Placeholder/BlankTex"), SinsWorld.downedAllUnique);
                //achievement.Call("AddAchievementWithoutReward", Instance, "Seven Deadly Sins", "Unleash all Sins", Instance.GetTexture("Extra/Placeholder/BlankTex"), SinsWorld.downedSins);
                //achievement.Call("AddAchievementWithoutReward", Instance, "The Confession", "Redeem your sins", Instance.GetTexture("Extra/Placeholder/BlankTex"), SinsWorld.downedOrigin);
                //achievement.Call("AddAchievementWithoutReward", Instance, "The End of Madness", "Defeat final boss", Instance.GetTexture("Extra/Placeholder/BlankTex"), SinsWorld.downedMadness);
            }
        }
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            if (!Main.gameMenu)
            {
                shakeTick++;
                if (shakeIntensity >= 0 && shakeTick >= 12)
                {
                    shakeIntensity--;
                }
                if (shakeIntensity > 10)
                {
                    shakeIntensity = 10;
                }
                if (shakeIntensity < 0)
                {
                    shakeIntensity = 0;
                }
                if (!Main.gamePaused && Main.hasFocus)
                {
                    Main.screenPosition += new Vector2(shakeIntensity * Main.rand.NextFloatDirection() / 2f, shakeIntensity * Main.rand.NextFloatDirection() / 2f);
                }
            }
            else
            {
                shakeIntensity = 0;
                shakeTick = 0;
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            InterfaceHelper.ModifyInterfaceLayers(layers);
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
            {
                return;
            }
            if (Main.musicVolume != 0f && Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                SinsPlayer sinsPlayer = Main.LocalPlayer.GetModPlayer<SinsPlayer>();
                Mod mod = ModLoader.GetMod("SinsMod");
                if (SinsMusicLoaded)
                {
                    mod = ModLoader.GetMod("SinsModMusic");
                }
                /*if (NPC.AnyNPCs(NPCID.Frog))
                {
                    if (SinsMusicLoaded)
                    {
                        music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/");
                    }
                    else
                    {
                        music = MusicID.Boss1;
                    }
                    priority = MusicPriority.BossMedium;
                }*/
                if (sinsPlayer.ZoneNightEnergy)
                {
                    if (SinsMusicLoaded)
                    {
                       /* string type = "Sounds/Music/RoOW1-9";
                        music = mod.GetSoundSlot(SoundType.Music, type);*/
                        music = MusicID.Eerie;
                    }
                    else
                    {
                        music = MusicID.Eerie;
                    }
                    priority = MusicPriority.BiomeHigh;
                }
                if (sinsPlayer.ZoneMystic)
                {
                    if (SinsMusicLoaded)
                    {
                        string type = "Sounds/Music/RoOW1-9";
                        /*type = "Sounds/Music/RoOW10-29";
                        type = "Sounds/Music/RoOW30-";*/
                        music = mod.GetSoundSlot(SoundType.Music, type);
                    }
                    else if (Main.dayTime)
                    {
                        //music = MusicID.Ice;
                    }
                    priority = MusicPriority.BiomeHigh;
                }
                if (sinsPlayer.ZoneDistortion)
                {
                    if (SinsMusicLoaded)
                    {
                        music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/CA");
                    }
                    else if (Main.dayTime)
                    {
                        //music = MusicID.Snow;
                    }
                    priority = MusicPriority.BiomeHigh;
                }
                if (sinsPlayer.ZoneTartarus)
                {
                    if (SinsMusicLoaded)
                    {
                        music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/P");
                    }
                    else
                    {
                        music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/P");
                    }
                    priority = MusicPriority.Environment;
                }
            }
        }
        public override void AddRecipeGroups()
        {
            List<int> Pickaxe = new List<int>();
            List<int> Axe = new List<int>();
            List<int> Hammer = new List<int>();
            List<int> Sword = new List<int>();
            for (int i = 0; i < Main.itemTexture.Length; i++)
            {
                Item item = new Item();
                item.SetDefaults(i, false);
                if (item.pick > 0)
                {
                    Pickaxe.Add(item.type);
                }
                if (item.axe > 0)
                {
                    Axe.Add(item.type);
                }
                if (item.hammer > 0)
                {
                    Hammer.Add(item.type);
                }
                if (item.melee && (item.useStyle == 1 || item.useStyle == 3) && item.damage > 0 && !item.noMelee && !item.noUseGraphic && !item.channel)
                {
                    Sword.Add(item.type);
                }
                swordList = Sword;
            }
            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType("MysticWood"));
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType("DistortionWood"));
            }
            RecipeGroup group = new RecipeGroup(() => "Copper or Tin Bars", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("SinsMod:AnyCopperBar", group);
            group = new RecipeGroup(() => "Iron or Lead Bars", new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("SinsMod:AnyIronBar", group);
            group = new RecipeGroup(() => "Silver or Tungsten Bars", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("SinsMod:AnySilverBar", group);
            group = new RecipeGroup(() => "Gold or Platinum Bars", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("SinsMod:AnyGoldBar", group);
            group = new RecipeGroup(() => "Demonite or Crimtane Bars", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("SinsMod:AnyDemoniteBar", group);
            group = new RecipeGroup(() => "Cobalt or Paladium Bars", new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup.RegisterGroup("SinsMod:AnyCobaltBar", group);
            group = new RecipeGroup(() => "Mythril or Orihalcum Bars", new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup.RegisterGroup("SinsMod:AnyMythrilBar", group);
            group = new RecipeGroup(() => "Adamantite or Titanium Bars", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup("SinsMod:AnyAdamantiteBar", group);
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " string", new int[]
            {
                ItemID.WhiteString,
                ItemID.BlackString,
                ItemID.BlueString,
                ItemID.BrownString,
                ItemID.CyanString,
                ItemID.GreenString,
                ItemID.LimeString,
                ItemID.OrangeString,
                ItemID.PinkString,
                ItemID.PurpleString,
                ItemID.RainbowString,
                ItemID.RedString,
                ItemID.SkyBlueString,
                ItemID.TealString,
                ItemID.VioletString,
                ItemID.YellowString
            });
            RecipeGroup.RegisterGroup("SinsMod:AnyString", group);

            /*group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " pickaxe", Pickaxe.ToArray());
            RecipeGroup.RegisterGroup("SinsMod:AnyPickaxe", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " axe", Axe.ToArray());
            RecipeGroup.RegisterGroup("SinsMod:AnyAxe", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " hammer", Hammer.ToArray());
            RecipeGroup.RegisterGroup("SinsMod:AnyHammer", group);*/
        }
        public override void AddRecipes()
        {
            ModRecipe recipe;
            #region ore
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.TinOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.CopperOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.CopperOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.TinOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.LeadOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.IronOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.IronOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.LeadOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.TungstenOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.SilverOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SilverOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.TungstenOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.PlatinumOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.GoldOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.GoldOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.PlatinumOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.CrimtaneOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.DemoniteOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.DemoniteOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.CrimtaneOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.PalladiumOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.CobaltOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.CobaltOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.PalladiumOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.OrichalcumOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.MythrilOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.MythrilOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.OrichalcumOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.TitaniumOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.AdamantiteOre);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.AdamantiteOre);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.TitaniumOre);
            recipe.AddRecipe();
            #endregion
            #region bar
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.TinBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.CopperBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.CopperBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.TinBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.LeadBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.IronBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.IronBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.LeadBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.TungstenBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.SilverBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SilverBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.TungstenBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.PlatinumBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.GoldBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.GoldBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.PlatinumBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.CrimtaneBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.DemoniteBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.DemoniteBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.CrimtaneBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.PalladiumBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.CobaltBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.CobaltBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.PalladiumBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.OrichalcumBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.MythrilBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.MythrilBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.OrichalcumBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.TitaniumBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.AdamantiteBar);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.AdamantiteBar);
            recipe.AddTile(TileID.Extractinator);
            recipe.SetResult(ItemID.TitaniumBar);
            recipe.AddRecipe();
            #endregion
            #region weapon
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Gel, 50);
            recipe.AddRecipeGroup("Wood", 24);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.SlimeStaff);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("SinsMod:AnyGoldBar", 8);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.SunplateBlock, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.Starfury);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "TerraWheel");
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(ItemID.Terrarian);
            recipe.AddRecipe();
            #endregion
            #region acc
            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("SinsMod:AnyCobaltBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.CobaltShield);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Cloud, 50);
            recipe.AddIngredient(ItemID.Gel, 50);
            recipe.AddRecipeGroup("SinsMod:AnyString", 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.ShinyRedBalloon);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "ThrowerEmblem");
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.AvengerEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Cobweb, 30);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(ItemID.WhiteString);
            recipe.AddRecipe();
            #endregion
            #region misc
            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "BlackCoin");
            recipe.SetResult(ItemID.PlatinumCoin, 100);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ChaosElementalBanner, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.RodofDiscord);
            recipe.AddRecipe();
            #endregion
        }
    }
}