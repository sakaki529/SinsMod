using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Tools
{
    public class NightmareTool : ModItem
    {
        public override void SetStaticDefaults()
	    {
            DisplayName.SetDefault("Nightmare Tool");
            Tooltip.SetDefault("Right click to change tool type" +
                "\nPress the Nightmare Tool key to enable/disable destruct mode" +
                "\nThis mode work when enable pickaxe mode or hammer mode");
        }
		public override void SetDefaults()
		{
            item.width = 48;
            item.height = 48;
            item.damage = 666;
			item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.useTime = 11;
            item.useAnimation = 11;
            item.useStyle = 1;
			item.knockBack = 7.5f;
            item.tileBoost += 50;
            item.pick = 50000;
            item.axe = 0;
            item.hammer = 0;
            item.value = Item.sellPrice(0, 80, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item66;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.altFunctionUse == 2)
            {
                item.useTime = 20;
                item.useAnimation = 20;
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                item.autoReuse = false;
                if (modPlayer.NightmareToolType == 0 && modPlayer.useDelay == 0)
                {
                    modPlayer.NightmareToolType = 1;
                    modPlayer.useDelay += 10;
                    item.UseSound = SoundID.Item71;
                }
                if (modPlayer.NightmareToolType == 1 && modPlayer.useDelay == 0)
                {
                    modPlayer.NightmareToolType = 2;
                    modPlayer.useDelay += 10;
                    item.UseSound = SoundID.Item88;
                }
                if (modPlayer.NightmareToolType == 2 && modPlayer.useDelay == 0)
                {
                    modPlayer.NightmareToolType = 0;
                    modPlayer.useDelay += 10;
                    item.UseSound = SoundID.Item66;
                }
            }
            else
            {
                item.autoReuse = true;
                if (modPlayer.NightmareToolType == 0)
                {
                    item.pick = 50000;
                    item.axe = 0;
                    item.hammer = 0;
                    item.useTime = 1;
                    item.useAnimation = 11;
                    item.UseSound = SoundID.Item66;
                }
                if (modPlayer.NightmareToolType == 1)
                {
                    item.pick = 0;
                    item.axe = 50000 / 5;
                    item.hammer = 0;
                    item.useTime = 1;
                    item.useAnimation = 11;
                    item.UseSound = SoundID.Item71;
                }
                if (modPlayer.NightmareToolType == 2)
                {
                    item.pick = 0;
                    item.axe = 0;
                    item.hammer = 200;
                    item.useTime = 3;
                    item.useAnimation = 11;
                    item.UseSound = SoundID.Item88;
                }
            }
            return base.CanUseItem(player);
        }
        public override bool UseItem(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.altFunctionUse != 2)
            {
                if (modPlayer.NightmareToolDestructMode)
                {
                    bool flag = false;
                    for (int i = Player.tileTargetX - 2; i <= Player.tileTargetX + 2; i++)
                    {
                        for (int j = Player.tileTargetY - 2; j <= Player.tileTargetY + 2; j++)
                        {
                            if (Main.tile[i, j].active() && modPlayer.NightmareToolType == 0)
                            {
                                Vector2 position = new Vector2(Player.tileTargetX * 16, Player.tileTargetY * 16);
                                Vector2 center = Main.screenPosition + new Vector2((float)Main.mouseX - 50, (float)Main.mouseY - 50);
                                if (modPlayer.NightmareToolType == 0)
                                {
                                    WorldGen.KillTile(i, j, false, false, false);
                                }
                                int num = Dust.NewDust(center, 100, 100, 182);
                                Main.dust[num].scale = 2.1f;
                                Main.dust[num].noGravity = true;
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(17, -1, -1, null, 0, i, j, 0f, 0, 0, 0);
                                }
                                flag = true;
                            }
                            if (Main.tile[i, j].wall != 0 && modPlayer.NightmareToolType == 2)
                            {
                                Vector2 position = new Vector2(Player.tileTargetX * 16, Player.tileTargetY * 16);
                                Vector2 center = Main.screenPosition + new Vector2((float)Main.mouseX - 50, (float)Main.mouseY - 50);
                                WorldGen.KillWall(i, j, false);
                                int num = Dust.NewDust(center, 100, 100, 182);
                                Main.dust[num].scale = 2.1f;
                                Main.dust[num].noGravity = true;
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(17, -1, -1, null, 0, i, j, 0f, 0, 0, 0);
                                }
                                flag = true;
                            }
                        }
                    }
                    if (flag)
                    {
                        Main.PlaySound(SoundID.NPCKilled, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 56, 1);
                    }
                }
            }
            return true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 300);
            target.AddBuff(BuffID.Bleeding, 300);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 300);
            target.AddBuff(BuffID.Bleeding, 300);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(2) == 0)
            {
                int num = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 172);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.05f;
                Main.dust[num].scale = 1.0f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                Main.dust[num].color = new Color(255, 255, 255);
                Main.dust[num].alpha = 100;
            }
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AxionTool");
            recipe.AddIngredient(null, "EssenceOfMadness", 8);
            recipe.AddIngredient(null, "NightmareBar", 8);
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}