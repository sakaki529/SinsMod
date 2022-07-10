using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.MultiType
{
    public class BrutalDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Right click to throw dagger");
        }
		public override void SetDefaults()
		{
			item.damage = 11;
            item.crit = 20;
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
			item.width = 24;
			item.height = 26;
            item.useTime = 4;
			item.useAnimation = 4;
			item.useStyle = 3;
			item.knockBack = 6;
            item.shootSpeed = 15;
            item.rare = 7;
			item.value = Item.sellPrice(0, 6, 0, 0);
            item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
            item.GetGlobalItem<SinsItem>().isMultiType = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.damage = player.altFunctionUse != 2 ? 11 : 44;
            item.crit = player.altFunctionUse != 2 ? 20 : 4;
            item.melee = player.altFunctionUse != 2 ? true : false;
            item.thrown = player.altFunctionUse != 2 ? false : true;
            item.noMelee = player.altFunctionUse != 2 ? false : true;
            item.noUseGraphic = player.altFunctionUse != 2 ? false : true;
            item.useTurn = player.altFunctionUse != 2 ? true : false;
            item.useStyle = player.altFunctionUse != 2 ? 3 : 1;
            item.useTime = player.altFunctionUse != 2 ? 4 : 14;
            item.useAnimation = player.altFunctionUse != 2 ? 4 : 14;
            item.shoot = player.altFunctionUse != 2 ? 0 : mod.ProjectileType("BrutalDagger");
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return player.altFunctionUse == 2;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(BuffID.Bleeding, 300);
            if (player.altFunctionUse != 2)
            {
                if (!Main.player[item.owner].moonLeech)
                {
                    if (target.type != 488)
                    {
                        float num = damage / 2;
                        if ((int)num <= 0)
                        {
                            return;
                        }
                        Main.player[Main.myPlayer].statLife += (int)num;
                        Main.player[Main.myPlayer].HealEffect((int)num);
                    }
                }
            }
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 300);
            if (player.altFunctionUse != 2)
            {
                if (!Main.player[item.owner].moonLeech)
                {
                    float num = damage / 2;
                    if ((int)num <= 0)
                    {
                        return;
                    }
                    Main.player[Main.myPlayer].statLife += (int)num;
                    Main.player[Main.myPlayer].HealEffect((int)num);
                }
            }
        }
    }
}