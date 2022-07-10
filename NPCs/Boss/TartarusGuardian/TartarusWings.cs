using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.TartarusGuardian
{
    public class TartarusWings : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian of Tartarus");
            NPCID.Sets.TrailingMode[npc.type] = 1;
            NPCID.Sets.TrailCacheLength[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 30;
            npc.lifeMax = 7500000;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.netAlways = true;
            npc.HitSound = null;
            npc.DeathSound = null;
            npc.value = Item.sellPrice(0, 0, 0, 0);
            npc.alpha = 255;
            npc.npcSlots = 10f;
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
            npc.dontTakeDamage = true;
            npc.dontCountMe = true;
            npc.GetGlobalNPC<SinsNPC>().trail = true;
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 10000000 + 500000 * numPlayers;
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return false;
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                float num = Main.rand.Next(-100, 100) / 100;
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusWings1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusWings2"), npc.scale);
            }
        }
        public override bool PreAI()
        {
            npc.boss = true;
            if (Main.npc[(int)npc.ai[0]].type != mod.NPCType("TartarusBody2"))
            {
                npc.active = false;
            }
            Player player = Main.player[npc.target];
            npc.alpha = Main.npc[(int)npc.ai[0]].alpha;
            npc.Center = Main.npc[(int)npc.ai[0]].Center;
            npc.rotation = Main.npc[(int)npc.ai[0]].rotation;
            if (!Main.npc[(int)npc.ai[0]].active)
            {
                npc.life = 0;
                if (npc.alpha <= 254)
                {
                    npc.HitEffect(0, 1);
                }
                npc.active = false;
            }
            return base.PreAI();
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