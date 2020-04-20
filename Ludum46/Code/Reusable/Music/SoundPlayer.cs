using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Ludum46.Code.Reusable
{
    public class SoundPlayer
    {
        public enum Type
        {
            Drag,
            Heart,
            Hit,
            NewSkele,
            SkeleDeath2,
            swing1,
            swing3,
            waterfall,
        }

        private Dictionary<Type, SoundEffect> sounds;

        public SoundPlayer(Ludum46 game)
        {
            sounds = new Dictionary<Type, SoundEffect>
            {
                { Type.Hit, game.Content.Load<SoundEffect>("res/sound/hit") }
            };
        }

        public void PlaySound(Type type)
        {
            if (sounds.ContainsKey(type))
                sounds[type].Play();
        }
    }
}
