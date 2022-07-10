using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Ranged.Ammo
{
    public class PhantasmalAmalgam : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantasmal Amalgam");
			Tooltip.SetDefault("Not consumable"
                + "\nAmalgam of phantoms");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
		public override void SetDefaults()
		{
            item.ranged = true;
			item.width = 22;
			item.height = 22;
			item.maxStack = 1;
			item.rare = 9;
			item.value = Item.sellPrice(0, 0, 0, 0);
            item.consumable = false;
            item.ammo = mod.ItemType("PhantasmalAmalgam");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm, 50);
            recipe.AddIngredient(mod.ItemType("MysticBar"), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}