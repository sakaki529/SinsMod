using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.MultiType
{
    public class Mjolnir : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Mjolnir");
            Tooltip.SetDefault("When you held this, gives Izuna Buff." +
                "\nYou can check buff effect from buff icon" +
                "\nRight click to throw mjolnir");
		}
		public override void SetDefaults()
		{
            item.width = 24;
			item.height = 24;
			item.damage = 440;
            item.melee = true;
            item.autoReuse = true;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 1;
			item.knockBack = 6;
            item.shootSpeed = 28f;
			item.value = Item.sellPrice(0, 65, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().isMultiType = true;
        }
        public override void HoldItem(Player player)
        {
            player.AddBuff(mod.BuffType("Izuna"), 2);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.melee = player.altFunctionUse != 2 ? true : false;
            item.thrown = player.altFunctionUse != 2 ? false : true;
            item.noMelee = player.altFunctionUse != 2 ? false : true;
            item.noUseGraphic = player.altFunctionUse != 2 ? false : true;
            item.useTurn = player.altFunctionUse != 2 ? true : false;
            item.useTime = player.altFunctionUse != 2 ? 6 : 12;
            item.useAnimation = player.altFunctionUse != 2 ? 6 : 12;
            item.shoot = player.altFunctionUse != 2 ? 0 : mod.ProjectileType("Mjolnir");
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return player.altFunctionUse == 2;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            int num = Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("Electricity"), damage, 0f, player.whoAmI, target.whoAmI, 1);
            Main.projectile[num].melee = true;
            Main.projectile[num].thrown = false;
        }
        public override void ModifyHitPvp(Player player, Player target, ref int damage, ref bool crit)
        {
            int num = Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("Electricity"), damage, 0f, player.whoAmI, target.whoAmI, 1);
            Main.projectile[num].melee = true;
            Main.projectile[num].thrown = false;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (player.altFunctionUse != 2)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 57);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].velocity *= 0.05f;
                    Main.dust[num].scale = 0.75f;
                    Main.dust[num].alpha = 100;
                }
            }
        }
        public override void AddRecipes()
		{
			/*ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(null, "HephaestusForge");
            recipe.SetResult(this);
			recipe.AddRecipe();*/
		}
    }
}