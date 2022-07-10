using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Envy
{
    [AutoloadBossHead]
    public class Envy : ModNPC
    {
        internal const int size = 120;
        internal const int particleSize = 12;
        internal IList<Particle> particles = new List<Particle>();
        internal float[,] aura = new float[size, size];
        private bool SecondPhase = true;
        private bool Esc;
        private int EscTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sin of Envy");
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 96;
            npc.height = 96;
            npc.lifeMax = 26000;
            npc.damage = 120;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true; 
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath62;
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 90, 0, 0);
            bossBag = mod.ItemType("EnvyBag");
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[BuffID.Ichor] = false;
                npc.buffImmune[mod.BuffType("Nightmare")] = false;
                npc.buffImmune[mod.BuffType("Chroma")] = false;
                npc.buffImmune[mod.BuffType("SuperSlow")] = false;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
            }
            else
            {
                music = MusicID.Boss2;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 160;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[3] == 0f)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        Vector2 drawPos = npc.Center - new Vector2(size / 2, size / 2) - Main.screenPosition;
                        drawPos.X += x * 2 - size / 2;
                        drawPos.Y += y * 2 - size / 2;
                        spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Sins/Envy/Particle"), drawPos, null, npc.GetAlpha(Color.White) * aura[x, y], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            else
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                MiscShaderData deathShader = GameShaders.Misc["SinsMod:DeathAnimation"];
                deathShader.UseOpacity(1f);
                if (npc.ai[3] > 30f)
                {
                    deathShader.UseOpacity(1f - (npc.ai[3] - 30f) / 150f);
                }
                if (npc.ai[3] != 0f)
                {
                    deathShader.Apply(null);
                }
            }
            Texture2D texture = Main.npcTexture[npc.type];
            SpriteEffects spriteEffects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.rand.NextBool(10))
            {
                for (int i = 0; i < Main.rand.Next(8, 13); i++)
                {
                    Vector2 vec = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, -51));
                    vec.Normalize();
                    Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-npc.width / 2, npc.width / 2 + 1), npc.Center.Y + Main.rand.Next(0, npc.height / 2 + 1), vec.X * 12f, vec.Y * 12f, mod.ProjectileType("CursedFlame"), (int)(damage * 0.6), 0f, Main.myPlayer, 0f, 0f);
                }
            }
            base.HitEffect(hitDirection, damage);
        }
        public override bool PreAI()
        {
            if (!Main.dedServ)
            {
                Particle.UpdateParticles(size, particleSize, aura, particles);
            }
            if (npc.ai[3] > 0f)
            {
                ResetEffects();
                for (int i = 0; i < npc.buffImmune.Length; i++)
                {
                    npc.buffImmune[i] = true;
                }
                if (npc.rotation != 0f)
                {
                    npc.rotation *= 0.95f;
                    if (npc.rotation < 0.05f || npc.rotation > -0.05f)
                    {
                        npc.rotation = 0f;
                    }
                }
                npc.dontTakeDamage = true;
                if (npc.ai[3] == 1f)
                {
                    Main.PlaySound(npc.DeathSound, npc.Center);
                }
                npc.ai[3] += 1f;
                //npc.velocity = Vector2.UnitY * npc.velocity.Length();
                npc.velocity.X *= 0.95f;
                npc.velocity.Y *= 0.95f;
                if (npc.velocity.X < 0.5f && npc.velocity.Y < 0.5f)
                {
                    npc.velocity = Vector2.Zero;
                }
                if (npc.ai[3] > 120f)
                {
                    //npc.Opacity = 1f - (npc.ai[3] - 120f) / 60f;
                }
                if (Main.rand.NextBool(5) && npc.ai[3] < 120f)
                {
                    for (int dustNumber = 0; dustNumber < 3; dustNumber++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.Left, npc.width, npc.height / 2, 242, 0f, 0f, 0, SinsColor.Envy, 1f)];
                        dust.position = npc.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(npc.width * 1.5f, npc.height * 1.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
                        dust.velocity.X = 0f;
                        dust.velocity.Y = -Math.Abs(dust.velocity.Y - dustNumber + npc.velocity.Y - 4f) * 3f;
                        dust.noGravity = true;
                        dust.fadeIn = 1f;
                        dust.scale = 1f + Main.rand.NextFloat() + dustNumber * 0.3f;
                    }
                }
                if (npc.ai[3] >= 180f)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
                return false;
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
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (player.dead || !player.active)
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
            if (SecondPhase)
            {
                
            }
            /*{
                for (int i = 0; i < Main.rand.Next(8, 13); i++)
                {
                    Vector2 vec = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-50, -101));
                    vec.Normalize();
                    Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-npc.width / 2, npc.width / 2 + 1), npc.Center.Y + Main.rand.Next(0, npc.height / 2 + 1), vec.X * 12f, vec.Y * 12f, CursedFlame, (int)(npc.damage * 0.6), 0f, Main.myPlayer, 0f, 0f);
                }
            }*/
        }
        public override bool CheckDead()
        {
            if (npc.ai[3] == 0f)
            {
                npc.ai[3] = 1f;
                npc.damage = 0;
                npc.life = npc.lifeMax;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                return false;
            }
            return base.CheckDead();
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return null;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EnvyTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EssenceOfEnvy"), Main.rand.Next(4, 9));
                switch (Main.rand.Next(3))
                {
                    case 0:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                        break;
                    case 1:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                        break;
                    case 2:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                        break;
                }
            }
            SinsWorld.downedEnvy = true;
        }
    }
}