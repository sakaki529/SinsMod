using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.NormalNPCs
{
    public class BigMetalSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal Slime");
            Main.npcFrameCount[npc.type] = 4;
            animationType = 244;
        }
        public override void SetDefaults()
        {
            npc.width = 70;
            npc.height = 46;
            npc.lifeMax = 50;
            npc.damage = 40;
            npc.knockBackResist = 0.1f;
            npc.aiStyle = 1;
            npc.npcSlots = 0.5f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.sellPrice(0, 0, 20, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            banner = mod.NPCType("MetalSlime");
            bannerItem = mod.ItemType("MetalSlimeBanner");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 75;
            npc.damage = 60;
            npc.value = Item.buyPrice(0, 0, 80, 30);
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
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, -2f * hitDirection, -1.0f, 0, default(Color), 1f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, 2f * hitDirection, -1.0f, 0, default(Color), 1f);
                }
                Dust.NewDust(npc.position, npc.width, npc.height, 11, -2f * hitDirection, -1.0f, 0, default(Color), 1f);
                Dust.NewDust(npc.position, npc.width, npc.height, 11, -2f * hitDirection, -1.0f, 0, default(Color), 1f);
                Dust.NewDust(npc.position, npc.width, npc.height, 11, 2f * hitDirection, -1.0f, 0, default(Color), 1f);
                Dust.NewDust(npc.position, npc.width, npc.height, 1, 2f * hitDirection, -1.0f, 0, default(Color), 1f);
            }
        }
        public override void AI()
        {
            if (npc.wet == true)
            {
                npc.velocity.Y = +60;
            }
        }
        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(1, 3));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MetalNugget"), Main.rand.Next(4, 5));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.IronBar, Main.rand.Next(5, 7));
                if (Main.rand.Next(20) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MetalDetector);
                }
             }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(1, 2));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MetalNugget"), Main.rand.Next(3, 5));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.IronBar, Main.rand.Next(4, 6));
                if (Main.rand.Next(40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MetalDetector);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.playerSafe || !Main.hardMode || !Main.dayTime || Main.eclipse)
            {
                return 0f;
            }
            return SpawnCondition.OverworldDaySlime.Chance * 0.01f;
        }
    }
}