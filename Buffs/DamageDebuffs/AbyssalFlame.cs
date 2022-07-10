using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.DamageDebuffs
{
    public class AbyssalFlame : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Abyssal Flame");
            Description.SetDefault("Your body is blazing");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().AbyssalFlame = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().AbyssalFlame = true;
        }
    }
}