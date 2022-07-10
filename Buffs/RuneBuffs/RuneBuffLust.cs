using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.RuneBuffs
{
    public class RuneBuffLust : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mystica");
            Description.SetDefault("");
            canBeCleared = false;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().RuneBuffLust = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().RuneBuffLust = true;
        }
    }
}