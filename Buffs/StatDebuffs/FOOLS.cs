using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.StatDebuffs
{
    public class FOOLS : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("FOOLS!");
            Description.SetDefault("");
            //Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 10f, 10f, 10f);
            player.moveSpeed += 50f;
            player.accRunSpeed = 100f;
            player.jumpBoost = true;
            player.meleeCrit = 100;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
        }
    }
}