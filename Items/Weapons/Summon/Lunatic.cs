using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Summon
{
    public class Lunatic : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Lunatic");
            Tooltip.SetDefault("Increases damage at night" +
                "\n'Do you feel it, the moon's power?'");
		}
		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.damage = 96;
            item.mana = 10;
            item.summon = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useTime = 20;
			item.useAnimation = 20;
			item.knockBack = 9.5f;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("Lunatic");
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;
			item.UseSound = SoundID.Item45;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
                return false;
            }
            float num = 1f;
            float num2 = 0f;
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.minion && projectile.owner == player.whoAmI)
                {
                    num2 += projectile.minionSlots;
                    if (num2 + num > player.maxMinions)
                    {
                        return false;
                    }
                }
            }
            return player.altFunctionUse != 2;
        }
    }
}