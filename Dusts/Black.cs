using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Dusts
{
    public class Black : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.2f;
            dust.noGravity = true;
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.5f;
            dust.scale *= 0.95f;
            float light = 0.35f * dust.scale;
            if (!dust.noLight)
            {
                Lighting.AddLight(dust.position, light * 2.0f, light * 0.5f, light * 0.5f);
            }
            if (dust.scale < 0.3f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}