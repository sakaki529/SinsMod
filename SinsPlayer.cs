using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Bluemagic;
using Laugicality;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.Graphics.Capture;
//using ThoriumMod;

namespace SinsMod
{
    public class SinsPlayer : ModPlayer
    {
        //Mod
        private readonly Mod Bluemagic = ModLoader.GetMod("Bluemagic");
        private readonly Mod Laugicality = ModLoader.GetMod("Laugicality");
        private readonly Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");
        //Name
        internal bool Debug;
        internal bool Dev;
        internal bool Cleyera;
        //Stat
        public int chargeTime;
        public int dashTime;
        public int delInstantDummyTime;
        public int mountDelay;
        public int nothingnessTime;
        public int shakeTime;
        public bool teleporting;
        public float teleportTime;
        public int teleportStyle;
        public int teleportDelay;
        public int useDelay;
        public float stealth = 1f;//func like player.stealth
        public float stealthStat = 1f;
        public float stealthEffect;//be used for draw
        private static Point16[] lineTopLeft = new Point16[20];
        //cooldown
        public int lifeElixirCooldown;
        public int nullificationPotionCooldown;
        public int reviveCooldown;
        //SpecialAbility

        //HotKey
        public bool AllDebuffImmunity;
        public bool NightmareToolDestructMode;
        //Biome
        public bool ZoneNightEnergy;
        public bool ZoneMystic;
        public bool ZoneDistortion;
        public bool ZoneTartarus;
        public bool ZoneMadness;
        //Item
        public int xBlasterType;
        public int subterraneanCount;
        public int AxionToolType;
        public int NightmareToolType;
        public int spiralTime;
        public bool FOOLS;
        public int FOOLScounter;
        public bool LunarArcanumOrb;
        public Vector2? FinalDeadPosition;
        public static bool DelInstantDummy;
        //DeadlySinsItem
        public bool SinsThanksItem;
        public bool EnvyItem;
        public bool GluttonyItem;
        public bool GreedItem;
        public bool LustItem;
        public bool PrideItem;
        public bool SlothItem;
        public bool WrathItem;
        public bool OriginItem;
        public bool MadnessItem;
        //SinsBossEffect
        public static bool EnvyBoss;
        public static bool GluttonyBoss;
        public static bool GreedBoss;
        public static bool LustBoss;
        public static bool PrideBoss;
        public static bool SlothBoss;
        public static bool WrathBoss;
        public static bool OriginBoss;
        public static bool MadnessBoss;
        //Buff
        //pet
        public bool CleyeraPet;
        public bool CleyeraLightPet;
        public bool KobyPet;
        //minion
        public bool LunaticMinion;
        public bool MoonlightMinion;
        public bool NightfallMinion;
        public bool NightmareProbeMinion;
        public bool PhantasmDragonMinion;
        public bool PolarNightRavenMinion;
        public bool SmallBlackCoreMinion;
        public bool TartarusMinion;
        public bool WhiteNightFairyMinion;
        //buff
        public bool WeaponImbueLifeDrain;
        //debuff
        public bool AbyssalFlame;
        public bool Agony;
        public bool Chroma;
        public bool DistortionSeeing;
        public bool Nightmare;
        public bool Sin;
        public bool SuperSlow;
        public bool Nothingness;
        public bool RuneBuffEnvy;
        public bool RuneBuffGluttony;
        public bool RuneBuffGreed;
        public bool RuneBuffLust;
        public bool RuneBuffPride;
        public bool RuneBuffSloth;
        public bool RuneBuffWrath;
        //SetBonus
        public bool setAndromeda;
        public bool setEibon;
        public bool setMagellanic;
        public bool setMidnight;
        public bool setMilkyWay;
        public bool setNightfall;
        public bool setNightmare;
        public bool setPolarNight;
        public bool setSpiral;
        public bool setTrueMidnight;
        public bool setUnconscious;
        public bool setWhiteNight;
        //SetBonusMisc
        public int andromedaShields;
        public int andromedaCounter;
        public bool andromedaDashing;
        public bool andromedaDashConsumedFlare;
        public Vector2[] andromedaShieldPos = new Vector2[3];
        public Vector2[] andromedaShieldVel = new Vector2[3];
        public int milkyStarCounter;
        public int milkyStarNum;
        public bool saveStealth;
        public bool spiralStealthActive;
        public bool unconsciousStealthActive;
        //Armor
        public int andromedaCount;
        public int andromedaVisibleCount;
        //Acc
        public int ModDash;
        public bool AbyssalFlameRelic;
        public bool KillAngler;
        public bool CoM;
        public bool eGear;
        public bool SmallNatureCore;
        public bool TartarusDarknessImmune;
        public bool VitalityStone;
        public bool XEmblem;
        public bool NightmareShield;
        public bool Hover;
        public bool SkyWalk;
        public bool InfinityFlight;
        public bool Invincible;
        public bool UnlimitedMana;
        internal bool UniverseSoul;
        internal bool EternitySoul;
        //AccMisc
        public int InvincibleTimer;
        public int MinionCrit;
        //Dmg
        internal bool BossActive;
        public bool NoDamageClass;
        //ExAcc
        public bool ExtraAccessory2;
        public bool ExtraAccessoryLimitBreak;
        public int ExtraAccessorySlotsCount;
        public bool BluemagicExAcc;

