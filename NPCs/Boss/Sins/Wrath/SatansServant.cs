using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Wrath
{
    public class SatansServant : ModNPC
    {
        private bool defenceMode;
        private bool Esc;
        private int EscTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Satan's Servant");
            Main.npcFrameCount[npc.type] = 12;
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 32;
            npc.lifeMax = 80000;
            npc.damage = 120;
            npc.defense = 20;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.lavaImmune = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit9;
            npc.DeathSound = SoundID.NPCDeath12;
            npc.npcSlots = 1f;
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
            npc.damage = 180;
        }
        public override void FindFrame(int frameHeight)
        {
            int minFrameHeightMult = defenceMode ? 6 : 0;
            int maxFrame = defenceMode ? 12 : 6;
            npc.frameCounter++;
            if (npc.frameCounter >= 5)
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
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
                }
                return;
            }
            if (npc.life <= 0)
            {
                for (int j = 0; j < 20; j++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
                }
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Boss/SatansServant1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Boss/SatansServant2"), npc.scale);
            }
        }
        public override bool PreAI()
        {
            npc.boss = true;
            if (defenceMode)
            {
                npc.GetGlobalNPC<SinsNPC>().damageMult = 0.01f;
            }
            if (npc.velocity.X >= 0f)
            {
                npc.spriteDirection = 1;
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 3.14f;
            }
            return base.PreAI();
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
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
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.5f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                {
                    npc.velocity.X = 2f;
                }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                {
                    npc.velocity.X = -2f;
                }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                {
                    npc.velocity.Y = 1f;
                }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                {
                    npc.velocity.Y = -1f;
                }
            }
            npc.TargetClosest(true);
            float[] acceleX = new float[2];
            acceleX[0] = 0.6f;
            acceleX[1] = 0.3f;
            float[] acceleY = new float[3];
            acceleY[0] = 0.3f;
            acceleY[1] = 0.35f;
            acceleY[2] = 0.2f;
            float maxVelX = 12f;
            float maxVelY = 7f;
            if (npc.direction == -1 && npc.velocity.X > -maxVelX)
            {
                npc.velocity.X = npc.velocity.X - acceleX[0];
                if (npc.velocity.X > maxVelX)
                {
                    npc.velocity.X = npc.velocity.X - acceleX[0];
                }
                else
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + acceleX[1];
                    }
                }
                if (npc.velocity.X < -maxVelX)
                {
                    npc.velocity.X = -maxVelX;
                }
            }
            else
            {
                if (npc.direction == 1 && npc.velocity.X < maxVelX)
                {
                    npc.velocity.X = npc.velocity.X + acceleX[0];
                    if (npc.velocity.X < -maxVelX)
                    {
                        npc.velocity.X = npc.velocity.X + acceleX[0];
                    }
                    else
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - acceleX[1];
                        }
                    }
                    if (npc.velocity.X > maxVelX)
                    {
                        npc.velocity.X = maxVelX;
                    }
                }
            }
            if (npc.directionY == -1 && npc.velocity.Y > -maxVelY)
            {
                npc.velocity.Y = npc.velocity.Y - acceleY[0];
                if (npc.velocity.Y > maxVelY)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleY[1];
                }
                else
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleY[2];
                    }
                }
                if (npc.velocity.Y < -maxVelY)
                {
                    npc.velocity.Y = -maxVelY;
                }
            }
            else
            {
                if (npc.directionY == 1 && npc.velocity.Y < maxVelY)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleY[0];
                    if (npc.velocity.Y < -maxVelY)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleY[1];
                    }
                    else
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - acceleY[2];
                        }
                    }
                    if (npc.velocity.Y > maxVelY)
                    {
                        npc.velocity.Y = maxVelY;
                    }
                }
            }
            npc.ai[1] += 1f;
            if (npc.ai[1] > 200f)
            {
                npc.ai[1] = 0f;
                float acceleX2 = 0.2f;
                float acceleY2 = 0.1f;
                float maxVelX2 = 4f;
                float maxVelY2 = 1.5f;
                acceleX2 = 0.6f;
                acceleY2 = 0.4f;
                maxVelX2 = 32f;
                maxVelY2 = 16f;
                if (npc.ai[1] > 1000f)
                {
                    npc.ai[1] = 0f;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] > 0f)
                {
                    if (npc.velocity.Y < maxVelY2)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleY2;
                    }
                }
                else
                {
                    if (npc.velocity.Y > -maxVelY2)
                    {
                        npc.velocity.Y = npc.velocity.Y - acceleY2;
                    }
                }
                if (npc.ai[2] < -150f || npc.ai[2] > 150f)
                {
                    if (npc.velocity.X < maxVelX2)
                    {
                        npc.velocity.X = npc.velocity.X + acceleX2;
                    }
                }
                else
                {
                    if (npc.velocity.X > -maxVelX2)
                    {
                        npc.velocity.X = npc.velocity.X - acceleX2;
                    }
                }
                if (npc.ai[2] > 300f)
                {
                    npc.ai[2] = -300f;
                }
            }
        }
        public override bool PreNPCLoot()
        {
            if (!Main.expertMode || Main.rand.Next(5) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Heart, 1, false, 0, false, false);
            }
            return false;
        }
    }
}