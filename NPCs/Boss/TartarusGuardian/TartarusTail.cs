using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.TartarusGuardian
{
    [AutoloadBossHead]
    public class TartarusTail : ModNPC
    {
        private bool Spawn;
        private const float speed = 22f;
        private const float turnSpeed = 0.44f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian of Tartarus");
            NPCID.Sets.TrailingMode[npc.type] = 1;
            NPCID.Sets.TrailCacheLength[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 30;
            npc.lifeMax = 7500000;
            npc.damage = 300;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit56;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/TartarusRoar");
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.alpha = 255;
            npc.npcSlots = 10f;
            npc.dontCountMe = true;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DSDM");
            }
            else
            {
                music = MusicID.Boss3;
            }
            npc.GetGlobalNPC<SinsNPC>().trail = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 10000000 + 500000 * numPlayers;
            npc.damage = 600;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            Player player = Main.player[npc.target];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            damage *= 0.95f;
            if (((damage >= 60000) || modPlayer.NoDamageClass) && !modPlayer.Dev)
            {
                damage = 0;
                return false;
            }
            if (crit)
            {
                if (damage >= 30000)
                {
                    damage = 0;
                    return false;
                }
            }
            return base.StrikeNPC(ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                float num = Main.rand.Next(-100, 100) / 100;
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusTail1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusTail2"), npc.scale);
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (Spawn)
            {
                npc.alpha = Main.npc[(int)npc.ai[1]].alpha;
            }
            if (Main.npc[(int)npc.ai[1]].alpha < 128 && !Spawn)
            {
                npc.alpha -= 42;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                    Spawn = true;
                }
            }
            /*if (player.dead || !player.active)
            {
                npc.alpha = Main.npc[(int)npc.ai[1]].alpha;
            }
            else if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                npc.alpha -= 42;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }*/
            npc.alpha = Main.npc[(int)npc.ai[1]].alpha;
            if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[2]].type != mod.NPCType("TartarusHead"))
            {
                npc.life = 0;
                if (npc.alpha <= 254)
                {
                    npc.HitEffect(0, 1);
                }
                npc.active = false;
            }
            int num = (int)(npc.position.X / 16f) - 1;
            int num2 = (int)((npc.position.X + npc.width) / 16f) + 2;
            int num3 = (int)(npc.position.Y / 16f) - 1;
            int num4 = (int)((npc.position.Y + npc.height) / 16f) + 2;
            if (num < 0)
            {
            }
            if (num2 > Main.maxTilesX)
            {
                num2 = Main.maxTilesX;
            }
            if (num3 < 0)
            {
            }
            if (num4 > Main.maxTilesY)
            {
                num4 = Main.maxTilesY;
            }
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(false);
            }
            float num5 = speed;
            float num6 = turnSpeed;
            Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num7 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float num8 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);
            num7 = (int)(num7 / 16f) * 16;
            num8 = (int)(num8 / 16f) * 16;
            vector.X = (int)(vector.X / 16f) * 16;
            vector.Y = (int)(vector.Y / 16f) * 16;
            num7 -= vector.X;
            num8 -= vector.Y;
            float num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
            if (npc.ai[1] > 0f && npc.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    num7 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - vector.X;
                    num8 = Main.npc[(int)npc.ai[1]].position.Y + (Main.npc[(int)npc.ai[1]].height / 2) - vector.Y;
                }
                catch
                { }
                npc.rotation = (float)Math.Atan2(num8, num7) + 1.57f;
                num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
                int width = npc.width;
                num9 = (num9 - width) / num9;
                num7 *= num9;
                num8 *= num9;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num7;
                npc.position.Y = npc.position.Y + num8;
                if (num7 < 0f)
                {
                    npc.spriteDirection = -1;
                    return;
                }
                if (num7 > 0f)
                {
                    npc.spriteDirection = 1;
                    return;
                }
            }
            else
            {
                num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
                float num10 = Math.Abs(num7);
                float num11 = Math.Abs(num8);
                float num12 = num5 / num9;
                num7 *= num12;
                num8 *= num12;
                if ((npc.velocity.X > 0f && num7 > 0f) || (base.npc.velocity.X < 0f && num7 < 0f) || (npc.velocity.Y > 0f && num8 > 0f) || (npc.velocity.Y < 0f && num8 < 0f))
                {
                    if (npc.velocity.X < num7)
                    {
                        npc.velocity.X = npc.velocity.X + num6;
                    }
                    else
                    {
                        if (npc.velocity.X > num7)
                        {
                            npc.velocity.X = npc.velocity.X - num6;
                        }
                    }
                    if (npc.velocity.Y < num8)
                    {
                        npc.velocity.Y = npc.velocity.Y + num6;
                    }
                    else
                    {
                        if (npc.velocity.Y > num8)
                        {
                            npc.velocity.Y = npc.velocity.Y - num6;
                        }
                    }
                    if (Math.Abs(num8) < num5 * 0.2 && ((npc.velocity.X > 0f && num7 < 0f) || (npc.velocity.X < 0f && num7 > 0f)))
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num6 * 2f;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y - num6 * 2f;
                        }
                    }
                    if (Math.Abs(num7) < num5 * 0.2 && ((npc.velocity.Y > 0f && num8 < 0f) || (npc.velocity.Y < 0f && num8 > 0f)))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num6 * 2f;
                            return;
                        }
                        npc.velocity.X = npc.velocity.X - num6 * 2f;
                        return;
                    }
                }
                else
                {
                    if (num10 > num11)
                    {
                        if (npc.velocity.X < num7)
                        {
                            npc.velocity.X = npc.velocity.X + num6 * 1.1f;
                        }
                        else
                        {
                            if (npc.velocity.X > num7)
                            {
                                npc.velocity.X = npc.velocity.X - num6 * 1.1f;
                            }
                        }
                        if ((Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < num5 * 0.5)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y + num6;
                                return;
                            }
                            npc.velocity.Y = npc.velocity.Y - num6;
                            return;
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y < num8)
                        {
                            npc.velocity.Y = npc.velocity.Y + num6 * 1.1f;
                        }
                        else
                        {
                            if (npc.velocity.Y > num8)
                            {
                                npc.velocity.Y = npc.velocity.Y - num6 * 1.1f;
                            }
                        }
                        if ((Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < num5 * 0.5)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num6;
                                return;
                            }
                            npc.velocity.X = npc.velocity.X - num6;
                        }
                    }
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}