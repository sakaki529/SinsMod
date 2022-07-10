using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Magic
{
    public class AnimaOrbCleyera : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Anima Orb");
            Tooltip.SetDefault("Unleash anima of Cleyera" + 
                "\nRight click to shoot souls all direction");
        }
		public override void SetDefaults()
		{
            item.width = 32;
            item.height = 32;
            item.damage = 529;
            item.crit += 16;
            item.mana = 15;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useTime = 3;
            item.useAnimation = 22;
            item.knockBack = 0.5f;
            item.shoot = mod.ProjectileType("FlamingPumpkin");
            item.shootSpeed = 20;
            item.UseSound = SoundID.Item62;
            item.rare = 11;
            item.value = Item.sellPrice(0, 52, 9, 0);
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            mult = modPlayer.Cleyera ? 0 : 1;
        }
        public override bool CanUseItem(Player player)
        {
            item.useStyle = player.altFunctionUse != 2 ? 5 : 4;
            item.useTime = player.altFunctionUse != 2 ? 3 : 40;
            item.useAnimation = player.altFunctionUse != 2 ? 22 : 40;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                float spread = 6f;
                float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
                double deltaAngle = spread / 2f;
                double offsetAngle;
                for (int i = 0; i < 50; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage / 4, knockBack, item.owner);
                }
                return false;
            }
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OrbStand");
            recipe.AddIngredient(ItemID.Pumpkin, 529);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}