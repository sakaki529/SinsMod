using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Wrath
{
    [AutoloadBossHead]
    public class EyeOfSatan : ModNPC
    {
        private bool FirstPhase = true;
        private bool SecondPhase;
        private bool defenceMode;
        private bool Esc;
        private int EscTimer;
        public override string BossHeadTexture => "SinsMod/NPCs/Boss/Sins/Wrath/Wrath_Head_Boss";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Eye of Satan");
            Main.npcFrameCount[npc.type] = 4;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 96;
            npc.height = 96;
            npc.lifeMax = 1800000;
            npc.damage = 300;
            npc.defense = 180;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath10;
            npc.npcSlots = 5f;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[BuffID.Ichor] = false;
                npc.buffImmune[mod.BuffType("Nightmare")] = false;
                npc.buffImmune[mod.BuffType("Chroma")] = false;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
            }
            else
            {
                music = MusicID.Boss1;
            }
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 450;
        }
        public override void FindFrame(int frameHeight)
        {
            int minFrameHeightMult = defenceMode ? 2 : 0;
            int maxFrame = defenceMode ? 4 : 2;
            npc.frameCounter++;
            if (npc.frameCounter >= 12)
            {
                npc.frame.Y += frameHeight;
                npc.frameCounter = 0;
            }
            if (npc.frame.Y >= frameHeight * maxFrame || npc.frame.Y < frameHeight * minFrameHeightMult)
            {
                npc.frame.Y = frameHeight * minFrameHeightMult;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
            if (npc.life <= 0)
            {
                for (int j = 0; j < 50; j++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2 * hitDirection, -1f, 0, default(Color), 1f);
                }
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), npc.velocity, mod.GetGoreSlot("Gores/Boss/EoS1"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + npc.height / 2), npc.velocity, mod.GetGoreSlot("Gores/Boss/EoS2"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X + npc.width / 2, npc.position.Y), npc.velocity, mod.GetGoreSlot("Gores/Boss/EoS2"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + npc.height / 2), npc.velocity, mod.GetGoreSlot("Gores/Boss/EoS3"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X + npc.width / 2, npc.position.Y), npc.velocity, mod.GetGoreSlot("Gores/Boss/EoS3"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X + npc.width / 2, npc.position.Y + npc.height / 2), npc.velocity, mod.GetGoreSlot("Gores/Boss/EoS1"), npc.scale);
                for (int k = 0; k < 4; k++)
                {
                    int num = Utils.SelectRandom(Main.rand, new int[]
                    {
                        mod.GetGoreSlot("Gores/Boss/EoS4"),
                        mod.GetGoreSlot("Gores/Boss/EoS5"),
                        mod.GetGoreSlot("Gores/Boss/EoS6")
                    });
                    Vector2 velocity = new Vector2(Main.rand.Next(-80, 81) * 0.1f, Main.rand.Next(-60, 21) * 0.1f);
                    Gore.NewGore(npc.Center + new Vector2(Main.rand.Next(0, npc.width + 1), Main.rand.Next(0, npc.height + 1)), velocity, num, 1f);
                }
            }
        }
        public override bool PreAI()
        {
            if (defenceMode)
            {
                npc.GetGlobalNPC<SinsNPC>().damageMult = 0.01f;
            }
            if (npc.velocity.X >= 0f)
            {
                npc.spriteDirection = 1;
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
            }
            return base.PreAI();
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.rotation = (float)Math.Atan2(player.Center.Y - npc.Center.Y, player.Center.X - npc.Center.X) - 1.57f;
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            if (player.dead || !player.active || Main.dayTime)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (player.dead || !player.active || Main.dayTime)
                {
                    Esc = true;
                }
            }
            if (Esc)
            {
                npc.velocity.Y--;
                EscTimer++;
                if (EscTimer > 120)
                {
                    npc.active = false;
                }
                return;
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return null;
        }
        public override bool CheckDead()
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 48, mod.NPCType("Wrath"));
            return true;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}