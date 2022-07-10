using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Weapons.Melee
{
    public class Chronos : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chronos");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.damage = 210;
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item71;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("SuperSlow"), 180);
            Vector2 source = player.MountedCenter + new Vector2( Main.rand.NextFloatDirection() * 16f, Main.rand.NextFloatDirection() * 16f);
            Vector2 dir = (target.Center - source).SafeNormalize(Vector2.Zero);
            Dust dust = Dust.NewDustPerfect(target.Center - dir * 30f, mod.DustType("Slash"), dir * 15f, 0, Color.White, crit ? 1.5f : 1f);
            dust.noLight = true;
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ChronosSlash"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("SuperSlow"), 180);
            Vector2 source = player.MountedCenter + new Vector2(Main.rand.NextFloatDirection() * 16f, Main.rand.NextFloatDirection() * 16f);
            Vector2 dir = (target.Center - source).SafeNormalize(Vector2.Zero);
            Dust dust = Dust.NewDustPerfect(target.Center - dir * 30f, mod.DustType("Slash"), dir * 15f, 0, Color.White, crit ? 1.5f : 1f);
            dust.noLight = true;
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ChronosSlash"), (int)((double)item.damage * player.meleeDamage), 0, player.whoAmI);
        }
    }
}