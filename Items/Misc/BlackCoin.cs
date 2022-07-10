using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Misc
{
    public class BlackCoin : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Black Coin");
			Tooltip.SetDefault("Can be crafted to and from 100 platinum coins" +
                "\nCan be used for Coin Gun");
        }
		public override void SetDefaults()
		{
            item.width = 12;
            item.height = 14;
            item.damage = 2000;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.notAmmo = true;
            item.ammo = AmmoID.Coin;
            item.useStyle = 0;
            item.useTime = 10;
            item.useAnimation = 15;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("BlackCoin");
            item.maxStack = 999;
            item.value = Item.sellPrice(100, 0, 0, 0);
            //item.createTile = mod.TileType("BlackCoinPile");
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = SinsColor.MediumBlack;
                }
            }
            int Consumable = tooltips.FindIndex(x => x.Name == "Consumable");
            tooltips.RemoveAt(Consumable);
            if (!SinsMod.Instance.CheatSheetLoaded)
            {
                int Material = tooltips.FindIndex(x => x.Name == "Material");
                tooltips.RemoveAt(Material);
            }
            //Causes error on cheat sheet

            //Causes error because useStyle = 0
            /*Player player = Main.player[Main.myPlayer];
            int Damage = tooltips.FindIndex(x => x.Name == "Damage");
            int CritChance = tooltips.FindIndex(x => x.Name == "CritChance");
            int Speed = tooltips.FindIndex(x => x.Name == "Speed");
            int Knockback = tooltips.FindIndex(x => x.Name == "Knockback");
            for (int i = 0; i < 59; i++)                                        
            {
                if (i < player.inventory.Length)
                {
                    if (player.inventory[i].useAmmo == AmmoID.Coin)
                    {
                        tooltips.RemoveAt(Damage);
                        tooltips.Insert(Damage, new TooltipLine(mod, "Damage", (int)((double)item.damage * player.rangedDamage) + " ranged damage"));
                        tooltips.RemoveAt(CritChance);
                        tooltips.Insert(CritChance, new TooltipLine(mod, "CritChance", player.rangedCrit - player.inventory[player.selectedItem].crit + Main.HoverItem.crit + " critical strike chance"));
                        tooltips.RemoveAt(Speed);
                        tooltips.Insert(Speed, new TooltipLine(mod, "Speed", "Very fast speed"));
                        tooltips.RemoveAt(Knockback);
                        tooltips.Insert(Knockback, new TooltipLine(mod, "Knockback", "No knockback"));
                        return;
                    }
                }
            }
            tooltips.RemoveAt(Damage);
            tooltips.RemoveAt(CritChance);
            tooltips.RemoveAt(Speed);
            tooltips.RemoveAt(Knockback);*/
        }
        public override bool CanPickup(Player player)
        {
            /*for (int i = 59; i > 0; i--)
            {
                if (i < player.inventory.Length - 9 && player.inventory[i].type != 0)
                {
                    return false;
                }
            }*/
            return base.CanPickup(player);
        }
        public override bool OnPickup(Player player)
        {
            if (item.IsTheSameAs(player.inventory[player.selectedItem]))
            {
                for (int i = 59; i > 0; i--)
                {
                    if (i < player.inventory.Length - 9 && player.inventory[i].type == 0)
                    {
                        ItemText.NewText(item, item.stack, false, false);
                        Main.PlaySound(SoundID.CoinPickup, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
                        player.inventory[i].SetDefaults(item.type);
                        player.inventory[i].stack = item.stack;
                        return false;
                    }
                }
            }
            for (int i = 59; i > 0; i--)
            {
                if (i < player.inventory.Length)
                {
                    if (item.IsTheSameAs(player.inventory[i]) && player.inventory[i].stack < item.maxStack)
                    {
                        ItemText.NewText(item, item.stack, false, false);
                        Main.PlaySound(SoundID.CoinPickup, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
                        player.inventory[i].stack += item.stack;
                        return false;
                    }
                    else if (i == 1)
                    {
                        for (int j = 59; j > 0; j--)
                        {
                            if (j < player.inventory.Length - 9 && player.inventory[j].type == 0)
                            {
                                ItemText.NewText(item, item.stack, false, false);
                                Main.PlaySound(SoundID.CoinPickup, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
                                player.inventory[j].SetDefaults(item.type);
                                player.inventory[j].stack = item.stack;
                                return false;
                            }
                        }
                    }
                }
            }
            return base.OnPickup(player);//Work when exception has happened
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Extra/Item/BlackCoin_Extra");
            Color color = Lighting.GetColor((int)(item.position.X + item.width * 0.5) / 16, (int)(item.position.Y + item.height * 0.5) / 16);
            Color alpha = item.GetAlpha(color);
            if (!Main.gamePaused && (Math.Abs(item.velocity.X) + Math.Abs(item.velocity.Y)) > 0.2)
            {
                float num = Main.rand.Next(500) - (Math.Abs(item.velocity.X) + Math.Abs(item.velocity.Y)) * 20f;
                int num2 = 2;
                num -= num2 * 20;
                if (item.isBeingGrabbed)
                {
                    num /= 100f;
                }
                if (num < (color.R / 70 + 1))
                {
                    int num3= Dust.NewDust(item.position - new Vector2(1f, 2f), item.width, item.height, 63, 0f, 0f, 0, default(Color), 1.5f);
                    Main.dust[num3].velocity *= 0f;
                    Main.dust[num3].noGravity = true;
                }
            }
            float num4 = item.velocity.X * 0.2f;
            float num5 = item.height - Main.itemTexture[item.type].Height;
            float num6 = item.width / 2 - Main.itemTexture[item.type].Width / 2;
            Main.itemFrameCounter[whoAmI]++;
            if (Main.itemFrameCounter[whoAmI] > 5)
            {
                Main.itemFrameCounter[whoAmI] = 0;
                Main.itemFrame[whoAmI]++;
            }
            if (Main.itemFrame[whoAmI] > 7)
            {
                Main.itemFrame[whoAmI] = 0;
            }
            int width = texture.Width;
            int num7 = texture.Height / 8;
            num6 = item.width / 2 - texture.Width / 2;
            Main.spriteBatch.Draw(texture, new Vector2(item.position.X - Main.screenPosition.X + (width / 2) + num6, item.position.Y - Main.screenPosition.Y + (float)(num7 / 2) + num5), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, Main.itemFrame[whoAmI] * num7 + 1, Main.itemTexture[item.type].Width, num7)), alpha, num4, new Vector2((width / 2), (num7 / 2)), scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumCoin, 100);
            recipe.SetResult(this);
            recipe.AddRecipe();
            if (SinsMod.Instance.BluemagicLoaded)
            {
                recipe = new ModRecipe(mod);
                recipe.AddIngredient(ModLoader.GetMod("Bluemagic").ItemType("PuriumCoin"));
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}