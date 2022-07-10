using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.StatDebuffs
{
    public class SuperSlow : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Super Slow");
            Description.SetDefault("Decreases your movement speed");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().SuperSlow = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SinsNPC>().SuperSlow = true;
        }
    }
}