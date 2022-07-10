using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.StatDebuffs
{
    public class Agony : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Agony");
            Description.SetDefault("");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().Agony = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().Agony = true;
        }
    }
}