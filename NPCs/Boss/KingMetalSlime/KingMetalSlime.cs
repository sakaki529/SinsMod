using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.KingMetalSlime
{
    [AutoloadBossHead]
    public class KingMetalSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal King");
            Main.npcFrameCount[npc.type] = 6;
            animationType = NPCID.KingSlime;
        }
        public override void SetDefaults()
        {
            npc.width = 85;
            npc.height = 59;
            npc.lifeMax = 800;
            npc.damage = 82;
            npc.knockBackResist = 50f;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.npcSlots = 5f;
            npc.scale = 2f;
            npc.value = Item.sellPrice(0, 30, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[mod.BuffType("SuperSlow")] = false;
            }
            bossBag = mod.ItemType("KingMetalSlimeBag");
            music = MusicID.Boss3;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 108;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage = 1;
            if(crit)
            {
                damage = 2;
            }
            if(Main.expertMode)
            {
                if (Main.rand.Next(25) == 0)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("MetalSlimeNoLoot")); ;
                            break;
                        case 1:
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SpikedMetalSlime"));
                            break;
                    }
                    return false;
                }
                if(Main.rand.Next(150) == 0)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("BigMetalSlimeNoLoot"));
                    return false;
                }
            }
            else
            {
                if (Main.rand.Next(40) == 0)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("MetalSlimeNoLoot"));
                    return false;
                }
                if (Main.rand.Next(200) == 0)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("BigMetalSlimeNoLoot"));
                    return false;
                }
            }
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, 2.5f * hitDirection, -1.5f, 0, default(Color), 2.2f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, -2.5f * hitDirection, -1.5f, 0, default(Color), 2.2f);
                }
                Dust.NewDust(npc.position, npc.width, npc.height, 11, 2.5f * hitDirection, -2.5f, 0, default(Color), 1.4f);
                Dust.NewDust(npc.position, npc.width, npc.height, 11, 2.5f * hitDirection, -2.5f, 0, default(Color), 1.4f);
                Dust.NewDust(npc.position, npc.width, npc.height, 11, -2.5f * hitDirection, -2.5f, 0, default(Color), 1.4f);
                Dust.NewDust(npc.position, npc.width, npc.height, 1, -2.5f * hitDirection, -2.5f, 0, default(Color), 1.4f);
            }
        }
        public override void AI()
        {
            npc.spriteDirection = 0;
            Player player = Main.player[npc.target];
            npc.knockBackResist = 0.2f * Main.knockBackMultiplier;
            npc.dontTakeDamage = false;
            npc.noTileCollide = false;
            npc.noGravity = false;
            npc.reflectingProjectiles = false;
            if (npc.ai[0] != 7f && Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.ai[0] = 7f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                }
            }
            if (npc.ai[0] == 0f)
            {
                npc.TargetClosest(true);
                Vector2 vector = Main.player[npc.target].Center - npc.Center;
                if (npc.velocity.X != 0f || npc.velocity.Y > 100f || npc.justHit || vector.Length() < 80f)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    return;
                }
            }
            else
            {
                if (npc.ai[0] == 1f)
                {
                    npc.ai[1] += 1f;
                    if (npc.ai[1] > 36f)
                    {
                        npc.ai[0] = 2f;
                        npc.ai[1] = 0f;
                        return;
                    }
                }
                else
                {
                    if (npc.ai[0] == 2f)
                    {
                        if ((Main.player[npc.target].Center - npc.Center).Length() > 600f)
                        {
                            npc.ai[0] = 5f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                        }
                        if (npc.velocity.Y == 0f)
                        {
                            npc.TargetClosest(true);
                            npc.velocity.X = npc.velocity.X * 0.85f;
                            npc.ai[1] += 1f;
                            float num = 15f + 30f * ((float)npc.life / npc.lifeMax);
                            float num2 = 3f + 4f * (1f - (float)npc.life / npc.lifeMax);
                            float num3 = 4f;
                            if (!Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                            {
                                num3 += 2f;
                            }
                            if (npc.ai[1] > num)
                            {
                                npc.ai[3] += 1f;
                                if (npc.ai[3] >= 3f)
                                {
                                    npc.ai[3] = 0f;
                                    num3 *= 2f;
                                    num2 /= 2f;
                                }
                                npc.ai[1] = 0f;
                                npc.velocity.Y = npc.velocity.Y - num3;
                                npc.velocity.X = num2 * npc.direction;
                            }
                        }
                        else
                        {
                            npc.knockBackResist = 0f;
                            npc.velocity.X = npc.velocity.X * 0.99f;
                            if (npc.direction < 0 && npc.velocity.X > -1f)
                            {
                                npc.velocity.X = -1f;
                            }
                            if (npc.direction > 0 && npc.velocity.X < 1f)
                            {
                                npc.velocity.X = 1f;
                            }
                        }
                        npc.ai[2] += 1f;
                        if (npc.ai[2] > 210.0 && npc.velocity.Y == 0f && Main.netMode != 1)
                        {
                            int num4 = Main.rand.Next(3);
                            if (num4 == 0)
                            {
                                npc.ai[0] = 3f;
                            }
                            else
                            {
                                if (num4 == 1)
                                {
                                    npc.ai[0] = 4f;
                                    npc.noTileCollide = true;
                                    npc.velocity.Y = -8f;
                                }
                                else
                                {
                                    if (num4 == 2)
                                    {
                                        npc.ai[0] = 6f;
                                    }
                                    else
                                    {
                                        npc.ai[0] = 2f;
                                    }
                                }
                            }
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            return;
                        }
                    }
                    else
                    {
                        if (npc.ai[0] == 3f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.85f;
                            npc.dontTakeDamage = true;
                            npc.ai[1] += 1f;
                            if (npc.ai[1] >= 180f)
                            {
                                npc.ai[0] = 2f;
                                npc.ai[1] = 0f;
                            }
                            if (Main.expertMode)
                            {
                                npc.ReflectProjectiles(npc.Hitbox);
                                npc.reflectingProjectiles = true;
                                return;
                            }
                        }
                        else
                        {
                            if (npc.ai[0] == 4f)
                            {
                                npc.noTileCollide = true;
                                npc.noGravity = true;
                                npc.knockBackResist = 0f;
                                if (npc.velocity.X < 0f)
                                {
                                    npc.direction = -1;
                                }
                                else
                                {
                                    npc.direction = 1;
                                }
                                //npc.spriteDirection = npc.direction;
                                npc.TargetClosest(true);
                                Vector2 center = Main.player[npc.target].Center;
                                center.Y -= 350f;
                                Vector2 vector2 = center - npc.Center;
                                if (npc.ai[2] == 1f)
                                {
                                    npc.ai[1] += 1f;
                                    vector2 = Main.player[npc.target].Center - npc.Center;
                                    vector2.Normalize();
                                    vector2 *= 8f;
                                    npc.velocity = (npc.velocity * 4f + vector2) / 5f;
                                    if (npc.ai[1] > 6f)
                                    {
                                        npc.ai[1] = 0f;
                                        npc.ai[0] = 4.1f;
                                        npc.ai[2] = 0f;
                                        npc.velocity = vector2;
                                        return;
                                    }
                                }
                                else
                                {
                                    if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) < 40f && npc.Center.Y < Main.player[npc.target].Center.Y - 300f)
                                    {
                                        npc.ai[1] = 0f;
                                        npc.ai[2] = 1f;
                                        return;
                                    }
                                    vector2.Normalize();
                                    vector2 *= 12f;
                                    npc.velocity = (npc.velocity * 5f + vector2) / 6f;
                                    return;
                                }
                            }
                            else
                            {
                                if (npc.ai[0] == 4.1f)
                                {
                                    npc.knockBackResist = 0f;
                                    if (npc.ai[2] == 0f && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1) && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                                    {
                                        npc.ai[2] = 1f;
                                    }
                                    if (npc.position.Y + npc.height >= Main.player[npc.target].position.Y || npc.velocity.Y <= 0f)
                                    {
                                        npc.ai[1] += 1f;
                                        if (npc.ai[1] > 10f)
                                        {
                                            npc.ai[0] = 2f;
                                            npc.ai[1] = 0f;
                                            npc.ai[2] = 0f;
                                            npc.ai[3] = 0f;
                                            if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                                            {
                                                npc.ai[0] = 5f;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (npc.ai[2] == 0f)
                                        {
                                            npc.noTileCollide = true;
                                            npc.noGravity = true;
                                            npc.knockBackResist = 0f;
                                        }
                                    }
                                    npc.velocity.Y = npc.velocity.Y + 0.2f;
                                    if (npc.velocity.Y > 16f)
                                    {
                                        npc.velocity.Y = 16f;
                                        return;
                                    }
                                }
                                else
                                {
                                    if (npc.ai[0] == 5f)
                                    {
                                        if (npc.velocity.X > 0f)
                                        {
                                            npc.direction = 1;
                                        }
                                        else
                                        {
                                            npc.direction = -1;
                                        }
                                        //npc.spriteDirection = npc.direction;
                                        npc.noTileCollide = true;
                                        npc.noGravity = true;
                                        npc.knockBackResist = 0f;
                                        Vector2 value = Main.player[npc.target].Center - npc.Center;
                                        value.Y -= 4f;
                                        if (value.Length() < 200f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                                        {
                                            npc.ai[0] = 2f;
                                            npc.ai[1] = 0f;
                                            npc.ai[2] = 0f;
                                            npc.ai[3] = 0f;
                                        }
                                        if (value.Length() > 10f)
                                        {
                                            value.Normalize();
                                            value *= 10f;
                                        }
                                        npc.velocity = (npc.velocity * 4f + value) / 5f;
                                        return;
                                    }
                                    if (npc.ai[0] == 6f)
                                    {
                                        npc.knockBackResist = 0f;
                                        if (npc.velocity.Y == 0f)
                                        {
                                            npc.TargetClosest(true);
                                            npc.velocity.X = npc.velocity.X * 0.8f;
                                            npc.ai[1] += 1f;
                                            if (npc.ai[1] > 5f)
                                            {
                                                npc.ai[1] = 0f;
                                                npc.velocity.Y = npc.velocity.Y - 4f;
                                                if (Main.player[npc.target].position.Y + Main.player[npc.target].height < npc.Center.Y)
                                                {
                                                    npc.velocity.Y = npc.velocity.Y - 1.25f;
                                                }
                                                if (Main.player[npc.target].position.Y + Main.player[npc.target].height < npc.Center.Y - 40f)
                                                {
                                                    npc.velocity.Y = npc.velocity.Y - 1.5f;
                                                }
                                                if (Main.player[npc.target].position.Y + Main.player[npc.target].height < npc.Center.Y - 80f)
                                                {
                                                    npc.velocity.Y = npc.velocity.Y - 1.75f;
                                                }
                                                if (Main.player[npc.target].position.Y + Main.player[npc.target].height < npc.Center.Y - 120f)
                                                {
                                                    npc.velocity.Y = npc.velocity.Y - 2f;
                                                }
                                                if (Main.player[npc.target].position.Y + Main.player[npc.target].height < npc.Center.Y - 160f)
                                                {
                                                    npc.velocity.Y = npc.velocity.Y - 2.25f;
                                                }
                                                if (Main.player[npc.target].position.Y + Main.player[npc.target].height < npc.Center.Y - 200f)
                                                {
                                                    npc.velocity.Y = npc.velocity.Y - 2.5f;
                                                }
                                                if (!Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                                                {
                                                    npc.velocity.Y = npc.velocity.Y - 2f;
                                                }
                                                npc.velocity.X = 12 * npc.direction;
                                                npc.ai[2] += 1f;
                                            }
                                        }
                                        else
                                        {
                                            npc.velocity.X = npc.velocity.X * 0.98f;
                                            if (npc.direction < 0 && npc.velocity.X > -8f)
                                            {
                                                npc.velocity.X = -8f;
                                            }
                                            if (npc.direction > 0 && npc.velocity.X < 8f)
                                            {
                                                npc.velocity.X = 8f;
                                            }
                                        }
                                        if (npc.ai[2] >= 3f && npc.velocity.Y == 0f)
                                        {
                                            npc.ai[0] = 2f;
                                            npc.ai[1] = 0f;
                                            npc.ai[2] = 0f;
                                            npc.ai[3] = 0f;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (npc.ai[0] == 7f)
                                        {
                                            npc.damage = 0;
                                            npc.life = npc.lifeMax;
                                            npc.defense = 9999;
                                            npc.noTileCollide = true;
                                            npc.alpha += 7;
                                            if (npc.alpha > 255)
                                            {
                                                npc.alpha = 255;
                                            }
                                            npc.velocity.X = npc.velocity.X * 0.98f;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("KingMetalSlimeTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("KingMetalSlimeMask"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(20, 31));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.IronBar, Main.rand.Next(10, 21));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.HallowedBar, Main.rand.Next(15, 31));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofFright, Main.rand.Next(6, 14));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofMight, Main.rand.Next(6, 14));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofSight, Main.rand.Next(6, 14));
                if (Main.rand.Next(20) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MetalDetector);
                }
            }
            SinsWorld.downedKingMetalSlime = true;
        }
    }
}