        public override void ResetEffects()
        {
            Debug = false;
            //Biome
            ZoneMadness = false;
            //Item
            FOOLS = false;
            LunarArcanumOrb = false;
            //DeadlySinsItem
            SinsThanksItem = false;
            EnvyItem = false;
            GluttonyItem = false;
            GreedItem = false;
            LustItem = false;
            PrideItem = false;
            SlothItem = false;
            WrathItem = false;
            OriginItem = false;
            MadnessItem = false;
            //Buff
            //pet
            CleyeraPet = false;
            CleyeraLightPet = false;
            KobyPet = false;
            //minion
            LunaticMinion = false;
            MoonlightMinion = false;
            NightfallMinion = false;
            NightmareProbeMinion = false;
            PhantasmDragonMinion = false;
            PolarNightRavenMinion = false;
            SmallBlackCoreMinion = false;
            TartarusMinion = false;
            WhiteNightFairyMinion = false;
            //buff
            WeaponImbueLifeDrain = false;
            //debuff
            AbyssalFlame = false;
            Agony = false;
            Chroma = false;
            DistortionSeeing = false;
            Nightmare = false;
            Sin = false;
            SuperSlow = false;
            Nothingness = false;
            //runebuff
            RuneBuffEnvy = false;
            RuneBuffGluttony = false;
            RuneBuffGreed = false;
            RuneBuffLust = false;
            RuneBuffPride = false;
            RuneBuffSloth = false;
            RuneBuffWrath = false;
            //SetBonus
            setAndromeda = false;
            setEibon = false;
            setMagellanic = false;
            setMidnight = false;
            setMilkyWay = false;
            setNightfall = false;
            setNightmare = false;
            setPolarNight = false;
            setSpiral = false;
            setTrueMidnight = false;
            setUnconscious = false;
            setWhiteNight = false;
            //SetBonusMisc
            //Armor
            andromedaCount = 0;
            andromedaVisibleCount = 0;
            //Acc
            ModDash = 0;
            AbyssalFlameRelic = false;
            KillAngler = false;
            CoM = false;
            eGear = false;
            SmallNatureCore = false;
            TartarusDarknessImmune = false;
            VitalityStone = false;
            XEmblem = false;
            NightmareShield = false;
            Hover = false;
            SkyWalk = false;
            InfinityFlight = false;
            Invincible = false;
            UnlimitedMana = false;
            UniverseSoul = false;
            EternitySoul = false;
            //AccMisc
            MinionCrit = 0;
            //Dmg
            NoDamageClass = false;
            //ExAcc
            if (ExtraAccessory2)
            {
                if (ExtraAccessoryLimitBreak)
                {
                    player.extraAccessorySlots = 2;
                }
                else if (Main.expertMode)
                {
                    player.extraAccessorySlots = 2;
                }
            }
        }
        /*public override bool Autoload(ref string name)
        {
            IL.Terraria.Main.DrawUnderworldBackground += DrawUnderworldBackground;
            return base.Autoload(ref name);
        }*/
        public override void Initialize()
        {
            ZoneNightEnergy = false;
            ZoneMystic = false;
            ZoneDistortion = false;
            ZoneTartarus = false;
        }
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            tag["ExtraAccessory2"] = ExtraAccessory2;
            tag["ExtraAccessoryLimitBreak"] = ExtraAccessoryLimitBreak;
            return tag;
        }
        public override void Load(TagCompound tag)
        {
            ExtraAccessory2 = tag.GetBool("ExtraAccessory2");
            ExtraAccessoryLimitBreak = tag.GetBool("ExtraAccessoryLimitBreak");
        }
        public override void UpdateBiomes()
        {
            ZoneNightEnergy = SinsWorld.NightEnergyTiles > 100;
            ZoneMystic = SinsWorld.MysticTiles > 100;
            ZoneDistortion = SinsWorld.DistortionTiles > 100;
            ZoneTartarus = SinsWorld.TartarusTiles > 250;
        }
        public override bool CustomBiomesMatch(Player other)
        {
            SinsPlayer modOther = other.GetModPlayer<SinsPlayer>();
            bool allMatch = true;
            allMatch &= ZoneNightEnergy == modOther.ZoneNightEnergy;
            allMatch &= ZoneMystic == modOther.ZoneMystic;
            allMatch &= ZoneDistortion == modOther.ZoneDistortion;
            allMatch &= ZoneTartarus == modOther.ZoneTartarus;
            return allMatch;
        }
        public override void CopyCustomBiomesTo(Player other)
        {
            SinsPlayer modOther = other.GetModPlayer<SinsPlayer>();
            modOther.ZoneNightEnergy = ZoneNightEnergy;
            modOther.ZoneMystic = ZoneMystic;
            modOther.ZoneDistortion = ZoneDistortion;
            modOther.ZoneTartarus = ZoneTartarus;
        }
        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = ZoneNightEnergy;
            flags[1] = ZoneMystic;
            flags[2] = ZoneDistortion;
            flags[3] = ZoneTartarus;
            writer.Write(flags);
        }
        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ZoneNightEnergy = flags[0];
            ZoneMystic = flags[1];
            ZoneDistortion = flags[2];
            ZoneTartarus = flags[3];
        }
        public override Texture2D GetMapBackgroundImage()
        {
            if (ZoneMystic)
            {
                return mod.GetTexture("Backgrounds/MapBG/MapBGMystic");
            }
            if (ZoneDistortion)
            {
                return mod.GetTexture("Backgrounds/MapBG/MapBGDistortion");
            }
            if (ZoneTartarus)
            {
                return mod.GetTexture("Backgrounds/MapBG/MapBGTartarus");
            }
            return null;
        }
        public override void UpdateBiomeVisuals()
        {
            bool Envy = NPC.AnyNPCs(mod.NPCType("Envy")) || NPC.AnyNPCs(mod.NPCType("Envy"));
            player.ManageSpecialBiomeVisuals("SinsMod:Envy", Envy);
            bool Gluttony = NPC.AnyNPCs(mod.NPCType("Gluttony")) || NPC.AnyNPCs(mod.NPCType("Gluttony"));
            player.ManageSpecialBiomeVisuals("SinsMod:Gluttony", Gluttony);
            bool Greed = NPC.AnyNPCs(mod.NPCType("Greed")) || NPC.AnyNPCs(mod.NPCType("Greed"));
            player.ManageSpecialBiomeVisuals("SinsMod:Greed", Greed);
            bool Lust = NPC.AnyNPCs(mod.NPCType("Lust")) || NPC.AnyNPCs(mod.NPCType("Lust"));
            player.ManageSpecialBiomeVisuals("SinsMod:Lust", Lust);
            bool Pride = NPC.AnyNPCs(mod.NPCType("Pride")) || NPC.AnyNPCs(mod.NPCType("Pride"));
            player.ManageSpecialBiomeVisuals("SinsMod:Pride", Pride);
            bool Sloth = NPC.AnyNPCs(mod.NPCType("Sloth")) || NPC.AnyNPCs(mod.NPCType("Sloth"));
            player.ManageSpecialBiomeVisuals("SinsMod:Sloth", Sloth);
            bool Wrath = NPC.AnyNPCs(mod.NPCType("Wrath")) || NPC.AnyNPCs(mod.NPCType("Wrath"));
            player.ManageSpecialBiomeVisuals("SinsMod:Wrath", Wrath);
            bool Sins = NPC.AnyNPCs(mod.NPCType("OriginWhite")) || NPC.AnyNPCs(mod.NPCType("OriginBlack")) || NPC.AnyNPCs(mod.NPCType("Eden"));
            player.ManageSpecialBiomeVisuals("SinsMod:Sins", Sins);
            bool Nothingness = NPC.AnyNPCs(mod.NPCType("BlackCrystalCore")) || NPC.AnyNPCs(mod.NPCType("WillOfMadness")) || NPC.AnyNPCs(mod.NPCType("BlackCrystal")) || NPC.AnyNPCs(mod.NPCType("BlackCrystalNoMove"));
            player.ManageSpecialBiomeVisuals("SinsMod:Nothingness", Nothingness);
            //Biome
            bool zoneMystic = ZoneMystic;
            player.ManageSpecialBiomeVisuals("SinsMod:Mystic", zoneMystic);
            bool zoneDistortion = ZoneDistortion;
            player.ManageSpecialBiomeVisuals("SinsMod:Distortion", zoneDistortion);
        }
        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType("DeadlySinsThanks"), false);
            item.stack = 1;
            items.Add(item);

            item = new Item();
            item.SetDefaults(mod.ItemType("LimitCutter"), false);
            item.stack = 1;
            items.Add(item);

            item = new Item();
            item.SetDefaults(mod.ItemType("HopelessMode"), false);
            item.stack = 1;
            items.Add(item);
        }
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (Invincible)
            {
                return false;
            }
            return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (Invincible)
            {
                return false;
            }
            return base.CanBeHitByProjectile(proj);
        }
        public override void PreUpdateBuffs()
        {

        }
        public override void PostUpdateBuffs()
        {
            if (Nothingness)
            {
                Nothing();
            }
        }
        public override void UpdateLifeRegen()
        {
            if (setNightmare)
            {
                player.lifeRegen += player.statLife < player.statLifeMax2 / 2 ? 20 : 10;
            }
        }
        public override void UpdateBadLifeRegen()
        {
            if (player.HasBuff(BuffID.ShadowFlame))
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 30;
            }
            if (player.HasBuff(BuffID.Daybreak))
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 25;
            }
            if (AbyssalFlame)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 44;
            }
            if (Chroma)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 30;
            }
            if (Nightmare)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 66;
            }
            if (Sin)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 60;
            }
            if (SuperSlow)
            {
                player.velocity /= 5;
            }
        }
        public override void PreUpdate()
        {
            BossActive = SinsNPC.BossActiveCheck();
            /*EnvyBoss = NPC.AnyNPCs(mod.NPCType("Leviathan")) || NPC.AnyNPCs(mod.NPCType("Envy"));
            GluttonyBoss = NPC.AnyNPCs(mod.NPCType("DesertKing")) || NPC.AnyNPCs(mod.NPCType("Gluttony"));
            GreedBoss = NPC.AnyNPCs(mod.NPCType("Mammon")) || NPC.AnyNPCs(mod.NPCType("Greed"));
            LustBoss = NPC.AnyNPCs(mod.NPCType("Asmodeus")) || NPC.AnyNPCs(mod.NPCType("Lust"));
            PrideBoss = NPC.AnyNPCs(mod.NPCType("")) || NPC.AnyNPCs(mod.NPCType("Pride"));*/
            SlothBoss = NPC.AnyNPCs(mod.NPCType("Sloth")) || NPC.AnyNPCs(mod.NPCType("Sloth"));
            /*WrathBoss = NPC.AnyNPCs(mod.NPCType("")) || NPC.AnyNPCs(mod.NPCType("Wrath"));
            OriginBoss = NPC.AnyNPCs(mod.NPCType("Adam")) || NPC.AnyNPCs(mod.NPCType("Eve")) || NPC.AnyNPCs(mod.NPCType("Eden")) || NPC.AnyNPCs(mod.NPCType("OriginWhite")) || NPC.AnyNPCs(mod.NPCType("OriginBlack"));
            MadnessBoss = NPC.AnyNPCs(mod.NPCType("BlackCrystalNoMove")) || NPC.AnyNPCs(mod.NPCType("BlackCrystal")) || NPC.AnyNPCs(mod.NPCType("BlackCrystalCore")) || NPC.AnyNPCs(mod.NPCType("WillOfMadness"));
            */
            ExtraAccessorySlotsCount = player.extraAccessorySlots;
            DelInstantDummy = delInstantDummyTime > 0;
            for (int i = 0; i < SinsMod.DevList.Count; i++)
            {
                if (player.name == SinsMod.DevList[0] || player.name == SinsMod.DevList[1] || player.name == SinsMod.DevList[2])
                {
                    Cleyera = true;
                }
                if (player.name == SinsMod.DevList[i])
                {
                    Dev = true;
                }
            }
            if (chargeTime > 0 && !player.channel)
            {
                chargeTime = 0;
            }
            milkyStarCounter -= (milkyStarCounter > 0) ? 1 : 0;
            if (milkyStarNum > 4 || milkyStarNum < 0)
            {
                milkyStarNum = 0;
            }
            UpdateTeleportVisuals();
            delInstantDummyTime -= (delInstantDummyTime > 0) ? 1 : 0;
            mountDelay -= (mountDelay > 0) ? 1 : 0;
            nothingnessTime -= (nothingnessTime > 0) ? 1 : 0;
            lifeElixirCooldown -= (lifeElixirCooldown > 0) ? 1 : 0;
            nullificationPotionCooldown -= (nullificationPotionCooldown > 0) ? 1 : 0;
            reviveCooldown -= (reviveCooldown > 0) ? 1 : 0;
            shakeTime -= (shakeTime > 0) ? 1 : 0;
            teleportDelay -= (teleportDelay > 0) ? 1 : 0;
            useDelay -= (useDelay > 0) ? 1 : 0;
            InvincibleTimer -= (InvincibleTimer > 0) ? 1 : 0;
            stealth += (stealth < 1f && !spiralStealthActive && !unconsciousStealthActive) ? 0.05f : 0;
            if (stealth > 1f)
            {
                stealth = 1f;
            }
            stealthStat += (stealthStat < 1f && !spiralStealthActive && !unconsciousStealthActive) ? 0.05f : 0;
            if (stealthStat > 1f)
            {
                stealthStat = 1f;
            }
            stealthEffect = 1f - stealth;
            spiralTime -= (spiralTime > 0) ? 1 : 0;
            FOOLScounter += FOOLS ? 1 : 0;
            if (FOOLS && FOOLScounter == 180)
            {
                #region Annoy
                /*switch (Main.rand.Next(7))
                {
                    case 0:
                        Main.NewText("<Excalibur> My legend dates back to the 12th Century you see.", 255, 255, 255, false);
                        Main.NewText("             My legend is quite old.", 255, 255, 255, false);
                        Main.NewText("             The 12th Century was a long time ago.", 255, 255, 255, false);
                        break;
                    case 1:
                        Main.NewText("<Excalibur> The taller the chefs hat the greater the chef....", 255, 255, 255, false);
                        Main.NewText("             FOOLS!", 255, 255, 255, false);
                        Main.NewText("             Who said I was a chef?!", 255, 255, 255, false);
                        break;
                    case 2:
                        Main.NewText("<Excalibur> Before becoming my Meister there is a list of 1,000 provisions you must persue.", 255, 255, 255, false);
                        Main.NewText("             Be sure to look through all of them, they're important.", 255, 255, 255, false);
                        Main.NewText("             I greatly look forward to your participation in number 452: The five hour story telling party.", 255, 255, 255, false);
                        break;
                    case 3:
                        Main.NewText("<Excalibur> SILENCE!", 255, 255, 255, false);
                        Main.NewText("             This is number 349 of the 1,000 provisions you must observe. ", 255, 255, 255, false);
                        Main.NewText("             Meisters should eat everything regardless of personal likes and dislikes. ", 255, 255, 255, false);
                        Main.NewText("             Never say anything as selfish as 'I don't like carrots.' again.", 255, 255, 255, false);
                        break;
                    case 4:
                        Main.NewText("<Excalibur> No. 172: Seek harmony.", 255, 255, 255, false);
                        Main.NewText("             Seek it, thus.", 255, 255, 255, false);
                        Main.NewText("             First! A haircut!", 255, 255, 255, false);
                        break;
                    case 5:
                        Main.NewText("<Excalibur> My legend dates back to the 12th Century you see.", 255, 255, 255, false);
                        Main.NewText("             My legend is quite old.", 255, 255, 255, false);
                        Main.NewText("             The 12th Century was a long time ago.", 255, 255, 255, false);
                        break;
                    case 6:
                        Main.NewText("<Excalibur> Of course they are my family. Isn't it obvious?", 255, 255, 255, false);
                        Main.NewText("             And a wonderful family they were.", 255, 255, 255, false);
                        Main.NewText("             It happened long ago when I was still in the flower of my youth.", 255, 255, 255, false);
                        Main.NewText("             The cities began to grow wild, people lost hope for the future.", 255, 255, 255, false);
                        Main.NewText("             They became lazy, idol time wasters.", 255, 255, 255, false);
                        Main.NewText("             And to my everlasting shame I was no exception.", 255, 255, 255, false);
                        Main.NewText("             Thus, I began to watch the 7 O'Clock news religiously every night!", 255, 255, 255, false);
                        break;
                }*/
                #endregion
                FOOLScounter = 0;
            }
            if (subterraneanCount > 1)
            {
                subterraneanCount = 0;
            }
            if (InfinityFlight)
            {
                player.wingTime = player.wingTimeMax;
            }
            if (SinsMod.AllDebuffImmunity.JustPressed && CoM)
            {
                AllDebuffImmunity = !AllDebuffImmunity;
                if (AllDebuffImmunity)
                {
                    Main.NewText("You are immune to all debuff", 255, 255, 255, false);
                }
                else
                {
                    Main.NewText("You are not immune to all debuff", 255, 0, 0, false);
                }
            }
            if (SinsMod.NightmareToolDestructMode.JustPressed && player.inventory[player.selectedItem].type == mod.ItemType("NightmareTool"))
            {
                NightmareToolDestructMode = !NightmareToolDestructMode;
                if (NightmareToolDestructMode)
                {
                    Main.NewText("Nightmare Tool: Destruct Mode: Start-up", 150, 150, 150, false);
                }
                else
                {
                    Main.NewText("Nightmare Tool: Destruct Mode: Initialization", 150, 150, 150, false);
                }
            }
            if (!player.dead)
            {
                if (EnvyBoss)
                {
                    player.AddBuff(mod.BuffType("EnvyDebuffEffect"), 2);
                }
                if (GluttonyBoss)
                {
                    player.AddBuff(mod.BuffType("GluttonyDebuffEffect"), 2);
                }
                if (GreedBoss)
                {
                    player.AddBuff(mod.BuffType("GreedDebuffEffect"), 2);
                }
                if (LustBoss)
                {
                    player.AddBuff(mod.BuffType("LustDebuffEffect"), 2);
                }
                if (PrideBoss)
                {
                    player.AddBuff(mod.BuffType("PrideDebuffEffect"), 2);
                }
                if (SlothBoss)
                {
                    player.AddBuff(mod.BuffType("SlothDebuffEffect"), 2);
                    player.position -= player.velocity / 5f;
                    //player.position.X -= player.velocity.X / 5f;
                }
                if (WrathBoss)
                {
                    player.AddBuff(mod.BuffType("WrathDebuffEffect"), 2);
                }
                if (OriginBoss)
                {
                    player.AddBuff(mod.BuffType("OriginDebuffEffect"), 2);
                }
                if (MadnessBoss)
                {
                    player.AddBuff(mod.BuffType("MadnessDebuffEffect"), 2);
                }
            }
            if (nothingnessTime > 0)
            {
                Nothingness = true;
                if (player.dead)
                {
                    nothingnessTime = 0;
                }
                else if (!player.HasBuff(mod.BuffType("Nothingness")))
                {
                    player.AddBuff(mod.BuffType("Nothingness"), nothingnessTime);
                }
            }
            if (lifeElixirCooldown > 0)
            {
                if (player.dead)
                {
                    lifeElixirCooldown = 0;
                }
                else if (!player.HasBuff(mod.BuffType("LifeElixirSickness")))
                {
                    player.AddBuff(mod.BuffType("LifeElixirSickness"), lifeElixirCooldown, false);
                }
            }
            if (nullificationPotionCooldown > 0)
            {
                if (player.dead)
                {
                    nullificationPotionCooldown = 0;
                }
                else if (!player.HasBuff(mod.BuffType("NullificationPotionSickness")))
                {
                    player.AddBuff(mod.BuffType("NullificationPotionSickness"), nullificationPotionCooldown, false);
                }
            }
            if (reviveCooldown > 0)
            {
                if (player.dead)
                {
                    reviveCooldown = 0;
                }
                else if (!player.HasBuff(mod.BuffType("ReviveCooldown")))
                {
                    player.AddBuff(mod.BuffType("ReviveCooldown"), reviveCooldown, false);
                }
            }
            player.buffImmune[mod.BuffType("Nothingness")] = false;
            player.buffImmune[mod.BuffType("LifeElixirSickness")] = false;
            player.buffImmune[mod.BuffType("NullificationPotionSickness")] = false;
            player.buffImmune[mod.BuffType("ReviveCooldown")] = false;
            for (int i = 0; i < player.buffType.Length; i++)
            {
                if (player.buffType[i] > 0 && player.buffTime[i] > 0)
                {
                    if (player.buffType[i] == mod.BuffType("AndromedaBlaze1") || player.buffType[i] == mod.BuffType("AndromedaBlaze2") || player.buffType[i] == mod.BuffType("AndromedaBlaze3"))
                    {
                        player.buffTime[i] = 5;
                        if (!setAndromeda)
                        {
                            player.DelBuff(i);
                            i--;
                        }
                    }
                    if (player.buffType[i] == mod.BuffType("Nothingness") && nothingnessTime <= 0)
                    {
                        player.DelBuff(i);
                        i--;
                    }
                    if (player.buffType[i] == mod.BuffType("LifeElixirSickness") && lifeElixirCooldown <= 0)
                    {
                        player.DelBuff(i);
                        i--;
                    }
                    if (player.buffType[i] == mod.BuffType("NullificationPotionSickness") && nullificationPotionCooldown <= 0)
                    {
                        player.DelBuff(i);
                        i--;
                    }
                    if (player.buffType[i] == mod.BuffType("ReviveCooldown") && reviveCooldown <= 0)
                    {
                        player.DelBuff(i);
                        i--;
                    }
                }
            }
            Main.debuff[BuffID.Daybreak] = true;
            Main.buffTexture[BuffID.Daybreak] = mod.GetTexture("Extra/Alt/Daybreak");
            /*if (ZoneTartarus)
            {
                for (int i = 4; i >= 0; i--)
                {
                    tartarusTexture[i] = mod.GetTexture("Backgrounds/Tratarus/Underworld " + i);
                }
            }
            for (int i = 4; i >= 0; i--)
            {
                underworldTexture[i] = mod.GetTexture("Backgrounds/Underworld/Underworld " + i);
            }
            if (LocalPlayer.GetModPlayer<SinsPlayer>().ZoneTartarus)
            {
                Main.underworldTexture[i] = LocalPlayer.GetModPlayer<SinsPlayer>().tartarusTexture[i];
            }*/
        }
        /*public static Texture2D[] tartarusTexture = new Texture2D[5];
        public static Texture2D[] underworldTexture = new Texture2D[5];*/
        public override void PostUpdate()
        {
            if (player.mount.Type == mod.MountType("CleyeraMount"))
            {
                if (!player.controlJump && !player.controlUp && !player.controlDown && !player.controlRight && !player.controlLeft)
                {
                    //player.position.Y = player.oldPosition.Y;
                }
            }
            if (AxionToolType == 0 && useDelay == 10 && player.inventory[player.selectedItem].type == mod.ItemType("AxionTool"))
            {
                Main.NewText("Axion Tool: Mode Pickaxe", 150, 0, 150, false);
            }
            else if (AxionToolType == 1 && useDelay == 10 && player.inventory[player.selectedItem].type == mod.ItemType("AxionTool"))
            {
                Main.NewText("Axion Tool: Mode Axe", 150, 0, 150, false);
            }
            else if (AxionToolType == 2 && useDelay == 10 && player.inventory[player.selectedItem].type == mod.ItemType("AxionTool"))
            {
                Main.NewText("Axion Tool: Mode Hammer", 150, 0, 150, false);
            }
            if (NightmareToolType == 0 && useDelay == 10 && player.inventory[player.selectedItem].type == mod.ItemType("NightmareTool"))
            {
                Main.NewText("Nightmare Tool: Mode Pickaxe", 150, 150, 150, false);
            }
            else if (NightmareToolType == 1 && useDelay == 10 && player.inventory[player.selectedItem].type == mod.ItemType("NightmareTool"))
            {
                Main.NewText("Nightmare Tool: Mode Axe", 150, 150, 150, false);
            }
            else if (NightmareToolType == 2 && useDelay == 10 && player.inventory[player.selectedItem].type == mod.ItemType("NightmareTool"))
            {
                Main.NewText("Nightmare Tool: Mode Hammer", 150, 150, 150, false);
            }
            if (SinsMod.Instance.BluemagicLoaded)
            {
                BluemagicPostUpdate();
            }
        }
        private void BluemagicPostUpdate()
        {
            BluemagicPlayer BluemagicPlayer = player.GetModPlayer<BluemagicPlayer>();
            if (NPC.AnyNPCs(mod.NPCType("BlackCrystalNoMove")) || NPC.AnyNPCs(mod.NPCType("BlackCrystal")) || NPC.AnyNPCs(mod.NPCType("BlackCrystalCore")) || NPC.AnyNPCs(mod.NPCType("WillOfMadness")))
            {
                if (BluemagicPlayer.godmode)
                {
                    player.statLife -= 5;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.LifeRegen, 5, false, true);
                    Nothing();
                    if (player.statLife < 1)
                    {
                        player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " infected by madness"), 0.0, 0, false);
                    }
                    player.controlJump = false;
                    player.releaseJump = false;
                    player.dash = -1;
                    player.wingTime = 0;
                    player.wingTimeMax = 0;
                    player.velocity *= 0;
                }
            }
            BluemagicExAcc = player.GetModPlayer<BluemagicPlayer>().extraAccessory2;
        }
        public override void PostUpdateEquips()
        {
            if (Nothingness)
            {
                Nothing();
            }
            if (player.mount.Active || player.mount.Cart)
            {
                player.dashDelay = 60;
                ModDash = 0;
            }
            if (Hover)
            {
                if (player.controlDown && player.controlJump && !player.mount.Active && player.wingTime > 0f)
                {
                    float posY = player.oldPosition.Y;
                    player.position.Y = posY;
                    player.velocity.Y *= 1E-09f;
                    if (!InfinityFlight)
                    {
                        player.wingTime += 0.5f;
                    }
                }
            }
            else if (SkyWalk)
            {
                if (player.controlDown && player.controlJump && !player.mount.Active)
                {
                    player.gravity = 0f;
                    player.velocity.Y = 0;
                }
            }
            if (InfinityFlight)
            {
                player.wingTime = player.wingTimeMax;
            }
            else if(player.wingTime > player.wingTimeMax)
            {
                player.wingTime = player.wingTimeMax;
            }
            if (UnlimitedMana)
            {
                player.statMana = player.statManaMax2;
            }
            if (eGear)
            {
                player.position.X -= player.velocity.X / 4f;//75%
            }
            if (NightmareShield)
            {
                if (player.statLife < player.statLifeMax2 / 4 || InvincibleTimer > 0)
                {
                    Invincible = true;
                    player.endurance = 1f;
                }
            }
            if (VitalityStone)
            {
                float num = (float)player.statLife / player.statLifeMax2;
                player.allDamage += num / 10;
                player.meleeCrit += (int)(num * 10);
                player.rangedCrit += (int)(num * 10);
                player.magicCrit += (int)(num * 10);
                player.thrownCrit += (int)(num * 10);
            }
            if (andromedaCount > 0)
            {
                Lighting.AddLight(player.Center, 0.25f * andromedaCount, 0.5f * andromedaCount, 1.0f * andromedaCount);
            }
            if (SinsMod.Instance.LaugicalityLoaded)
            {
                LaugicalityPostUpdateEquips();
            }
            if (SinsMod.Instance.ThoriumLoaded)
            {
                ThoriumPostUpdateEquips();
            }
        }
        private void LaugicalityPostUpdateEquips()
        {
            LaugicalityPlayer laugicalityPlayer = player.GetModPlayer<LaugicalityPlayer>();
            if (UnlimitedMana)
            {
                if (laugicalityPlayer.Lux < laugicalityPlayer.LuxMax + laugicalityPlayer.LuxMaxPermaBoost)
                {
                    laugicalityPlayer.Lux = laugicalityPlayer.LuxMax + laugicalityPlayer.LuxMaxPermaBoost;
                }
                if (laugicalityPlayer.Vis < laugicalityPlayer.VisMax + laugicalityPlayer.VisMaxPermaBoost)
                {
                    laugicalityPlayer.Vis = laugicalityPlayer.VisMax + laugicalityPlayer.VisMaxPermaBoost;
                }
                if (laugicalityPlayer.Mundus < laugicalityPlayer.MundusMax + laugicalityPlayer.MundusMaxPermaBoost)
                {
                    laugicalityPlayer.Mundus = laugicalityPlayer.MundusMax + laugicalityPlayer.MundusMaxPermaBoost;
                }
            }
        }
        private void ThoriumPostUpdateEquips()
        {
            //ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            if (UnlimitedMana)
            {
                /*thoriumPlayer.bardResource = thoriumPlayer.bardResourceMax + thoriumPlayer.bardResourceMax2;
                if (thoriumPlayer.bardResource < thoriumPlayer.bardResourceMax + thoriumPlayer.bardResourceMax2)
                {
                    thoriumPlayer.bardResource = thoriumPlayer.bardResourceMax + thoriumPlayer.bardResourceMax2;
                }*/
            }
            if (XEmblem)
            {
                /*thoriumPlayer.throwerOveruse = 0;
                if (thoriumPlayer.throwerOveruse > 0)
                {
                    thoriumPlayer.throwerOveruse = 0;
                }*/
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.grappling[0] == -1 && !player.tongued && modPlayer.ModDash > 0)
            {
                DashMovement();
                HorizontalMovement();
            }
        }
        public override void PostUpdateMiscEffects()
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (SinsWorld.LimitCut)
            {
                if (!Main.expertMode)
                {
                    SinsWorld.LimitCut = false;
                    string key = "Mods.SinsMod.LimitCut";
                    string text = "deactivated";
                    if (Main.netMode != 2)
                    {
                        Main.NewText(Language.GetTextValue(key, text), new Color(255, 255, 255));
                    }
                    else
                    {
                        NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), new Color(255, 255, 255));
                    }
                }
            }
            if (SinsWorld.Hopeless)
            {
                player.immune = false;
                if (player.endurance >= 1f)
                {
                    player.endurance *= 0f;
                }
            }
            if (setAndromeda)
            {
                andromedaCounter++;
                int num = 240;
                if (andromedaCounter >= num)
                {
                    if (andromedaShields > 0 && andromedaShields < 3)
                    {
                        for (int num2 = 0; num2 < Player.MaxBuffs; num2++)
                        {
                            if (player.buffType[num2] == mod.BuffType("AndromedaBlaze1") || player.buffType[num2] == mod.BuffType("AndromedaBlaze2"))
                            {
                                player.DelBuff(num2);
                            }
                        }
                    }
                    if (andromedaShields < 3)
                    {
                        int type = andromedaShields == 2 ? mod.BuffType("AndromedaBlaze3") : modPlayer.andromedaShields == 1 ? mod.BuffType("AndromedaBlaze2") : mod.BuffType("AndromedaBlaze1");
                        player.AddBuff(type, 5, false);
                        for (int num3 = 0; num3 < 16; num3++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 172, 0f, 0f, 100, default(Color), 1f)];
                            dust.noGravity = true;
                            dust.scale = 1.7f;
                            dust.fadeIn = 0.5f;
                            dust.velocity *= 5f;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                        }
                        andromedaCounter = 0;
                    }
                    else
                    {
                        andromedaCounter = num;
                    }
                }
                for (int num4 = andromedaShields; num4 < 3; num4++)
                {
                    andromedaShieldPos[num4] = Vector2.Zero;
                }
                for (int num5 = 0; num5 < andromedaShields; num5++)
                {
                    andromedaShieldPos[num5] += andromedaShieldVel[num5];
                    Vector2 value = (player.miscCounter / 100f * 6.28318548f + num5 * (6.28318548f / andromedaShields)).ToRotationVector2() * 6f;
                    value.X = player.direction * 20;
                    andromedaShieldVel[num5] = (value - andromedaShieldPos[num5]) * 0.2f;
                }
                if (player.dashDelay >= 0)
                {
                    andromedaDashing = false;
                    andromedaDashConsumedFlare = false;
                }
                andromedaShields = player.HasBuff(mod.BuffType("AndromedaBlaze3")) ? 3 : player.HasBuff(mod.BuffType("AndromedaBlaze2")) ? 2 : player.HasBuff(mod.BuffType("AndromedaBlaze1")) ? 1 : 0;
            }
            else
            {
                andromedaShieldPos[0] = andromedaShieldPos[1] = andromedaShieldPos[2] = Vector2.Zero;
                andromedaShields = 0;
                andromedaCounter = 0;
            }
            if (setMilkyWay)
            {
                int num = 0;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("MilkyStar") && Main.projectile[i].owner == player.whoAmI)
                    {
                        num++;
                    }
                }
                if (milkyStarCounter == 0 && num < 4)
                {
                    milkyStarCounter = 120;
                    double num2 = Main.rand.NextDouble();
                    double num3 = Math.Atan2(12, 12) - (num2 / 2f);
                    double num4 = num2 / 4;
                    for (int i = 0; i < 4 - num; i++)
                    {
                        double num5 = num3 + num4 * (i + i * i) / 2.0 + (32f * i);
                        if (milkyStarNum % 2 == 0)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, (float)(Math.Sin(num5) * 6), (float)(Math.Cos(num5) * 6), mod.ProjectileType("MilkyStar"), (int)((double)200 * player.minionDamage), 5f, player.whoAmI, 4 * (320 / 4), milkyStarNum);
                        }
                        else
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, (float)(-Math.Sin(num5) * 6), (float)(-Math.Cos(num5) * 6), mod.ProjectileType("MilkyStar"), (int)((double)200 * player.minionDamage), 5f, player.whoAmI, 4 * (320 / 4), milkyStarNum);
                        }
                        milkyStarNum++;
                    }
                }
            }
            else
            {
                milkyStarCounter = 0;
            }
            if (setSpiral)
            {
                if (player.controlDown && player.releaseDown)
                {
                    if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
                    {
                        spiralStealthActive = !spiralStealthActive;
                    }
                }
                if (player.mount.Active || player.dashDelay < 0)
                {
                    spiralStealthActive = false;
                }
                if (spiralStealthActive)
                {
                    if (stealth < 0.03)
                    {
                        stealth = 0.03f;
                    }
                    else
                    {
                        stealth -= 0.05f;
                    }
                    stealthStat = MathHelper.Clamp(stealthStat - 0.05f, 0f, 1f);
                    player.moveSpeed *= 0.4f;
                }
                player.rangedDamage += (1f - stealthStat) * 0.9f;
                player.rangedCrit += (int)((1f - stealthStat) * 25f);
                player.aggro -= (int)((1f - stealthStat) * 2000f);
            }
            else
            {
                spiralStealthActive = false;
            }
            if (setUnconscious)
            {
                if (player.controlDown && player.releaseDown)
                {
                    if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
                    {
                        unconsciousStealthActive = !unconsciousStealthActive;
                    }
                }
                if (player.mount.Active || player.dashDelay < 0)
                {
                    unconsciousStealthActive = false;
                }
                if (unconsciousStealthActive)
                {
                    if (stealth < 0.03)
                    {
                        stealth = 0.03f;
                    }
                    else
                    {
                        stealth -= 0.1f;
                    }
                    stealthStat = MathHelper.Clamp(stealthStat - 0.1f, 0f, 1f);
                }
                /*player.meleeDamage += (1f - stealthStat) * 0.5f;
                player.meleeCrit += (int)((1f - stealthStat) * 100f);*/
                player.aggro -= (int)((1f - stealthStat) * int.MaxValue);
            }
            else
            {
                unconsciousStealthActive = false;
            }
            if (Main.myPlayer == player.whoAmI)
            {
                if (ZoneTartarus)
                {
                    if (Main.myPlayer == player.whoAmI)
                    {
                        if (!TartarusDarknessImmune)
                        {
                            player.bleed = true;
                            player.headcovered = true;
                            player.blackout = true;
                        }
                    }
                }
            }
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            bool flag = player.townNPCs > 2f;
            if (stealthEffect > 0f && player.townNPCs < 3f)
            {
                float num = stealthEffect * 0.9f;
                if (unconsciousStealthActive)
                {
                    num = stealthEffect;
                }
                r *= 1f - num;
                g *= 1f - num;
                b *= 1f - num;// * 0.89f
                a *= 1f - num;
                player.armorEffectDrawOutlines = false;
                player.armorEffectDrawOutlinesForbidden = false;
                player.armorEffectDrawShadow = false;
                player.armorEffectDrawShadowSubtle = false;
                player.stealth = stealth;
            }
            if (player.HasBuff(BuffID.ShadowFlame))
            {
                if (Main.rand.Next(5) < 4 && drawInfo.shadow == 0f && !player.dead)
                {
                    int num = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y - 2f), player.width + 4, player.height + 4, 27, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 180, default(Color), 1.95f);
                    Main.dust[num].noGravity = true;
                    Dust dust = Main.dust[num];
                    dust.velocity *= 0.75f;
                    Dust dust2 = Main.dust[num];
                    dust2.velocity.X = dust2.velocity.X * 0.75f;
                    Dust dust3 = Main.dust[num];
                    dust3.velocity.Y = dust3.velocity.Y - 1f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[num].noGravity = false;
                        dust = Main.dust[num];
                        dust.scale *= 0.5f;
                    }
                }
            }
            if (player.HasBuff(BuffID.Daybreak))
            {
                if (Main.rand.Next(4) < 3 && drawInfo.shadow == 0f && !player.dead)
                {
                    int num = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y - 2f), player.width + 4, player.height + 4, 158, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[num].noGravity = true;
                    Dust dust = Main.dust[num];
                    dust.velocity *= 2.8f;
                    Dust dust2 = Main.dust[num];
                    dust2.velocity.Y = dust2.velocity.Y - 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[num].noGravity = false;
                        dust = Main.dust[num];
                        dust.scale *= 0.5f;
                    }
                }
                Lighting.AddLight((int)(player.position.X / 16f), (int)(player.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
                /*Rectangle hitbox = player.Hitbox;
                for (int num = 0; num < 20; num++)
                {
                    int num2 = Utils.SelectRandom<int>(Main.rand, new int[]
                    {
                        6,
                        259,
                        158
                    });
                    int num3 = Dust.NewDust(hitbox.TopLeft(), player.width, player.height, num2, 0f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num3].alpha = 200;
                    Dust dust = Main.dust[num3];
                    dust.velocity *= 1.4f;
                    dust = Main.dust[num3];
                    dust.scale += Main.rand.NextFloat();
                }*/
            }
            if (AbyssalFlame)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f && !player.dead)
                {
                    int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width, player.height, 21, player.velocity.X * 0.6f, -1f, 0, default(Color), 0.75f);
                    int dust2 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width, player.height, 21, player.velocity.X * 0.6f, -3f, 200, default(Color), 1.5f);
                    int dust3 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width, player.height, 21, player.velocity.X * 0.6f, -1.5f, 200, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.5f;
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].velocity *= 1.5f;
                    Main.dust[dust3].noGravity = true;
                    Main.dust[dust3].velocity *= 1.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].scale *= 0.25f;
                        Main.dust[dust2].scale *= 0.75f;
                        Main.dust[dust3].scale *= 0.75f;
                    }
                    Main.playerDrawDust.Add(dust);
                    Main.playerDrawDust.Add(dust2);
                    Main.playerDrawDust.Add(dust3);
                }
                if (flag)
                {
                    r *= 0.4f;
                    g *= 0.05f;
                    b *= 0.4f;
                    fullBright = true;
                }
            }
            if (Chroma)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f && !player.dead)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 267, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, Main.DiscoColor, 1.2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.8f;
                    Main.dust[dust].velocity.Y = 0.0f;
                    Main.playerDrawDust.Add(dust);
                }
                Lighting.AddLight(player.Center, Main.DiscoR / 255, Main.DiscoG / 255, Main.DiscoB / 255);
            }
            if (Nightmare)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f && !player.dead)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 182, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.8f;
                    Main.dust[dust].velocity.Y = 0.0f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].scale *= 1.2f;
                    }
                    Main.playerDrawDust.Add(dust);
                }
                if (flag)
                {
                    r *= 0.8f;
                    g *= 0.0f;
                    b *= 0.0f;
                    fullBright = true;
                }
            }
            if (Sin)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f && !player.dead)
                {
                    int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width, player.height, mod.DustType("Black"), player.velocity.X * 0.6f, -1f, 0, default(Color), 0.75f);
                    int dust2 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width, player.height, mod.DustType("Black"), player.velocity.X * 0.6f, -3f, 100, default(Color), 1.5f);
                    int dust3 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width, player.height, mod.DustType("Black"), player.velocity.X * 0.6f, -1.5f, 200, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.5f;
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].velocity *= 1.5f;
                    Main.dust[dust3].noGravity = true;
                    Main.dust[dust3].velocity *= 1.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].scale *= 0.25f;
                        Main.dust[dust2].scale *= 0.75f;
                        Main.dust[dust3].scale *= 0.75f;
                    }
                    Main.playerDrawDust.Add(dust);
                    Main.playerDrawDust.Add(dust2);
                    Main.playerDrawDust.Add(dust3);
                }
                if (flag)
                {
                    r *= 0.01f;
                    g *= 0.01f;
                    b *= 0.01f;
                    fullBright = true;
                }
            }
            if (SuperSlow)
            {
                if (Main.rand.Next(5) == 0 && drawInfo.shadow == 0f && !player.dead)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 235, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 0.7f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                }
                if (flag)
                {
                    r *= 0.1f;
                    g *= 0.1f;
                    b *= 0.1f;
                    fullBright = true;
                }
            }
            if (setAndromeda && (Math.Abs(player.velocity.X) > 0.05 || Math.Abs(player.velocity.Y) > 0.05) && !player.mount.Active && !player.dead)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 172, player.velocity.X * -0.4f, player.velocity.Y * 0.4f, 100, default(Color), 0.9f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].color = new Color(255, 255, 255);
                    Main.dust[dust].fadeIn = 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                if (flag)
                {
                    r *= 0.1f;
                    g *= 0.1f;
                    b *= 0.3f;
                    fullBright = true;
                }
            }
            if (setMidnight && (Math.Abs(player.velocity.X) > 0.05 || Math.Abs(player.velocity.Y) > 0.05) && !player.mount.Active && !player.dead)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 27, player.velocity.X * -0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.0f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].fadeIn = 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                if (flag)
                {
                    r *= 0.5f;
                    g *= 0.1f;
                    b *= 0.5f;
                    fullBright = true;
                }
            }
            if (setNightfall && (Math.Abs(player.velocity.X) > 0.05 || Math.Abs(player.velocity.Y) > 0.05) && !player.mount.Active && !player.dead)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 174, player.velocity.X * -0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.0f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].fadeIn = 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                if (flag)
                {
                    r *= 0.4f;
                    g *= 0.2f;
                    b *= 0.1f;
                    fullBright = true;
                }
            }
            if (setNightmare && (Math.Abs(player.velocity.X) > 0.05 || Math.Abs(player.velocity.Y) > 0.05) && !player.mount.Active && !player.dead)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 172, player.velocity.X * -0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.0f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                    Main.dust[dust].color = new Color(255, 255, 255);
                    Main.dust[dust].fadeIn = 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                if (flag)
                {
                    r *= 0.5f;
                    g *= 0.5f;
                    b *= 0.5f;
                    fullBright = true;
                }
            }
            if (setPolarNight && (Math.Abs(player.velocity.X) > 0.05 || Math.Abs(player.velocity.Y) > 0.05) && !player.mount.Active && !player.dead)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 186, player.velocity.X * -0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.0f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].fadeIn = 0.5f;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                    Main.playerDrawDust.Add(dust);
                }
                if (flag)
                {
                    r *= 0.5f;
                    g *= 0.5f;
                    b *= 0.5f;
                    fullBright = true;
                }
            }
            if (setTrueMidnight && (Math.Abs(player.velocity.X) > 0.05 || Math.Abs(player.velocity.Y) > 0.05) && !player.mount.Active && !player.dead)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 21, player.velocity.X * -0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.0f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].fadeIn = 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                if (flag)
                {
                    r *= 0.5f;
                    g *= 0.25f;
                    b *= 0.5f;
                    fullBright = true;
                }
            }
            if (setWhiteNight && (Math.Abs(player.velocity.X) > 0.05 || Math.Abs(player.velocity.Y) > 0.05) && !player.mount.Active && !player.dead)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 247, player.velocity.X * -0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.0f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].fadeIn = 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                if (flag)
                {
                    r *= 1.0f;
                    g *= 1.0f;
                    b *= 1.0f;
                    fullBright = true;
                }
            }
            if (player.HeldItem.type == mod.ItemType("MagellanicBlaze") && !player.dead)
            {
                Vector2 hand = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
                if (player.direction != 1)
                {
                    hand.X = player.bodyFrame.Width - hand.X;
                }
                if (player.gravDir != 1f)
                {
                    hand.Y = player.bodyFrame.Height - hand.Y;
                }
                hand -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
                Vector2 dustPos = player.RotatedRelativePoint(player.position + hand, true) - player.velocity;
                for (int i = 0; i < 3; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.Center, 0, 0, 60, player.direction * 2, 0f, 100, default(Color), 2.3f)];//86
                    dust.position = dustPos + player.velocity;
                    dust.velocity = Utils.RandomVector2(Main.rand, -0.5f, 0.5f) + player.velocity * 0.5f;
                    dust.noGravity = true;
                    if (Main.rand.Next(2) == 0)
                    {
                        dust.color = Color.Crimson;
                    }
                }
            }
        }
        public override void FrameEffects()
        {
            if (Nothingness)
            {
                Nothing();
            }
            if (player.HeldItem.type == mod.ItemType("GoldRush"))
            {
                player.handon = (sbyte)mod.GetEquipSlot("GoldRush", EquipType.HandsOn);
            }
        }
        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (AbyssalFlameRelic && item.melee && !item.noMelee && !item.noUseGraphic && Main.rand.Next(2) == 0)
            {
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 21, player.velocity.X * 0.2f + (player.direction * 3), player.velocity.Y * 0.2f, 0, default(Color), 1.2f);
                Main.dust[num].noGravity = true;
            }
            if (WeaponImbueLifeDrain && item.melee && !item.noMelee && !item.noUseGraphic && Main.rand.Next(2) == 0)
            {
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 235, player.velocity.X * 0.2f + (player.direction * 3), player.velocity.Y * 0.2f, 0, default(Color), 1.0f);
                Main.dust[num].noGravity = true;
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (AbyssalFlameRelic && item.melee && !item.noMelee && !item.noUseGraphic)
            {
                target.AddBuff(mod.BuffType("AbyssalFlame"), 120);
            }
            if (WeaponImbueLifeDrain && item.melee && !item.noMelee && !item.noUseGraphic && target.type != NPCID.TargetDummy && !player.moonLeech)
            {
                float num = Main.rand.Next(1, 3) + (crit ? 1 : 0);
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = player.whoAmI;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, player.whoAmI, num2, num);
            }
            base.OnHitNPC(item, target, damage, knockback, crit);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (AbyssalFlameRelic && proj.melee && !proj.noEnchantments)
            {
                target.AddBuff(mod.BuffType("AbyssalFlame"), 120);
            }
            if (WeaponImbueLifeDrain && proj.melee && !proj.noEnchantments && target.type != NPCID.TargetDummy && !player.moonLeech && proj.numHits < 2)
            {
                float num = Main.rand.Next(0, 2) + (crit ? 1 : 0);
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = player.whoAmI;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, player.whoAmI, num2, num);
            }
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }
        public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        {
            if (AbyssalFlameRelic && item.melee && !item.noMelee && !item.noUseGraphic)
            {
                target.AddBuff(mod.BuffType("AbyssalFlame"), 120);
            }
            if (WeaponImbueLifeDrain && item.melee && !item.noMelee && !item.noUseGraphic && !player.moonLeech)
            {
                float num = Main.rand.Next(1, 3) + (crit ? 1 : 0);
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = player.whoAmI;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, player.whoAmI, num2, num);
            }
            base.OnHitPvp(item, target, damage, crit);
        }
        public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
        {
            if (AbyssalFlameRelic && proj.melee && !proj.noEnchantments)
            {
                target.AddBuff(mod.BuffType("AbyssalFlame"), 120);
            }
            if (WeaponImbueLifeDrain && proj.melee && !proj.noEnchantments && !player.moonLeech && proj.numHits < 2)
            {
                float num = Main.rand.Next(0, 2) + (crit ? 1 : 0);
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = player.whoAmI;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, player.whoAmI, num2, num);
            }
            base.OnHitPvpWithProj(proj, target, damage, crit);
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Invincible)
            {
                return false;
            }
            if (AbyssalFlameRelic)
            {
                if (Main.rand.Next(100) < 15)
                {
                    damage = 1;
                    InvincibleTimer = player.longInvince ? 90 : 60;
                }
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            double num = Main.CalculatePlayerDamage((int)damage, player.statDefense);
            if (crit)
            {
                damage *= 2;
            }
            if (num >= 1.0)
            {
                num = (int)((1f - player.endurance) * num);
                if (num < 1.0)
                {
                    num = 1.0;
                }
                if (ConsumeAndromedaFlare())
                {
                    float num2 = 0.3f;
                    num = (int)((1f - num2) * num);
                    if (num < 1.0)
                    {
                        num = 1.0;
                    }
                    if (player.whoAmI == Main.myPlayer)
                    {
                        int num3 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("AndromedaCounter"), 300, 15f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num3].Kill();
                    }
                }
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (reviveCooldown <= 0)
            {
                if (setNightmare)
                {
                    int num = 20;
                    for (int i = 0; i < num; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Vector2 vector = Vector2.Normalize(new Vector2(10f, 0f));
                            vector = vector.RotatedBy((i - (num / 2 - 1)) * 6.28318548f / num, default(Vector2)) + player.Center;
                            Vector2 vector2 = vector - player.Center;
                            int dust = Dust.NewDust(vector + vector2, 0, 0, 172, vector2.X * 2f, vector2.Y * 2f, 0, default(Color), 1.6f - 0.3f * j);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity = Vector2.Normalize(vector2) * (12f - 2 * j);
                            Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                            if (j == 0)
                            {
                                dust = Dust.NewDust(vector + vector2, 0, 0, 182, vector2.X * 2f, vector2.Y * 2f, 0, default(Color), 1.2f);
                                Main.dust[dust].noGravity = true;
                                Main.dust[dust].velocity = Vector2.Normalize(vector2) * 14f;
                            }
                        }
                    }
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Custom1").WithVolume(0.4f), player.Center);
                    player.statLife += player.statLifeMax2 / 2;
                    player.HealEffect(player.statLifeMax2 / 2, true);
                    reviveCooldown = 3600;
                    InvincibleTimer = 60;
                    player.immune = true;
                    player.immuneTime = 60;
                    return false;
                }
            }
            if (AbyssalFlame)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " burned out.");
            }
            if (Chroma)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " burned out.");
            }
            if (Nightmare)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " fallen to nightmare.");
            }
            if (Sin)
            {
                int choice = Main.rand.Next(2);
                if (choice == 0)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(player.name + " couldn't stand " + player.name + "'s sins.");
                }
                if (choice == 1)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(player.name + " dead by " + player.name + "'s karma.");
                }
            }
            if (player.head == mod.GetEquipSlot("OrgaMask", EquipType.Head))
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " stopped.");
                //Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/フリージア"));
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            FinalDeadPosition = player.position;
        }
        private bool ConsumeAndromedaFlare()
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (!modPlayer.setAndromeda || modPlayer.andromedaShields <= 0)
            {
                return false;
            }
            if (Main.netMode == 1 && player.whoAmI != Main.myPlayer)
            {
                return true;
            }
            modPlayer.andromedaShields--;
            for (int i = 0; i < Player.MaxBuffs; i++)
            {
                if (player.buffType[i] == mod.BuffType("AndromedaBlaze1") || player.buffType[i] == mod.BuffType("AndromedaBlaze2") || player.buffType[i] == mod.BuffType("AndromedaBlaze3"))
                {
                    player.DelBuff(i);
                }
            }
            if (modPlayer.andromedaShields > 0)
            {
                int type = modPlayer.andromedaShields == 3 ? mod.BuffType("AndromedaBlaze3") : modPlayer.andromedaShields == 2 ? mod.BuffType("AndromedaBlaze2") : mod.BuffType("AndromedaBlaze1");
                player.AddBuff(type, 5, false);
            }
            modPlayer.andromedaCounter = 0;
            return true;
        }
        private void HorizontalMovement()
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (modPlayer.ModDash == 2 && player.dashDelay < 0 && player.whoAmI == Main.myPlayer)
            {
                Rectangle rectangle = new Rectangle((int)(player.position.X + player.velocity.X * 0.5 - 4.0), (int)(player.position.Y + player.velocity.Y * 0.5 - 4.0), player.width + 8, player.height + 8);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[player.whoAmI] <= 0)
                    {
                        NPC nPC = Main.npc[i];
                        Rectangle rect = nPC.getRect();
                        if (rectangle.Intersects(rect) && (nPC.noTileCollide || player.CanHit(nPC)))
                        {
                            if (!modPlayer.andromedaDashConsumedFlare)
                            {
                                modPlayer.andromedaDashConsumedFlare = true;
                                ConsumeAndromedaFlare();
                            }
                            float num = 300f * player.meleeDamage;
                            float num2 = 9f;
                            bool crit = false;
                            if (player.kbGlove)
                            {
                                num2 *= 2f;
                            }
                            if (player.kbBuff)
                            {
                                num2 *= 1.5f;
                            }
                            if (Main.rand.Next(100) < player.meleeCrit)
                            {
                                crit = true;
                            }
                            int direction = player.direction;
                            if (player.velocity.X < 0f)
                            {
                                direction = -1;
                            }
                            if (player.velocity.X > 0f)
                            {
                                direction = 1;
                            }
                            if (player.whoAmI == Main.myPlayer)
                            {
                                player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
                                int num3 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("AndromedaCounter"), 300, 15f, Main.myPlayer, 0f, 0f);
                                Main.projectile[num3].Kill();
                            }
                            nPC.immune[player.whoAmI] = 6;
                            player.immune = true;
                            player.immuneNoBlink = true;
                            player.immuneTime = 4;
                        }
                    }
                }
            }
        }
        private void DashMovement()
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.dashDelay > 0)
            {
                return;
            }
            if (player.dashDelay < 0)
            {
                float num = 12f;
                float num2 = 0.992f;
                float num3 = Math.Max(player.accRunSpeed, player.maxRunSpeed);
                float num4 = 0.96f;
                int dashDelay = 20;
                if (ModDash == 2)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int num5 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + 4f), player.width, player.height - 8, 172, 0f, 0f, 100, default(Color), 1.7f);
                        Main.dust[num5].velocity *= 0.1f;
                        Main.dust[num5].scale *= 1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num5].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                        Main.dust[num5].noGravity = true;
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[num5].fadeIn = 0.5f;
                        }
                    }
                    num = 14f;
                    num2 = 0.985f;
                    num4 = 0.94f;
                    dashDelay = 20;
                }
                else if (ModDash == 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int num5;
                        if (player.velocity.Y == 0f)
                        {
                            num5 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 172, 0f, 0f, 100, default(Color), 1.1f);
                        }
                        else
                        {
                            num5 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 172, 0f, 0f, 100, default(Color), 1.1f);
                        }
                        Main.dust[num5].velocity *= 0.1f;
                        Main.dust[num5].scale *= 1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num5].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                        Main.dust[num5].color = new Color(255, 255, 255);
                        Main.dust[num5].alpha = 100;
                        Main.dust[num5].noGravity = true;
                        //Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    }
                    num = 20f;
                    num2 = 0.985f;
                    num4 = 0.94f;
                    dashDelay = 20;
                }
                if (ModDash > 0)
                {
                    player.vortexStealthActive = false;
                    spiralStealthActive = false;
                    unconsciousStealthActive = false;
                    if (player.velocity.X > num || player.velocity.X < -num)
                    {
                        player.velocity.X = player.velocity.X * num2;
                        return;
                    }
                    if (player.velocity.X > num3 || player.velocity.X < -num3)
                    {
                        player.velocity.X = player.velocity.X * num4;
                        return;
                    }
                    player.dashDelay = dashDelay;
                    if (player.velocity.X < 0f)
                    {
                        player.velocity.X = -num3;
                        return;
                    }
                    if (player.velocity.X > 0f)
                    {
                        player.velocity.X = num3;
                        return;
                    }
                }
            }
            else
            {
                if (ModDash > 0 && !player.mount.Active)
                {
                    if (ModDash == 2)
                    {
                        int num6 = 0;
                        bool flag = false;
                        if (dashTime > 0)
                        {
                            dashTime--;
                        }
                        if (dashTime < 0)
                        {
                            dashTime++;
                        }
                        if (player.controlRight && player.releaseRight)
                        {
                            if (dashTime > 0)
                            {
                                num6 = 1;
                                flag = true;
                                dashTime = 0;
                                modPlayer.andromedaDashing = true;
                                modPlayer.andromedaDashConsumedFlare = false;
                            }
                            else
                            {
                                dashTime = 15;
                            }
                        }
                        else
                        {
                            if (player.controlLeft && player.releaseLeft)
                            {
                                if (dashTime < 0)
                                {
                                    num6 = -1;
                                    flag = true;
                                    dashTime = 0;
                                    modPlayer.andromedaDashing = true;
                                    modPlayer.andromedaDashConsumedFlare = false;
                                }
                                else
                                {
                                    dashTime = -15;
                                }
                            }
                        }
                        if (flag)
                        {
                            player.velocity.X = 21.9f * num6;
                            Point point = (player.Center + new Vector2(num6 * player.width / 2 + 2, player.gravDir * (-player.height) / 2f + player.gravDir * 2f)).ToTileCoordinates();
                            Point point2 = (player.Center + new Vector2(num6 * player.width / 2 + 2, 0f)).ToTileCoordinates();
                            if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                            {
                                player.velocity.X = player.velocity.X / 2f;
                            }
                            player.dashDelay = -1;
                            for (int num7 = 0; num7 < 20; num7++)
                            {
                                int num8 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 172, 0f, 0f, 100, default(Color), 2f);
                                Dust dust = Main.dust[num8];
                                dust.position.X = dust.position.X + Main.rand.Next(-5, 6);
                                Dust dust2 = Main.dust[num8];
                                dust2.position.Y = dust2.position.Y + Main.rand.Next(-5, 6);
                                Main.dust[num8].velocity *= 0.2f;
                                Main.dust[num8].scale *= 1f + Main.rand.Next(20) * 0.01f;
                                Main.dust[num8].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                                Main.dust[num8].noGravity = true;
                                Main.dust[num8].fadeIn = 0.5f;
                            }
                            return;
                        }
                    }
                    else if (ModDash == 1)
                    {
                        int num6 = 0;
                        bool flag = false;
                        if (dashTime > 0)
                        {
                            dashTime--;
                        }
                        if (dashTime < 0)
                        {
                            dashTime++;
                        }
                        if (player.controlRight && player.releaseRight)
                        {
                            if (dashTime > 0)
                            {
                                num6 = 1;
                                flag = true;
                                dashTime = 0;
                            }
                            else
                            {
                                dashTime = 20;
                            }
                        }
                        else
                        {
                            if (player.controlLeft && player.releaseLeft)
                            {
                                if (dashTime < 0)
                                {
                                    num6 = -1;
                                    flag = true;
                                    dashTime = 0;
                                }
                                else
                                {
                                    dashTime = -20;
                                }
                            }
                        }
                        if (flag)
                        {
                            player.immune = true;
                            player.immuneTime = 30;
                            InvincibleTimer = 30;
                            for (int i = 0; i < player.hurtCooldowns.Length; i++)
                            {
                                player.hurtCooldowns[i] = player.immuneTime;
                            }
                            player.velocity.X = 23.9f * num6;
                            Point point = (player.Center + new Vector2(num6 * player.width / 2 + 2, player.gravDir * -player.height / 2f + player.gravDir * 2f)).ToTileCoordinates();
                            Point point2 = (player.Center + new Vector2(num6 * player.width / 2 + 2, 0f)).ToTileCoordinates();
                            if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                            {
                                player.velocity.X = player.velocity.X / 2f;
                            }
                            player.dashDelay = -1;
                            for (int num7 = 0; num7 < 20; num7++)
                            {
                                int num8 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 172, 0f, 0f, 100, default(Color), 1.2f);
                                Dust dust = Main.dust[num8];
                                dust.position.X = dust.position.X + Main.rand.Next(-5, 6);
                                Dust dust2 = Main.dust[num8];
                                dust2.position.Y = dust2.position.Y + Main.rand.Next(-5, 6);
                                Main.dust[num8].velocity *= 0.2f;
                                Main.dust[num8].scale *= 0.9f + Main.rand.Next(20) * 0.01f;
                                Main.dust[num8].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                                //Main.dust[num8].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                                Main.dust[num8].alpha = 100;
                                Main.dust[num8].noGravity = true;
                            }
                            return;
                        }
                    }
                }
            }
        }
        public void Teleport(Vector2 newPos, int Style = 0, int extraInfo = 0)
        {
            try
            {
                player.grappling[0] = -1;
                player.grapCount = 0;
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].aiStyle == 7)
                    {
                        Main.projectile[i].Kill();
                    }
                }
                int extraInfo2 = 0;
                if (Style == 4)
                {
                    extraInfo2 = player.lastPortalColorIndex;
                }
                float num = MathHelper.Clamp(1f - player.teleportTime * 0.99f, 0.01f, 1f);
                TeleportEffect(player.getRect(), Style, extraInfo2, num);
                float num2 = Vector2.Distance(player.position, newPos);
                PressurePlateHelper.UpdatePlayerPosition(player);
                player.position = newPos;
                player.fallStart = (int)(player.position.Y / 16f);
                if (player.whoAmI == Main.myPlayer)
                {
                    bool flag = false;
                    if (num2 < new Vector2(Main.screenWidth, Main.screenHeight).Length() / 2f + 100f)
                    {
                        int time = 0;
                        if (Style == 1)
                        {
                            time = 10;
                        }
                        Main.SetCameraLerp(0.1f, time);
                        flag = true;
                    }
                    else
                    {
                        Main.BlackFadeIn = 255;
                        Lighting.BlackOut();
                        Main.screenLastPosition = Main.screenPosition;
                        Main.screenPosition.X = player.position.X + (player.width / 2) - (Main.screenWidth / 2);
                        Main.screenPosition.Y = player.position.Y + (player.height / 2) - (Main.screenHeight / 2);
                        Main.quickBG = 10;
                    }
                    if (num > 0.1f || !flag || Style != 0)
                    {
                        if (Main.mapTime < 5)
                        {
                            Main.mapTime = 5;
                        }
                        Main.maxQ = true;
                        Main.renderNow = true;
                    }
                }
                if (Style == 4)
                {
                    player.lastPortalColorIndex = extraInfo;
                    extraInfo2 = player.lastPortalColorIndex;
                    player.portalPhysicsFlag = true;
                    player.gravity = 0f;
                }
                PressurePlateHelper.UpdatePlayerPosition(player);
                for (int j = 0; j < 3; j++)
                {
                    player.UpdateSocialShadow();
                }
                player.oldPosition = player.position + player.BlehOldPositionFixer;
                TeleportEffect(player.getRect(), Style, extraInfo2, num);
                teleportTime = 1f;
                teleportStyle = Style;
            }
            catch
            {
            }
        }
        private void TeleportEffect(Rectangle effectRect, int Style, int extraInfo = 0, float dustCountMult = 1f)
        {
            if (Style == 0)//mechanism
            {
                Main.PlaySound(SoundID.Item8, effectRect.X + effectRect.Width / 2, effectRect.Y + effectRect.Height / 2);
                int num = effectRect.Width * effectRect.Height / 5;
                num = (int)(num * dustCountMult);
                for (int i = 0; i < num; i++)
                {
                    int num2 = Dust.NewDust(new Vector2(effectRect.X, effectRect.Y), effectRect.Width, effectRect.Height, 159, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num2].scale = Main.rand.Next(20, 70) * 0.01f;
                    if (i < 10)
                    {
                        Main.dust[num2].scale += 0.25f;
                    }
                    if (i < 5)
                    {
                        Main.dust[num2].scale += 0.25f;
                    }
                }
                return;
            }
            if (Style == 1)
            {
                Main.PlaySound(SoundID.Item8, effectRect.X + effectRect.Width / 2, effectRect.Y + effectRect.Height / 2);
                int num3 = effectRect.Width * effectRect.Height / 5;
                for (int j = 0; j < num3; j++)
                {
                    int num4 = Dust.NewDust(new Vector2(effectRect.X, effectRect.Y), effectRect.Width, effectRect.Height, 164, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num4].scale = Main.rand.Next(20, 70) * 0.01f;
                    if (j < 10)
                    {
                        Main.dust[num4].scale += 0.25f;
                    }
                    if (j < 5)
                    {
                        Main.dust[num4].scale += 0.25f;
                    }
                }
                return;
            }
            if (Style == 2)
            {
                for (int k = 0; k < 50; k++)
                {
                    Main.dust[Dust.NewDust(new Vector2(effectRect.X, effectRect.Y), effectRect.Width, effectRect.Height, 58, 0f, 0f, 150, Color.GhostWhite, 1.2f)].velocity *= 0.5f;
                }
                return;
            }
            if (Style == 3)//mirror
            {
                Main.PlaySound(SoundID.Item6, effectRect.X + effectRect.Width / 2, effectRect.Y + effectRect.Height / 2);
                for (int l = 0; l < 50; l++)
                {
                    int num5 = Dust.NewDust(new Vector2(effectRect.X, effectRect.Y), effectRect.Width, effectRect.Height, 180, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num5].noGravity = true;
                    for (int m = 0; m < 5; m++)
                    {
                        if (Main.rand.Next(3) == 0)
                        {
                            Main.dust[num5].velocity *= 0.75f;
                        }
                    }
                    if (Main.rand.Next(3) == 0)
                    {
                        Main.dust[num5].velocity *= 2f;
                        Main.dust[num5].scale *= 1.2f;
                    }
                    if (Main.rand.Next(3) == 0)
                    {
                        Main.dust[num5].velocity *= 2f;
                        Main.dust[num5].scale *= 1.2f;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num5].fadeIn = Main.rand.Next(75, 100) * 0.01f;
                        Main.dust[num5].scale = Main.rand.Next(25, 75) * 0.01f;
                    }
                    Main.dust[num5].scale *= 0.8f;
                }
                return;
            }
            if (Style == 4)
            {
                Main.PlaySound(SoundID.Item8, effectRect.X + effectRect.Width / 2, effectRect.Y + effectRect.Height / 2);
                int num6 = effectRect.Width * effectRect.Height / 5;
                num6 = (int)(num6 * dustCountMult);
                for (int n = 0; n < num6; n++)
                {
                    Dust dust = Main.dust[Dust.NewDust(effectRect.TopLeft(), effectRect.Width, effectRect.Height, 263, 0f, 0f, 0, default(Color), 1f)];
                    dust.color = PortalHelper.GetPortalColor(extraInfo);
                    dust.noLight = true;
                    dust.noGravity = true;
                    dust.scale = 1.2f;
                    dust.fadeIn = 0.4f;
                    dust.color.A = 255;
                }
            }
        }
        public void UpdateTeleportVisuals()
        {
            if (teleportTime > 0f)
            {
                if (teleportStyle == 0)
                {
                    if (Main.rand.Next(100) <= 100f * teleportTime * 2f)
                    {
                        int num = Dust.NewDust(new Vector2(player.getRect().X, player.getRect().Y), player.getRect().Width, player.getRect().Height, 159, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num].scale = teleportTime * 1.5f;
                        Main.dust[num].noGravity = true;
                        Main.dust[num].velocity *= 1.1f;
                    }
                }
                else
                {
                    if (teleportStyle == 1)
                    {
                        if (Main.rand.Next(100) <= 100f * teleportTime)
                        {
                            int num2 = Dust.NewDust(new Vector2(player.getRect().X, player.getRect().Y), player.getRect().Width, player.getRect().Height, 164, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num2].scale = teleportTime * 1.5f;
                            Main.dust[num2].noGravity = true;
                            Main.dust[num2].velocity *= 1.1f;
                        }
                    }
                    else
                    {
                        if (teleportStyle == 2)
                        {
                            teleportTime = 0.005f;
                        }
                        else
                        {
                            if (teleportStyle == 3)
                            {
                                teleportTime = 0.005f;
                            }
                            else
                            {
                                if (teleportStyle == 4)
                                {
                                    teleportTime -= 0.02f;
                                    if (Main.rand.Next(100) <= 100f * teleportTime)
                                    {
                                        Dust expr_249 = Main.dust[Dust.NewDust(player.position, player.width, player.height, 263, 0f, 0f, 0, default(Color), 1f)];
                                        expr_249.color = PortalHelper.GetPortalColor(player.lastPortalColorIndex);
                                        expr_249.noLight = true;
                                        expr_249.noGravity = true;
                                        expr_249.scale = 1.2f;
                                        expr_249.fadeIn = 0.4f;
                                    }
                                }
                            }
                        }
                    }
                }
                teleportTime -= 0.005f;
            }
        }
        private void Nothing()
        {
            player.ResetEffects();
            player.releaseMount = false;
            if (player.mount.Active)
            {
                player.mount.Dismount(player);
            }
            player.head = -1;
            player.body = -1;
            player.legs = -1;
            player.handon = -1;
            player.handoff = -1;
            player.back = -1;
            player.front = -1;
            player.shoe = -1;
            player.waist = -1;
            player.shield = -1;
            player.neck = -1;
            player.face = -1;
            player.balloon = -1;
            player.wingTime = -1;
            player.wingTimeMax = -1;
            player.dash = -1;
            ModDash = -1;
            Nothingness = true;
        }
        public override void ModifyScreenPosition()
        {
            if (!Main.gameMenu)
            {
                if (!Main.gamePaused && Main.hasFocus)
                {
                    if (shakeTime > 0)
                    {
                        Main.screenPosition.X = Main.screenPosition.X + Main.rand.Next(-15, 15);
                        Main.screenPosition.Y = Main.screenPosition.Y + Main.rand.Next(-15, 15);
                    }
                }
            }
        }
        #region Draw
        private static readonly Dictionary<int, Texture2D> ItemGlowMask = new Dictionary<int, Texture2D>();
        internal static void Unload()
        {
            ItemGlowMask.Clear();
        }
        public static void AddGlowMask(int itemType, string texturePath)
        {
            ItemGlowMask[itemType] = ModContent.GetTexture(texturePath);//0.11
            //ItemGlowMask[itemType] = ModLoader.GetTexture(texturePath);//0.10
        }
        public static void InsertAfterVanillaLayer(List<PlayerLayer> layers, string vanillaLayerName, PlayerLayer newPlayerLayer)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Name == vanillaLayerName && layers[i].mod == "Terraria")
                {
                    layers.Insert(i + 1, newPlayerLayer);
                    return;
                }
            }
            layers.Add(newPlayerLayer);
        }
        public static void DrawItemGlowMask(Texture2D texture, PlayerDrawInfo info)
        {
            Item item = info.drawPlayer.HeldItem;
            Color color = new Color(250, 250, 250, item.alpha);
            if (info.shadow != 0f || info.drawPlayer.frozen || ((info.drawPlayer.itemAnimation <= 0 || item.useStyle == 0) && (item.holdStyle <= 0 || info.drawPlayer.pulley)) || info.drawPlayer.dead || item.noUseGraphic || (info.drawPlayer.wet && item.noWet))
            {
                return;
            }
            Vector2 offset = new Vector2();
            float rotOffset = 0;
            Vector2 origin = new Vector2();
            if (item.useStyle == 5)
            {
                if (Item.staff[item.type])
                {
                    rotOffset = 0.785f * info.drawPlayer.direction;
                    if (info.drawPlayer.gravDir == -1f)
                    {
                        rotOffset -= 1.57f * info.drawPlayer.direction;
                    }
                    origin = new Vector2(texture.Width * 0.5f * (1 - info.drawPlayer.direction), (info.drawPlayer.gravDir == -1f) ? 0 : texture.Height);
                    int x = -(int)origin.X;
                    ItemLoader.HoldoutOrigin(info.drawPlayer, ref origin);
                    offset = new Vector2(origin.X + x, 0);
                }
                else
                {
                    offset = new Vector2(10, texture.Height / 2);
                    ItemLoader.HoldoutOffset(info.drawPlayer.gravDir, item.type, ref offset);
                    origin = new Vector2(-offset.X, texture.Height / 2);
                    if (info.drawPlayer.direction == -1)
                    {
                        origin.X = texture.Width + offset.X;
                    }
                    offset = new Vector2(texture.Width / 2, offset.Y);
                }
            }
            else
            {
                origin = new Vector2(texture.Width * 0.5f * (1 - info.drawPlayer.direction), (info.drawPlayer.gravDir == -1f) ? 0 : texture.Height);
            }
            Main.playerDrawData.Add(new DrawData(texture, info.itemLocation - Main.screenPosition + offset, texture.Bounds, color, info.drawPlayer.itemRotation + rotOffset, origin, item.scale, info.spriteEffects, 0));
        }
        public static readonly PlayerLayer Head = new PlayerLayer("SinsMod", "Head", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            SinsPlayer modPlayer = drawPlayer.GetModPlayer<SinsPlayer>();
            Mod mod = ModLoader.GetMod("SinsMod");
            float opacity = SinsGlow.DrawOpacity(drawPlayer);
            if (!drawPlayer.dead)
            {
                Texture2D texture = mod.GetTexture("Extra/Placeholder/BlankTex");
                Color color = Color.White * opacity;
                Vector2 Position = drawInfo.position;
                Vector2 origin = new Vector2(drawPlayer.legFrame.Width * 0.5f, drawPlayer.legFrame.Height * 0.5f);
                Vector2 pos = new Vector2((int)(Position.X - Main.screenPosition.X - (drawPlayer.bodyFrame.Width / 2) + (drawPlayer.width / 2)), (int)(Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2));
                if (drawPlayer.head == mod.GetEquipSlot("CleyeraMask", EquipType.Head))
                {
                    texture = mod.GetTexture("Glow/Armor/CleyeraMask_Head_Glow");
                    color = Color.White;
                    DrawData data = new DrawData(texture, pos, drawPlayer.bodyFrame, color, 0f, origin, 1f, drawInfo.spriteEffects, 0);
                    Main.playerDrawData.Add(data);
                    int value = Main.rand.Next(1, 3);
                    Vector2 vector = new Vector2(Main.rand.Next(-8, 8) * 0.2f, Main.rand.Next(-8, 8) * 0.2f);
                    color = new Color(160 - value * 10, 160 - value * 10, 160 - value * 10, 60 - value * 10);
                    DrawData data2 = new DrawData(texture, pos + vector, drawPlayer.bodyFrame, color, 0f, origin, 1f, drawInfo.spriteEffects, 0);
                    Main.playerDrawData.Add(data2);
                    return;
                }
                if (drawPlayer.head == mod.GetEquipSlot("MagellanicHelmet", EquipType.Head))
                {
                    texture = mod.GetTexture("Glow/Armor/MagellanicHelmet_Head_Glow");
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0) * opacity;
                }
                if (drawPlayer.head == mod.GetEquipSlot("MidnightHelm", EquipType.Head))
                {
                    texture = mod.GetTexture("Glow/Armor/MidnightHelm_Head_Glow");
                    color = new Color(255, 255, 255, 127) * opacity;
                }
                if (drawPlayer.head == mod.GetEquipSlot("MilkyWayHelmet", EquipType.Head))
                {
                    texture = mod.GetTexture("Glow/Armor/MilkyWayHelmet_Head_Glow");
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0) * opacity;
                }
                if (drawPlayer.head == mod.GetEquipSlot("NightfallHelm", EquipType.Head))
                {
                    texture = mod.GetTexture("Glow/Armor/NightfallHelm_Head_Glow");
                    color = new Color(255, 255, 255, 127) * opacity;
                }
                if (drawPlayer.head == mod.GetEquipSlot("NightmareHelm", EquipType.Head))
                {
                    texture = mod.GetTexture("Glow/Armor/NightmareHelm_Head_Glow");
                    color = new Color(255, 255, 255, 127) * opacity;
                }
                if (drawPlayer.head == mod.GetEquipSlot("SpiralHelmet", EquipType.Head))
                {
                    texture = mod.GetTexture("Glow/Armor/SpiralHelmet_Head_Glow");
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0) * opacity;
                }
                if (drawPlayer.head == mod.GetEquipSlot("TrueMidnightHelm", EquipType.Head))
                {
                    texture = mod.GetTexture("Glow/Armor/TrueMidnightHelm_Head_Glow");
                    color = new Color(255, 255, 255, 127) * opacity;
                }
                if (drawPlayer.head == mod.GetEquipSlot("WhiteNightHelm", EquipType.Head))
                {
                    texture = mod.GetTexture("Glow/Armor/WhiteNightHelm_Head_Glow");
                    color = new Color(255, 255, 255, 127) * opacity;
                }
                DrawData drawData = new DrawData(texture, pos, drawPlayer.bodyFrame, color, 0f, origin, 1f, drawInfo.spriteEffects, 0);
                drawData.shader = drawInfo.headArmorShader;
                if (texture != mod.GetTexture("Extra/Placeholder/BlankTex"))
                {
                    Main.playerDrawData.Add(drawData);
                }
            }
        });
        public static readonly PlayerLayer Body = new PlayerLayer("SinsMod", "Body", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            SinsPlayer modPlayer = drawPlayer.GetModPlayer<SinsPlayer>();
            Mod mod = ModLoader.GetMod("SinsMod");
            float opacity = SinsGlow.DrawOpacity(drawPlayer);
            if (!drawPlayer.dead)
            {
                Texture2D texture = mod.GetTexture("Extra/Placeholder/BlankTex");
                Color color = Color.White * opacity;
                if (drawPlayer.body == mod.GetEquipSlot("MagellanicBreastplate", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/MagellanicBreastplate_Body_Glow");
                    if (!drawPlayer.Male)
                    {
                        texture = mod.GetTexture("Glow/Armor/MagellanicBreastplate_FemaleBody_Glow");
                    }
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0);
                }
                if (drawPlayer.body == mod.GetEquipSlot("MidnightArmor", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/MidnightArmor_Body_Glow");
                    if (!drawPlayer.Male)
                    {
                        texture = mod.GetTexture("Glow/Armor/MidnightArmor_FemaleBody_Glow");
                    }
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.body == mod.GetEquipSlot("MilkyWayPlate", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/MilkyWayPlate_Body_Glow");
                    if (!drawPlayer.Male)
                    {
                        texture = mod.GetTexture("Glow/Armor/MilkyWayPlate_FemaleBody_Glow");
                    }
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0);
                }
                if (drawPlayer.body == mod.GetEquipSlot("NightmareArmor", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/NightmareArmor_Body_Glow");
                    if (!drawPlayer.Male)
                    {
                        texture = mod.GetTexture("Glow/Armor/NightmareArmor_FemaleBody_Glow");
                    }
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.body == mod.GetEquipSlot("NightfallArmor", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/NightfallArmor_Body_Glow");
                    if (!drawPlayer.Male)
                    {
                        texture = mod.GetTexture("Glow/Armor/NightfallArmor_FemaleBody_Glow");
                    }
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.body == mod.GetEquipSlot("SpiralBreastplate", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/SpiralBreastplate_Body_Glow");
                    if (!drawPlayer.Male)
                    {
                        texture = mod.GetTexture("Glow/Armor/SpiralBreastplate_FemaleBody_Glow");
                    }
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0);
                }
                if (drawPlayer.body == mod.GetEquipSlot("TrueMidnightArmor", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/TrueMidnightArmor_Body_Glow");
                    if (!drawPlayer.Male)
                    {
                        texture = mod.GetTexture("Glow/Armor/TrueMidnightArmor_FemaleBody_Glow");
                    }
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.body == mod.GetEquipSlot("WhiteNightArmor", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/WhiteNightArmor_Body_Glow");
                    if (!drawPlayer.Male)
                    {
                        texture = mod.GetTexture("Glow/Armor/WhiteNightArmor_FemaleBody_Glow");
                    }
                    color = new Color(255, 255, 255, 127);
                }
                Vector2 Position = drawInfo.position;
                Vector2 origin = new Vector2(drawPlayer.legFrame.Width * 0.5f, drawPlayer.legFrame.Height * 0.5f);
                Vector2 pos = new Vector2((int)(Position.X - Main.screenPosition.X - (drawPlayer.bodyFrame.Width / 2) + (drawPlayer.width / 2)), (int)(Position.Y - Main.screenPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2);
                DrawData drawData = new DrawData(texture, pos, drawPlayer.bodyFrame, color * opacity, 0f, origin, 1f, drawInfo.spriteEffects, 0);
                drawData.shader = drawInfo.bodyArmorShader;
                if (texture != mod.GetTexture("Extra/Placeholder/BlankTex"))
                {
                    Main.playerDrawData.Add(drawData);
                }
            }
        });
        public static readonly PlayerLayer Arms = new PlayerLayer("SinsMod", "Arms", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            SinsPlayer modPlayer = drawPlayer.GetModPlayer<SinsPlayer>();
            Mod mod = ModLoader.GetMod("SinsMod");
            float opacity = SinsGlow.DrawOpacity(drawPlayer);
            if (!drawPlayer.dead)
            {
                Texture2D texture = mod.GetTexture("Extra/Placeholder/BlankTex");
                Color color = Color.White * opacity;
                if (drawPlayer.body == mod.GetEquipSlot("MagellanicBreastplate", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/MagellanicBreastplate_Arms_Glow");
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0);
                }
                if (drawPlayer.body == mod.GetEquipSlot("MidnightArmor", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/MidnightArmor_Arms_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.body == mod.GetEquipSlot("MilkyWayPlate", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/MilkyWayPlate_Arms_Glow");
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0);
                }
                if (drawPlayer.body == mod.GetEquipSlot("NightfallArmor", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/NightfallArmor_Arms_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.body == mod.GetEquipSlot("TrueMidnightArmor", EquipType.Body))
                {
                    texture = mod.GetTexture("Glow/Armor/TrueMidnightArmor_Arms_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                Vector2 Position = drawInfo.position;
                Vector2 origin = new Vector2(drawPlayer.legFrame.Width * 0.5f, drawPlayer.legFrame.Height * 0.5f);
                Vector2 pos = new Vector2((int)(Position.X - Main.screenPosition.X - (drawPlayer.bodyFrame.Width / 2) + (drawPlayer.width / 2)), (int)(Position.Y - Main.screenPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2);
                DrawData drawData = new DrawData(texture, pos, drawPlayer.bodyFrame, color * opacity, 0f, origin, 1f, drawInfo.spriteEffects, 0);
                drawData.shader = drawInfo.bodyArmorShader;
                if (texture != mod.GetTexture("Extra/Placeholder/BlankTex"))
                {
                    Main.playerDrawData.Add(drawData);
                }
            }
        });
        public static readonly PlayerLayer Legs = new PlayerLayer("SinsMod", "Legs", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            SinsPlayer modPlayer = drawPlayer.GetModPlayer<SinsPlayer>();
            Mod mod = ModLoader.GetMod("SinsMod");
            float opacity = SinsGlow.DrawOpacity(drawPlayer);
            if (!drawPlayer.dead)
            {
                Texture2D texture = mod.GetTexture("Extra/Placeholder/BlankTex");
                Color color = Color.White * opacity;
                if (drawPlayer.legs == mod.GetEquipSlot("MagellanicLeggings", EquipType.Legs))
                {
                    texture = mod.GetTexture("Glow/Armor/MagellanicLeggings_Legs_Glow");
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0);
                }
                if (drawPlayer.legs == mod.GetEquipSlot("MidnightLeggings", EquipType.Legs))
                {
                    texture = mod.GetTexture("Glow/Armor/MidnightLeggings_Legs_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.legs == mod.GetEquipSlot("MilkyWayLeggings", EquipType.Legs))
                {
                    texture = mod.GetTexture("Glow/Armor/MilkyWayLeggings_Legs_Glow");
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0);
                }
                if (drawPlayer.legs == mod.GetEquipSlot("NightfallLeggings", EquipType.Legs))
                {
                    texture = mod.GetTexture("Glow/Armor/NightfallLeggings_Legs_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.legs == mod.GetEquipSlot("NightmareLeggings", EquipType.Legs))
                {
                    texture = mod.GetTexture("Glow/Armor/NightmareLeggings_Legs_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.legs == mod.GetEquipSlot("SpiralLeggings", EquipType.Legs))
                {
                    texture = mod.GetTexture("Glow/Armor/SpiralLeggings_Legs_Glow");
                    color = new Color((byte)62.5f, (byte)62.5f, (byte)62.5f, 0);
                }
                if (drawPlayer.legs == mod.GetEquipSlot("TrueMidnightLeggings", EquipType.Legs))
                {
                    texture = mod.GetTexture("Glow/Armor/TrueMidnightLeggings_Legs_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                Vector2 Position = drawInfo.position;
                Vector2 origin = new Vector2(drawPlayer.legFrame.Width * 0.5f, drawPlayer.legFrame.Height * 0.5f);
                Vector2 pos = new Vector2((int)(Position.X - Main.screenPosition.X - (drawPlayer.bodyFrame.Width / 2) + (drawPlayer.width / 2)), (int)(Position.Y - Main.screenPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2);
                DrawData drawData = new DrawData(texture, pos, drawPlayer.legFrame, color * opacity, 0f, origin, 1f, drawInfo.spriteEffects, 0);
                drawData.shader = drawInfo.legArmorShader;
                if (texture != mod.GetTexture("Extra/Placeholder/BlankTex"))
                {
                    Main.playerDrawData.Add(drawData);
                }
            }
        });
        public static readonly PlayerLayer Wings = new PlayerLayer("SinsMod", "Wings", PlayerLayer.Wings, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("SinsMod");
            bool drawBehind = true;
            float opacity = SinsGlow.ShadowOpacity(drawPlayer);
            if (!drawPlayer.dead)
            {
                float num = 1f;
                float num2 = drawPlayer.stealth;
                if (num2 < 0.03)
                {
                    num2 = 0.03f;
                }
                float num3 = (1f + num2 * 10f) / 11f;
                if (num2 < 0f)
                {
                    num2 = 0f;
                }
                if (num2 >= 1f - drawPlayer.shadow && drawPlayer.shadow > 0f)
                {
                    num2 = drawPlayer.shadow * 0.5f;
                }
                num = num3;
                int num4 = 0;
                int num5 = 0;
                Texture2D texture = mod.GetTexture("Extra/Placeholder/BlankTex");
                Color color = Color.White;
                if (drawPlayer.wings == mod.GetEquipSlot("AndromedaWings", EquipType.Wings))
                {
                    texture = mod.GetTexture("Glow/Equippable/AndromedaWings_Wings_Glow");
                    color = new Color(250, 250, 250, 127);
                }
                if (drawPlayer.wings == mod.GetEquipSlot("MagellanicMantle", EquipType.Wings))
                {
                    color = new Color(255, 255, 255, 0);
                    color = Color.Lerp(Color.HotPink, Color.Crimson, (float)Math.Cos(6.28318548f * (drawPlayer.miscCounter / 100f)) * 0.4f + 0.5f);
                    Color color2 = new Color(255, 255, 255, 0);
                    color2 = Color.Lerp(Color.HotPink, Color.Crimson, (float)Math.Cos(6.28318548f * (drawPlayer.miscCounter / 100f)) * 0.4f + 0.5f);
                    color2.A = (byte)(220f * num);
                    texture = mod.GetTexture("Items/Equippable/Wings/MagellanicMantle_Wings");
                    Texture2D glow = mod.GetTexture("Glow/Equippable/MagellanicMantle_Wings_Glow");
                    Vector2 Position2 = drawInfo.position;
                    Vector2 origin2 = new Vector2(texture.Width / 2, texture.Height / 8);
                    Vector2 pos2 = new Vector2((int)(Position2.X - Main.screenPosition.X + (drawPlayer.width / 2) - (9 * drawPlayer.direction)) + num5 * drawPlayer.direction, (int)(Position2.Y - Main.screenPosition.Y + (drawPlayer.height / 2) + 2f * drawPlayer.gravDir + num4 * drawPlayer.gravDir));
                    DrawData data = new DrawData(texture, pos2, new Rectangle(0, texture.Height / 4 * drawPlayer.wingFrame, texture.Width, texture.Height / 4), color * opacity * 1f, 0f, origin2, 1f, drawInfo.spriteEffects, 0);
                    data.shader = drawInfo.wingShader;
                    Main.playerDrawData.Add(data);
                    data = new DrawData(glow, pos2, new Rectangle(0, glow.Height / 4 * drawPlayer.wingFrame, glow.Width, glow.Height / 4), color2 * opacity * 0.2f, 0f, origin2, 1f, drawInfo.spriteEffects, 0);
                    data.shader = drawInfo.wingShader;
                    Main.playerDrawData.Add(data);
                    return;
                }
                if (drawPlayer.wings == mod.GetEquipSlot("MidnightPowerOrb", EquipType.Wings))
                {
                    texture = mod.GetTexture("Glow/Equippable/MidnightPowerOrb_Wings_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.wings == mod.GetEquipSlot("MilkyWayWings", EquipType.Wings))
                {
                    texture = mod.GetTexture("Glow/Equippable/MilkyWayWings_Wings_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                if (drawPlayer.wings == mod.GetEquipSlot("NightmareSoul", EquipType.Wings))
                {
                    texture = mod.GetTexture("Glow/Equippable/NightmareSoul_Wings_Glow");
                    color = SinsColor.NightmareRed;
                }
                if (drawPlayer.wings == mod.GetEquipSlot("SpiralBooster", EquipType.Wings))
                {
                    texture = mod.GetTexture("Glow/Equippable/SpiralBooster_Wings_Glow");
                    color = new Color(255, 255, 255, 127);
                }
                Vector2 Position = drawInfo.position;
                Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 8);
                Vector2 pos = new Vector2((int)(Position.X - Main.screenPosition.X + (drawPlayer.width / 2) - (9 * drawPlayer.direction)) + num5 * drawPlayer.direction, (int)(Position.Y - Main.screenPosition.Y + (drawPlayer.height / 2) + 2f * drawPlayer.gravDir + num4 * drawPlayer.gravDir));
                DrawData drawData = new DrawData(texture, pos, new Rectangle(0, texture.Height / 4 * drawPlayer.wingFrame, texture.Width, texture.Height / 4), color * SinsGlow.ShadowOpacity(drawPlayer), 0f, origin, 1f, drawInfo.spriteEffects, 0); drawData.shader = drawInfo.wingShader;
                drawData.shader = drawInfo.wingShader;
                if (texture != mod.GetTexture("Extra/Placeholder/BlankTex"))
                {
                    Main.playerDrawData.Add(drawData);
                    if (drawBehind && (drawPlayer.wings == mod.GetEquipSlot("NightmareSoul", EquipType.Wings) || drawPlayer.wings == mod.GetEquipSlot("MidnightPowerOrb", EquipType.Wings)))
                    {
                        for (int i = drawPlayer.shadowPos.Length - 2; i >= 0; i--)
                        {
                            int num6 = 0;
                            int num7 = 0;
                            Color color2 = color * SinsGlow.ShadowOpacity(drawPlayer);
                            color2.A = 0;
                            color2 *= MathHelper.Lerp(1f, 0f, i / 3f);
                            color2 *= 0.1f;
                            Vector2 pos2 = drawPlayer.shadowPos[i] - drawPlayer.position;
                            for (float j = 0f; j < 1f; j += 0.01f)
                            {
                                Vector2 value = new Vector2(2f, 0f).RotatedBy(j / 0.04f * 6.28318548f, default(Vector2));
                                drawData = new DrawData(texture, value + pos2 * j + new Vector2((int)(Position.X - Main.screenPosition.X + (drawPlayer.width / 2) - (9 * drawPlayer.direction)) + num7 * drawPlayer.direction, (int)(Position.Y - Main.screenPosition.Y + (drawPlayer.height / 2) + 2f * drawPlayer.gravDir + num6 * drawPlayer.gravDir)), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, Main.wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, Main.wingsTexture[drawPlayer.wings].Width, Main.wingsTexture[drawPlayer.wings].Height / 4)), color2 * (1f - drawPlayer.shadow), drawPlayer.bodyRotation, new Vector2(Main.wingsTexture[drawPlayer.wings].Width / 2, Main.wingsTexture[drawPlayer.wings].Height / 8), 1f, drawInfo.spriteEffects, 0);
                                drawData.shader = drawInfo.wingShader;
                                Main.playerDrawData.Add(drawData);
                            }
                        }
                    }
                }
            }
        });
        public static readonly PlayerLayer BalloonAcc = new PlayerLayer("SinsMod", "BalloonAcc", PlayerLayer.BalloonAcc, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            SinsPlayer modPlayer = drawPlayer.GetModPlayer<SinsPlayer>();
            Mod mod = ModLoader.GetMod("SinsMod");
            float opacity = SinsGlow.DrawOpacity(drawPlayer);
            Vector2 Position = drawInfo.position;
            float num = drawPlayer.mount.PlayerOffset;
            Position.Y += num / 2;
            if (!drawPlayer.dead)
            {
                Texture2D texture = mod.GetTexture("Extra/Placeholder/BlankTex");
                Color color = Lighting.GetColor((int)(Position.X + drawPlayer.width * 0.5) / 16, (int)((Position.Y + drawPlayer.height * 0.5) / 16.0));
                if (drawPlayer.miscEquips[4].type == mod.ItemType("UnconsciousEye"))
                {
                    texture = mod.GetTexture("Extra/Player/UnconsciousEye_Balloon");
                }
                int num2 = DateTime.Now.Millisecond % 800 / 200;
                Vector2 vector = Main.OffsetsPlayerOffhand[drawPlayer.bodyFrame.Y / 56];
                if (drawPlayer.direction != 1)
                {
                    vector.X = drawPlayer.width - vector.X;
                }
                if (drawPlayer.gravDir != 1f)
                {
                    vector.Y -= drawPlayer.height;
                }
                Vector2 origin = new Vector2(26 + drawPlayer.direction * 4, 28f + drawPlayer.gravDir * 6f);
                Vector2 pos = new Vector2((int)(Position.X - Main.screenPosition.X + vector.X), (int)(Position.Y - Main.screenPosition.Y + vector.Y * drawPlayer.gravDir));
                DrawData drawData = new DrawData(texture, pos, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, texture.Height / 4 * num2, texture.Width, texture.Height / 4)), color * opacity, drawPlayer.bodyRotation, origin, 1f, drawInfo.spriteEffects, 0);
                drawData.shader = drawPlayer.miscEquips[4].type != mod.ItemType("UnconsciousEye") ? drawPlayer.cBalloon : drawPlayer.cGrapple;
                if (texture != mod.GetTexture("Extra/Placeholder/BlankTex"))
                {
                    Main.playerDrawData.Add(drawData);
                }
            }
            Position.Y -= num / 2;
        });
        public static readonly PlayerLayer MiscEffects = new PlayerLayer("SinsMod", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("SinsMod");
            if (!drawPlayer.dead)
            {
                /*Vector2 Position = drawInfo.position;
                PlayerDrawInfo playerDrawInfo = default(PlayerDrawInfo);
                Color color = Lighting.GetColor((int)(Position.X + drawPlayer.width * 0.5) / 16, (int)((Position.Y + drawPlayer.height * 0.5) / 16.0));
                if (drawPlayer.inventory[drawPlayer.selectedItem].type == mod.ItemType("LunarArcanum"))
                {
                    Vector2 vector = Position + (drawPlayer.itemLocation - drawPlayer.position);
                    playerDrawInfo.itemLocation = vector;
                    Texture2D texture2D4 = Main.extraTexture[64];
                    Rectangle rectangle = texture2D4.Frame(1, 9, 0, drawPlayer.miscCounter % 54 / 6);
                    Vector2 value3 = new Vector2(rectangle.Width / 2 * drawPlayer.direction, 0f);
                    Vector2 origin = rectangle.Size() / 2f;
                    DrawData drawData = new DrawData(texture2D4, (vector - Main.screenPosition + value3).Floor(), new Microsoft.Xna.Framework.Rectangle?(rectangle), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color).MultiplyRGBA(new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.8f))), drawPlayer.itemRotation, origin, drawPlayer.inventory[drawPlayer.selectedItem].scale, drawInfo.spriteEffects, 0);
                    Main.playerDrawData.Add(drawData);
                    texture2D4 = Main.glowMaskTexture[195];
                    drawData = new DrawData(texture2D4, (vector - Main.screenPosition + value3).Floor(), new Microsoft.Xna.Framework.Rectangle?(rectangle), new Color(250, 250, 250, drawPlayer.inventory[drawPlayer.selectedItem].alpha) * 0.5f, drawPlayer.itemRotation, origin, drawPlayer.inventory[drawPlayer.selectedItem].scale, drawInfo.spriteEffects, 0);
                    Main.playerDrawData.Add(drawData);
                }*/
            }
        });
        public static readonly PlayerLayer SolarShield = new PlayerLayer("SinsMod", "AndromedaShield", PlayerLayer.SolarShield, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            SinsPlayer modPlayer = drawPlayer.GetModPlayer<SinsPlayer>();
            Mod mod = ModLoader.GetMod("SinsMod");
            if (modPlayer.andromedaShields > 0 && !drawPlayer.dead && modPlayer.setAndromeda)
            {
                Vector2 Position = drawInfo.position;
                Texture2D texture = modPlayer.andromedaShields == 1 ? mod.GetTexture("Extra/Player/AndromedaShield1") : (modPlayer.andromedaShields == 2 ? mod.GetTexture("Extra/Player/AndromedaShield2") : (modPlayer.andromedaShields == 3 ? mod.GetTexture("Extra/Player/AndromedaShield3") : mod.GetTexture("Extra/Placeholder/BlankTex")));
                Color color = new Color(255, 255, 255, 127);
                float num = (modPlayer.andromedaShieldPos[0] * new Vector2(1f, 0.5f)).ToRotation();
                if (drawPlayer.direction == -1)
                {
                    num += 3.14159274f;
                }
                num += 0.06283186f * drawPlayer.direction;
                DrawData drawData = new DrawData(texture, new Vector2((int)(Position.X - Main.screenPosition.X + (drawPlayer.width / 2)), (int)(Position.Y - Main.screenPosition.Y + (drawPlayer.height / 2))) + modPlayer.andromedaShieldPos[0], null, color, num, texture.Size() / 2f, 1f, drawInfo.spriteEffects, 0);
                drawData.shader = drawInfo.bodyArmorShader;
                if (texture != mod.GetTexture("Extra/Placeholder/BlankTex"))
                {
                    Main.playerDrawData.Add(drawData);
                }
            }
        });
        public static readonly PlayerLayer abilityEffect = new PlayerLayer("SinsMod", "abilityEffect", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            /*Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("SinsMod");
            SinsPlayer modPlayer = drawPlayer.GetModPlayer<SinsPlayer>();
            if (modPlayer.ability > 0)
            {
                int num = Main.rand.Next(20);
                if (Main.rand.Next(20) == 0 && lineTopLeft[num].Y <= -20)
                {
                    lineTopLeft[Main.rand.Next(20)] = new Point16(Main.rand.Next(1, drawPlayer.width), drawPlayer.height);
                }
                for (int i = 0; i < 20; i++)
                {
                    int x = (int)lineTopLeft[i].X;
                    int y = (int)lineTopLeft[i].Y;
                    if (y >= -20)
                    {
                        lineTopLeft[i] = new Point16(x, y - 1);
                    }
                    if (x != 0 && !drawPlayer.invis)
                    {
                        int num2 = (drawPlayer.height - y) / 2;
                        num2 = (num2 >= 10) ? 10 : num2;
                        Color color = Ability.GetColorFromAbilityID(modPlayer.ability);
                        int num3 = 180;
                        for (int j = 0; j < num2; j++)
                        {
                            if (y + j * 2 >= 0)
                            {
                                color.A = (byte)(num3 - j * 16);
                                DrawData drawData = new DrawData(Main.magicPixel, new Vector2((float)(int)(drawPlayer.position.X - Main.screenPosition.X + (float)x), drawPlayer.position.Y - Main.screenPosition.Y + (float)y + (float)(j * 2)), new Rectangle?(new Rectangle(0, 0, 1, 2)), color);
                                Main.playerDrawData.Add(drawData);
                                drawData = new DrawData(Main.magicPixel, new Vector2((float)(int)(drawPlayer.position.X - Main.screenPosition.X + (float)x + 1f), drawPlayer.position.Y - Main.screenPosition.Y + (float)y + (float)(j * 2)), new Rectangle?(new Rectangle(0, 0, 1, 2)), color);
                                Main.playerDrawData.Add(drawData);
                            }
                        }
                    }
                }
            }*/
        });
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (player.HeldItem.type >= ItemID.Count && ItemGlowMask.TryGetValue(player.HeldItem.type, out Texture2D textureItem))
            {
                InsertAfterVanillaLayer(layers, "HeldItem", new PlayerLayer(mod.Name, "GlowMaskHeldItem", delegate (PlayerDrawInfo info)
                {
                    DrawItemGlowMask(textureItem, info);
                }));
            }
            int headLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("Head"));
            if (headLayer != -1)
            {
                Head.visible = true;
                layers.Insert(headLayer + 1, Head);
            }
            int bodyLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("Body"));
            if (bodyLayer != -1)
            {
                Body.visible = true;
                layers.Insert(bodyLayer + 1, Body);
            }
            int armLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("Arms"));
            if (armLayer != -1)
            {
                Arms.visible = true;
                layers.Insert(armLayer + 1, Arms);
            }
            int legLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("Legs"));
            if (legLayer != -1)
            {
                Legs.visible = true;
                layers.Insert(legLayer + 1, Legs);
            }
            int wingLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("Wings"));
            if (wingLayer != -1)
            {
                Wings.visible = true;
                layers.Insert(wingLayer + 1, Wings);
            }
            BalloonAcc.visible = true;
            layers.Insert(8, BalloonAcc);
            MiscEffects.visible = true;
            layers.Add(MiscEffects);
            SolarShield.visible = true;
            layers.Add(SolarShield);
            abilityEffect.visible = true;
            layers.Add(abilityEffect);
        }
        #endregion
    }
}