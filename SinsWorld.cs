using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace SinsMod
{
    public class SinsWorld : ModWorld
    {
        //Mod mod = ModLoader.GetMod("SinsMod");
        //Gen
        internal static bool Large;
        internal static bool Midium;
        internal static bool Small;
        //Mode
        public static bool LimitCut;
        public static bool Hopeless;
        public static bool SinsBossRush;
        //Event
        public static bool EclipsePassed;
        public static bool PumpkinMoonPassed;
        public static bool SnowMoonPassed;
        internal static bool MadnessField;
        public static int NightEnergyDropped;
        //X
        public static int MemoryOfX = 1;
        public static bool downedX;
        //Boss
        public static bool downedKingMetalSlime;
        public static bool downedEnvy;
        public static bool downedGluttony;
        public static bool downedGreed;
        public static bool downedLust;
        public static bool downedPride;
        public static bool downedSloth;
        public static bool downedWrath;
        public static bool downedSins;
        public static bool downedOrigin;
        public static bool downedMadness;
        public static bool downedAcedia;
        public static bool downedVain;
        public static bool downedTartarus;
        public static bool downedLunarEye;
        //MiniBoss
        //TileCount
        public static int NightEnergyTiles;
        public static int MysticTiles;
        public static int DistortionTiles;
        public static int TartarusTiles;

        public override void Initialize()
        {
            //Gen
            Large = Main.maxTilesY >= 2400;
            Midium = Main.maxTilesY >= 1800 && Main.maxTilesY < 2400;
            Small = Main.maxTilesY >= 1200 && Main.maxTilesY < 1800;
            //Mode
            Hopeless = false;
            SinsBossRush = false;
            //Event
            EclipsePassed = false;
            PumpkinMoonPassed = false;
            SnowMoonPassed = false;
            MadnessField = false;
            NightEnergyDropped = 0;
            //X
            MemoryOfX = 1;
            downedX = false;
            //Boss
            downedKingMetalSlime = false;
            downedEnvy = false;
            downedGluttony = false;
            downedGreed = false;
            downedLust = false;
            downedPride = false;
            downedSloth = false;
            downedWrath = false;
            downedSins = false;
            downedMadness = false;
            downedAcedia = false;
            downedVain = false;
            downedTartarus = false;
            downedLunarEye = false;
            //MiniBoss
        }
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            //Gen
            //Mode
            tag["LimitCut"] = LimitCut;
            tag["Hopeless"] = Hopeless;
            //Event
            tag["EclipsePassed"] = EclipsePassed;
            tag["PumpkinMoonPassed"] = PumpkinMoonPassed;
            tag["SnowMoonPassed"] = SnowMoonPassed;
            tag["NightEnergyDropped"] = NightEnergyDropped;
            //X
            tag["MemoryOfX"] = MemoryOfX;
            tag["downedX"] = downedX;
            //Boss
            tag["downedKingMetalSlime"] = downedKingMetalSlime;
            tag["downedEnvy"] = downedEnvy;
            tag["downedGluttony"] = downedGluttony;
            tag["downedGreed"] = downedGreed;
            tag["downedLust"] = downedLust;
            tag["downedPride"] = downedPride;
            tag["downedSloth"] = downedSloth;
            tag["downedWrath"] = downedWrath;
            tag["downedSins"] = downedSins;
            tag["downedOrigin"] = downedOrigin;
            tag["downedMadness"] = downedMadness;
            tag["downedAcedia"] = downedAcedia;
            tag["downedVain"] = downedVain;
            tag["downedTartarus"] = downedTartarus;
            tag["downedLunarEye"] = downedLunarEye;
            //MiniBoss
            return tag;
        }
        public override void Load(TagCompound tag)
        {
            //Gen
            //Mode
            LimitCut = tag.GetBool("LimitCut");
            Hopeless = tag.GetBool("Hopeless");
            //Event
            EclipsePassed = tag.GetBool("EclipsePassed");
            PumpkinMoonPassed = tag.GetBool("PumpkinMoonPassed");
            SnowMoonPassed = tag.GetBool("SnowMoonPassed");
            NightEnergyDropped = tag.GetInt("NightEnergyDropped");
            //X
            MemoryOfX = tag.GetInt("MemoryOfX");
            downedX = tag.GetBool("downedX");
            //Boss
            downedKingMetalSlime = tag.GetBool("downedKingMetalSlime");
            downedEnvy = tag.GetBool("downedEnvy");
            downedGluttony = tag.GetBool("downedGluttony");
            downedGreed = tag.GetBool("downedGreed");
            downedLust = tag.GetBool("downedLust");
            downedPride = tag.GetBool("downedPride");
            downedSloth = tag.GetBool("downedSloth");
            downedWrath = tag.GetBool("downedWrath");
            downedSins = tag.GetBool("downedSins");
            downedOrigin = tag.GetBool("downedOrigin");
            downedMadness = tag.GetBool("downedMadness");
            downedAcedia = tag.GetBool("downedAcedia");
            downedVain = tag.GetBool("downedVain");
            downedTartarus = tag.GetBool("downedTartarus");
            downedLunarEye = tag.GetBool("downedLunarEye");
            //MiniBoss
        }
        public override void ResetNearbyTileEffects()
        {
            SinsPlayer modPlayer = Main.LocalPlayer.GetModPlayer<SinsPlayer>();
            NightEnergyTiles = 0;
            MysticTiles = 0;
            DistortionTiles = 0;
            TartarusTiles = 0;
        }
        public override void TileCountsAvailable(int[] tileCounts)
        {
            NightEnergyTiles = tileCounts[mod.TileType("NightEnergizedOre")];
            MysticTiles = tileCounts[mod.TileType("MysticGrass")] + tileCounts[mod.TileType("MysticStone")] + tileCounts[mod.TileType("MysticOre")];
            DistortionTiles = tileCounts[mod.TileType("DistortionGrass")] + tileCounts[mod.TileType("DistortionStone")];
            TartarusTiles = tileCounts[mod.TileType("TartarusBedrock")];
        }
        public override void PreUpdate()
        {
            
        }
        public override void PostUpdate()
        {
            MadnessField = NPC.AnyNPCs(mod.NPCType("BlackCrystalNoMove"));
            /*while (MemoryOfX > 100 || MemoryOfX < 1)
            { }*/
            if (Main.eclipse)
            {
                EclipsePassed = true;
            }
            if (Main.pumpkinMoon && NPC.waveNumber >= 15)
            {
                PumpkinMoonPassed = true;
            }
            if (Main.snowMoon && NPC.waveNumber >= 15)
            {
                SnowMoonPassed = true;
            }
            if (downedEnvy && downedGluttony && downedGreed && downedLust && downedPride && downedSloth && downedWrath)
            {
                downedSins = true;
            }
            if (MemoryOfX > 9)
            {
                downedX = true;
            }
        }
        public override void PostWorldGen()
        {
            // Place some items in Ice Chests
            int[] itemsToPlaceInWaterChests = new int[] { mod.ItemType("IceShortsword") };
            int itemsToPlaceInWaterChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 11 * 36 && WorldGen.genRand.Next(4) == 0)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == 0)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInWaterChests[itemsToPlaceInWaterChestsChoice]);
                            itemsToPlaceInWaterChestsChoice = (itemsToPlaceInWaterChestsChoice + 1) % itemsToPlaceInWaterChests.Length;
                            break;
                        }
                    }
                }
            }
            for (int i = Main.maxTilesY - 220; i < Main.maxTilesY; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    DelLiquid(j, i);
                }
            }
        }
        private void DelLiquid(int i, int j)
        {
            Main.tile[i, j].lava(false);
            Main.tile[i, j].liquid = 0;
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Final Cleanup"));
            /*if (WorldGen.crimson)
            {
                tasks.Insert(num + 1, new PassLegacy("Mystic Biome", delegate (GenerationProgress progress)
                {
                    progress.Message = "Mystic Biome Progress...";
                    MysticBiomeGen();
                }));
            }
            else
            {
                tasks.Insert(num + 1, new PassLegacy("Distortion Biome", delegate (GenerationProgress progress)
                {
                    progress.Message = "Distortion Biome Progress...";
                    DistortionBiomeGen();
                }));
            }*/
            int extraTask = 1 + (SinsMod.Instance.CalamityLoaded ? 2 : 0) + (SinsMod.Instance.ChaoticUprisingLoaded ? 2 : 0);
            tasks.Insert(num + extraTask, new PassLegacy("Tartarus", delegate (GenerationProgress progress)
            {
                progress.Message = "Tartarus Progress...";
                TartarusGen();
            }));
        }
        private void MysticBiomeGen()
        {
            throw new NotImplementedException();
        }
        private void DistortionBiomeGen()
        {
            //throw new NotImplementedException();
            Random random = new Random();
            int maxTilesX = Main.maxTilesX;
            int num = maxTilesX / 10;
            int maxValue = num * 7;
            int minValue = num * 2;
            int num2 = WorldGen.genRand.Next(minValue, maxValue);
            int num3 = num2;
            int num4 = num3 + 70;
            int num5 = num3 + 380;
            int num6 = 0;
            for (int i = 0; i < Main.maxTilesY / 2; i++)
            {
                num6++;
                num3 = num2;
                for (int j = 0; j < 450; j++)
                {
                    num3++;
                    if (Main.tile[num3, num6] != null && Main.tile[num3, num6].active())
                    {
                        int[] array = new int[1];
                        int[] source = array;
                        if (source.Contains(Main.tile[num3, num6].type))
                        {
                            if (Main.tile[num3, num6 + 1] == null)
                            {
                                if (Main.rand.Next(0, 50) == 1)
                                {
                                    int num7 = 0;
                                    if (num3 < num4 - 1)
                                    {
                                        int maxValue2 = num4 - num3;
                                        num7 = Main.rand.Next(maxValue2);
                                    }
                                    if (num3 > num5 + 1)
                                    {
                                        int maxValue2 = num3 - num5;
                                        num7 = Main.rand.Next(maxValue2);
                                    }
                                    if (num7 < 10)
                                    {
                                        Main.tile[num3, num6].type = 0;
                                    }
                                }
                            }
                            else
                            {
                                int num7 = 0;
                                if (num3 < num4 - 1)
                                {
                                    int maxValue2 = num4 - num3;
                                    num7 = Main.rand.Next(maxValue2);
                                }
                                if (num3 > num5 + 1)
                                {
                                    int maxValue2 = num3 - num5;
                                    num7 = Main.rand.Next(maxValue2);
                                }
                                if (num7 < 10)
                                {
                                    Main.tile[num3, num6].type = 0;
                                }
                            }
                        }
                        int[] source2 = new int[]
                        {
                            2,
                            23,
                            109,
                            199
                        };
                        if (source2.Contains(Main.tile[num3, num6].type))
                        {
                            if (Main.tile[num3, num6 + 1] == null)
                            {
                                if (random.Next(0, 50) == 1)
                                {
                                    int num7 = 0;
                                    if (num3 < num4 - 1)
                                    {
                                        int maxValue2 = num4 - num3;
                                        num7 = Main.rand.Next(maxValue2);
                                    }
                                    if (num3 > num5 + 1)
                                    {
                                        int maxValue2 = num3 - num5;
                                        num7 = Main.rand.Next(maxValue2);
                                    }
                                    if (num7 < 18)
                                    {
                                        Main.tile[num3, num6].type = (ushort)mod.TileType("DistortionGrass");
                                    }
                                }
                            }
                            else
                            {
                                int num7 = 0;
                                if (num3 < num4 - 1)
                                {
                                    int maxValue2 = num4 - num3;
                                    num7 = Main.rand.Next(maxValue2);
                                }
                                if (num3 > num5 + 1)
                                {
                                    int maxValue2 = num3 - num5;
                                    num7 = Main.rand.Next(maxValue2);
                                }
                                if (num7 < 18)
                                {
                                    Main.tile[num3, num6].type = (ushort)mod.TileType("DistortionGrass");
                                }
                            }
                        }
                        int[] source3 = new int[]
                        {
                            1,
                            25,
                            117,
                            203
                        };
                        if (source3.Contains(Main.tile[num3, num6].type))
                        {
                            if (Main.tile[num3, num6 + 1] == null)
                            {
                                if (random.Next(0, 50) == 1)
                                {
                                    int num7 = 0;
                                    if (num3 < num4 - 1)
                                    {
                                        int maxValue2 = num4 - num3;
                                        num7 = Main.rand.Next(maxValue2);
                                    }
                                    if (num3 > num5 + 1)
                                    {
                                        int maxValue2 = num3 - num5;
                                        num7 = Main.rand.Next(maxValue2);
                                    }
                                    if (num7 < 18)
                                    {
                                        Main.tile[num3, num6].type = (ushort)mod.TileType("DistortionStone");
                                    }
                                }
                            }
                            else
                            {
                                int num7 = 0;
                                if (num3 < num4 - 1)
                                {
                                    int maxValue2 = num4 - num3;
                                    num7 = Main.rand.Next(maxValue2);
                                }
                                if (num3 > num5 + 1)
                                {
                                    int maxValue2 = num3 - num5;
                                    num7 = Main.rand.Next(maxValue2);
                                }
                                if (num7 < 18)
                                {
                                    Main.tile[num3, num6].type = (ushort)mod.TileType("DistortionStone");
                                }
                            }
                        }
                        int[] source4 = new int[]
                        {
                            3,
                            24,
                            110,
                            113,
                            115,
                            201,
                            205,
                            52,
                            62,
                            32,
                            165
                        };
                        if (source4.Contains(Main.tile[num3, num6].type))
                        {
                            if (Main.tile[num3, num6 + 1] == null)
                            {
                                if (random.Next(0, 50) == 1)
                                {
                                    int num7 = 0;
                                    if (num3 < num4 - 1)
                                    {
                                        int maxValue2 = num4 - num3;
                                        num7 = Main.rand.Next(maxValue2);
                                    }
                                    if (num3 > num5 + 1)
                                    {
                                        int maxValue2 = num3 - num5;
                                        num7 = Main.rand.Next(maxValue2);
                                    }
                                    if (num7 < 18)
                                    {
                                        Main.tile[num3, num6].type = (ushort)mod.TileType("DistortionHerb");
                                        //Main.tile[num3, num6].active(false);
                                    }
                                }
                            }
                            else
                            {
                                int num7 = 0;
                                if (num3 < num4 - 1)
                                {
                                    int maxValue2 = num4 - num3;
                                    num7 = Main.rand.Next(maxValue2);
                                }
                                if (num3 > num5 + 1)
                                {
                                    int maxValue2 = num3 - num5;
                                    num7 = Main.rand.Next(maxValue2);
                                }
                                if (num7 < 18)
                                {
                                    Main.tile[num3, num6].type = (ushort)mod.TileType("DistortionHerb");
                                    //Main.tile[num3, num6].active(false);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void TartarusGen()
        {
            //throw new NotImplementedException();
            Random random = new Random();
            int maxTilesX = Main.maxTilesX;
            int num = 0;
            //int num = maxTilesX / 8;
            int num2 = num * 1;
            int num3 = num2;
            int num4 = num3;
            int num5 = num4 + 0;
            int num6 = num4 + 400;
            int num7 = 0;
            for (int i = 0; i < Main.maxTilesY; i++)
            {
                num7++;
                num4 = num3;
                for (int j = 0; j < 500; j++)
                {
                    num4++;
                    if (Main.tile[num4, num7] != null && Main.tile[num4, num7].active())
                    {
                        int[] lava = new int[]
                        {
                            TileID.Lavafall,
                            TileID.LavaDrip
                        };
                        if (lava.Contains(Main.tile[num4, num7].type))
                        {
                            if (Main.tile[num4, num7 + 1] == null)
                            {
                                if (Main.rand.Next(0, 50) == 0)
                                {
                                    int num8 = 0;
                                    if (num4 < num5 - 1)
                                    {
                                        int maxValue = num5 - num4;
                                        num8 = Main.rand.Next(maxValue);
                                    }
                                    if (num4 > num6 + 1)
                                    {
                                        int maxValue = num4 - num6;
                                        num8 = Main.rand.Next(maxValue);
                                    }
                                    if (num8 < 10)
                                    {
                                        Main.tile[num4, num7].active(false);
                                        Main.tile[num4, num7].type = (ushort)mod.TileType("TartarusBedrock");
                                    }
                                }
                            }
                            else
                            {
                                int num8 = 0;
                                if (num4 < num5 - 1)
                                {
                                    int maxValue = num5 - num4;
                                    num8 = Main.rand.Next(maxValue);
                                }
                                if (num4 > num6 + 1)
                                {
                                    int maxValue = num4 - num6;
                                    num8 = Main.rand.Next(maxValue);
                                }
                                if (num8 < 10)
                                {
                                    Main.tile[num4, num7].active(false);
                                    Main.tile[num4, num7].type = (ushort)mod.TileType("TartarusBedrock");
                                }
                            }
                        }
                        int[] ash = new int[]
                        {
                            TileID.Ash
                        };
                        if (ash.Contains(Main.tile[num4, num7].type))
                        {
                            if (Main.tile[num4, num7 + 1] == null)
                            {
                                if (Main.rand.Next(0, 50) == 0)
                                {
                                    int num8 = 0;
                                    if (num4 < num5 - 1)
                                    {
                                        int maxValue = num5 - num4;
                                        num8 = Main.rand.Next(maxValue);
                                    }
                                    if (num4 > num6 + 1)
                                    {
                                        int maxValue = num4 - num6;
                                        num8 = Main.rand.Next(maxValue);
                                    }
                                    if (num8 < 10)
                                    {
                                        Main.tile[num4, num7].type = (ushort)mod.TileType("TartarusBedrock");
                                    }
                                }
                            }
                            else
                            {
                                int num8 = 0;
                                if (num4 < num5 - 1)
                                {
                                    int maxValue = num5 - num4;
                                    num8 = Main.rand.Next(maxValue);
                                }
                                if (num4 > num6 + 1)
                                {
                                    int maxValue = num4 - num6;
                                    num8 = Main.rand.Next(maxValue);
                                }
                                if (num8 < 10)
                                {
                                    Main.tile[num4, num7].type = (ushort)mod.TileType("TartarusBedrock");
                                }
                            }
                        }
                        int[] ore = new int[]
                        {
                            TileID.Hellstone
                        };
                        if ((ash.Contains(Main.tile[num4, num7].type) || TileID.Sets.Ore[Main.tile[num4, num7].type]) && num7 >= Main.maxTilesY - 220)
                        {
                            if (Main.tile[num4, num7 + 1] == null)
                            {
                                if (Main.rand.Next(0, 50) == 0)
                                {
                                    int num8 = 0;
                                    if (num4 < num5 - 1)
                                    {
                                        int maxValue = num5 - num4;
                                        num8 = Main.rand.Next(maxValue);
                                    }
                                    if (num4 > num6 + 1)
                                    {
                                        int maxValue = num4 - num6;
                                        num8 = Main.rand.Next(maxValue);
                                    }
                                    if (num8 < 10)
                                    {
                                        Main.tile[num4, num7].type = (ushort)mod.TileType("TartarusBedrock");
                                    }
                                }
                            }
                            else
                            {
                                int num8 = 0;
                                if (num4 < num5 - 1)
                                {
                                    int maxValue = num5 - num4;
                                    num8 = Main.rand.Next(maxValue);
                                }
                                if (num4 > num6 + 1)
                                {
                                    int maxValue = num4 - num6;
                                    num8 = Main.rand.Next(maxValue);
                                }
                                if (num8 < 10)
                                {
                                    Main.tile[num4, num7].type = (ushort)mod.TileType("TartarusBedrock");
                                }
                            }
                        }
                        int[] plants = new int[]
                        {
                            3,
                            24,
                            110,
                            113,
                            115,
                            201,
                            205,
                            52,
                            62,
                            32,
                            165,
                            TileID.ImmatureHerbs,
                            TileID.MatureHerbs,
                            TileID.BloomingHerbs
                        };
                        for (int k = 0; k < plants.Length; k++)
                        {
                            if (Main.tile[num4, num7].type == plants[k] && num7 >= Main.maxTilesY - 220)
                            {
                                if (Main.tile[num4, num7 + 1] == null)
                                {
                                    if (random.Next(0, 50) == 0)
                                    {
                                        int num8 = 0;
                                        if (num4 < num5 - 1)
                                        {
                                            int maxValue = num5 - num4;
                                            num8 = Main.rand.Next(maxValue);
                                        }
                                        if (num4 > num6 + 1)
                                        {
                                            int maxValue = num4 - num6;
                                            num8 = Main.rand.Next(maxValue);
                                        }
                                        //if (num8 < 18)
                                        {
                                            Main.tile[num4, num7].type = (ushort)mod.TileType("TartarusBedrock");
                                        }
                                    }
                                }
                                else
                                {
                                    int num8 = 0;
                                    if (num4 < num5 - 1)
                                    {
                                        int maxValue = num5 - num4;
                                        num8 = Main.rand.Next(maxValue);
                                    }
                                    if (num4 > num6 + 1)
                                    {
                                        int maxValue = num4 - num6;
                                        num8 = Main.rand.Next(maxValue);
                                    }
                                    //if (num8 < 18)
                                    {
                                        Main.tile[num4, num7].type = (ushort)mod.TileType("TartarusBedrock");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            int size = WorldGen.genRand.Next(101, 142);
            GenIsland(new Point(num6 - 60, Main.maxTilesY / (Small ? 15 : 20)), size, mod.TileType("TartarusBedrock"));
        }
        public static void GenIsland(Point topCentre, int size, int type)
        {
            Mod mod = ModLoader.GetMod("SinsMod");
            for (int i = -size / 2; i < size / 2; ++i)
            {
                int repY = (size / 2) - Math.Abs(i);
                int offset = repY / 5;
                repY += WorldGen.genRand.Next(4);
                for (int j = -offset; j < repY; ++j)
                {
                    WorldGen.PlaceTile(topCentre.X + i, topCentre.Y + j, type);
                    if (j == -offset && type == mod.TileType("TartarusBedrock"))
                    {
                        for (int k = -4; k < 5; k++)
                        {
                            WorldGen.PlaceTile(topCentre.X + k, topCentre.Y + j, mod.TileType("TartarusBedrock"), false, true);
                        }
                        WorldGen.PlaceObject(topCentre.X, topCentre.Y + j - 1, mod.TileType("AlterOfConfession"));
                    }
                }
            }
        }
        public static bool MysticOreCount()
        {
            Mod mod = ModLoader.GetMod("SinsMod");
            int num = 0;
            float num2 = Main.maxTilesX / 4200;
            int num3 = (int)(200f * num2);
            for (int i = 5; i < Main.maxTilesX - 5; i++)
            {
                int num4 = 5;
                while (num4 < Main.worldSurface)
                {
                    if (Main.tile[i, num4].active() && (Main.tile[i, num4].type == mod.TileType("MysticOre")))
                    {
                        num++;
                        if (num > num3)
                        {
                            return false;
                        }
                    }
                    num4++;
                }
            }
            return true;
        }
        public static bool NightEnergyOreCount()
        {
            Mod mod = ModLoader.GetMod("SinsMod");
            int num = 0;
            float num2 = Main.maxTilesX / 4200;
            int num3 = (int)(200f * num2);
            for (int i = 5; i < Main.maxTilesX - 5; i++)
            {
                int num4 = 5;
                while (num4 < Main.worldSurface)
                {
                    if (Main.tile[i, num4].active() && (Main.tile[i, num4].type == mod.TileType("NightEnergizedOre")))
                    {
                        num++;
                        if (num > num3)
                        {
                            return false;
                        }
                    }
                    num4++;
                }
            }
            return true;
        }
        public static void MeteorOreDrop(int type)
        {
            Mod mod = ModLoader.GetMod("SinsMod");
            if (type != mod.TileType("MysticOre") && type != mod.TileType("NightEnergizedOre"))
            {
                return;
            }
            bool flag = true;
            if (Main.netMode == 1)
            {
                return;
            }
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active)
                {
                    flag = false;
                    break;
                }
            }
            if (type == mod.TileType("MysticOre"))
            {
                if (!MysticOreCount())
                {
                    return;
                }
            }
            if (type == mod.TileType("NightEnergizedOre"))
            {
                if (!NightEnergyOreCount())
                {
                    return;
                }
            }
            float num = 600f;
            while (!flag)
            {
                float num2 = Main.maxTilesX * 0.08f;
                int num3 = Main.rand.Next(150, Main.maxTilesX - 150);
                while (num3 > Main.spawnTileX - num2 && num3 < Main.spawnTileX + num2)
                {
                    num3 = Main.rand.Next(150, Main.maxTilesX - 150);
                }
                int j = (int)(Main.worldSurface * 0.3);
                while (j < Main.maxTilesY)
                {
                    if (Main.tile[num3, j].active() && Main.tileSolid[Main.tile[num3, j].type])
                    {
                        int num4 = 0;
                        int num5 = 15;
                        for (int k = num3 - num5; k < num3 + num5; k++)
                        {
                            for (int l = j - num5; l < j + num5; l++)
                            {
                                if (WorldGen.SolidTile(k, l))
                                {
                                    num4++;
                                    if (Main.tile[k, l].type == 189 || Main.tile[k, l].type == 202)
                                    {
                                        num4 -= 100;
                                    }
                                }
                                else
                                {
                                    if (Main.tile[k, l].liquid > 0)
                                    {
                                        num4--;
                                    }
                                }
                            }
                        }
                        if (num4 < num)
                        {
                            num -= 0.5f;
                            break;
                        }
                        flag = MeteorOreRun(num3, j, type);
                        if (flag)
                        {
                            break;
                        }
                        break;
                    }
                    else
                    {
                        j++;
                    }
                }
                if (num < 100f)
                {
                    return;
                }
            }
        }
        public static bool MeteorOreRun(int i, int j, int type)
        {
            Mod mod = ModLoader.GetMod("SinsMod");
            if (type != mod.TileType("MysticOre") && type != mod.TileType("NightEnergizedOre"))
            {
                return false;
            }
            if (i < 50 || i > Main.maxTilesX - 50)
            {
                return false;
            }
            if (j < 50 || j > Main.maxTilesY - 50)
            {
                return false;
            }
            int num = 35;
            Rectangle rectangle = new Rectangle((i - num) * 16, (j - num) * 16, num * 2 * 16, num * 2 * 16);
            for (int k = 0; k < 255; k++)
            {
                if (Main.player[k].active)
                {
                    Rectangle rectangle2 = new Rectangle((int)(Main.player[k].position.X + Main.player[k].width / 2 - NPC.sWidth / 2 - NPC.safeRangeX), (int)(Main.player[k].position.Y + Main.player[k].height / 2 - NPC.sHeight / 2 - NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                    if (rectangle.Intersects(rectangle2))
                    {
                        return false;
                    }
                }
            }
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active)
                {
                    Rectangle rectangle3 = new Rectangle((int)Main.npc[k].position.X, (int)Main.npc[k].position.Y, Main.npc[k].width, Main.npc[k].height);
                    if (rectangle.Intersects(rectangle3))
                    {
                        return false;
                    }
                }
            }
            for (int m = i - num; m < i + num; m++)
            {
                for (int n = j - num; n < j + num; n++)
                {
                    if (Main.tile[m, n].active() && Main.tile[m, n].type == 21)
                    {
                        return false;
                    }
                }
            }
            num = WorldGen.genRand.Next(17, 23);
            for (int num2 = i - num; num2 < i + num; num2++)
            {
                for (int num3 = j - num; num3 < j + num; num3++)
                {
                    if (num3 > j + Main.rand.Next(-2, 3) - 5)
                    {
                        float num4 = Math.Abs(i - num2);
                        float num5 = Math.Abs(j - num3);
                        float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                        if (num6 < num * 0.9 + Main.rand.Next(-4, 5))
                        {
                            if (!Main.tileSolid[Main.tile[num2, num3].type])
                            {
                                Main.tile[num2, num3].active(false);
                            }
                            Main.tile[num2, num3].type = (ushort)type;
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(8, 14);
            for (int num7 = i - num; num7 < i + num; num7++)
            {
                for (int num8 = j - num; num8 < j + num; num8++)
                {
                    if (num8 > j + Main.rand.Next(-2, 3) - 4)
                    {
                        float num9 = Math.Abs(i - num7);
                        float num10 = Math.Abs(j - num8);
                        float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                        if (num11 < num * 0.8 + Main.rand.Next(-3, 4))
                        {
                            Main.tile[num7, num8].active(false);
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(25, 35);
            for (int num12 = i - num; num12 < i + num; num12++)
            {
                for (int num13 = j - num; num13 < j + num; num13++)
                {
                    float num14 = Math.Abs(i - num12);
                    float num15 = Math.Abs(j - num13);
                    float num16 = (float)Math.Sqrt(num14 * num14 + num15 * num15);
                    if (num16 < num * 0.7)
                    {
                        if (Main.tile[num12, num13].type == 5 || Main.tile[num12, num13].type == 32 || Main.tile[num12, num13].type == 352)
                        {
                            WorldGen.KillTile(num12, num13, false, false, false);
                        }
                        Main.tile[num12, num13].liquid = 0;
                    }
                    if (Main.tile[num12, num13].type == type)
                    {
                        if (!WorldGen.SolidTile(num12 - 1, num13) && !WorldGen.SolidTile(num12 + 1, num13) && !WorldGen.SolidTile(num12, num13 - 1) && !WorldGen.SolidTile(num12, num13 + 1))
                        {
                            Main.tile[num12, num13].active(false);
                        }
                        else
                        {
                            if ((Main.tile[num12, num13].halfBrick() || Main.tile[num12 - 1, num13].topSlope()) && !WorldGen.SolidTile(num12, num13 + 1))
                            {
                                Main.tile[num12, num13].active(false);
                            }
                        }
                    }
                    WorldGen.SquareTileFrame(num12, num13, true);
                    WorldGen.SquareWallFrame(num12, num13, true);
                }
            }
            num = WorldGen.genRand.Next(23, 32);
            for (int num17 = i - num; num17 < i + num; num17++)
            {
                for (int num18 = j - num; num18 < j + num; num18++)
                {
                    if (num18 > j + WorldGen.genRand.Next(-3, 4) - 3 && Main.tile[num17, num18].active() && Main.rand.Next(10) == 0)
                    {
                        float num19 = Math.Abs(i - num17);
                        float num20 = Math.Abs(j - num18);
                        float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
                        if (num21 < num * 0.8)
                        {
                            if (Main.tile[num17, num18].type == 5 || Main.tile[num17, num18].type == 32 || Main.tile[num17, num18].type == 352)
                            {
                                WorldGen.KillTile(num17, num18, false, false, false);
                            }
                            Main.tile[num17, num18].type = (ushort)type;
                            WorldGen.SquareTileFrame(num17, num18, true);
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(30, 38);
            for (int num22 = i - num; num22 < i + num; num22++)
            {
                for (int num23 = j - num; num23 < j + num; num23++)
                {
                    if (num23 > j + WorldGen.genRand.Next(-2, 3) && Main.tile[num22, num23].active() && Main.rand.Next(20) == 0)
                    {
                        float num24 = Math.Abs(i - num22);
                        float num25 = Math.Abs(j - num23);
                        float num26 = (float)Math.Sqrt(num24 * num24 + num25 * num25);
                        if (num26 < num * 0.85)
                        {
                            if (Main.tile[num22, num23].type == 5 || Main.tile[num22, num23].type == 32 || Main.tile[num22, num23].type == 352)
                            {
                                WorldGen.KillTile(num22, num23, false, false, false);
                            }
                            Main.tile[num22, num23].type = (ushort)type;
                            WorldGen.SquareTileFrame(num22, num23, true);
                        }
                    }
                }
            }
            NightEnergyDropped += 1;
            if (Main.netMode != 1)
            {
                string key = "Mods.SinsMod.MeteorLanded";
                string arg0 = "Mystic";
                Color color = Main.mcColor;
                if (type == mod.TileType("NightEnergizedOre"))
                {
                    key = "Mods.SinsMod.NightEnergyGen";
                }
                if (Main.netMode != 2)
                {
                    Main.NewText(Language.GetTextValue(key, arg0), color);
                }
                else
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, arg0), color);
                }
                NetMessage.SendTileSquare(-1, i, j, 40, TileChangeType.None);
            }
            return true;
        }
    }
}