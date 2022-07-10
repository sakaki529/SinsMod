using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class InstantDummy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Instant Dummy");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 30;
            item.damage = 0;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = 0;
            item.rare = 1;
            item.GetGlobalItem<SinsItem>().isAltFunction = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine[] lines = new TooltipLine[9];
            lines[0] = new TooltipLine(mod, "1", "Creates a instant dummy");
            lines[1] = new TooltipLine(mod, "2", "Right click to kill all instant dummies");
            lines[2] = new TooltipLine(mod, "3", "You can change the instant dummy's status by typing a command");
            lines[3] = new TooltipLine(mod, "4", "Chat with /help, You can check commands");
            lines[4] = new TooltipLine(mod, "5", "-Status-");
            lines[5] = new TooltipLine(mod, "6", " Defence: " + SinsNPC.InstantDummyDefence.ToString());
            lines[6] = new TooltipLine(mod, "7", " Damage Multiplier: " + SinsNPC.InstantDummyDamegeMultiplier.ToString() + "f(" + (SinsNPC.InstantDummyDamegeMultiplier * 100).ToString() + "%)");
            lines[7] = new TooltipLine(mod, "8", " Buff Immune: " + SinsNPC.InstantDummyBuffImmunity.ToString());
            lines[8] = new TooltipLine(mod, "9", " Scale: " + SinsNPC.InstantDummyScale.ToString() + "f(" + (SinsNPC.InstantDummyScale * 100).ToString() + "%)");
            for (int i = 0; i < lines.Length; i++)
            {
                tooltips.Add(lines[i]);
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                //player.GetModPlayer<SinsPlayer>().delInstantDummyTime = 2;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == mod.NPCType("InstantDummy"))
                    {
                        Main.npc[i].ai[0] = 255;
                        Main.npc[i].life = 0;
                        Main.npc[i].lifeRegen = 0;
                        Main.npc[i].checkDead();
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0);
                            //NetMessage.SendData(28, -1, -1, null, 1, Main.npc[i].lifeMax, 0f, -Main.npc[i].direction, 1);
                        }
                    }
                }
            }
            else if (player.whoAmI == Main.myPlayer && !SinsNPC.BossActiveCheck())
            {
                Vector2 position = Main.MouseWorld;
                Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("Where_is_my_Soul"), 0, 0, Main.myPlayer, mod.NPCType("InstantDummy"));
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TargetDummy);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}