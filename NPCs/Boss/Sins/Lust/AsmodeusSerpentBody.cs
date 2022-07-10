using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Lust
{
    public class AsmodeusSerpentBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Servant of Asmodeus");
        }
        public override void SetDefaults()
        {
            npc.width = 20;
            npc.height = 20;
            npc.aiStyle = 6;
            aiType = -1;
            npc.lifeMax = 600000;
            npc.damage = 60;
            npc.defense = 40;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.netAlways = true;
            npc.chaseable = false;
            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath18;
            npc.npcSlots = 1f;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[31] = false;
                npc.buffImmune[mod.BuffType("Chroma")] = false;
            }
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
            npc.GetGlobalNPC<SinsNPC>().damageMult = 0.1f;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 900000;
            npc.damage = 100;
        }
        public override void AI()
        {
            npc.boss = true;
            Player player = Main.player[npc.target];
            if (!Main.npc[(int)npc.ai[1]].active)
            {
                npc.life = 0;
                npc.HitEffect(0, 0);
                npc.active = false;
            }
            npc.alpha = Main.npc[(int)npc.ai[1]].alpha;
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