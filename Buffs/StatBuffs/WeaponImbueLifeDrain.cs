using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.StatBuffs
{
    public class WeaponImbueLifeDrain : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Weapon Imbue: Life Drain");
            Description.SetDefault("Melee attacks restores your health");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.meleeBuff[Type] = true;
            longerExpertDebuff = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinsPlayer>().WeaponImbueLifeDrain = true;
        }
    }
}