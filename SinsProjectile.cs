using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SinsMod
{
    public class SinsProjectile : GlobalProjectile
    {
        public bool trail;
        public bool drawCenter;
        public bool glow;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override bool PreDraw(Projectile projectile, SpriteBatch spriteBatch, Color lightColor)
        {
            if (trail)
            {

            }
            if (drawCenter)
            {
                Texture2D texture = Main.projectileTexture[projectile.type];
                SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
                int num2 = num * projectile.frame;
                Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, num2, texture.Width, num), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, spriteEffects, 0f);
                return false;
            }
            if (glow)
            {

            }
            return base.PreDraw(projectile, spriteBatch, lightColor);
        }
        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (target.type == NPCID.Angler || target.type == NPCID.SleepingAngler)
            {
                if (modPlayer.KillAngler && projectile.friendly/* && projectile.CanHit(projectile)*/)
                {
                    return true;
                }
            }
            return base.CanHitNPC(projectile, target);
        }
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            Rectangle myRect = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
            if (target.active && !target.dontTakeDamage && ((projectile.friendly && (!target.friendly || (target.type == 22 && projectile.owner < 255 && Main.player[projectile.owner].killGuide) || (target.type == 54 && projectile.owner < 255 && Main.player[projectile.owner].killClothier))) || (projectile.hostile && target.friendly && !target.dontTakeDamageFromHostiles))/* && (projectile.owner < 0 || Main.npc[i].immune[projectile.owner] == 0 || projectile.maxPenetrate == 1)*/)
            {
                bool flag2 = false;
                if (projectile.type == 11 && (target.type == 47 || target.type == 57))
                {
                    flag2 = true;
                }
                else
                {
                    if (projectile.type == 31 && target.type == 69)
                    {
                        flag2 = true;
                    }
                    else
                    {
                        if (target.trapImmune && projectile.trap)
                        {
                            flag2 = true;
                        }
                        else
                        {
                            if (target.immortal && projectile.npcProj)
                            {
                                flag2 = true;
                            }
                        }
                    }
                }
                if (!flag2)
                {
                    bool flag3;
                    if (target.type == 414)
                    {
                        Rectangle rect = target.getRect();
                        int num5 = 8;
                        rect.X -= num5;
                        rect.Y -= num5;
                        rect.Width += num5 * 2;
                        rect.Height += num5 * 2;
                        flag3 = projectile.Colliding(myRect, rect);
                    }
                    else
                    {
                        flag3 = projectile.Colliding(myRect, target.getRect());
                    }
                    if (flag3)
                    {
                        bool flag4 = !projectile.npcProj && !projectile.trap;
                        bool flag5 = false;
                        if (flag4)
                        {
                            if (projectile.melee && Main.rand.Next(1, 101) <= player.meleeCrit)
                            {
                                flag5 = true;
                            }
                            if (projectile.ranged && Main.rand.Next(1, 101) <= player.rangedCrit)
                            {
                                flag5 = true;
                            }
                            if (projectile.magic && Main.rand.Next(1, 101) <= player.magicCrit)
                            {
                                flag5 = true;
                            }
                            if (projectile.thrown && Main.rand.Next(1, 101) <= player.thrownCrit)
                            {
                                flag5 = true;
                            }
                            int num13 = projectile.type;
                            if (num13 - 688 <= 2)
                            {
                                if (Main.player[projectile.owner].setMonkT3)
                                {
                                    if (Main.rand.Next(4) == 0)
                                    {
                                        flag5 = true;
                                    }
                                }
                                else
                                {
                                    if (Main.player[projectile.owner].setMonkT2 && Main.rand.Next(6) == 0)
                                    {
                                        flag5 = true;
                                    }
                                }
                            }
                        }
                        if (flag4)
                        {
                            if (!target.immortal && target.lifeMax > 5 && projectile.friendly && !projectile.hostile && projectile.aiStyle != 59)
                            {
                                if (projectile.arrow && projectile.type != ProjectileID.PhantasmArrow && projectile.type != mod.ProjectileType("SpiralArrow") && player.GetModPlayer<SinsPlayer>().spiralTime > 0)
                                {
                                    Vector2 vector2 = player.position + player.Size * Utils.RandomVector2(Main.rand, 0f, 1f);
                                    Vector2 vector3 = target.DirectionFrom(vector2) * 6f;
                                    int dmg = (int)(projectile.damage * 0.3);
                                    Projectile.NewProjectile(vector2.X, vector2.Y, vector3.X, vector3.Y, mod.ProjectileType("SpiralArrow"), dmg, 0f, projectile.owner, target.whoAmI, 0f);
                                    Projectile.NewProjectile(vector2.X, vector2.Y, vector3.X, vector3.Y, mod.ProjectileType("SpiralArrow"), dmg, 0f, projectile.owner, target.whoAmI, 15f);
                                    Projectile.NewProjectile(vector2.X, vector2.Y, vector3.X, vector3.Y, mod.ProjectileType("SpiralArrow"), dmg, 0f, projectile.owner, target.whoAmI, 30f);
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (target.type == mod.NPCType("BlackCrystalCore") || target.type == mod.NPCType("BlackCrystalCoreClone") || target.type == mod.NPCType("BCCSummonAttack") || target.type == mod.NPCType("BCCSummonHeal") || target.type == mod.NPCType("WillOfMadness"))
            {
                if ((NPCs.Boss.Madness.BlackCrystalCore.isMeleeResist && projectile.melee) || (NPCs.Boss.Madness.BlackCrystalCore.isRangedResist && projectile.ranged) || (NPCs.Boss.Madness.BlackCrystalCore.isMagicResist && projectile.magic) || (NPCs.Boss.Madness.BlackCrystalCore.isThrownResist && projectile.thrown) || (NPCs.Boss.Madness.BlackCrystalCore.isSummonResist && (projectile.minion || projectile.sentry)))
                {
                    damage = 0;
                }
                else if (!projectile.melee && !projectile.magic && !projectile.ranged && !projectile.thrown && !projectile.minion && !projectile.sentry && projectile.type != mod.ProjectileType("Nothingness"))
                {
                    damage = 0;
                }
            }
            if (target.type == mod.NPCType("BlackCrystalNoMove") || target.type == mod.NPCType("BlackCrystal") || target.type == mod.NPCType("BlackCrystalSmall"))
            {
                if (!projectile.melee && !projectile.magic && !projectile.ranged && !projectile.thrown && !projectile.minion && !projectile.sentry)
                {
                    damage /= 10;
                }
            }
            if (SinsMod.Instance.MinionCritLoaded)
            {
                //MinionCrit();
                if (projectile.minion || projectile.sentry)
                {
                    if (modPlayer.XEmblem)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            crit = true;
                            return;
                        }
                    }
                }
            }
            base.ModifyHitNPC(projectile, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void PostAI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.PureSpray)
            {
                SinsTile.Convert((int)(projectile.position.X + (projectile.width / 2)) / 16, (int)(projectile.position.Y + (projectile.height / 2)) / 16, 0);
            }
            base.PostAI(projectile);
        }
    }
}