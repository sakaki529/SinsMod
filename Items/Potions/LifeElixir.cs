using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Potions
{
    public class LifeElixir : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Life Elixir");
        }
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.width = 20;
            item.height = 28;
            item.maxStack = 99;
            item.consumable = true; 
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 11;
            item.potion = true;
            item.healLife = 100;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            int HealLife = list.FindIndex(line => line.Name == "HealLife");
            list.RemoveAt(HealLife);
            list.Insert(HealLife, new TooltipLine(mod, "HealLife", "Restores your life to max"));
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            int cooldown = Main.player[Main.myPlayer].GetModPlayer<SinsPlayer>().lifeElixirCooldown;
            if (cooldown > 0 && Main.player[Main.myPlayer].potionDelay <= 0)
            {
                Texture2D texture = Main.cdTexture;
                Vector2 slotSize = new Vector2(52f, 52f);
                position -= slotSize * Main.inventoryScale / 2f - frame.Size() * scale / 2f;
                Vector2 drawPos = position + slotSize * Main.inventoryScale / 2f/* - texture.Size() * Main.inventoryScale / 2f*/;
                float alpha = 0.1f + 0.9f * (cooldown / 3600f);
                Vector2 textureOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
                spriteBatch.Draw(texture, drawPos, null, drawColor * alpha, 0f, textureOrigin, Main.inventoryScale, SpriteEffects.None, 0f);
            }
        }
        public override bool CanUseItem(Player player)
        {
            //return !player.HasBuff(mod.BuffType("LifeElixirSickness"));
            return player.GetModPlayer<SinsPlayer>().lifeElixirCooldown <= 0;
        }
        public override bool UseItem(Player player)
        {
            item.healLife = player.statLifeMax2;
            player.GetModPlayer<SinsPlayer>().lifeElixirCooldown = player.pStone ? 2700 : 3600;
            //player.AddBuff(mod.BuffType("LifeElixirSickness"), player.pStone ? 2700 : 3600);
            return true;
        }    }
}