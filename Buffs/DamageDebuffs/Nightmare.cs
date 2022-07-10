using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.DamageDebuffs
{
    public class Nightmare : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Nightmare");
            Description.SetDefault("");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().Nightmare = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().Nightmare = true;
        }
    }
}