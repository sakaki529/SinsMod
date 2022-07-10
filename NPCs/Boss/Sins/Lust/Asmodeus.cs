using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Lust
{
    [AutoloadBossHead]
    public class Asmodeus : ModNPC
    {
        private bool FirstPhase = true;
        private bool Esc;
        private int EscTimer;
        private float colorAmount;
        private int summonedSerp;
        public static int summonedSerpOut;
        private bool noVamp;
        public override string BossHeadTexture => "SinsMod/NPCs/Boss/Sins/Lust/Lust_Head_Boss";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asmodeus");
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 140;
            npc.height = 180;
            npc.lifeMax = 2000000;
            npc.damage = 200;
            npc.defense = 70;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            //npc.HitSound = SoundID.NPCHit49;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/Hit1");
            npc.DeathSound = SoundID.NPCDeath51;
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[BuffID.Ichor] = false;
                npc.buffImmune[mod.BuffType("Chroma")] = false;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
            }
            else
            {
                music = MusicID.Boss4;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 300;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (colorAmount > 1.0f)
            {
                colorAmount = 1.0f;
            }
            if (colorAmount < 0f)
            {
                colorAmount = 0f;
            }
            Color color = Color.Lerp(Color.Orange, Color.HotPink, colorAmount);
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D extraTex = mod.GetTexture("Extra/NPC/Asmodeus_Extra");
            SpriteEffects effects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(extraTex, npc.Center - Main.screenPosition, npc.frame, npc.GetAlpha(color), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (target.HasBuff(BuffID.Lovestruck))
            {
                target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " fall in love"), 9999, 0, false);
            }
            else
            {
                target.AddBuff(BuffID.Lovestruck, 600);
                target.AddBuff(BuffID.Ichor, 300);
                target.AddBuff(BuffID.Poisoned, 300);
                target.AddBuff(BuffID.Venom, 300);
                target.AddBuff(BuffID.ChaosState, 300);
            }
        }
        public override bool PreAI()
        {
            noVamp = !NPC.AnyNPCs(mod.NPCType("AsmodeusSerpentHead"));
            npc.dontTakeDamage = !noVamp;
            summonedSerpOut = summonedSerp;
            return base.PreAI();
        }
        public override void AI()
        {
            bool LC = SinsWorld.LimitCut;
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
            if (summonedSerp == 0)
            {
                SummonSerp(player);
                SummonVamp(player);
            }
            if(npc.life < npc.lifeMax / 2 && summonedSerp == 1)
            {
                SummonSerp(player);
                SummonVamp(player);
            }
            if (LC && npc.life < npc.lifeMax / 10 && summonedSerp == 2)
            {
                SummonSerp(player);
                SummonVamp(player);
            }
            /*
             * ai[0]: main state
             * ai[1]: sub state
             */
            if (npc.ai[0] == 0)
            {
                float speed = 10f;
                Vector2 targetPos = player.Center;
                if (npc.ai[1] == 1)
                {
                    targetPos = new Vector2(player.Center.X + (player.Center.X < npc.Center.X ? 600 : -600), player.Center.Y - 480);
                    speed = 14f;
                }
                Vector2 vector = targetPos - npc.Center;
                double magnitude = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                if (magnitude > speed)
                {
                    vector *= speed / (float)magnitude;
                }
                float turnResist = 36f;
                vector = (npc.velocity * turnResist + vector) / (turnResist + 1f);
                magnitude = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                if (magnitude > speed)
                {
                    vector *= speed / (float)magnitude;
                }
                npc.velocity = vector;
                npc.rotation = npc.velocity.X / 32f;
                npc.spriteDirection = player.Center.X < npc.Center.X ? -1 : 1;
                npc.localAI[0]++;
                if (npc.ai[1] == 0)
                {
                    if (npc.localAI[0] > 240 && Main.rand.Next(240) == 0)
                    {
                        npc.ai[1] = 1;
                        npc.localAI[0] = 0;
                        npc.localAI[1] = 0;
                    }
                }
                if (npc.ai[1] == 1)
                {
                    Vector2 vector2 = new Vector2(npc.position.X + (npc.width / 2) + (Main.rand.Next(20) * npc.direction), npc.position.Y + npc.height * 0.8f);
                    if (npc.localAI[0] % 10f == 9f && npc.position.Y + npc.height < player.position.Y && Collision.CanHit(vector2, 1, 1, player.position, player.width, player.height))
                    {
                        Main.PlaySound(SoundID.Item92, npc.Center);
                        float num = 14f;
                        if (Main.expertMode)
                        {
                            num += Main.rand.Next(2, 4);
                        }
                        if (Main.expertMode && npc.life < npc.lifeMax * 0.15)
                        {
                            num += 2f;
                        }
                        float num2 = player.position.X + player.width * 0.5f - vector2.X + Main.rand.Next(-80, 81);
                        float num3 = player.position.Y + player.height * 0.5f - vector2.Y + Main.rand.Next(-40, 41);
                        float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                        num4 = num / num4;
                        num2 *= num4;
                        num3 *= num4;
                        Projectile.NewProjectile(vector2.X, vector2.Y, num2, num3, mod.ProjectileType("SeraphBlast"), 80, 0f, Main.myPlayer, 0f, 0f);
                    }
                    if (npc.localAI[0] > 180 && Main.rand.Next(200) == 0)
                    {
                        npc.ai[1] = 0;
                        npc.localAI[0] = 0;
                        npc.localAI[1] = 0;
                        npc.ai[0] = 1;
                    }
                }
            }
            else if (npc.ai[0] == 1)
            {
                npc.rotation = npc.velocity.X / 20;
                npc.spriteDirection = npc.velocity.X < 0 ? -1 : 1;
                if (npc.ai[1] == 0)
                {
                    int maxCharged = 1;
                    if (LC && npc.life < npc.lifeMax * 0.4f)
                    {
                        maxCharged = 4;
                    }
                    else if (LC || (Main.expertMode && npc.life < npc.lifeMax * 0.4f) || npc.life < npc.lifeMax * 0.1f)
                    {
                        maxCharged = 3;
                    }
                    else if (Main.expertMode || npc.life < npc.lifeMax * 0.75f)
                    {
                        maxCharged = 2;
                    }
                    if (npc.localAI[1] >= maxCharged)
                    {
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                        npc.localAI[0] = 0;
                        npc.localAI[1] = 0;
                        return;
                    }
                    float velMult = Main.expertMode ? (LC ? 36 : 34) : 30;
                    float newVel = MathHelper.Clamp(velMult + 8f * (1.0f - npc.life / npc.lifeMax), velMult, velMult + 8);
                    npc.velocity = npc.DirectionTo(player.Center) * newVel;
                    Main.PlaySound(SoundID.ForceRoar, (int)npc.Center.X, (int)npc.Center.Y, -1, 1f, 0f);
                    npc.ai[1] = 1;
                    npc.localAI[0] = 0;
                    npc.localAI[1]++;
                }
                else if (npc.ai[1] == 1)
                {
                    npc.localAI[0]++;
                    npc.velocity *= 0.99f; 
                    if (npc.localAI[0] > 45)
                    {
                        npc.ai[1] = 0;
                    }
                }
            }
        }
        private void SummonSerp(Player player)
        {
            if (player.whoAmI > -1 && !player.dead && player.active)
            {
                int num = summonedSerp > 0 ? 6 : 4;
                for (int i = 0; i < num; i++)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("AsmodeusSerpentHead"));
                }
            }
            summonedSerp++;
        }
        private void SummonVamp(Player player)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 vampPos = player.Center + Utils.RotatedBy(new Vector2(640, 0), i / 10 + i * (6.28f / 10f), default(Vector2));
                NPC.NewNPC((int)vampPos.X, (int)vampPos.Y, mod.NPCType("AsmodeusVamp"), npc.whoAmI, 0, 0, 0);
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return null;
        }
        public override bool CheckDead()
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 48, mod.NPCType("Lust"));
            return true;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}