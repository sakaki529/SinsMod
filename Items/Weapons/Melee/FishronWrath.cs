using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class FishronWrath : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishron Wrath");
			Tooltip.SetDefault("Right click to shoot dukenado");
        }
        public override void SetDefaults()
        { 
            item.width = 40;
            item.height = 40;
            item.damage = 88;
            item.melee = true;
			item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;             
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 10.0f;
            item.shootSpeed = 20;
            item.value = Item.buyPrice(0, 26, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item45;
            item.scale = 1.2f;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
            item.useTime = player.altFunctionUse != 2 ? 15 : 120;
            item.useAnimation = player.altFunctionUse != 2 ? 15 : 120;
            item.shoot = player.altFunctionUse != 2 ? mod.ProjectileType("TyphoonMelee") : mod.ProjectileType("DukenadoBolt");
            item.shootSpeed = player.altFunctionUse != 2 ? 20 : 40;
            return base.CanUseItem(player);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			for (int i = 0; i < 15; i++)
			{
				position = player.Center + new Vector2(-(float)Main.rand.Next(0, 901) * player.direction, -600f);
				position.Y -= 100 * i;
				Vector2 heading = target - position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}
				heading.Normalize();
				heading *= new Vector2(speedX, speedY).Length();
				speedX = heading.X;
				speedY = heading.Y + Main.rand.Next(-800, 800) * 0.02f;
                Projectile.NewProjectile(position.X, position.Y, speedX / 2, speedY / 2, type, damage, knockBack, player.whoAmI, 0f, ceilingLimit);
            }
			return false;
		}
    }
}