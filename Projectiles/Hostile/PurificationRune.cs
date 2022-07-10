using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class PurificationRune : ModProjectile
    {
        private int Count;
        private bool Start;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("紋章");
            Main.projFrames[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 1040;
            projectile.height = 1040;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.scale = 0.1f;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.friendly)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool PreAI()
        {
            if (projectile.friendly)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].friendly && projectile.Hitbox.Intersects(Main.npc[i].Hitbox) && !Main.npc[i].dontTakeDamage)
                    {
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                        Main.npc[i].StrikeNPCNoInteraction(Main.player[projectile.owner].statLife * 2 + Main.npc[i].defense / 2, 0f, -Main.npc[i].direction, false, false, false);
                    }
                }
            }
            if ((int)projectile.ai[0] >= 1 && (int)projectile.ai[0] <= 5)
            {
                projectile.frame = 1;
            }
            projectile.rotation += 0.2f;
            if (projectile.scale < 2.0f && !Start)
            {
                projectile.scale += 0.1f;
            }
            else
            {
                Start = true;
                Count++;
                projectile.friendly = (int)projectile.ai[1] == 1;
                projectile.hostile = (int)projectile.ai[1] != 1;
            }
            if (Count > 120)
            {
                projectile.friendly = false;
                projectile.hostile = false;
                projectile.scale -= 0.1f;
                if (projectile.scale <= 0f)
                {
                    projectile.Kill();
                }
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Nothingness"), 20);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.GetModPlayer<SinsPlayer>().nothingnessTime = 20;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.GetModPlayer<SinsPlayer>().nothingnessTime = 20;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Color color = lightColor;
            switch ((int)projectile.ai[0])
            {
                case 0:
                    color = Color.White;
                    break;
                case 1:
                    color = new Color(255, 0, 0);
                    break;
                case 2:
                    color = new Color(0, 255, 0);
                    break;
                case 3:
                    color = new Color(0, 0, 255);
                    break;
                case 4:
                    color = new Color(255, 255, 0);
                    break;
                case 5:
                    color = new Color(60, 60, 60);
                    break;
            }
            color.A = 160;
            return color;
        }
    }
}