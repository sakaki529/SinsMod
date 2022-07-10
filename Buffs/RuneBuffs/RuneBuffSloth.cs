using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.RuneBuffs
{
    public class RuneBuffSloth : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Protego");
            Description.SetDefault("");
            canBeCleared = false;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().RuneBuffSloth = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().RuneBuffSloth = true;
        }
    }
}