using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.RuneBuffs
{
    public class RuneBuffGreed : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Greedy");
            Description.SetDefault("");
            canBeCleared = false;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().RuneBuffGreed = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().RuneBuffGreed = true;
        }
    }
}