using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.DamageDebuffs
{
    public class Sin : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Sin");
            Description.SetDefault("You fell your sins...");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().Sin = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().Sin = true;
        }
    }
}