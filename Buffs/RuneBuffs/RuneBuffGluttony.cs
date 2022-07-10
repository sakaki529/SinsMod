using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.RuneBuffs
{
    public class RuneBuffGluttony : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Hunger");
            Description.SetDefault("");
            canBeCleared = false;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().RuneBuffGluttony = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().RuneBuffGluttony = true;
        }
    }
}