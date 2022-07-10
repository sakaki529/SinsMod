using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.StatDebuffs
{
    public class GreedDebuffEffect : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Greed");
            Description.SetDefault("");
            //Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
        }
    }
}