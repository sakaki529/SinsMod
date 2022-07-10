using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace SinsMod.Sounds.NPCKilled
{
	public class BCKilled : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume * 1f;
			soundInstance.Pan = pan;
			soundInstance.Pitch = 0.0f;
			return soundInstance;
		}
	}
}