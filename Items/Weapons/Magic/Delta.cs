using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
	public class Delta : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Delta");
            Tooltip.SetDefault("Right click to shoot small laser");
            customGlowMask = SinsGlow.SetStaticDefaultsGlowMask(this);
        }
		public override void SetDefaults()
		{
            item.width = 40;
			item.height = 30;
			item.damage = 61;
            item.mana = 15;
			item.magic = true;
			item.noMelee = true;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 5;
			item.useTime = 7;
			item.useAnimation = 7;
            item.shoot = mod.ProjectileType("DeltaRay");
            item.shootSpeed = 10f;
			item.value = Item.sellPrice(0, 22, 0, 0);
			item.rare = 7;
			item.UseSound = SoundID.Item13;
            item.glowMask = customGlowMask;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 0);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.damage = player.altFunctionUse != 2 ? 61 : 79;
            item.mana = player.altFunctionUse != 2 ? 9 : 3;
            item.channel = player.altFunctionUse != 2 ? true : false;
            item.useTime = player.altFunctionUse != 2 ? 7 : 4;
            item.useAnimation = player.altFunctionUse != 2 ? 7 : 4;
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("DeltaRay") : mod.ProjectileType("DeltaLaser");
            item.UseSound = player.altFunctionUse != 2 ? SoundID.Item13 : SoundID.Item91;
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LaserMachinegun);
            recipe.AddIngredient(ItemID.ChargedBlasterCannon);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}