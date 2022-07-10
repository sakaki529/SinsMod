using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;

namespace SinsMod.NPCs.Boss.Madness
{
    public class MirrorShield : ModNPC
    {
        public int Count;
        public bool Start = false;
        private bool Esc = false;
        private bool Teleporting = true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("‹¾");
        }
        public override void SetDefaults()
        {
            npc.width = 20;
            npc.height = 100;
            if (BlackCrystalCore.isThirdPhase)
            {
                npc.lifeMax = 100000;
                npc.damage = 300;
            }
            else if (BlackCrystalCore.isSecondPhase)
            {
                npc.lifeMax = 75000;
                npc.damage = 300;
            }
            else
            {
                npc.lifeMax = 50000;
                npc.damage = 300;
            }
            npc.aiStyle = -1;
            aiType = -1;
            npc.defense = 500;
            npc.knockBackResist = 0f;
            //npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCCCKilled");
            npc.npcSlots = 10f;
            npc.netAlways = true;
            npc.reflectingProjectiles = true;
            npc.scale = 1.5f;
            npc.alpha = 255;
            npc.timeLeft = 1800;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (BlackCrystalCore.isThirdPhase)
            {
                npc.lifeMax = 100000 + 10000 * numPlayers;
                npc.damage = 300;
            }
            else if (BlackCrystalCore.isSecondPhase)
            {
                npc.lifeMax = 75000 + 10000 * numPlayers;
                npc.damage = 300;
            }
            else
            {
                npc.lifeMax = 50000 + 10000 * numPlayers;
                npc.damage = 300;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D extra = mod.GetTexture("Extra/NPC/MirrorShield_Extra");
            SpriteEffects effects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            spriteBatch.Draw(extra, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 2; i++)
            {
                int d = Dust.NewDust(npc.position, npc.width, npc.height, 235, hitDirection, -1f, 50, default(Color), 0.9f);
                Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
            }
            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (npc.width / 2);
                npc.position.Y = npc.position.Y + (npc.height / 2);
                for (int j = 0; j < 20; j++)
                {
                    int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 235, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num].velocity *= 3f;
                    Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num].scale = 0.5f;
                        Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int k = 0; k < 40; k++)
                {
                    int num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 245, 0f, 0f, 100, default(Color), 1.0f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 5f;
                    num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 245, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num2].velocity *= 2f;
                }
            }
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            return false;
        }
        private void Resist(ref double damage)
        {
            damage = 0;
            damage *= 0;
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCResist").WithVolume(0.5f), npc.position);
        }
        public override bool PreAI()
        {
            Player player = Main.player[npc.target];
            npc.velocity = Vector2.Zero;
            npc.netUpdate = true;
            npc.spriteDirection = 0;
            if (!Start)
            {
                npc.alpha -= 20;
                if (npc.alpha <= 0)
                {
                    npc.alpha = 0;
                    Start = true;
                }
                return false;
            }
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
            }
            if (player.dead || !player.active)
            {
                Esc = true;
            }
            if (npc.timeLeft < 50 || !Main.npc[(int)npc.ai[0]].active || Esc)
            {
                npc.alpha += 20;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
                return false;
            }
            NewAI(player);
            return false;
        }
        private void NewAI(Player player)
        {
            float dist = Vector2.Distance(player.Center, npc.Center);
            float num = 3200f;
            if (dist > num)
            {
                npc.alpha += 20;
                if (npc.alpha >= 255)
                {
                    //npc.Center = new Vector2(player.Center.X - Main.npc[(int)npc.ai[0]].Center.X, player.Center.Y - Main.npc[(int)npc.ai[0]].Center.Y);
                    Teleporting = true;
                }
            }
            if (Teleporting)
            {
                npc.alpha -= 20;
                if (npc.alpha <= 0)
                {
                    npc.alpha = 0;
                    Teleporting = false;
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.0f;
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}