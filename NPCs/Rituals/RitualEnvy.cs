using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Rituals
{
    public class RitualEnvy : ModNPC
    {
        public override string Texture => "SinsMod/NPCs/Rituals/Ritual";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            //npc.width = 408;
            //npc.height = 408;
            npc.width = 254;
            npc.height = 254;
            npc.damage = 0;
            npc.alpha = 0;
            npc.lifeMax = 1000;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.scale = 0f;
            npc.dontCountMe = true;
            npc.dontTakeDamage = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            /*Vector2 vector = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
            Texture2D texture = Main.npcTexture[npc.type];
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale * 0.4175f, SpriteEffects.None, 0f);*/
            Vector2 vector = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
            Texture2D texture = Main.npcTexture[npc.type];
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), -npc.rotation, npc.frame.Size() / 2f, npc.scale * 0.6f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale * 0.36f, SpriteEffects.None, 0f);
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Color color = SinsColor.Envy;
            color.A = 0;
            return color;
        }
        public override void AI()
        {
            if (npc.ai[1] == 0)
            {
                //Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("Ritual2"), 0, 0f, Main.myPlayer, npc.type);
            }
            npc.rotation += 0.01f;
            npc.velocity.Y = 0f;
            npc.velocity.X = 0f;
            npc.ai[1] += 1f;
            if (npc.ai[1] > 180f)
            {
                npc.scale -= 0.05f;
                if (npc.scale < 0.1f)
                {
                    npc.active = false;
                }
            }
            else
            {
                if (npc.scale < 1.0f)
                {
                    npc.scale += 0.025f;
                }
                if (npc.scale > 1.0f)
                {
                    npc.scale = 1.0f;
                }
            }
            if (npc.ai[1] == 180f && !NPC.AnyNPCs(mod.NPCType("LeviathanHead")))
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 14, mod.NPCType("LeviathanHead"));
            }
            Lighting.AddLight(npc.Center, 1.0f, 1.0f, 1.0f);
            
            if (npc.alpha == 0)
            {
                int num;
                for (int num2 = 0; num2 < 2; num2 = num + 1)
                {
                    float num3 = Main.rand.Next(2, 4);
                    float num4 = npc.scale;
                    if (num2 == 1)
                    {
                        num4 *= 0.42f;
                        num3 *= -0.75f;
                    }
                    Vector2 vector2 = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    vector2.Normalize();
                    int num5 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 74, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].noLight = true;
                    Main.dust[num5].position = npc.Center + vector2 * 127f * num4;
                    if (Main.rand.Next(8) == 0)
                    {
                        Main.dust[num5].velocity = vector2 * -num3 * 2f;
                        Dust dust = Main.dust[num5];
                        dust.scale += 0.5f;
                    }
                    else
                    {
                        Main.dust[num5].velocity = vector2 * -num3;
                    }
                    num = num2;
                }
            }
            if (npc.ai[1] >= 60f)
            {
                int num;
                for (int num2 = 0; num2 < 1; num2 = num + 1)
                {
                    float scaleFactor = Main.rand.Next(1, 3);
                    Vector2 vector2 = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    vector2.Normalize();
                    int num3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 74, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num3].noGravity = true;
                    Main.dust[num3].noLight = true;
                    Main.dust[num3].position = npc.Center;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num3].velocity = vector2 * scaleFactor * 2f;
                        Dust dust = Main.dust[num3];
                        dust.scale += 0.5f;
                    }
                    else
                    {
                        Main.dust[num3].velocity = vector2 * scaleFactor;
                    }
                    Main.dust[num3].fadeIn = 2f;
                    num = num2;
                }
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool CheckDead()
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}