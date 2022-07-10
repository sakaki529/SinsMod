using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Dusts
{
    public class MysticWaterSplash : ModDust
    {
        public override void SetDefaults()
        {
            updateType = 33;
        }
        public override void OnSpawn(Dust dust)
        {
            dust.alpha = 170;
            dust.velocity *= 0.5f;
            dust.velocity.Y += 1f;
        }
    }
}