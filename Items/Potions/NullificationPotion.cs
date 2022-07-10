using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Potions
{
    public class NullificationPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nullification Potion");
            Tooltip.SetDefault("Delete all debuff");
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
            item.maxStack = 30;
            item.consumable = true; 
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 9;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            int cooldown = Main.player[Main.myPlayer].GetModPlayer<SinsPlayer>().nullificationPotionCooldown;
            if (cooldown > 0)
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
            //return !player.HasBuff(mod.BuffType("NullificationPotionSickness"));
            return player.GetModPlayer<SinsPlayer>().nullificationPotionCooldown <= 0;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<SinsPlayer>().nullificationPotionCooldown = player.pStone ? 2700 : 3600;
            //player.AddBuff(mod.BuffType("NullificationPotionSickness"), player.pStone ? 2700 : 3600);
            for (int i = 0; i < player.buffType.Length; i++)
            {
                if (player.buffType[i] > 0 && player.buffTime[i] > 0 && Main.debuff[player.buffType[i]])
                {
                    player.DelBuff(i);
                }
            }
            return true;
        }
    }
}