using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.TartarusGuardian
{
    [AutoloadBossHead]
    public class TartarusBody : ModNPC
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
            npc.width = 30;
            npc.height = 30;
            npc.lifeMax = 7500000;
            npc.damage = 500;
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
            npc.value = Item.sellPrice(0, 0, 0, 0);
            npc.alpha = 255;
            npc.npcSlots = 10f;
            npc.dontCountMe = true;
            npc.chaseable = false;
            npc.canGhostHeal = false;
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
            npc.damage = 750;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            Player player = Main.player[npc.target];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            
            if (((damage >= 100000) || modPlayer.NoDamageClass) && !modPlayer.Dev)
            {
                damage = 0;
                return false;
            }
            if (crit)
            {
                if (damage >= 50000)
                {
                    damage = 0;
                    return false;
                }
            }
            damage /= 1000f;
            return base.StrikeNPC(ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                float num = Main.rand.Next(-100, 100) / 100;
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusBody1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusBody2"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusBody3"), npc.scale);
            }
        }
        public override void AI()
        {
            if (TartarusHead.isSegmentAttack || Main.netMode != 1)
            {
                if (Main.rand.Next(3600) == 0)
                {
                    //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("BlackMatter"), Main.expertMode ? 75 : 90, 1f, Main.myPlayer, 0f, 0f);
                }
                if (Main.rand.Next(4800) == 0)
                {
                    //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("SpreadShortcut"), 75, 1f, Main.myPlayer, mod.ProjectileType("BlackMatter"), 1f);
                }
            }
            Player player = Main.player[npc.target];
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
            float num = speed;
            float num2 = turnSpeed;
            Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num3 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float num4 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);
            num3 = (int)(num3 / 16f) * 16;
            num4 = (int)(num4 / 16f) * 16;
            vector2.X = (int)(vector2.X / 16f) * 16;
            vector2.Y = (int)(vector2.Y / 16f) * 16;
            num3 -= vector2.X;
            num4 -= vector2.Y;
            float num5 = (float)Math.Sqrt((num3 * num3 + num4 * num4));
            if (npc.ai[1] > 0f && npc.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    num3 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - vector2.X;
                    num4 = Main.npc[(int)npc.ai[1]].position.Y + (Main.npc[(int)npc.ai[1]].height / 2) - vector2.Y;
                }
                catch
                { }
                npc.rotation = (float)Math.Atan2(num4, num3) + 1.57f;
                num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                int width = npc.width;
                num5 = (num5 - width) / num5;
                num3 *= num5;
                num4 *= num5;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num3;
                npc.position.Y = npc.position.Y + num4;
                if (num3 < 0f)
                {
                    npc.spriteDirection = -1;
                    return;
                }
                if (num3 > 0f)
                {
                    npc.spriteDirection = 1;
                    return;
                }
            }
            else
            {
                num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                float num6 = Math.Abs(num3);
                float num7 = Math.Abs(num4);
                float num8 = num / num5;
                num3 *= num8;
                num4 *= num8;
                if ((npc.velocity.X > 0f && num3 > 0f) || (npc.velocity.X < 0f && num3 < 0f) || (npc.velocity.Y > 0f && num4 > 0f) || (npc.velocity.Y < 0f && num4 < 0f))
                {
                    if (npc.velocity.X < num3)
                    {
                        npc.velocity.X = npc.velocity.X + num2;
                    }
                    else
                    {
                        if (npc.velocity.X > num3)
                        {
                            npc.velocity.X = npc.velocity.X - num2;
                        }
                    }
                    if (npc.velocity.Y < num4)
                    {
                        npc.velocity.Y = npc.velocity.Y + num2;
                    }
                    else
                    {
                        if (npc.velocity.Y > num4)
                        {
                            npc.velocity.Y = npc.velocity.Y - num2;
                        }
                    }
                    if (Math.Abs(num4) < num * 0.2 && ((npc.velocity.X > 0f && num3 < 0f) || (npc.velocity.X < 0f && num3 > 0f)))
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num2 * 2f;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y - num2 * 2f;
                        }
                    }
                    if (Math.Abs(num3) < num * 0.2 && ((npc.velocity.Y > 0f && num4 < 0f) || (npc.velocity.Y < 0f && num4 > 0f)))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num2 * 2f;
                            return;
                        }
                        npc.velocity.X = npc.velocity.X - num2 * 2f;
                        return;
                    }
                }
                else
                {
                    if (num6 > num7)
                    {
                        if (npc.velocity.X < num3)
                        {
                            npc.velocity.X = npc.velocity.X + num2 * 1.1f;
                        }
                        else
                        {
                            if (npc.velocity.X > num3)
                            {
                                npc.velocity.X = npc.velocity.X - num2 * 1.1f;
                            }
                        }
                        if ((Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < num * 0.5)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y + num2;
                                return;
                            }
                            npc.velocity.Y = npc.velocity.Y - num2;
                            return;
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y < num4)
                        {
                            npc.velocity.Y = npc.velocity.Y + num2 * 1.1f;
                        }
                        else
                        {
                            if (npc.velocity.Y > num4)
                            {
                                npc.velocity.Y = npc.velocity.Y - num2 * 1.1f;
                            }
                        }
                        if ((Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < num * 0.5)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num2;
                                return;
                            }
                            npc.velocity.X = npc.velocity.X - num2;
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