using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class XBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("X Blade");
            Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.damage = 100;
            item.crit += 100;
            item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 10;
			item.value = Item.sellPrice(10, 0, 0, 0);
            item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.useTurn = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (target.boss == false)
            {
                damage = target.lifeMax + target.defense / 2;
            }
            if (target.type == 325 || target.type == 327 || target.type == 344 || target.type == 345 || target.type == 346)
            {
                damage = 1000;
            }
            if (target.type == 290 || target.type == 87 || target.type == 88 || target.type == 89 || target.type == 90 || target.type == 91 || target.type == 92)
            {
                damage = target.lifeMax + target.defense / 2;
            }
        }
	}
}