using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod
{
    public class SinsNPC : GlobalNPC
    {	
        //PreDraw
        public bool deathEffect;
        public bool trail;
        public bool pulse;
        public bool drawCenter;
        public bool glow;
        //SinsBossEffect
        public bool EnvyBoss;
        public bool GluttonyBoss;
        public bool GreedBoss;
        public bool LustBoss;
        public bool PrideBoss;
        public bool SlothBoss;
        public bool WrathBoss;
        public bool OriginBoss;
        public bool MadnessBoss;
        //Buff
        public bool Agony;
        public bool AbyssalFlame;
        public bool Chroma;
        public bool DistortionSeeing;
        public bool Nightmare;
        public bool Sin;
        public bool SuperSlow;
        public bool Nothingness;
        public bool RuneBuffEnvy;
        public bool RuneBuffGluttony;
        public bool RuneBuffGreed;
        public bool RuneBuffLust;
        public bool RuneBuffPride;
        public bool RuneBuffSloth;
        public bool RuneBuffWrath;
        //stat
        public bool BlackBruteBuff;
        //Misc
        private bool HopelessLoot;
        //InstantDummyStat
        internal static int InstantDummyDefence;
        internal static float InstantDummyDamegeMultiplier = 1.0f;
        internal static bool InstantDummyBuffImmunity = false;
        internal static float InstantDummyScale = 1.0f;
        //ai
        internal float damageMult = 1.0f;
        internal bool defenceMode;
        public float[] modAI = new float[4];

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override void ResetEffects(NPC npc)
        {
            //Buff
            AbyssalFlame = false;
            Agony = false;
            Chroma = false;
            DistortionSeeing = false;
            Nightmare = false;
            Sin = false;
            SuperSlow = false;
            Nothingness = false;
            RuneBuffEnvy = false;
            RuneBuffGluttony = false;
            RuneBuffGreed = false;
            RuneBuffLust = false;
            RuneBuffPride = false;
            RuneBuffSloth = false;
            RuneBuffWrath = false;
            //stat
            BlackBruteBuff = false;
        }
        public override void SetDefaults(NPC npc)
        {
            //base.SetDefaults(npc);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (deathEffect && npc.ai[3] != 0f)
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
            if (trail && !deathEffect)
            {
                for (int i = 0; i < NPCID.Sets.TrailCacheLength[npc.type]; i++)
                {
                    int num = Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type];
                    int num2 = num * (int)npc.frameCounter;
                    Rectangle rectangle = new Rectangle(0, num2, Main.npcTexture[npc.type].Width, num);
                    Vector2 vector = rectangle.Size() / 2f;
                    Color color = npc.GetAlpha(drawColor);
                    color.R = (byte)(color.R * (10 - i) / 20);
                    color.G = (byte)(color.G * (10 - i) / 20);
                    color.B = (byte)(color.B * (10 - i) / 20);
                    color.A = (byte)(color.A * (10 - i) / 20);
                    Main.spriteBatch.Draw(Main.npcTexture[npc.type], npc.oldPos[i] + npc.Size / 2f - Main.screenPosition + new Vector2(0f, npc.gfxOffY), npc.frame/*rectangle*/, color, npc.rotation, vector, npc.scale, npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                }
            }
            if (pulse && !deathEffect)
            {
                Texture2D texture = Main.npcTexture[npc.type];
                Vector2 vector = npc.Center - Main.screenPosition;
                Vector2 vector2 = new Vector2(texture.Width / 2, texture.Height / Main.npcFrameCount[npc.type] / 2);
                Vector2 value = vector - new Vector2(300f, 310f);
                Color color = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
                float num = 0f;
                vector -= new Vector2(texture.Width, texture.Height / Main.npcFrameCount[npc.type]) * npc.scale / 2f;
                vector += vector2 * npc.scale + new Vector2(0f, num + 0 + npc.gfxOffY);
                float scaleFactor = 4f + (npc.GetAlpha(color).ToVector3() - new Vector3(0.5f)).Length() * 4f;
                for (int j = 0; j < 4; j++)
                {
                    Main.spriteBatch.Draw(texture, vector + npc.velocity.RotatedBy(j * 1.57079637f, default(Vector2)) * scaleFactor, npc.frame, new Color(64, 64, 64, 64) * npc.Opacity, npc.rotation, vector2, npc.scale, npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                }
            }
            if (drawCenter)
            {
                Texture2D texture = Main.npcTexture[npc.type];
                SpriteEffects spriteEffects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), npc.frame, npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
                return false;
            }
            if (glow)
            {

            }
            return base.PreDraw(npc, spriteBatch, drawColor);
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (deathEffect)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            }
            if (npc.type == mod.NPCType("DesertKingHand"))
            {
                Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f - 5f * npc.ai[0], npc.position.Y + 20f);
                int num;
                for (int i = 0; i < 2; i = num + 1)
                {
                    float num2 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - vector.X;
                    float num3 = Main.npc[(int)npc.ai[1]].position.Y + (Main.npc[(int)npc.ai[1]].height / 2) - vector.Y;
                    float num4;
                    if (i == 0)
                    {
                        num2 -= 200f * npc.ai[0];
                        num3 += 130f - 50;
                        num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                        num4 = 92f / num4;
                        vector.X += num2 * num4;
                        vector.Y += num3 * num4;
                    }
                    else
                    {
                        num2 -= 50f * npc.ai[0];
                        num3 += 80f - 50;
                        num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                        num4 = 60f / num4;
                        vector.X += num2 * num4;
                        vector.Y += num3 * num4;
                    }
                    float rotation = (float)Math.Atan2(num3, num2) - 1.57f;
                    Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
                    Main.spriteBatch.Draw(mod.GetTexture("Extra/NPC/DesertKingHand_Extra"), new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.boneArmTexture.Width, Main.boneArmTexture.Height)), npc.GetAlpha(color), rotation, new Vector2((float)Main.boneArmTexture.Width * 0.5f, (float)Main.boneArmTexture.Height * 0.5f), 0.8f, SpriteEffects.None, 0f);
                    if (i == 0)
                    {
                        vector.X += num2 * num4 / 2f;
                        vector.Y += num3 * num4 / 2f;
                    }
                    else
                    {
                        if (npc.active)
                        {
                            vector.X += num2 * num4 - 16f;
                            vector.Y += num3 * num4 - 6f;
                            int num5 = Dust.NewDust(new Vector2(vector.X, vector.Y), 30, 10, 5, num2 * 0.02f, num3 * 0.02f, 0, default(Color), 1.5f);
                            Main.dust[num5].noGravity = true;
                        }
                    }
                    num = i;
                }
            }
            //base.PostDraw(npc, spriteBatch, drawColor);
        }
        public override bool CheckDead(NPC npc)
        {
            if (deathEffect)
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
                return true;
            }
            return base.CheckDead(npc);
        }
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.TargetDummy)
            {
                if (BossActiveCheck())
                {
                    return false;
                }
            }
            return base.CanBeHitByItem(npc, player, item);
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type == NPCID.TargetDummy)
            {
                if (BossActiveCheck())
                {
                    return false;
                }
            }
            return base.CanBeHitByProjectile(npc, projectile);
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (BlackBruteBuff)
            {
                damage = (int)((double)damage * 1.5f);
            }
            base.ModifyHitPlayer(npc, target, ref damage, ref crit);
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.type == mod.ProjectileType("Adoration") && npc.type != NPCID.TargetDummy && !npc.boss)
            {
                damage = 0;
            }
        }
        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            Player player = Main.player[npc.target];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            SinsWorld modWorld = ModContent.GetInstance<SinsWorld>();
            bool debug = modPlayer.Debug;
            if (SinsWorld.Hopeless)
            {
                damage *= 0.9f;
            }
            if (BlackBruteBuff)
            {
                damage *= 0.95f;
            }
            damage *= damageMult;
            if (defenceMode)
            {
                damage = 1;
                return false;
            }
            if (crit)
            {
                if (npc.type == mod.NPCType("BlackCrystalNoMove") || npc.type == mod.NPCType("BlackCrystal") || npc.type == mod.NPCType("BlackCrystalSmall") || npc.type == mod.NPCType("BlackCrystalCore") || npc.type == mod.NPCType("BlackCrystalCoreClone") || npc.type == mod.NPCType("BCCSummonAttack") || npc.type == mod.NPCType("BCCSummonHeal") || npc.type == mod.NPCType("MirrorShield") || npc.type == mod.NPCType("WillOfMadness"))
                {
                    if (modPlayer.UniverseSoul)
                    {
                        damage /= 2.5f;
                    }
                    if (modPlayer.EternitySoul)
                    {
                        damage /= 4.5f;
                    }
                    if (!modPlayer.UniverseSoul && !modPlayer.EternitySoul)
                    {
                        damage *= 2;
                    }
                    if (damage >= 20000 && !debug)
                    {
                        damage = 0;
                        if (npc.type != mod.NPCType("WillOfMadness"))
                        {
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCResist").WithVolume(0.5f), npc.Center);
                        }
                        return false;
                    }
                }
                if (npc.type == mod.NPCType("TartarusHead") || npc.type == mod.NPCType("TartarusBody") || npc.type == mod.NPCType("TartarusBody2"))
                {
                    if (modPlayer.UniverseSoul)
                    {
                        damage /= 2.5f;
                    }
                    if (modPlayer.EternitySoul)
                    {
                        damage /= 4.5f;
                    }
                }
                if (npc.type == mod.NPCType("TartarusTail"))
                {
                    if (modPlayer.UniverseSoul)
                    {
                        damage /= 2.0f;
                    }
                    if (modPlayer.EternitySoul)
                    {
                        damage /= 3.5f;
                    }
                }
            }
            if (npc.type == mod.NPCType("BlackCrystalNoMove") || npc.type == mod.NPCType("BlackCrystal") || npc.type == mod.NPCType("BlackCrystalSmall") || npc.type == mod.NPCType("MirrorShield"))
            {
                damage /= 2;
                if (damage >= 20000 && !debug)
                {
                    damage = 0;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCResist").WithVolume(0.5f), npc.Center);
                }
                return false;
            }
            if (npc.type == mod.NPCType("BlackCrystalCore") || npc.type == mod.NPCType("BlackCrystalCoreClone") || npc.type == mod.NPCType("BCCSummonAttack") || npc.type == mod.NPCType("BCCSummonHeal"))
            {
                damage /= 3;
                if ((damage >= 10000 || modPlayer.NoDamageClass) && !debug)
                {
                    damage = 0;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCResist").WithVolume(0.5f), npc.Center);
                }
                return false;
            }
            if (npc.type == mod.NPCType("WillOfMadness"))
            {
                damage /= 5;
                if ((damage >= 10000 || modPlayer.NoDamageClass) && !debug)
                {
                    damage = 0;
                }
                return false;
            }
            return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void AI(NPC npc)
        {
            bool LimitCut = SinsWorld.LimitCut;
            if (npc.aiStyle == 5 || npc.aiStyle == 14)//demon eye and bat
            {
                if (npc.type != mod.NPCType("AsmodeusVamp"))
                {
                    if (LimitCut)
                    {
                        modAI[0]++;
                        if (modAI[0] >= 120 && Main.rand.Next(150) == 0)
                        {
                            npc.velocity = npc.DirectionTo(Main.player[npc.target].Center) * Main.rand.Next(6, 11);
                            modAI[0] = 0;
                        }
                    }
                }
            }
            if (npc.type == NPCID.TargetDummy)
            {
                npc.dontTakeDamage = BossActiveCheck();
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                if (LimitCut)
                {
                    if (npc.ai[0] == 0f)
                    {
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                    }
                }
            }
        }
        public static bool BossActiveCheck()
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].boss || Main.npc[i].type == NPCID.EaterofWorldsHead))
                {
                    return true;
                }
            }
            return false;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
            if (AbyssalFlame)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 4440;
                if (damage < 444)
                {
                    damage = 444;
                }
            }
            if (Chroma)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 3108;
                if (damage < 777)
                {
                    damage = 777;
                }
            }
            if (Nightmare)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 10000;
				if (damage < 2000)
				{
					damage = 2000;
				}
			}
            if (Sin)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 6660;
                if (damage < 666)
                {
                    damage = 666;
                }
            }
            if (SuperSlow)
            {
                npc.velocity /= 5;
            }
        }
		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
            if (AbyssalFlame)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 21, npc.velocity.X * 0.6f, -1f, 0, default(Color), 0.75f);
                    int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 21, npc.velocity.X * 0.6f, -3f, 200, default(Color), 1.5f);
                    int dust3 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 21, npc.velocity.X * 0.6f, -1.5f, 200, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.5f;
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].velocity *= 1.5f;
                    Main.dust[dust3].noGravity = true;
                    Main.dust[dust3].velocity *= 1.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].scale *= 0.25f;
                        Main.dust[dust2].scale *= 0.75f;
                        Main.dust[dust3].scale *= 0.75f;
                    }
                }
            }
            if (Chroma)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 267, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, Main.DiscoColor, 1.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].scale *= 0.8f;
                    }
                }
                Lighting.AddLight(npc.Center, Main.DiscoR / 255, Main.DiscoG / 255, Main.DiscoB / 255);
            }
            if (Nightmare)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 182, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.5f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust].scale *= 1.2f;
					}
				}
				Lighting.AddLight(npc.Center, 0.8f, 0.0f, 0.0f);
            }
            if (Sin)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("Black"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default(Color), 1.6f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity *= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].scale *= 0.8f;
                    }
                }
                drawColor = new Color(40, 40, 40);
            }
            if (SuperSlow)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 235, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.9f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale *= 0.7f;
                    }
                }
                drawColor = Color.DarkGray;
            }
        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Demolitionist)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("CautionBlock"));
                nextSlot++;
            }
            if (type == NPCID.Steampunker)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("MysticSolution"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("DistortionSolution"));
                nextSlot++;
            }
            if (type == NPCID.DyeTrader)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("GrayscaleDye"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("InvisibleDye"));
                nextSlot++;
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("TechnoDyeRed"));
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(mod.ItemType("TechnoDyeLightblue"));
                    nextSlot++;
                }
            }
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (BossActiveCheck())
            {
                spawnRate = 0;
                maxSpawns = 0;
            }
        }
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.GetModPlayer<SinsPlayer>().ZoneMadness)
            {
                ClearPoolWithExceptions(pool);
                pool[0] = 0f;
            }
            if (spawnInfo.player.GetModPlayer<SinsPlayer>().ZoneTartarus)
            {
                ClearPoolWithExceptions(pool);
                pool.Add(mod.NPCType("BlackBeast"), 0.1f);
                pool.Add(mod.NPCType("BlackShark"), 0.4f);
            }
        }
        internal void ClearPoolWithExceptions(IDictionary<int, float> pool)
        {
            try
            {
                Dictionary<int, float> keepPool = new Dictionary<int, float>();
                foreach (var kvp in pool)
                {
                    int npcID = kvp.Key;
                    ModNPC mnpc = NPCLoader.GetNPC(npcID);
                    if (mnpc != null && mnpc.mod != null) //splitting so you can add other exceptions if need be
                    {
                        /*if (mnpc.mod.Name.Equals("GRealm")) //do not remove GRealm spawns
                        {
                            keepPool.Add(npcID, kvp.Value);
                        }*/
                    }
                }
                pool.Clear();
                foreach (var newkvp in keepPool)
                {
                    pool.Add(newkvp.Key, newkvp.Value);
                }
                keepPool.Clear();
            }
            catch (Exception)
            {
                if (Main.netMode != 1)
                {

                }
            }
        }
        public override void NPCLoot(NPC npc)
        {
            bool limitCut = SinsWorld.LimitCut;
            bool hopeless = SinsWorld.Hopeless;
            if (npc.type == NPCID.Retinazer)
            {
                if (!Main.expertMode)
                {
                    if (Main.rand.Next(8) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LaserAntenna"));
                    }
                }
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                if (SinsWorld.NightEnergyDropped == 0)
                {
                    SinsWorld.MeteorOreDrop(mod.TileType("NightEnergizedOre"));
                }
                if (SinsWorld.NightEnergyDropped > 0 && SinsWorld.NightEnergyDropped < 3)
                {
                    if (!Main.dayTime && Main.rand.NextBool(10 * SinsWorld.NightEnergyDropped))
                    {
                        SinsWorld.MeteorOreDrop(mod.TileType("NightEnergizedOre"));
                    }
                }
                if (!Main.expertMode)
                {
                    if (Main.rand.Next(8) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThrowerEmblem"));
                    }
                    if (Main.rand.Next(6) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DemonGaze"));
                    }
                }
            }
            if (npc.type == NPCID.RuneWizard)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RuneStone"), 1);
                if (Main.expertMode)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RuneStone"), Main.rand.Next(1, 3));
                }
                else if(Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RuneStone"), 1);
                }
            }
            if (npc.type == NPCID.DukeFishron)
            {
                if (Main.expertMode && hopeless)
                {
                    if (Main.rand.Next(1000) == 0)//0.25%
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FishronWrath"));
                    }
                }
            }
            if (npc.type == NPCID.LunarTowerVortex)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VortexPillarMask"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VortexPillarTrophy"));
                }
            }
            if (npc.type == NPCID.CultistBoss)
            {
                if (Main.rand.Next(Main.expertMode ? 10 : 20) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RuneStone"), 1);
                }
            }
            if (npc.type == NPCID.Mothron)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    if (Main.rand.Next(Main.expertMode ? 3 : 4) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenHeroYoyo"));
                    }
                }
                if (Main.rand.Next(Main.expertMode ? 4 : 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EclipseDrip"), Main.rand.Next(1, 3));
                }
            }
            if (npc.type == NPCID.LunarTowerStardust)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StardustPillarMask"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StardustPillarTrophy"));
                }
            }
            if (npc.type == NPCID.LunarTowerNebula)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NebulaPillarMask"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NebulaPillarTrophy"));
                }
            }
            if (npc.type == NPCID.LunarTowerSolar)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SolarPillarMask"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SolarPillarTrophy"));
                }
            }
            if (hopeless)//Dont edit here
            {
                string note = "—¼bool‚ª‚È‚¢‚ÆƒNƒ‰ƒbƒVƒ…";
                if(!HopelessLoot)
                {
                    HopelessLoot = true;
                    npc.NPCLoot();
                }
                HopelessLoot = true;
            }
        }
    }
}