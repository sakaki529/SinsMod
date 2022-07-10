using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Buffs.StatBuffs
{
    public class Izuna : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Izuna");
            DisplayName.AddTranslation(GameCulture.Chinese, "óãçj");
            Description.SetDefault("Increases damage 15%" +
                "\nIncreases critical 10%" +
                "\nIncreases move speed" +
                "\nGives a chance to dodge attacks");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.electrified = true;
            Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 1.1f, 1.1f, 0.3f);
            player.moveSpeed += 8.0f;
            player.accRunSpeed = 15f;
            player.jumpBoost = true;
            player.meleeDamage += 0.15f;
            player.thrownDamage += 0.15f;
            player.meleeCrit += 10;
            player.thrownCrit += 10;
            player.dash = 1;
            player.blackBelt = true;
        }
    }
}