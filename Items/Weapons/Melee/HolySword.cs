using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class HolySword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("'Holy Sword' Excalibur");
            Tooltip.SetDefault("Right click to teleport the position of the mouse" +
                "\n'My legend dates back to the 12th Century you see...'");
		}
		public override void SetDefaults()
		{
            item.width = 40; 
			item.height = 40;
			item.damage = 1000;
            item.melee = true;
            item.autoReuse = true;
			item.useTurn = true;
            item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = 1;
			item.knockBack = 20;
			item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 11;
			item.UseSound = SoundID.Item1;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.noMelee = (player.altFunctionUse != 2) ? false : true;
            return base.CanUseItem(player);
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<SinsPlayer>().teleportDelay <= 0)
            {
                Vector2 vector;
                vector.X = Main.mouseX + Main.screenPosition.X;
                if (player.gravDir == 1f)
                {
                    vector.Y = Main.mouseY + Main.screenPosition.Y - player.height;
                }
                else
                {
                    vector.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
                }
                vector.X -= player.width / 2;
                player.GetModPlayer<SinsPlayer>().Teleport(vector, 0);
                player.GetModPlayer<SinsPlayer>().teleportDelay = 20;
            }
            else if (player.altFunctionUse != 2)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].friendly && Main.npc[i].type != NPCID.TargetDummy && !Main.npc[i].dontTakeDamage)
                    {
                        Main.npc[i].StrikeNPCNoInteraction((int)((double)2000 * player.meleeDamage), 0f, -Main.npc[i].direction, true, false, false);
                    }
                }
            }
            return true;
        }
        public override void HoldItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.name == "Hero the Brave" || player.name == "Cleyera")
            {
                //return;
            }
            modPlayer.FOOLS = true;
            //player.AddBuff(mod.BuffType("FOOLS"), 2);
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            damage = (int)((double)2000 * player.meleeDamage);
            crit = true;
        }
        public override void ModifyHitPvp(Player player, Player target, ref int damage, ref bool crit)
        {
            damage = (int)((double)200 * player.meleeDamage);
            crit = true;
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.X += hitbox.Width / 2;
            hitbox.Y += hitbox.Height / 2;
            hitbox.Width = 3000;
            hitbox.Height = 2000;
            hitbox.X -= hitbox.Width / 2;
            hitbox.Y -= hitbox.Height / 2;
        }
    }
}