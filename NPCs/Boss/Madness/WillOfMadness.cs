using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Madness
{
    public class WillOfMadness : ModNPC
    {
        private bool Esc;
        private int Players = 1;
        private int EscTimer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Will of Madness");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 22;
            npc.height = 22;
            npc.lifeMax = 50000;
            npc.damage = 100;
            npc.defense = 200;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.npcSlots = 5f;
            npc.netAlways = true;
            npc.scale = 2f;
            npc.value = Item.buyPrice(60, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            bossBag = mod.ItemType("MadnessBag");
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AI");
            }
            else
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AI");
            }
            if (npc.ai[0] != 32767)
            {
                npc.GetGlobalNPC<SinsNPC>().pulse = true;
                npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[0] == 32767)
            {
                Color color = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
                Vector2 vector = npc.position + new Vector2(npc.width, npc.height) / 2f + Vector2.UnitY * npc.gfxOffY - Main.screenPosition;
                Texture2D texture2D = Main.npcTexture[npc.type];
                Rectangle rectangle = texture2D.Frame(1, Main.npcFrameCount[npc.type], 0, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]);
                Color alpha = npc.GetAlpha(color);
                Vector2 vector2 = rectangle.Size() / 2f;
                float num = (float)Math.Cos(6.28318548f * (npc.localAI[0] / 60f)) + 3f + 3f;
                for (float num2 = 0f; num2 < 4f; num2 += 1f)
                {
                    SpriteBatch spriteBatch2 = Main.spriteBatch;
                    Texture2D texture2D2 = texture2D;
                    Vector2 vector3 = vector;
                    Vector2 unitY = Vector2.UnitY;
                    double radians = num2 * 1.57079637f;
                    spriteBatch2.Draw(texture2D2, vector3 + unitY.RotatedBy(radians, default(Vector2)) * num, rectangle, alpha * 0.75f, npc.rotation, vector2, npc.scale, 0, 0f);
                }
                return false;
            }
            return base.PreDraw(spriteBatch, drawColor);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 75000;
            npc.damage = 100;
            npc.defense = 200;
            if (numPlayers > 0)
            {
                Players = numPlayers;
            }
        }
        public override void FindFrame(int frameHeight)
        {
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
                npc.position.X = npc.position.X - (npc.width / 2);
                npc.position.Y = npc.position.Y - (npc.height / 2);
                for (int j = 0; j < 20; j++)
                {
                    int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 50, default(Color), 1.2f);
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
                    int num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 0, default(Color), 1.0f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 5f;
                    Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                    num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 245, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num2].velocity *= 2f;
                    Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                }
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (item.type == mod.ProjectileType("ElementalBlade"))
            {
                npc.life = 1;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if ((projectile.penetrate == -1 || projectile.penetrate > 10) && !projectile.minion && !projectile.sentry)
            {
                projectile.penetrate = 8;
            }
            if (projectile.type == mod.ProjectileType("ElementalBlade") || projectile.type == mod.ProjectileType("ElementalBeam") || projectile.type == mod.ProjectileType("ElementalBeamSmall") || projectile.type == mod.ProjectileType("ElementalShower"))
            {
                npc.life = 1;
            }
        }
        public override bool PreAI()
        {
            Player player = Main.player[npc.target];
            npc.spriteDirection = 0;
            player.releaseMount = false;
            if (player.mount.Active)
            {
                player.mount.Dismount(player);
            }
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
                if (npc.ai[0] == 32767)
                {
                    npc.alpha += 15;
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                    }
                    return false;
                }
                npc.velocity.Y--;
                EscTimer++;
                if (EscTimer > 120)
                {
                    npc.active = false;
                }
                return false;
            }
            if (npc.ai[0] == 32767)
            {
                npc.damage = 0;
                npc.life = npc.lifeMax;
                npc.velocity = Vector2.Zero;
                npc.dontTakeDamage = true;
                npc.localAI[0]++;
                if (npc.localAI[0] == 550)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Madness_rebirth").WithVolume(1.2f), npc.Center);
                }
                if (npc.localAI[0] == 675)
                {
                    npc.hide = true;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("MadnessBossEffect"), 0, 0, Main.myPlayer);
                }
                if (npc.localAI[0] > 695)
                {
                    npc.active = false;
                    for (int i = 0; i < 20; i++)
                    {
                        int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 50, default(Color), 1.2f);
                        Main.dust[num].velocity *= 3f;
                        Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[num].scale = 0.5f;
                            Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                        }
                    }
                    for (int j = 0; j < 40; j++)
                    {
                        int num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 0, default(Color), 1.0f);
                        Main.dust[num2].noGravity = true;
                        Main.dust[num2].velocity *= 5f;
                        Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                        num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 245, 0f, 0f, 100, default(Color), 1.2f);
                        Main.dust[num2].velocity *= 2f;
                        Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                    }
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 28, mod.NPCType("BlackCrystalCore"), npc.whoAmI);
                }
                npc.localAI[1]++;
                int delay = 50;
                if (npc.localAI[0] > 650)
                {
                    delay = 3;
                }
                else if (npc.localAI[0] > 590)
                {
                    delay = 9;
                }
                else if (npc.localAI[0] > 530)
                {
                    delay = 15;
                }
                else if (npc.localAI[0] > 440)
                {
                    delay = 30;
                }
                if (npc.localAI[0] > 380)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        int dust = Dust.NewDust(new Vector2(npc.Center.X - 60, npc.Center.Y - 60), 120, 120, 21, 0f, 0f, 150, default(Color), 1.3f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].fadeIn = 1.5f;
                        Main.dust[dust].scale = 1.4f;
                        Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= Main.rand.Next(50, 100) * 0.04f;
                        vector.Normalize();
                        vector *= 160f;
                        Main.dust[dust].position = npc.Center - vector;
                        Vector2 newVelocity = npc.Center - Main.dust[dust].position;
                        Main.dust[dust].velocity = newVelocity * 0.1f;
                    }
                }
                if (npc.localAI[1] >= delay && npc.localAI[0] > 440)
                {
                    float num9 = 50f;
                    int num10 = 0;
                    while (num10 < num9)
                    {
                        Vector2 vector3 = Vector2.UnitX * 0f;
                        vector3 += -Utils.RotatedBy(Vector2.UnitY, num10 * (6.28318548f / num9), default(Vector2)) * new Vector2(240f, 240f);
                        vector3 = Utils.RotatedBy(vector3, npc.velocity.ToRotation(), default(Vector2));
                        int num11 = Dust.NewDust(npc.Center, 0, 0, 62, 0f, 0f, 150, default(Color), 1f);
                        Main.dust[num11].scale = 1.75f;
                        Main.dust[num11].noGravity = true;
                        Main.dust[num11].position = npc.Center + vector3;
                        Main.dust[num11].velocity = -1f * (npc.velocity * 0f + Utils.SafeNormalize(vector3, Vector2.UnitY) * 14f);
                        num10++;
                    }
                    npc.localAI[1] = 0;
                }
                return false;
            }
            if (Main.rand.Next(2) == 0)
            {
                int d = Dust.NewDust(npc.position, npc.width, npc.height, 186, 0, 0, 0, default(Color), 1.0f);
                Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
            if (npc.Hitbox.Intersects(player.Hitbox))
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " infected by madness"), 1000.0, 0, false);
            }
            float speed = Main.expertMode ? (SinsWorld.LimitCut ? 40 : 36) : 32;
            npc.localAI[0] += 1f;
            float dist = Vector2.Distance(npc.Center, player.Center);
            if (npc.localAI[0] > 0f && dist > 60f)
            {
                npc.localAI[0] = 0f;
                Vector2 Velocity = npc.DirectionTo(player.Center) * speed;
                npc.velocity = Vector2.Lerp(npc.velocity, Velocity, 0.0333333351f);
            }
            npc.localAI[1]++;
            if (npc.localAI[1] >= 90)
            {
                if (Main.rand.Next(60) == 0)
                {
                    int num = Main.rand.Next(1, 4);
                    npc.velocity *= npc.DirectionTo(Main.player[npc.target].Center) * (Main.rand.Next(2) == 0 ? num : -num);
                    npc.localAI[1] = 0;
                }
            }
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (npc.ai[0] == 32767)
            {
                return;
            }
            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " infected by madness"), 1000.0, 0, false);
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return npc.ai[0] != 32767;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("LifeElixir");
            if (SinsWorld.Hopeless && Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Nothingness"), Players);
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlackCrystalTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EssenceOfMadness"), Main.rand.Next(4, 9));
                switch (Main.rand.Next(3))
                {
                    case 0:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlackCrystalStaff"));
                        break;
                    case 1:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlackCoreStaff"));
                        break;
                    case 2:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WhiteCoreStaff"));
                        break;
                }
            }
            SinsWorld.downedMadness = true;
        }
    }
}