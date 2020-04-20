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
            HitPlayer
        }

        private Dictionary<Type, SoundEffect> soundsFast;
        private Dictionary<Type, SoundEffectInstance> soundLoops;

        public SoundPlayer(Ludum46 game)
        {
            soundsFast = new Dictionary<Type, SoundEffect>
            {
                { Type.Hit, game.Content.Load<SoundEffect>("res/sound/hit") },
                { Type.HitPlayer, game.Content.Load<SoundEffect>("res/sound/blaerhaeurt") },
                { Type.swing1, game.Content.Load<SoundEffect>("res/sound/swing1") },
                { Type.swing3, game.Content.Load<SoundEffect>("res/sound/swing3") },
                { Type.SkeleDeath2, game.Content.Load<SoundEffect>("res/sound/skeledeath2") },
            };

            soundLoops = new Dictionary<Type, SoundEffectInstance>
            {
                { Type.Drag, game.Content.Load<SoundEffect>("res/sound/drag").CreateInstance() },
            };
        }

        public void PlaySound(Type type)
        {
            if (soundsFast.ContainsKey(type))
            {
                if(type == Type.HitPlayer)
                    soundsFast[type].Play(0.5f, 1.0f, 0.0f);
                else
                    soundsFast[type].Play();
            }

            if (soundLoops.ContainsKey(type))
                soundLoops[type].Play();
        }
    }
}
