using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Madness
{
    [AutoloadBossHead]
    public class BlackCrystalNoMove : ModNPC
    {
        private bool Esc;
        private int Stage = 0;
        private int ShieldStrength = 100;
        private const int ShieldStrengthMax = 100;
        private int ShieldCounter;
        private int SpawnCountdown = 120;
        private int[] Count = new int[2];
        public override string BossHeadTexture => "SinsMod/NPCs/Boss/Madness/BlackCrystalSmall";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Crystal");
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.width = 26; 
            npc.height = 48; 
            npc.lifeMax = 2500000; 
            npc.damage = 300; 
            npc.defense = 100;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.lavaImmune = true; 
            npc.noGravity = true;  
            npc.noTileCollide = true; 
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCKilled");
            npc.npcSlots = 5f;
            npc.netAlways = true;
            npc.scale = 2.0f;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AI");
            }
            else
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AI");
            }
            npc.GetGlobalNPC<SinsNPC>().pulse = true;
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 4000000 + 250000 * numPlayers;
            npc.damage = 300;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            float num = (float)ShieldStrength / ShieldStrengthMax;
            if (ShieldStrength > 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

                var center = npc.Center - Main.screenPosition;
                float num2 = 0f;
                if (npc.ai[3] > 0f && npc.ai[3] <= 30f)
                {
                    num2 = 1f - npc.ai[3] / 30f;
                }
                DrawData drawData = new DrawData(TextureManager.Load("Images/Misc/noise"), center - new Vector2(0, 10), new Rectangle(0, 0, 600, 600), Color.Lerp(SinsColor.MediumBlack, Color.Black, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.4f + 0.5f) * (num * 0.8f + 0.2f), npc.rotation, new Vector2(300f, 300f), npc.scale * (0.4f + num2 * 0.05f), SpriteEffects.None, 0);
                GameShaders.Misc["ForceField"].UseColor(new Vector3(1f + num2 * 0.5f));
                GameShaders.Misc["ForceField"].Apply(drawData);
                drawData.Draw(Main.spriteBatch);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
                return;
            }
            if (npc.ai[3] > 0f)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                var center = npc.Center - Main.screenPosition;
                float num3 = npc.ai[3] / 120f;
                float num4 = Math.Min(npc.ai[3] / 30f, 1f);
                DrawData drawData = new DrawData(TextureManager.Load("Images/Misc/Perlin"), center - new Vector2(0, 10), new Rectangle(0, 0, 600, 600), new Color(new Vector4(1f - (float)Math.Sqrt(num4))), npc.rotation, new Vector2(300f, 300f), npc.scale * (0.4f + num4), SpriteEffects.None, 0);
                GameShaders.Misc["ForceField"].UseColor(new Vector3(2f));
                GameShaders.Misc["ForceField"].Apply(drawData);
                drawData.Draw(Main.spriteBatch);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
                return;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = 0;
            npc.frameCounter++;
            if (npc.frameCounter >= 4)
            {
                npc.frameCounter = 0;
                npc.frame.Y = npc.frame.Y + frameHeight;
            }
            if (npc.frame.Y / frameHeight >= Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 2; i++)
            {
                int d = Dust.NewDust(npc.position, npc.width, npc.height, 186, hitDirection, -1f, 0, default(Color), 1.0f);
                Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (npc.width / 2);
                npc.position.Y = npc.position.Y + (npc.height / 2);
                npc.width = 50;
                npc.height = 50;
                npc.position.X = npc.position.X - (npc.width / 2);
                npc.position.Y = npc.position.Y - (npc.height / 2);
                for (int j = 0; j < 20; j++)
                {
                    int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num].velocity *= 3f;
                    Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num].scale = 0.5f;
                        Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int k = 0; k < 40; k++)
                {
                    int num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 100, default(Color), 1.0f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 5f;
                    Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                    num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 245, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num2].velocity *= 2f;
                    Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                }
            }
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            return false;
        }
        public override bool PreAI()
        {
            if (ShieldStrength > 0)
            {
                npc.dontTakeDamage = true;
                for (int i = 0; i < 255; i++)
                {
                    Player player = Main.player[i];
                    if (player.active)
                    {
                        if (player.Center.Y < npc.Center.Y && Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 6400f)
                        {
                            player.GetModPlayer<SinsPlayer>().ZoneMadness = true;
                        }
                    }
                }
            }
            else if (npc.dontTakeDamage)
            {
                npc.ai[3] = 1f;
                npc.dontTakeDamage = false;
            }
            if (npc.ai[3] > 0)
            {
                npc.ai[3]--;
            }
            return true;
        }
        public override void AI()
        {
            npc.spriteDirection = 0;
            Player player = Main.player[npc.target];
            player.releaseMount = false;
            if (player.mount.Active)
            {
                player.mount.Dismount(player);
            }
            //player.ManageSpecialBiomeVisuals("WaterDistortion", player.active, new Vector2(player.position.X, player.position.Y));
            //player.ManageSpecialBiomeVisuals("HeatDistortion", player.active, new Vector2(player.position.X, player.position.Y));
            bool flag = npc.localAI[0] == 0f;
            if (flag)
            {
                npc.localAI[0] = npc.Center.Y;
                npc.netUpdate = true;
            }
            bool flag2 = npc.Center.Y >= npc.localAI[0];
            if (flag2)
            {
                npc.localAI[1] = -1f;
                npc.netUpdate = true;
            }
            bool flag3 = npc.Center.Y <= npc.localAI[0] - 10f;
            if (flag3)
            {
                npc.localAI[1] = 1f;
                npc.netUpdate = true;
            }
            npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y + 0.05f * npc.localAI[1], -2f, 2f);
            npc.velocity.X *= 0f;
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
                npc.alpha += 4;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
                return;
            }
            npc.ai[2]++;
            if (npc.ai[2] >= 5f)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player target = Main.player[i];
                    if (target.active && !target.dead)
                    {
                        ShineAttack(target.whoAmI);
                    }
                }
                npc.ai[2] = -20f;
            }
            Count[0]++;
            if (Count[0] == 1200)
            {
                Vector2 center = npc.Center;
                if (Main.player[Main.myPlayer].active && !Main.player[Main.myPlayer].dead && Vector2.Distance(Main.player[Main.myPlayer].Center, center) < 2800f)
                {
                    Main.PlaySound(SoundID.Zombie, (int)Main.player[Main.myPlayer].position.X, (int)Main.player[Main.myPlayer].position.Y, 104, 1f, 0f);
                }
                if (Main.netMode != 1)
                {
                    npc.TargetClosest(false);
                    Vector2 vector = player.Center - npc.Center;
                    vector.Normalize();
                    float num3 = -1f;
                    if (vector.X < 0f)
                    {
                        num3 = 1f;
                    }
                    vector = vector.RotatedBy(-num3 * 6.2831854820251465 / 6.0, default(Vector2));
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector.X, vector.Y, mod.ProjectileType("MadnessRay"), 100, 0f, Main.myPlayer, num3 * 6.28318548f / 225f/*450*/, npc.whoAmI);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -vector.X, -vector.Y, mod.ProjectileType("MadnessRay"), 100, 0f, Main.myPlayer, num3 * 6.28318548f / 225f/*450*/, npc.whoAmI);
                    npc.netUpdate = true;
                }
            }
            if (Count[0] > 1260)
            {
                Count[0] = 0;
            }
            //Count[1]++;
            if (Count[1] == 180)
            {
                //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("SpreadShortcut"), 85, 1f, Main.myPlayer, mod.ProjectileType("BlackMatter"), npc.life < npc.lifeMax / 2 ? 0f : 2f);
                int projectilesNum = Main.rand.Next(7, 8);
                if (Main.expertMode)
                {
                    projectilesNum += Main.rand.Next(3, 4);
                }
                for (int i = 0; i < projectilesNum; i++)
                {
                    float num = 1f + Main.rand.Next(-25, 126) / 100f;
                    double radians = Main.rand.Next(-180, 180) * 3.1415926535897931 / 180.0;
                    Vector2 vector = new Vector2(0f, -5f * num).RotatedBy(radians, default(Vector2));
                    int num2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector.X, vector.Y, mod.ProjectileType("BlackMatter"), 85, 1f, Main.myPlayer, 0f, 0f);
                    if (npc.life > npc.lifeMax / 2)
                    {
                        Main.projectile[num2].tileCollide = true;
                    }
                }
                Count[1] = 0;
            }
            if (!npc.dontTakeDamage)
            {

            }
            if (Stage == 5 && ShieldStrength <= 25)
            {
                if (ShieldStrength > 0)
                {
                    ShieldCounter++;
                    if (ShieldCounter >= 4)
                    {
                        ShieldStrength--;
                        ShieldCounter = 0;
                    }
                }
            }
            else if (Stage == 4 && ShieldStrength <= 50)
            {
                int bossS4 = mod.NPCType("");
                bool anyBossS4 = NPC.AnyNPCs(bossS4);
                if (ShieldStrength > 25)
                {
                    if (ShieldCounter >= 4)
                    {
                        ShieldStrength--;
                        ShieldCounter = 0;
                    }
                }
                if (ShieldStrength == 25)
                {
                    if (!anyBossS4 && npc.ai[0] == 255)
                    {
                        npc.ai[0] = 0;
                        Stage = 5;
                    }
                    else if (!anyBossS4 && SpawnCountdown <= 0)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, bossS4);
                        SpawnCountdown = 120;
                    }
                    else if (!anyBossS4 && SpawnCountdown > 0)
                    {
                        SpawnCountdown--;
                    }
                }
            }
            else if (Stage == 3 && ShieldStrength <= 75)
            {
                int bossS3 = mod.NPCType("");
                bool anyBossS3 = NPC.AnyNPCs(bossS3);
                if (ShieldStrength > 50)
                {
                    if (ShieldCounter >= 4)
                    {
                        ShieldStrength--;
                        ShieldCounter = 0;
                    }
                }
                if (ShieldStrength == 50)
                {
                    if (!anyBossS3 && npc.ai[0] == 255)
                    {
                        npc.ai[0] = 0;
                        Stage = 4;
                    }
                    else if (!anyBossS3 && SpawnCountdown <= 0)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, bossS3);
                        SpawnCountdown = 120;
                    }
                    else if (!anyBossS3 && SpawnCountdown > 0)
                    {
                        SpawnCountdown--;
                    }
                }
            }
            else if (Stage == 2)
            {
                int bossS2 = mod.NPCType("");
                bool anyBossS2 = NPC.AnyNPCs(bossS2);
                if (ShieldStrength > 75)
                {
                    if (ShieldCounter >= 4)
                    {
                        ShieldStrength--;
                        ShieldCounter = 0;
                    }
                }
                if (ShieldStrength == 75)
                {
                    if (!anyBossS2 && npc.ai[0] == 255)
                    {
                        npc.ai[0] = 0;
                        Stage = 3;
                    }
                    else if (!anyBossS2 && SpawnCountdown <= 0)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, bossS2);
                        SpawnCountdown = 120;
                    }
                    else if (!anyBossS2 && SpawnCountdown > 0)
                    {
                        SpawnCountdown--;
                    }
                }
            }
            else if (Stage == 1)
            {
                int bossS1 = mod.NPCType("");
                bool anyBossS1 = NPC.AnyNPCs(bossS1);
                if (!anyBossS1 && npc.ai[0] == 255)
                {
                    npc.ai[0] = 0;
                    Stage = 2;
                }
                else if (!anyBossS1 && SpawnCountdown <= 0)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, bossS1);
                    SpawnCountdown = 120;
                }
                else if (!anyBossS1 && SpawnCountdown > 0)
                {
                    SpawnCountdown--;
                }
            }
        }
        private void ShineAttack(int target)
        {
            if (!Main.player[target].active || Main.player[target].dead)
            {
                return;
            }
            Vector2 vec = npc.DirectionTo(Main.player[target].Center);
            if (vec.HasNaNs())
            {
                vec = Vector2.UnitY;
            }
            int direction = (vec.X > 0f) ? 1 : -1;
            npc.direction = direction;
            if (Main.player[Main.myPlayer].active)
            {
                Vector2 vector = Main.player[target].position + Main.player[target].Size * Utils.RandomVector2(Main.rand, 0f, 1f) - npc.Center;
                int num3;
                for (int i = 0; i < 3; i = num3 + 1)
                {
                    Vector2 vector2 = npc.Center + vector;
                    if (i > 0)
                    {
                        vector2 = npc.Center + vector.RotatedByRandom(0.78539818525314331) * (Main.rand.NextFloat() * 0.5f + 0.75f);
                    }
                    float x = Main.rgbToHsl(new Color(20 + (int)(Main.DiscoR * 1.0f), 0, 20 + (int)(Main.DiscoR * 1.0f))).X;
                    Projectile.NewProjectile(vector2.X, vector2.Y, 0f, 0f, mod.ProjectileType("BlackCrystalExplosion"), 200, 0f, Main.myPlayer, -1, npc.whoAmI);
                    num3 = i;
                }
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (Esc)
            {
                return false;
            }
            scale = 1.2f;
            return null;
        }
        public override bool CheckDead()
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 41, mod.NPCType("BlackCrystal"), npc.whoAmI);
            return true;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}