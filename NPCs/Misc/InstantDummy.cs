using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Misc
{
    public class InstantDummy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Instant Dummy");
            Main.npcFrameCount[npc.type] = 11;
        }
        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 48;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 2147483647;
            npc.HitSound = SoundID.NPCHit15;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.chaseable = true;
            npc.trapImmune = false;
            npc.lavaImmune = false;
            npc.scale = SinsNPC.InstantDummyScale;
        }
        public override void AI()
        {
            npc.damage *= 0;
            npc.aiStyle *= 0;//ƒ}ƒ‹ƒ`‚ÅAI‚ª‹¶‚¤ƒoƒO‚ð‹­ˆø‚É–h‚®
            npc.defense = SinsNPC.InstantDummyDefence;
            npc.scale = SinsNPC.InstantDummyScale;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = SinsNPC.InstantDummyBuffImmunity;
            }
            if (Main.rand.Next(20) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        npc.HitSound = SoundID.NPCHit15;
                        break;
                    case 1:
                        npc.HitSound = SoundID.NPCHit16;
                        break;
                    case 2:
                        npc.HitSound = SoundID.NPCHit17;
                        break;
                }
            }
            if (SinsPlayer.DelInstantDummy)
            {
                npc.ai[0] = 255;
                npc.life = 0;
                npc.lifeRegen = 0;
                npc.checkDead();
                return;
            }
            if (SinsNPC.BossActiveCheck())
            {
                npc.ai[0] = 255;
                npc.life = 0;
                return;
            }
            npc.life = npc.lifeMax;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool CheckDead()
        {
            if (npc.ai[0] == 255)
            {
                return true;
            }
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage *= SinsNPC.InstantDummyDamegeMultiplier;
            return base.StrikeNPC(ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.life = npc.lifeMax;
            if (hitDirection != npc.direction)//³–Ê
            {
                npc.frame.Y = 400;
                npc.localAI[0] = 1f;
                npc.localAI[1] = 0f;
            }
            if (hitDirection == npc.direction)//”w–Ê
            {
                npc.frame.Y = 250;
                npc.localAI[0] = 0f;
                npc.localAI[1] = 1f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.localAI[0] >= 1f)
            {
                if (npc.localAI[0] == 1f)
                {
                    npc.frameCounter = 0;
                    npc.localAI[0]++;
                }
                if (npc.localAI[0] > 1f)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter > 3)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = npc.frame.Y - frameHeight;
                    }
                    if (npc.frame.Y == 200)
                    {
                        npc.frame.Y = 0;
                        npc.localAI[0] = 0f;
                    }
                }
            }
            if (npc.localAI[1] >= 1f)
            {
                if (npc.localAI[1] == 1f)
                {
                    npc.frameCounter = 0;
                    npc.localAI[1]++;
                }
                if (npc.localAI[1] > 1f)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter > 3)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = npc.frame.Y - frameHeight;
                    }
                    if (npc.frame.Y == 0)
                    {
                        npc.localAI[1] = 0f;
                    }
                }
            }
        }
    }
}