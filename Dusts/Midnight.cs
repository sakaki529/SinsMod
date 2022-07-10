using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Dusts
{
	public class Midnight : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.05f;
			dust.noGravity = true;
            dust.noLight = true;
			dust.scale *= 0.85f;
		}
		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.45f;
			dust.scale *= 0.98f;
			float light = 0.35f * dust.scale;
            Lighting.AddLight(dust.position, light * 1.2f, light * 1.2f, light * 1.2f);
            if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}