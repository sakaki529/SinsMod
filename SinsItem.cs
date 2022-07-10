using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SinsMod
{
    public class SinsItem : GlobalItem
    {
        private bool rarityStored;
        private int initialRarity;
        private int newRarity;
        public int CustomRarity;
        public bool isAltFunction;
        public bool isMultiType;
        internal bool isDevTools;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }
        public override void SetDefaults(Item item)
        {
            if (SinsMod.AutoReusableItemList.Contains(item.type))
            {
                item.autoReuse = true;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (!item.expert && line != null)
                {
                    if (line.mod == "Terraria" && line.Name == "ItemName")
                    {
                        if (CustomRarity != newRarity && rarityStored)
                        {
                            //CustomRarity = newRarity;
                        }
                        switch (CustomRarity)
                        {
                            case 12:
                                line.overrideColor = SinsColor.Rarity12;
                                break;
                            case 13:
                                line.overrideColor = SinsColor.Rarity13;
                                break;
                            case 14:
                                line.overrideColor = SinsColor.Rarity14;
                                break;
                            case 15:
                                line.overrideColor = SinsColor.Rarity15;
                                break;
                            case 16:
                                line.overrideColor = SinsColor.Rarity16;
                                break;
                            case 17:
                                line.overrideColor = SinsColor.Rarity17;
                                break;
                        }
                    }
                }
            }
            if (isAltFunction)//å„Ç…óàÇΩï˚Ç™è„Ç…Ç»ÇÈ textureÇ›ÇΩÇ¢Ç…
            {
                int num = -1;
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name.Equals("ItemName"))
                    {
                        num = i;
                        break;
                    }
                }
                tooltips.Insert(num + 1, new TooltipLine(mod, "RightClickableTag", "-Right Clickable-"));
                foreach (TooltipLine current in tooltips)
                {
                    if (current.mod == "SinsMod" && current.Name == "RightClickableTag")
                    {
                        current.overrideColor = SinsColor.AltUseableColor;
                    }
                }
            }
            if (isMultiType)
            {
                int num = -1;
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name.Equals("ItemName"))
                    {
                        num = i;
                        break;
                    }
                }
                tooltips.Insert(num + 1, new TooltipLine(mod, "MultiTypeTag", "-Multi Type Weapon-"));
                foreach (TooltipLine current in tooltips)
                {
                    if (current.mod == "SinsMod" && current.Name == "MultiTypeTag")
                    {
                        current.overrideColor = SinsColor.MultiTypeColor;
                    }
                }
            }
            /*if (isAltFunction)
            {
                TooltipLine line = new TooltipLine(mod, "Info", "[c/009600:<Right clickable>]");
                tooltips.Add(line);
            }
            if (isMultiType)
            {
                TooltipLine line = new TooltipLine(mod, "Info", "[c/ffb600:<Multi Type Weapon>]");
                tooltips.Add(line);
            }*/
            /*if (item.type == mod.ItemType("BlackCoin"))
            {
                int Material = tooltips.FindIndex(x => x.Name == "Material");
                tooltips.RemoveAt(Material);
            }*/
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.type < mod.ItemType("AndromedaBreastplate") || item.type > mod.ItemType("Volcanion"))//only items which from this mod
            {
                return;
            }
            if (!rarityStored)
            {
                Item items = new Item();
                items.SetDefaults(item.type, false);
                int customRarity = items.GetGlobalItem<SinsItem>().CustomRarity;
                initialRarity = customRarity > 11 ? customRarity : items.rare;
                rarityStored = true;
            }
            newRarity = initialRarity;
            int prefix = item.prefix;
            float num = 1f;
            float num2 = 1f;
            float num3 = 1f;
            float num4 = 1f;
            float num5 = 1f;
            float num6 = 1f;
            int num7 = 0;
            switch (prefix)
            {
                case 1:
                    num4 = 1.12f;
                    break;
                case 2:
                    num4 = 1.18f;
                    break;
                case 3:
                    num = 1.05f;
                    num7 = 2;
                    num4 = 1.05f;
                    break;
                case 4:
                    num = 1.1f;
                    num4 = 1.1f;
                    num2 = 1.1f;
                    break;
                case 5:
                    num = 1.15f;
                    break;
                case 6:
                    num = 1.1f;
                    break;
                case 7:
                    num4 = 0.82f;
                    break;
                case 8:
                    num2 = 0.85f;
                    num = 0.85f;
                    num4 = 0.87f;
                    break;
                case 9:
                    num4 = 0.9f;
                    break;
                case 10:
                    num = 0.85f;
                    break;
                case 11:
                    num3 = 1.1f;
                    num2 = 0.9f;
                    num4 = 0.9f;
                    break;
                case 12:
                    num2 = 1.1f;
                    num = 1.05f;
                    num4 = 1.1f;
                    num3 = 1.15f;
                    break;
                case 13:
                    num2 = 0.8f;
                    num = 0.9f;
                    num4 = 1.1f;
                    break;
                case 14:
                    num2 = 1.15f;
                    num3 = 1.1f;
                    break;
                case 15:
                    num2 = 0.9f;
                    num3 = 0.85f;
                    break;
                case 16:
                    num = 1.1f;
                    num7 = 3;
                    break;
                case 17:
                    num3 = 0.85f;
                    num5 = 1.1f;
                    break;
                case 18:
                    num3 = 0.9f;
                    num5 = 1.15f;
                    break;
                case 19:
                    num2 = 1.15f;
                    num5 = 1.05f;
                    break;
                case 20:
                    num2 = 1.05f;
                    num5 = 1.05f;
                    num = 1.1f;
                    num3 = 0.95f;
                    num7 = 2;
                    break;
                case 21:
                    num2 = 1.15f;
                    num = 1.1f;
                    break;
                case 22:
                    num2 = 0.9f;
                    num5 = 0.9f;
                    num = 0.85f;
                    break;
                case 23:
                    num3 = 1.15f;
                    num5 = 0.9f;
                    break;
                case 24:
                    num3 = 1.1f;
                    num2 = 0.8f;
                    break;
                case 25:
                    num3 = 1.1f;
                    num = 1.15f;
                    num7 = 1;
                    break;
                case 26:
                    num6 = 0.85f;
                    num = 1.1f;
                    break;
                case 27:
                    num6 = 0.85f;
                    break;
                case 28:
                    num6 = 0.85f;
                    num = 1.15f;
                    num2 = 1.05f;
                    break;
                case 29:
                    num6 = 1.1f;
                    break;
                case 30:
                    num6 = 1.2f;
                    num = 0.9f;
                    break;
                case 31:
                    num2 = 0.9f;
                    num = 0.9f;
                    break;
                case 32:
                    num6 = 1.15f;
                    num = 1.1f;
                    break;
                case 33:
                    num6 = 1.1f;
                    num2 = 1.1f;
                    num3 = 0.9f;
                    break;
                case 34:
                    num6 = 0.9f;
                    num2 = 1.1f;
                    num3 = 1.1f;
                    num = 1.1f;
                    break;
                case 35:
                    num6 = 1.2f;
                    num = 1.15f;
                    num2 = 1.15f;
                    break;
                case 36:
                    num7 = 3;
                    break;
                case 37:
                    num = 1.1f;
                    num7 = 3;
                    num2 = 1.1f;
                    break;
                case 38:
                    num2 = 1.15f;
                    break;
                case 39:
                    num = 0.7f;
                    num2 = 0.8f;
                    break;
                case 40:
                    num = 0.85f;
                    break;
                case 41:
                    num2 = 0.85f;
                    num = 0.9f;
                    break;
                case 42:
                    num3 = 0.9f;
                    break;
                case 43:
                    num = 1.1f;
                    num3 = 0.9f;
                    break;
                case 44:
                    num3 = 0.9f;
                    num7 = 3;
                    break;
                case 45:
                    num3 = 0.95f;
                    break;
                case 46:
                    num7 = 3;
                    num3 = 0.94f;
                    num = 1.07f;
                    break;
                case 47:
                    num3 = 1.15f;
                    break;
                case 48:
                    num3 = 1.2f;
                    break;
                case 49:
                    num3 = 1.08f;
                    break;
                case 50:
                    num = 0.8f;
                    num3 = 1.15f;
                    break;
                case 51:
                    num2 = 0.9f;
                    num3 = 0.9f;
                    num = 1.05f;
                    num7 = 2;
                    break;
                case 52:
                    num6 = 0.9f;
                    num = 0.9f;
                    num3 = 0.9f;
                    break;
                case 53:
                    num = 1.1f;
                    break;
                case 54:
                    num2 = 1.15f;
                    break;
                case 55:
                    num2 = 1.15f;
                    num = 1.05f;
                    break;
                case 56:
                    num2 = 0.8f;
                    break;
                case 57:
                    num2 = 0.9f;
                    num = 1.18f;
                    break;
                case 58:
                    num3 = 0.85f;
                    num = 0.85f;
                    break;
                case 59:
                    num2 = 1.15f;
                    num = 1.15f;
                    num7 = 5;
                    break;
                case 60:
                    num = 1.15f;
                    num7 = 5;
                    break;
                case 61:
                    num7 = 5;
                    break;
                case 81:
                    num2 = 1.15f;
                    num = 1.15f;
                    num7 = 5;
                    num3 = 0.9f;
                    num4 = 1.1f;
                    break;
                case 82:
                    num2 = 1.15f;
                    num = 1.15f;
                    num7 = 5;
                    num3 = 0.9f;
                    num5 = 1.1f;
                    break;
                case 83:
                    num2 = 1.15f;
                    num = 1.15f;
                    num7 = 5;
                    num3 = 0.9f;
                    num6 = 0.9f;
                    break;
            }
            float num8 = 1f * num * (2f - num3) * (2f - num6) * num4 * num2 * num5 * (1f + num7 * 0.02f);
            if (prefix == 62 || prefix == 69 || prefix == 73 || prefix == 77)
            {
                num8 *= 1.05f;
            }
            if (prefix == 63 || prefix == 70 || prefix == 74 || prefix == 78 || prefix == 67)
            {
                num8 *= 1.1f;
            }
            if (prefix == 64 || prefix == 71 || prefix == 75 || prefix == 79 || prefix == 66)
            {
                num8 *= 1.15f;
            }
            if (prefix == 65 || prefix == 72 || prefix == 76 || prefix == 80 || prefix == 68)
            {
                num8 *= 1.2f;
            }
            if (num8 >= 1.2)
            {
                newRarity += 2;
            }
            else
            {
                if (num8 >= 1.05)
                {
                    newRarity++;
                }
                else
                {
                    if (num8 <= 0.8)
                    {
                        newRarity -= 2;
                    }
                    else
                    {
                        if (num8 <= 0.95)
                        {
                            newRarity--;
                        }
                    }
                }
            }
            if (newRarity > 17)
            {
                newRarity = 17;
            }
            if (newRarity > 11)
            {
                item.rare = 11;
            }
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (SinsMod.Instance.FargoSoulsLoaded)
            {
                modPlayer.UniverseSoul = item.type == ModLoader.GetMod("FargowiltasSouls").ItemType("UniverseSoul");
                modPlayer.EternitySoul = item.type == ModLoader.GetMod("FargowiltasSouls").ItemType("EternitySoul");
            }
            if (SinsMod.Instance.FargoLoaded)
            {
                //modPlayer.UniverseSoul = item.type == ModLoader.GetMod("Fargowiltas").ItemType("UniverseSoul");
            }
        }
        public override void OnMissingMana(Item item, Player player, int neededMana)
        {
            if (player.GetModPlayer<SinsPlayer>().UnlimitedMana)
            {
                player.statMana += neededMana;
            }
        }
        public override void HoldItem(Item item, Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.FOOLS = item.type == mod.ItemType("HolySword");
            modPlayer.LunarArcanumOrb = item.type == mod.ItemType("LunarArcanum");
            modPlayer.Debug = modPlayer.Cleyera && isDevTools;
            if (!item.melee == true && !item.ranged == true && !item.magic == true && !item.thrown == true && !item.summon == true && item.damage >= 1)
            {
                modPlayer.NoDamageClass = true;
            }
            if (SinsMod.Instance.PumpkingLoaded)
            {
                if (item.type == ModLoader.GetMod("Pumpking").ItemType("PumpkingYoyo"))
                {
                    modPlayer.NoDamageClass = true;
                }
            }
        }
        public override bool? CanHitNPC(Item item, Player player, NPC target)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (target.type == NPCID.Angler || target.type == NPCID.SleepingAngler)
            {
                if (modPlayer.KillAngler)
                {
                    return true;
                }
            }
            return base.CanHitNPC(item, player, target);
        }
        public override void ModifyHitNPC(Item item, Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (target.type == mod.NPCType("BlackCrystalCore") || target.type == mod.NPCType("BlackCrystalCoreClone") || target.type == mod.NPCType("BCCSummonAttack") || target.type == mod.NPCType("BCCSummonHeal") || target.type == mod.NPCType("WillOfMadness"))
            {
                if (NPCs.Boss.Madness.BlackCrystalCore.isMeleeResist || (NPCs.Boss.Madness.BlackCrystalCore.isRangedResist && item.ranged) || (NPCs.Boss.Madness.BlackCrystalCore.isMagicResist && item.magic) || (NPCs.Boss.Madness.BlackCrystalCore.isThrownResist && item.thrown) || (NPCs.Boss.Madness.BlackCrystalCore.isSummonResist && item.summon))
                {
                    damage = 0;
                }
                else if (!item.melee && !item.magic && !item.ranged && !item.thrown && !item.summon)
                {
                    damage = 0;
                }
            }
            if (target.type == mod.NPCType("BlackCrystalNoMove") || target.type == mod.NPCType("BlackCrystal") || target.type == mod.NPCType("BlackCrystalSmall"))
            {
                if (!item.melee && !item.magic && !item.ranged && !item.thrown && !item.summon)
                {
                    damage /= 5;
                }
            }
            if (SinsMod.Instance.MinionCritLoaded)
            {
                if (item.summon)
                {
                    if (modPlayer.XEmblem)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            crit = true;
                            return;
                        }
                    }
                }
            }
            base.ModifyHitNPC(item, player, target, ref damage, ref knockBack, ref crit);
        }
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (context == "bossBag")
            {
                if (arg == ItemID.WallOfFleshBossBag)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        player.QuickSpawnItem(mod.ItemType("ThrowerEmblem"));
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        player.QuickSpawnItem(mod.ItemType("DemonGaze"));
                    }
                }
                if (arg == ItemID.TwinsBossBag)
                {
                    if (Main.rand.Next(8) == 0)
                    {
                        player.QuickSpawnItem(mod.ItemType("LaserAntenna"));
                    }
                }
                if (arg == ItemID.MoonLordBossBag)
                {
                    if (!modPlayer.ExtraAccessory2)
                    {
                        player.QuickSpawnItem(mod.ItemType("GiftFromSentients"));
                    }
                }
            }
        }
    }
}