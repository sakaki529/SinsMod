using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class Wrath : ModItem
	 {
	    public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("(Nameless)");
        }
		public override void SetDefaults()
		{
            item.width = 40;
			item.height = 40;
			item.damage = 500;
			item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
			item.useStyle = 1;
			item.useTime = 15;
			item.useAnimation = 15;
			item.knockBack = 15;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 13;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(10) == 0)
            {
                target.AddBuff(mod.BuffType("Nightmare"), 60);
            }
            //Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ApostlesScythe"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 0);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            if (Main.rand.Next(10) == 0)
            {
                target.AddBuff(mod.BuffType("Nightmare"), 60);
            }
            //Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ApostlesScythe"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI, target.whoAmI, 1);
        }
    }
}