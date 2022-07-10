using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace SinsMod.Sounds.Item
{
	public class Item2 : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume * 0.8f;
			soundInstance.Pan = pan;
			soundInstance.Pitch = 0.0f;
			return soundInstance;
		}
	}
}