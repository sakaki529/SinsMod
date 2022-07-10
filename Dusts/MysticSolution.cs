using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Dusts
{
    public class MysticSolution : ModDust
    {
        public override void SetDefaults()
        {
            updateType = 113;
        }
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = true;
        }
        public override bool Update(Dust dust)
        {
            dust.noLight = true;
            if (dust.noLight)
            {
                float num = dust.scale * 0.1f;
                if (num > 1f)
                {
                    num = 1f;
                }
                Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num, num * 0.7f, num);
            }
            return base.Update(dust);
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(200, 200, 200, 0);
        }
    }
}