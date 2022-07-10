using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SinsMod.NPCs.TownNPCs
{
    [AutoloadHead]
    public class RuneStoneDealer : ModNPC
    {
        public override string HeadTexture => "SinsMod/Extra/Placeholder/PlaceholderNPC_Head";
        public override string Texture => "SinsMod/Extra/Placeholder/PlaceholderNPC";
        public override string[] AltTextures => new[] { "SinsMod/Extra/Placeholder/PlaceholderNPC_Alt" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Stone Dealer");
            Main.npcFrameCount[npc.type] = 25;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 640;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
        }
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 80;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;//6 smoke
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                int num = 0;
                while (num < damage / npc.lifeMax * 100.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
                    num++;
                }
                return;
            }
            for (int i = 0; i < 50; i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * hitDirection, -2.5f, 0, default(Color), 1f);
            }
            Gore.NewGore(npc.position, npc.velocity, 73, 1f);
            Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 20f), npc.velocity, 74, 1f);
            Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 20f), npc.velocity, 74, 1f);
            Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 34f), npc.velocity, 75, 1f);
            Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 34f), npc.velocity, 75, 1f);
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int i = 0; i < 255; i++)
            {
                Player player = Main.player[i];
                if (player.active)
                {
                    for (int j = 0; j < player.inventory.Length; j++)
                    {
                        for (int k = 0; k < player.bank.item.Length; k++)
                        {
                            for (int l = 0; l < player.bank2.item.Length; l++)
                            {
                                for (int m = 0; m < player.bank3.item.Length; m++)
                                {
                                    if (player.inventory[j].type == mod.ItemType("RuneStone") || player.bank.item[k].type == mod.ItemType("RuneStone") || player.bank2.item[l].type == mod.ItemType("RuneStone") || player.bank3.item[m].type == mod.ItemType("RuneStone"))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    /*for (int j = 0; j < player.inventory.Length; j++)
                    {
                        if (player.inventory[j].type == mod.ItemType("RuneStone"))
                        {
                            return true;
                        }
                    }
                    for (int k = 0; k < player.bank.item.Length; k++)
                    {
                        if (player.bank.item[k].type == mod.ItemType("RuneStone"))
                        {
                            return true;
                        }
                    }
                    for (int l = 0; l < player.bank2.item.Length; l++)
                    {
                        if (player.bank2.item[l].type == mod.ItemType("RuneStone"))
                        {
                            return true;
                        }
                    }
                    for (int m = 0; m < player.bank3.item.Length; m++)
                    {
                        if (player.bank3.item[m].type == mod.ItemType("RuneStone"))
                        {
                            return true;
                        }
                    }*/
                }
            }
            return false;
        }
        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(4))
            {
                case 0:
                    return "Nobie";
                case 1:
                    return "Chokhmah";
                default:
                    return "Noah";
            }
        }
		public override string GetChat()
		{
            if (npc.homeless)
            {
                Main.npcChatCornerItem = mod.ItemType("RuneStone");
                if (Main.rand.Next(2) == 0)
                {
                    return "I heard you have Rune Stone!";
                }
                return "I'm dealing magical items.";
            }
            else
            {
                if (Main.bloodMoon)
                {
                    if (SinsMod.Instance.CalamityLoaded)
                    {
                        if (Main.rand.Next(3) == 0)
                        {
                            return "I feel like I heard the blood gods' roar...";
                        }
                    }
                    switch (Main.rand.Next(1))
                    {
                        case 0:
                            return "I feel madness from the moon... Do you?";

                        default:
                            return "...";
                    }
                }
                else
                {
                    int ChatCount = 0;
                    WeightedRandom<string> chat = new WeightedRandom<string>();
                    if (SinsMod.Instance.CalamityLoaded)
                    {
                        int DILF = NPC.FindFirstNPC(ModLoader.GetMod("CalamityMod").NPCType("DILF"));
                        if (DILF >= 0)
                        {
                            chat.Add("I want to learn ice magic from " + Main.npc[DILF].GivenName + ". Really interesting.");
                            ChatCount++;
                        }
                    }
                    if (SinsMod.Instance.SpiritLoaded)
                    {
                        int RuneWizard = NPC.FindFirstNPC(ModLoader.GetMod("SpiritMod").NPCType("RuneWizard"));
                        if (RuneWizard >= 0)
                        {
                            chat.Add("I think " + Main.npc[RuneWizard].GivenName + "'s mantle is cool. How do you think about it?");
                            ChatCount++;
                        }
                    }
                    if (!Main.dayTime)
                    {
                        chat.Add("Do you feel it, the moon's power?");
                        ChatCount++;
                    }
                    if (NPC.downedGolemBoss)
                    {
                        chat.Add("Moon's power is grow up than before. Take care.");
                        ChatCount++;
                    }
                    chat.Add("What's your favorite color? My favorite colors are white and black.");
                    ChatCount++;
                    chat.Add("What? I don't have any arms or legs? Oh, don't be ridiculous!");
                    ChatCount++;
                    chat.Add("Shut up. I'm gay.\n...It's joke.", 0.001);
                    if (Main.rand.Next(ChatCount) == 0)
                    {
                        Main.npcChatCornerItem = mod.ItemType("RuneStone");
                        return "If you have Rune Stone, I can trade it to something magical item.";
                    }
                    return chat;
                }
            }
		}
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
                return;
            }
            shop = false;
        }
        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ItemID.RuneHat);
            shop.item[nextSlot].shopCustomPrice = 1;
            shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.RuneRobe);
            shop.item[nextSlot].shopCustomPrice = 1;
            shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
            nextSlot++;
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("StormSpell"));
                shop.item[nextSlot].shopCustomPrice = 8;
                shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("AstralThread"));
                shop.item[nextSlot].shopCustomPrice = 8;
                shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
                nextSlot++;
            }
            shop.item[nextSlot].SetDefaults(mod.ItemType("DeathMirror"));
            shop.item[nextSlot].shopCustomPrice = 4;
            shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("LimitCutter"));
            shop.item[nextSlot].shopCustomPrice = 2;
            shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("HopelessMode"));
            shop.item[nextSlot].shopCustomPrice = 2;
            shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("GloriousTrophy"));
            shop.item[nextSlot].shopCustomPrice = 5;
            shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
            nextSlot++;
            if (Main.raining)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("RainStone"));
                shop.item[nextSlot].shopCustomPrice = 10;
                shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
                nextSlot++;
            }
            shop.item[nextSlot].SetDefaults(mod.ItemType("SandStoneCross"));
            shop.item[nextSlot].shopCustomPrice = 4;
            shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
            nextSlot++;
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("MoonRing"));
                shop.item[nextSlot].shopCustomPrice = 6;
                shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.AncientBattleArmorMaterial);
                shop.item[nextSlot].shopCustomPrice = 1;
                shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
                nextSlot++;
            }
            shop.item[nextSlot].SetDefaults(mod.ItemType("OrbStand"));
            shop.item[nextSlot].shopCustomPrice = 6;
            shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
            nextSlot++;
            if (Main.eclipse)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("EclipseDrip"));
                shop.item[nextSlot].shopCustomPrice = 2;
                shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
                nextSlot++;
            }
            if (SinsWorld.downedLunarEye)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("LunarDrip"));
                shop.item[nextSlot].shopCustomPrice = 4;
                shop.item[nextSlot].shopSpecialCurrency = SinsMod.RuneStoneCurrencyID;
                nextSlot++;
            }
        }
        public override void NPCLoot()
        {

        }
        public override bool CanGoToStatue(bool toKingStatue)
        {
            toKingStatue = true;
            return true;
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 10;
            knockback = 2f;
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType("StormSpell");
            attackDelay = 1;
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 10f;
            randomOffset = 1.05f;
        }
    }
}