using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.NormalNPCs
{
    public class MetalSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal Slime");
            Main.npcFrameCount[npc.type] = 2;
            animationType = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 24;
            npc.lifeMax = 20;
            npc.damage = 15;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = 1;
            npc.npcSlots = 0.5f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.sellPrice(0, 0, 10, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            banner = npc.type;
            bannerItem = mod.ItemType("MetalSlimeBanner");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 30;
            npc.damage = 23;
            npc.value = Item.buyPrice(0, 0, 30, 0);
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage = 1;
            if(crit)
            {
                damage = 2;
            }
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, 1.5f * hitDirection, 1f, 0, default(Color), 0.7f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, -1.5f * hitDirection, -1f, 0, default(Color), 0.7f);
                }
            }
        }
        public override void AI()
        {
            if(npc.wet == true)
            {
                npc.velocity.Y = +50;
            }
        }
        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(1, 3));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MetalNugget"), Main.rand.Next(2, 3));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.IronBar, Main.rand.Next(2, 3));
                if (Main.rand.Next(7143) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SlimeStaff);
                }
             }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(1, 2));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MetalNugget"), Main.rand.Next(1, 2));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.IronBar, Main.rand.Next(1, 2));
                if (Main.rand.Next(10000) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SlimeStaff);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.playerSafe || !Main.dayTime || Main.eclipse)
            {
                return 0f;
            }
            return SpawnCondition.OverworldDaySlime.Chance * (Main.hardMode && !NPC.downedMoonlord ? 0.08f : 0.01f);
        }
    }
}