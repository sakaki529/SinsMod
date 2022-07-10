using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    public class AbyssalFlameRelic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Flame Relic");
            Tooltip.SetDefault("Melee attacks and projectiles inflict abyssal flame debuff" +
                "\nReduces damage taken by 20%" +
                "\n15% chance to reduce damage to 1 when you take damage" +
                "\nGrants immunity to abyssal flame debuff and darkness of the tartarus");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 16;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.AbyssalFlameRelic = true;
            modPlayer.TartarusDarknessImmune = true;
            player.buffImmune[mod.BuffType("AbyssalFlame")] = true;
            player.endurance += 0.2f;
        }
    }
}