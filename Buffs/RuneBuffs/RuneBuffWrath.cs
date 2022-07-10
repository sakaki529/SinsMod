using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.RuneBuffs
{
    public class RuneBuffWrath : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Berserk");
            Description.SetDefault("");
            canBeCleared = false;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().RuneBuffWrath = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().RuneBuffWrath = true;
        }
    }
}