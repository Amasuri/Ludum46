using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Ludum46.Code.Reusable
{
    public class MusicPlayer
    {
        private enum MusicMachineState
        {
            Tree,
            Carry,
            Fight,
            HeartCarry,
            HeartFight,
        }

        private Dictionary<MusicMachineState, SoundEffectInstance> dynMusic;

        private const float MAX_VOL = 1f;
        private const float MIN_VOL = 0f;

        public MusicPlayer(Ludum46 game )
        {
            dynMusic = new Dictionary<MusicMachineState, SoundEffectInstance>();

            dynMusic.Add(MusicMachineState.Carry, game.Content.Load<SoundEffect>("res/music/dynamic/forager").CreateInstance());
            dynMusic.Add(MusicMachineState.Fight, game.Content.Load<SoundEffect>("res/music/dynamic/deadbeat").CreateInstance());
            dynMusic.Add(MusicMachineState.HeartFight, game.Content.Load<SoundEffect>("res/music/dynamic/battle_seeker").CreateInstance());
            dynMusic.Add(MusicMachineState.Tree, game.Content.Load<SoundEffect>("res/music/dynamic/gluttony").CreateInstance());
            dynMusic.Add(MusicMachineState.HeartCarry, game.Content.Load<SoundEffect>("res/music/dynamic/long_haul").CreateInstance());

            foreach (var item in this.dynMusic)
            {
                item.Value.Play();
                item.Value.IsLooped = true;
            }
        }

        public void Update(Ludum46 game)
        {
        }
    }
}
