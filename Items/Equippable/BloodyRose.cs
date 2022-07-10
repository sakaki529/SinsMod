using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
    [AutoloadEquip(EquipType.Waist)]
    public class BloodyRose : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloody Rose");
            string tooltip = "Unlimited mana";
            if (ModLoader.GetMod("ThoriumMod") != null)
            {
                //tooltip += "\nUnlimited inspiration";
            }
            if (ModLoader.GetMod("Laugicality") != null)
            {
                tooltip += "\nUnlimited Lux,Vis and Mundas";
            }
            //"\nGreatly increased mana regen"+ 
            //"\nGrants immunity to potion sickness debuff"+ 
            tooltip += "\nProvides life regeneration and reduces the cooldown of healing potions" +
                "\nIncreases pickup range for hearts and stars";
            Tooltip.SetDefault(tooltip);
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.value = Item.sellPrice(1, 20, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.UnlimitedMana = true;
            //player.buffImmune[21] = true;//PotionSickness
            player.buffImmune[BuffID.ManaSickness] = true;//ManaSickness
            player.pStone = true;
            player.manaFlower = true;
            player.lifeMagnet = true;
            player.manaMagnet = true;
            player.manaCost *= 0;
            player.lifeRegen += 10;
            //player.potionDelayTime *= 0;
            //player.potionDelay *= 0;
            //player.manaRegen = 9999;
            //player.manaRegenBonus = 9999;
            //player.manaRegenDelay *= 0;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfMadness", 12);
            recipe.AddIngredient(null, "NightmareBar", 12);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}