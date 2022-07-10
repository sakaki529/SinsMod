using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
/*using ThoriumMod;
using CalamityMod;
using CalamityMod.Items.CalamityCustomThrowingDamage;*/
using DBZMOD;
using Laugicality;
//using Redemption.Items.DruidDamageClass;

namespace SinsMod.Items.Equippable
{
    public class XEmblem : ModItem
    {
        //private readonly Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");
        //private readonly Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        private readonly Mod DBZMOD = ModLoader.GetMod("DBZMOD");
        private readonly Mod Laugicality = ModLoader.GetMod("Laugicality");
        //private readonly Mod Redemption = ModLoader.GetMod("Redemption");
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("X Emblem");
            string tooltip = "50% increased all";
            /*string tooltip = "50% increased melee, magic, ranged, thrown, minion";
            if (ModLoader.GetMod("ThoriumMod") != null)
            {
                //tooltip += ", bard, radiant";
            }
            if (ModLoader.GetMod("CalamityMod") != null)
            {
                tooltip += ", rogue";
            }
            if (ModLoader.GetMod("DBZMOD") != null)
            {
                tooltip += ", ki";
            }
            if (ModLoader.GetMod("Laugicality") != null)
            {
                tooltip += ", Mystic";
            }
            if (ModLoader.GetMod("Redemption") != null)
            {
                //tooltip += ", druid";
            }*/
            tooltip += " damage" +
                "\n50% increased melee, magic, ranged, thrown";
            if (ModLoader.GetMod("ThoriumMod") != null)
            {
                //tooltip += ", bard, radiant";
            }
            if (ModLoader.GetMod("CalamityMod") != null)
            {
                //tooltip += ", rogue";
            }
            if (ModLoader.GetMod("DBZMOD") != null)
            {
                tooltip += ", ki";
            }
            if (ModLoader.GetMod("Laugicality") != null)
            {
                tooltip += ",Mystic";
            }
            if (ModLoader.GetMod("Redemption") != null)
            {
                //tooltip += ", druid";
            }
            tooltip += " critical stike chance" +
                "\n30% inceased melee";
            if (ModLoader.GetMod("ThoriumMod") != null)
            {
                //tooltip += ",bard";
            }
            tooltip += " speed" +
                "\nIncreases melee knockback" +
                "\nIncreases max mana by 300" +
                "\n50% decreased mana costs";
            if (ModLoader.GetMod("ThoriumMod") != null)
            {
                //tooltip += "\nIncreases max inspiration";
            }
            tooltip += "\nIncreases your max number of minions by 10" +
                "\nIncreases your max number of sentries by 5" +
                "\n20% increased minion knockback" +
                "\n40% inceased thrown velocity" +
                "\nIncreases armor penetration by 100" +
                "\nRestores mana when damaged" +
                "\nGives the user master yoyo skills";
            if (ModLoader.GetMod("DBZMOD") != null)
            {
                tooltip += "\nGives the user grants Ki skills";
            }
            Tooltip.SetDefault(tooltip);
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(1, 40, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.defense = 6;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.XEmblem = true;
            player.armorPenetration += 100;
            player.allDamage += 0.5f;
            /*player.meleeDamage += 0.5f;
            player.magicDamage += 0.5f;
            player.thrownDamage += 0.5f;
            player.rangedDamage += 0.5f;
            player.minionDamage += 0.5f;*/
            player.meleeCrit += 50;
            player.magicCrit += 50;
            player.rangedCrit += 50;
            player.thrownCrit += 50;
            player.meleeSpeed += 0.3f;
            player.statManaMax2 += 300;
            player.manaCost *= 0.5f;
            player.magicCuffs = true;
            player.maxMinions += 10;
            player.maxTurrets += 5;
            player.minionKB += 0.2f;
            player.thrownVelocity += 0.4f;
            player.kbGlove = true;
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;
            if (SinsMod.Instance.ThoriumLoaded)
            {
                Thorium(player);
            }
            if (SinsMod.Instance.CalamityLoaded)
            {
                Calamity(player);
            }
            if (SinsMod.Instance.DBZLoaded)
            {
                DBZ(player);
            }
            if (SinsMod.Instance.LaugicalityLoaded)
            {
                Enigma(player);
            }
            if (SinsMod.Instance.RedemptionLoaded)
            {
                RedemptionDruid(player);
            }
        }
        private void Thorium(Player player)
        {
            /*ThoriumPlayer modPlayer = player.GetModPlayer<ThoriumPlayer>(ThoriumMod);
            modPlayer.symphonicDamage += 0.5f;
            modPlayer.symphonicCrit += 50;
            modPlayer.symphonicSpeed += 0.3f;
            modPlayer.bardResourceMax2 = 20;
            modPlayer.bardResourceDrop += 5f;
            modPlayer.bardHomingBool = true;
            modPlayer.bardHomingBonus = 5f;
            modPlayer.bardMute2 = true;
            modPlayer.tuner2 = true;
            modPlayer.bardBounceBonus = 5;

            modPlayer.radiantBoost += 0.5f;
            modPlayer.radiantCrit += 50;
            modPlayer.radiantSpeed -= 0.3f;
            modPlayer.healingSpeed += 0.3f;
            modPlayer.supportSash = true;
            modPlayer.quickBelt = true;
            modPlayer.crossHeal = true;
            modPlayer.healBloom = true;
            modPlayer.darkAura = true;
            modPlayer.healBonus += 10;*/
        }
        private void Calamity(Player player)
        {
            /*CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.5f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 50;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingVelocity += 0.4f;
            modPlayer.nanotech = true;*/
        }
        private void DBZ(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            //modPlayer.kiDamage += 0.5f;
            modPlayer.kiCrit += 50;
            modPlayer.chargeMoveSpeed = Math.Max(modPlayer.chargeMoveSpeed, 2f);
            modPlayer.kiKbAddition += 0.2f;
            modPlayer.kiDrainMulti -= 0.5f;
            modPlayer.kiMaxMult += 0.5f;
            modPlayer.kiRegen += 10;
            modPlayer.orbGrabRange += 6;
            modPlayer.orbHealAmount += 150;
            modPlayer.chargeLimitAdd += 8;
            modPlayer.flightSpeedAdd += 0.6f;
            modPlayer.flightUsageAdd += 3;
            modPlayer.zenkaiCharm = true;
        }
        private void Enigma(Player player)
        {
            LaugicalityPlayer modPlayer = player.GetModPlayer<LaugicalityPlayer>();
            //modPlayer.MysticDamage += 0.5f;
            modPlayer.MysticCrit += 50;
            modPlayer.LuxRegen += 5000;
            modPlayer.VisRegen += 5000;
            modPlayer.MundusRegen += 5000;
        }
        private void RedemptionDruid(Player player)
        {
            /*DruidDamagePlayer modPlayer = player.GetModPlayer<DruidDamagePlayer>();
            modPlayer.druidDamage += 0.5f;
            modPlayer.druidCrit += 50;*/
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            if (SinsMod.Instance.BluemagicLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("Bluemagic").ItemType("AvengerSeal"));
            }
            recipe.AddIngredient(null, "EssenceOfMadness", 12);
            recipe.AddIngredient(null, "NightmareBar", 12);
            if (SinsMod.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("DarksunFragment"), 80);
            }
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}