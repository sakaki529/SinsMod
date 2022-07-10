using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Lust
{
    public class AsmodeusVamp : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Servant of Asmodeus");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 22;
            npc.height = 18;
            npc.aiStyle = 14;
            npc.lifeMax = 18000;
            npc.damage = 130;
            npc.defense = 80;
            npc.knockBackResist = 0.8f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath4;
            npc.npcSlots = 0.5f;
            npc.buffImmune[31] = false;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 24000;
            npc.damage = 260;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D glowMask = mod.GetTexture("Glow/NPC/AsmodeusVamp_Glow");
            SpriteEffects effects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            spriteBatch.Draw(glowMask, npc.Center - Main.screenPosition, npc.frame, npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 6)
            {
                npc.frameCounter = 0;
                npc.frame.Y = npc.frame.Y + frameHeight;
            }
            if (npc.frame.Y / frameHeight >= Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }
        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            npc.rotation = npc.velocity.X / 15f;
            npc.ai[0]++;
            if (npc.ai[0] >= 120)
            {
                npc.velocity = npc.DirectionTo(Main.player[npc.target].Center) * Main.rand.Next(10, 14);
                npc.ai[0] = 0;
            }
            if (npc.wet)
            {
                if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.95f;
                }
                npc.velocity.Y = npc.velocity.Y - 0.5f;
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
                npc.TargetClosest(true);
            }
            if (npc.direction == -1 && npc.velocity.X > -4f)
            {
                npc.velocity.X = npc.velocity.X - 0.1f;
                if (npc.velocity.X > 4f)
                {
                    npc.velocity.X = npc.velocity.X - 0.1f;
                }
                else
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + 0.05f;
                    }
                }
                if (npc.velocity.X < -4f)
                {
                    npc.velocity.X = -4f;
                }
            }
            else
            {
                if (npc.direction == 1 && npc.velocity.X < 4f)
                {
                    npc.velocity.X = npc.velocity.X + 0.1f;
                    if (npc.velocity.X < -4f)
                    {
                        npc.velocity.X = npc.velocity.X + 0.1f;
                    }
                    else
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - 0.05f;
                        }
                    }
                    if (npc.velocity.X > 4f)
                    {
                        npc.velocity.X = 4f;
                    }
                }
            }
            if (npc.directionY == -1 && npc.velocity.Y > -1.5)
            {
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if (npc.velocity.Y > 1.5)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.05f;
                }
                else
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.03f;
                    }
                }
                if (npc.velocity.Y < -1.5)
                {
                    npc.velocity.Y = -1.5f;
                }
            }
            else
            {
                if (npc.directionY == 1 && npc.velocity.Y < 1.5)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.04f;
                    if (npc.velocity.Y < -1.5)
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.05f;
                    }
                    else
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - 0.03f;
                        }
                    }
                    if (npc.velocity.Y > 1.5)
                    {
                        npc.velocity.Y = 1.5f;
                    }
                }
            }
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}