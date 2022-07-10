using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Boss
{
    public class TartarusMeat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tartarus Meat");
            Tooltip.SetDefault("No one knows what kind of meat it is." +
                "\nThe Guardian do not allow you to enter the abyss" +
                "\nCan only be used in the Tartarus");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 20;
            item.value = 0;
            item.rare = 11;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 15;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<SinsPlayer>().ZoneTartarus && !NPC.AnyNPCs(mod.NPCType("TartarusHead")) && !NPC.AnyNPCs(mod.NPCType("TartarusBone")) && !NPC.AnyNPCs(mod.NPCType("TartarusBody")) && !NPC.AnyNPCs(mod.NPCType("TartarusTail")))
            {
                return true;
            }
            return false;
        }
        public override bool UseItem(Player player)
        {
            //Main.PlaySound(SoundID.Item2, (int)player.position.X, (int)player.position.Y);
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("TartarusHead"));
            return true;
        }
    }
}